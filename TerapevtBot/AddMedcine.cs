using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TerapevtBot
{
    public class AddMedcine
    {
        private static IServiceProvider _provider;

        public AddMedcine(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }


        public static async Task NewMedcine(Update update, TelegramBotClient Bot)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var message = update.Message;

            var scenario = new Scenario()
            {
                Id = Guid.NewGuid(),
                QuestionTreeId = Guid.Parse("guid добавления лекарства"),
                StartDate = DateTimeOffset.Now,
                UserId = message.From.Id
            };

            var firstQuestion = _context.Questions
                                        .Where(c => c.IsFirst == true)
                                        .Where(c => c.QuestionTreeId == scenario.QuestionTreeId)
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
                UserId = message.From.Id
            };

            _context.Add(history);
            _context.SaveChanges();
        }




        public static async Task ContinueMedcineAdding(Update update, TelegramBotClient Bot)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var scenario = _context.Scenarios.Where(c => c.StartDate != null & c.Finished == false).FirstOrDefault(); // находим сценарий, который стартовал, но еще не закончен

            if(scenario == null)
            {
                //что-то надо делать, если не найдено незавершенных сценариев, а мы оказались тут
            }

            var lastAskedQuestionOfScenario = _context.QuestionTreeHistories
                                                   .Include(c => c.Question)
                                                    .ThenInclude(c => c.Param)
                                                 .Where(c => c.ScenarioId == scenario.Id)
                                                 .OrderBy(c => c.CreateDate)
                                                 .FirstOrDefault();

            if (lastAskedQuestionOfScenario != null)
            {
                var question = lastAskedQuestionOfScenario.Question;
                var param = question.Param;

                var paramVlaue = new ParamValue()
                {
                    Id = Guid.NewGuid(),
                    ParamId = param.Id,
                    QuestionId = question.Id,
                    Value = update.Message.Text
                };
                //если параметр с СИ, то надо задать вопрос
            }

        }


        public static void AddParametrValue(Question question, Message message)
        {




        }


    }
}
