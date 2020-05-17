using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MihaZupan;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TerapevtBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static TelegramBotClient Bot;
        private static IServiceProvider _provider;


        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _provider = serviceProvider;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var p = new HttpToSocks5Proxy("51.15.109.147", 1337, "socksuser", "Xzws@:=AU$ytW9da5ArsnD\"}2=UZ*)wF<5mFVg9{Q-(`q");
            Bot = new TelegramBotClient("1154497230:AAHIkWfOeND7SpdTmgVLD-DzHMHdR-EOog4", p);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {



            while (!stoppingToken.IsCancellationRequested)
            {

                var cts = new CancellationTokenSource();
                Bot.StartReceiving(
                           new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                           cts.Token
                        );
                Console.ReadLine();
            }
        }

        public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            using IServiceScope scope = _provider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update),
                //UpdateType.EditedMessage => BotOnMessageReceived(update.Message),
                //UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery),
                //UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery),
                //UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult),
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception, cancellationToken);
            }
        }


        private static async Task BotOnMessageReceived(Update update)
        {
            var action = (update.Message.Text.Split(' ').First()) switch
            {
                "/add" => AddMedcine.NewMedcine(update, Bot, _provider),
                "/get" => GetMedcine.GetMedcineByName(update, Bot, _provider),
                _ => AddMedcine.ContinueMedcineAdding(update, Bot, _provider)
            };
            await action;


        }

        //static async Task Usage(Message message)
        //{
        //    const string usage = "Usage:\n" +
        //                            "/добавить_препарат\n" +
        //                            "/получить_препарат";

        //    await Bot.SendTextMessageAsync(
        //        chatId: message.Chat.Id,
        //        text: usage,
        //        replyMarkup: new ReplyKeyboardRemove()
        //    );
        //}




        public static async Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }

        private static async Task UnknownUpdateHandlerAsync(Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
        }
    }
}
