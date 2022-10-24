using Microsoft.Extensions.Configuration;
using StudyApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudyApp.DAO
{
    public class QuestionSetSQLDao : IQuestionSetDao
    {
        private readonly string _connectionString;
        public QuestionSetSQLDao()
        {
            _connectionString = Program.Configuration.GetConnectionString("PracticeQuestionDB");
        }

        public IList<string> GetQuestionListNames()
        {
            IList<string> names = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT list_name FROM list", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(Convert.ToString(reader["list_name"]));
                }
            }
            return names;
        }

        public QuestionList GetQuestionList(int questionListId)
        {
           QuestionList questionSet = null;
           using(SqlConnection conn = new SqlConnection(_connectionString))
           {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM list WHERE list_id = @list_id", conn);

                cmd.Parameters.AddWithValue("@list_id", questionListId);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    questionSet = CreateQuestionListFromReader(reader);
                }
           }
            return questionSet;
        }
        public QuestionList GetQuestionList(string questionListName)
        {
            QuestionList questionSet = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM list WHERE list_name = @list_name", conn);

                cmd.Parameters.AddWithValue("@list_name", questionListName);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    questionSet = CreateQuestionListFromReader(reader);
                }
            }
            return questionSet;
        }

        public IList<QuestionList> GetQuestionLists()
        {
            IList<QuestionList> questionSets = new List<QuestionList>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM list", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    questionSets.Add(CreateQuestionListFromReader(reader));
                }
            }
            return questionSets;
        }

        private IList<Question> GetQuestions(int ListId)
        {
            List<Question> questions = new List<Question>();

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT question.question_id, question.text, type.type_name FROM question " +
                                                "JOIN question_list ON question.question_id = question_list.question_id AND question_list.list_id = @list_id " +
                                                "JOIN type ON type.type_id = question.type_id", conn);

                cmd.Parameters.AddWithValue("@list_id", ListId);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Question question = CreateQuestionFromReader(reader);
                    questions.Add(question);
                }
            }

            return questions;
        }
        private IList<Answer> GetAnswers(int questionId)
        {
            List<Answer> answers = new List<Answer>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT answer.answer_id, text, is_correct FROM answer " +
                                                "JOIN question_answer ON question_answer.answer_id = answer.answer_id AND question_answer.question_id = @question_id", conn);

                cmd.Parameters.AddWithValue("@question_id", questionId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Answer answer = CreateAnswerFromReader(reader);
                    answers.Add(answer);
                }
            }
            return answers;
        }

        private QuestionList CreateQuestionListFromReader(SqlDataReader reader)
        {
            int listId = Convert.ToInt32(reader["list_id"]);

            QuestionList questionList = new QuestionList()
            {
                QuestionListId = listId,
                Name = Convert.ToString(reader["list_name"]),
                Questions = GetQuestions(listId)
            };

            return questionList;
        }
        private Question CreateQuestionFromReader(SqlDataReader reader)
        {
            int questionId = Convert.ToInt32(reader["question_id"]);

            return new Question()
            {
                QuestionId = questionId,
                Text = Convert.ToString(reader["text"]),
                Type = Convert.ToString(reader["type_name"]),
                Answers = GetAnswers(questionId)
            };
        }
        private Answer CreateAnswerFromReader(SqlDataReader reader)
        {
            return new Answer()
            {
                AnswerId = Convert.ToInt32(reader["answer_id"]),
                Text = Convert.ToString(reader["text"]),
                IsCorrect = Convert.ToBoolean(reader["is_correct"])
            };
        }
    }
}
