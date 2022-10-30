using System;
using StudyShared.DAO;
using Microsoft.Extensions.Configuration;
using System.IO;
using StudyShared.Models;

namespace StudyApp
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static void Main()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            string ApiUrl = Configuration.GetConnectionString("ApiURL");
            IQuestionListDao questionSetDao = new StudyAPIService(ApiUrl);

            Game game = new Game(questionSetDao);
            game.Run();
        }
    }    
}
