using System;
using System.Collections.Generic;
using System.Linq;
using StudyShared.DAO;
using StudyShared.Models;

namespace StudyApp
{
    class Game
    {
        private readonly string[] MAIN_MENU_ITEMS = new string[]
        {
            "View HighScores",
            "Select Question Set",
            "Play",
            "Exit Game"
        };

        public int Score { get; private set; }
        public int MaxScore { get; private set; }

        private IQuestionSetDao QuestionSetDao { get; }
        private string QuestionListName { get; set; }
        private QuestionList QuestionList { get; set; }
        private Random Rng { get; set; }
        private Action CurrentState { get; set; }
        private bool IsQuestionSetSelected => string.IsNullOrEmpty(QuestionListName) == false;
        private bool IsRunning { get; set; }

        public Game(IQuestionSetDao questionSetDao)
        {
            QuestionSetDao = questionSetDao;
        }

        public void Start()
        {
            IsRunning = true;
            CurrentState = MainMenu;

            while(IsRunning)
            {
                Console.Clear();
                CurrentState.Invoke();
            }
        }

        #region GameStates
        private void MainMenu()
        {
            Console.WriteLine($"Selected Question Set: {QuestionListName}\n");
            Console.WriteLine("Select Menu Option\n");

            ConsoleHelpers.ListItems(MAIN_MENU_ITEMS);
            int index = ConsoleHelpers.PromptUserForSelection(MAIN_MENU_ITEMS.Length);

            if(index == 0)
            {
                CurrentState = ListHighScores;
            }
            else if (index == 1)
            {
                CurrentState = SelectQuestionSet;
            }
            else if (index == 2)
            {
                if (IsQuestionSetSelected)
                {
                    HandleGameSetup();
                    CurrentState = GameLoop;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select a question set before playing. (press any key to continue)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                }
            }
            else if(index == 3)
            {
                ExitGame();
            }
        }
        private void ListHighScores()
        {
            Console.WriteLine("Not Implemented. (press any key to continue)");
            Console.ReadKey();
            CurrentState = MainMenu;
        }
        private void SelectQuestionSet()
        {
            IList<string> names = QuestionSetDao.GetQuestionListNames();

            ConsoleHelpers.ListItems(names);
            int index = ConsoleHelpers.PromptUserForSelection(names.Count);

            QuestionListName = names[index];
            
            CurrentState = MainMenu;
        }
        private void GameLoop()
        {
            Question question = GetNextQuestion();
            bool answerIsCorrect = false;
            bool IsSingleAnswer = question.Type == "Single Answer";

            AskQuestion(question);

            if (IsSingleAnswer)
            {
                int index = ConsoleHelpers.PromptUserForSelection(question.Answers.Count);
                answerIsCorrect = CheckAnswer(question, index);
            }
            else
            {
                string answerString = Console.ReadLine();
                answerIsCorrect = CheckAnswer(question, answerString, ',');
            }

            if (answerIsCorrect)
            {
                Score++;
            }

            if (QuestionList.Questions.Count == 0)
            {
                CurrentState = GameOver;
            }
        }
        private void GameOver()
        {
            Console.WriteLine($"Score: {Score}/{MaxScore}");
            Console.WriteLine("press any key to continue.");
            Console.ReadKey();
            CurrentState = MainMenu;
        }
        #endregion

        #region GameFunctionality
        private void HandleGameSetup()
        {
            Rng = new Random((int)DateTime.UtcNow.Ticks);
            QuestionList = QuestionSetDao.GetQuestionList(QuestionListName);
            Score = 0;
            MaxScore = QuestionList.Questions.Count;
        }
        private void ExitGame() 
        { 
            IsRunning = false;
        }
        private Question GetNextQuestion()
        {
            int i = Rng.Next(QuestionList.Questions.Count);
            Question question = QuestionList.Questions[i];
            QuestionList.Questions.RemoveAt(i);
            return question;
        }
        private void AskQuestion(Question question)
        {
            List<string> answers = question.Answers.Select(a => a.Text).ToList();
            Console.WriteLine(question.Text + "\n");
            ConsoleHelpers.ListItems(answers);
            Console.WriteLine();
        }
        private bool CheckAnswer(Question question, string answerString, char delimiter)
        {
            HashSet<int> resultSet = answerString.Split(delimiter)
                    .Select(a => int.Parse(a))
                    .ToHashSet();

            HashSet<int> expectedSet = new HashSet<int>();
            for (int i = 0; i < question.Answers.Count; i++)
            {
                if (question.Answers[i].IsCorrect)
                {
                    expectedSet.Add(i);
                }
            }

            return expectedSet.SetEquals(resultSet);
        }
        private bool CheckAnswer(Question question, int answerIndex)
        {
            return question.Answers[answerIndex].IsCorrect;
        }
        #endregion
    }
}
