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
    public class GetMedcine
    {
        public static async Task GetMedcineByName(Update update, TelegramBotClient Bot, IServiceProvider _provider)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var message = update.Message;
            var chatId = message.Chat.Id;

            var command = message.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (command.Count() < 2)
            {
                await Bot.SendTextMessageAsync(
                chatId: chatId,
                text: "Неверное использование команды. Возможно не указано название препарата или указано не верно."
                );
                return;
            }

            var medcineName = command[1].ToLower();
            var userId = message.From.Id;

            var medcineNameParam = _context.ParamValues
                                           .Where(c => c.Value.ToLower() == medcineName)
                                           .FirstOrDefault();

            if (medcineNameParam is null)
            {
                await Bot.SendTextMessageAsync(
                chatId: chatId,
                text: "Препарат с таким названием не найден."
                );
                return;
            }

            var messageText = string.Empty;

            var medcineParamValues = _context.ParamValues
                                             .Include(c => c.Param)
                                             .Where(c => c.MedcinId == medcineNameParam.MedcinId)
                                             .ToList();

            foreach (var paramValue in medcineParamValues)
            {
                var line = paramValue.Param.Name + " : " + paramValue.Value;
                if (paramValue.Param.HasUnit)
                {
                    line += " " + paramValue.Unit;
                }
                line += "\n";
                messageText += line;
            }

            await Bot.SendTextMessageAsync(
                chatId: chatId,
                text: messageText
                );

        }
    }
}
