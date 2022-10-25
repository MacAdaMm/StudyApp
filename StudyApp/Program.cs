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

        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            IQuestionSetDao questionSetDao = new QuestionSetSQLDao(Configuration.GetConnectionString("PracticeQuestionDB"));

            Game game = new Game(questionSetDao);

            game.Start();
        }
    }    
}
