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

            IQuestionListDao questionSetDao = new StudyAPIService(Configuration.GetConnectionString("ApiURL"));

            Game game = new Game(questionSetDao);
            game.Run();
        }
    }    
}
