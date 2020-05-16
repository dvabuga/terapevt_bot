using Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TerapevtBot
{
    public class QuestionService
    {
        private static IServiceProvider _provider;

        public QuestionService(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }


        public static async void Ask(Question question, Message message, TelegramBotClient Bot)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var chatId = message.Chat.Id;
            var userId = message.From.Id;
            var questionToSend = _context.Questions.Where(c => c.Id == question.Id).FirstOrDefault();

            await Bot.SendTextMessageAsync(
                chatId: chatId,
                text: questionToSend.Text
            );

            var history = new QuestionTreeHistory()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.Now,
                QuestionId = question.Id,
               // ScenarioId = question.ScenarioId,
                UserId = userId
            };
            _context.Add(history);
            _context.SaveChanges();
        }


        public static async void GetNextQuestion()
        {



        }
    }
}
