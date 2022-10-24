using System;
using StudyApp.DAO;
using Microsoft.Extensions.Configuration;
using System.IO;
using StudyApp.Models;

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

            IQuestionSetDao questionSetDao = new QuestionSetSQLDao();

            Game game = new Game(questionSetDao);

            game.Start();
        }
    }    
}
