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

        public string QuestionListName { get; private set; }
        public int Score { get; private set; }
        public int MaxScore { get; private set; }

        private ConsoleService ConsoleService { get; }
        private IQuestionListDao QuestionSetDao { get; }
        private Random Rng { get; }

        private IList<string> QuestionListNames { get; set; }
        private QuestionList QuestionList { get; set; }
        private Action CurrentState { get; set; }
        private bool IsRunning { get; set; }

        public Game(IQuestionListDao questionSetDao, int? seed= null)
        {
            QuestionSetDao = questionSetDao;
            ConsoleService = new ConsoleService();

            if (seed.HasValue)
            {
                Rng = new Random(seed.Value);
            }
            else
            {
                Rng = new Random((int)DateTime.UtcNow.Ticks);
            }
        }

        public void Run()
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
            ConsoleService.PrintMainMenu(MAIN_MENU_ITEMS, QuestionListName);
            int menuSelection = ConsoleService.PromptUserForSelection(MAIN_MENU_ITEMS.Length);

            if(menuSelection == 0)
            {
                CurrentState = HighScoresMenu;
            }
            else if (menuSelection == 1)
            {
                CurrentState = SelectQuestionSetMenu;
            }
            else if (menuSelection == 2)
            {
                if (string.IsNullOrEmpty(QuestionListName))
                {
                    ConsoleService.Pause("Please select a question set before playing. (press any key to continue)", ConsoleColor.Red);
                }
                else
                {
                    Play();
                }
            }
            else if(menuSelection == 3)
            {
                ExitGame();
            }
        }
        private void HighScoresMenu()
        {
            ConsoleService.Pause("Not Implemented. (press any key to continue)", ConsoleColor.Red);
            CurrentState = MainMenu;
        }
        private void SelectQuestionSetMenu()
        {
            QuestionListNames = QuestionSetDao.GetQuestionListNames();

            ConsoleService.ListMenuItems(QuestionListNames);
            int index = ConsoleService.PromptUserForSelection(QuestionListNames.Count);

            QuestionListName = QuestionListNames[index];
            CurrentState = MainMenu;
        }
        private void GameLoop()
        {
            Question question = GetNextQuestion();
            bool answerIsCorrect = false;
            
            string[] answers = question.Answers.Select(a => a.Text).ToArray();
            ConsoleService.AskQuestion(question.Text, answers);

            bool IsSingleAnswer = question.Type == "Single Answer";

            if (IsSingleAnswer)
            {
                int index = ConsoleService.PromptUserForSelection(question.Answers.Count);
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
                CurrentState = GameOverMenu;
            }
        }
        private void GameOverMenu()
        {
            ConsoleService.PrintGameOverScreen(Score, MaxScore);
            CurrentState = MainMenu;
        }
        #endregion

        #region GameFunctionality
        private void Play()
        {
            int index = QuestionListNames.IndexOf(QuestionListName) + 1; // hacky
            QuestionList = QuestionSetDao.GetQuestionList(index); // this should be cloned and cached for reuse.

            Score = 0;
            MaxScore = QuestionList.Questions.Count;
            CurrentState = GameLoop;
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
        private bool CheckAnswer(Question question, string answerString, char delimiter)
        {
            HashSet<int> resultSet = answerString
                .Split(delimiter)
                .Select(a => int.Parse(a))
                .ToHashSet();

            HashSet<int> expectedSet = new HashSet<int>();
            for (int i = 0; i < question.Answers.Count; i++)
            {
                if (question.Answers[i].IsCorrect)
                {
                    expectedSet.Add(i + 1);
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
