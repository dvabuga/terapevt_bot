using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TerapevtBot
{
    public class GetMedcine
    {
        private static IServiceProvider _provider;

        public GetMedcine(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public static async void GetMendcineByName(Update update, TelegramBotClient Bot)
        {



            

        }
    }
}
