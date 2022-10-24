using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp
{
    static class ConsoleHelpers
    {
        public static void ListItems(IList<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write($"{i + 1}) ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(items[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static int PromptUserForSelection(int maxValue)
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
    }
}
