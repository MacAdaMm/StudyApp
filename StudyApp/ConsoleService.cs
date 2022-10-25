using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp
{
    public class ConsoleService
    {
        public void PrintMainMenu(string[] menuItems, string selectedQuestionListName)
        {
            Console.WriteLine($"Selected Question Set: {selectedQuestionListName}\n");
            Console.WriteLine("Select Menu Option\n");
            ListMenuItems(menuItems);
        }
        public void PrintGameOverScreen(int score, int maxScore)
        {
            Console.WriteLine($"Score: {score}/{maxScore}");
            Pause("press any key to continue.", ConsoleColor.Yellow);
        }

        public void AskQuestion(string questionText, string[] answers)
        {
            Console.WriteLine(questionText + "\n");
            ListMenuItems(answers);
            Console.WriteLine();
        }

        public void ListMenuItems(IList<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write($"{i + 1}) ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(items[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public int PromptUserForSelection(int maxValue)
        {
            int selection;
            Console.Write(">>> ");
            while (!int.TryParse(Console.ReadLine(), out selection) || selection <= 0 || selection > maxValue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select a valid menu item.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">>> ");
            }

            return selection - 1;
        }
        public void Pause(string message, ConsoleColor color)
        {
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = consoleColor;
            Console.ReadKey();
        }
    }
}
