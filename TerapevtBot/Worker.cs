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
            var p = new HttpToSocks5Proxy("54.38.140.85", 1337, "proxyuser", "L#n*lg^2B%g5D^P&cf");
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

            }
        }

        public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
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
                "/��������_��������" => AddMedcine.NewMedcine(update, Bot),
                //"/��������_��������" => AddMedcine.NewMedcine(update, Bot),
                _ => AddMedcine.ContinueMedcineAdding(update, Bot)
            };
            await action;


        }

        //static async Task Usage(Message message)
        //{
        //    const string usage = "Usage:\n" +
        //                            "/��������_��������\n" +
        //                            "/��������_��������";

        //    await Bot.SendTextMessageAsync(
        //        chatId: message.Chat.Id,
        //        text: usage,
        //        replyMarkup: new ReplyKeyboardRemove()
        //    );
        //}

        static async Task SendReplyKeyboard(Message message)
        {
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new KeyboardButton[][]
                {
                        new KeyboardButton[] { "1.1", "1.2" },
                        new KeyboardButton[] { "2.1", "2.2" },
                },
                resizeKeyboard: true
            );

            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose",
                replyMarkup: replyKeyboardMarkup

            );
        }


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
