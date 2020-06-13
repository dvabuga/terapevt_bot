using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TerapevtBot
{
    public class AddMedcine
    {
        //private static IServiceProvider _provider;

        //public AddMedcine(IServiceProvider serviceProvider)
        //{
        //    _provider = serviceProvider;
        //}


        public static async Task NewMedcine(Update update, TelegramBotClient Bot, IServiceProvider _provider)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var message = update.Message;

            var scenario = new Scenario()
            {
                Id = Guid.NewGuid(),
                Type = ScenarioType.AddMedcine,
                Finished = false,
                //QuestionTreeId = Guid.Parse("guid добавления лекарства"),
                StartDate = DateTimeOffset.Now,
                UserId = message.From.Id
            };

            _context.Add(scenario);

            var medcine = new Medcin()
            {
                Id = Guid.NewGuid()
            };
            _context.Add(medcine);

            var firstQuestion = _context.Questions
                                        .Include(c => c.QuestionTree)
                                        .Where(c => c.IsFirst == true)
                                        .Where(c => c.QuestionTree.Type == QuestionTreeType.AddMedcine)
                                        .FirstOrDefault();

            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: firstQuestion.Text
            );

            var history = new QuestionTreeHistory()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.Now,
                QuestionId = firstQuestion.Id,
                //QuestionTreeId = scenario.QuestionTreeId,
                UserId = message.From.Id,
                MedcinId = medcine.Id,
                ScenarioId = scenario.Id
            };

            _context.Add(history);
            _context.SaveChanges();
        }


        public static async Task ContinueMedcineAdding(Update update, TelegramBotClient Bot, IServiceProvider _provider)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var scenario = _context.Scenarios.Where(c => c.StartDate != null & c.Finished == false).FirstOrDefault(); // находим сценарий, который стартовал, но еще не закончен
            var userId = update.Message.From.Id;

            if (scenario == null)
            {
                return;
                //что-то надо делать, если не найдено незавершенных сценариев, а мы оказались тут
            }

            var lastAskedQuestionOfScenario = _context.QuestionTreeHistories
                                                   .Include(c => c.Question)
                                                    .ThenInclude(c => c.Param)
                                                 .Where(c => c.ScenarioId == scenario.Id & c.UserId == userId)
                                                 .OrderByDescending(c => c.CreateDate)
                                                 .FirstOrDefault();

            var question = lastAskedQuestionOfScenario.Question;
            var param = question.Param;
            var message = update.Message;

            var paramValue = _context.ParamValues.Where(c => c.QuestionId == question.Id & c.MedcinId == lastAskedQuestionOfScenario.MedcinId).FirstOrDefault();
            if (lastAskedQuestionOfScenario != null)
            {
                var paramValueId = Guid.NewGuid();

                if (param.HasUnit)
                {
                    if (paramValue != null && !string.IsNullOrEmpty(paramValue.Value))
                    {
                        paramValue.Unit = update.Message.Text;
                    }
                }

                if (paramValue == null)
                {
                    var paramVlaue = new ParamValue()
                    {
                        Id = paramValueId,
                        ParamId = param.Id,
                        QuestionId = question.Id,
                        Value = update.Message.Text,
                        MedcinId = lastAskedQuestionOfScenario.MedcinId
                    };
                    _context.Add(paramVlaue);

                    var medcinParam = new MedcinParam()
                    {
                        Id = Guid.NewGuid(),
                        MedcinId = lastAskedQuestionOfScenario.MedcinId,
                        ParamsValueId = paramValueId
                    };

                    _context.Add(medcinParam);
                }

                //если параметр с СИ, то надо задать вопрос
            }

            _context.SaveChanges();
            paramValue = _context.ParamValues.Where(c => c.QuestionId == question.Id & c.MedcinId == lastAskedQuestionOfScenario.MedcinId).FirstOrDefault();

            //Ask unit Question - вынести в отдельный метод
            if (param.HasUnit && string.IsNullOrEmpty(paramValue.Unit)) //если добавленный параметр имеет СИ, то задаем соответствующий вопрос
            {
                var unitQuestion = _context.Questions
                                           .Where(c => c.Type == QuestionType.Unit & c.Id == question.RelatedQuestionId)
                                           .FirstOrDefault();

                if (unitQuestion.ResponseType == ResponseType.Buttons)
                {
                    var answers = JsonConvert.DeserializeObject<List<string>>(unitQuestion.Answers.ToString());

                    var keys = new KeyboardButton[answers.Count()];

                    for (var i = 0; i < answers.Count(); i++)
                    {
                        keys[i] = new KeyboardButton(answers[i]);
                    }

                    var replyKeyboardMarkup = new ReplyKeyboardMarkup(keys, true, true);

                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: unitQuestion.Text,
                        replyMarkup: replyKeyboardMarkup);
                }
                else
                {
                    await Bot.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text: unitQuestion.Text
                                );
                }

                return;
            }

            _context.SaveChanges();

            if (question.IsLast)
            {
                scenario.Finished = true;
                _context.SaveChanges();
                //ToDo прописать логику, если задан последний вопрос
                return;
            }

            //начинается логика определения следущего вопроса //ToDo вынести в отдельный метод
            var lastQuestionScenatioType = question.ScenarioType;
            var nextQuestionId = Guid.Empty;
            var scenarioObject = JsonConvert.DeserializeObject<JObject>(question.Scenario.ToString());

            if (lastQuestionScenatioType == QuestionScenarioType.Simple)
            {
                nextQuestionId = Guid.Parse(scenarioObject["next"].ToString());
            }
            else if (lastQuestionScenatioType == QuestionScenarioType.Complex)
            {
                var key = update.Message.Text; // ответ является ключом для получения следующего вопроса
                nextQuestionId = Guid.Parse(scenarioObject[key].ToString());
            }



            //получили id следующего вопроса, задаем вопрос (вызов следующего вопроса так же вынести в отдельный метод)
            await AskNextQuestion(scenario.Id, lastAskedQuestionOfScenario.MedcinId, update, nextQuestionId, Bot, _context);
        }

        private static async Task AskNextQuestion(Guid scenarioId, Guid medcinId, Update update, Guid nextQuestionId, TelegramBotClient Bot, ApplicationDbContext _context)
        {
            var chatId = update.Message.Chat.Id;
            var userId = update.Message.From.Id;

            var question = _context.Questions
                                   .Where(c => c.Id == nextQuestionId)
                                   .FirstOrDefault();

            if (/*question.Type == QuestionType.Parametr & */question.ScenarioType == QuestionScenarioType.Simple)
            {
                if (question.ResponseType == ResponseType.Text)
                {
                    await Bot.SendTextMessageAsync(
                                chatId: chatId,
                                text: question.Text
                                );
                }
                else
                {
                    var answers = JsonConvert.DeserializeObject<List<string>>(question.Answers.ToString());

                    var keys = new KeyboardButton[answers.Count()];

                    for (var i = 0; i < answers.Count(); i++)
                    {
                        keys[i] = new KeyboardButton(answers[i]);
                    }

                    var replyKeyboardMarkup = new ReplyKeyboardMarkup(keys, true, true);

                    await Bot.SendTextMessageAsync(
                        chatId: chatId,
                        text: question.Text,
                        replyMarkup: replyKeyboardMarkup);
                }

            }
            else if (question.ScenarioType == QuestionScenarioType.Complex)
            {
                var answers = JsonConvert.DeserializeObject<List<string>>(question.Answers.ToString());

                var keys = new List<KeyboardButton>();

                foreach (var answer in answers)
                {
                    var key = new KeyboardButton();
                    keys.Add(key);
                }
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(keys, true, true);

                await Bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose",
                    replyMarkup: replyKeyboardMarkup);
            }

            var history = new QuestionTreeHistory()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.Now,
                MedcinId = medcinId,
                ScenarioId = scenarioId,
                QuestionId = question.Id,
                UserId = userId
            };

            _context.Add(history);
            _context.SaveChanges();
        }



        public static void AddParametrValue(Question question, Message message)
        {




        }


    }
}
