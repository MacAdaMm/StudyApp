using System;
using System.Collections.Generic;
using RestSharp;
using StudyShared.Models;
using System.Net.Http;
using StudyShared.DAO;


namespace StudyApp
{
    class StudyAPIService : IQuestionSetDao
    {
        readonly RestClient client;
        private const string QUESTIONLIST_ROUTE = "questionlist";
        private readonly string QUESTIONLIST_NAMES_ROUTE = $"{QUESTIONLIST_ROUTE}/names";

        public StudyAPIService(string rootPath)
        {
            client = new RestClient(rootPath);
        }

        public void CheckForError(IRestResponse response, string errorMessage)
        {
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException(errorMessage);
            }
        }

        List<string> IQuestionSetDao.GetQuestionListNames()
        {
            RestRequest request = new RestRequest(QUESTIONLIST_NAMES_ROUTE);
            IRestResponse<List<string>> response = client.Get<List<string>>(request);
            CheckForError(response, "Something went wrong..");
            return response.Data;
        }

        public QuestionList GetQuestionList(string questionListName)
        {
            RestRequest request = new RestRequest($"{QUESTIONLIST_NAMES_ROUTE}?name={questionListName}");

            IRestResponse<QuestionList> response = client.Get<QuestionList>(request);
            CheckForError(response, "Something went wrong..");
            return response.Data;
        }

        public QuestionList GetQuestionList(int questionListId)
        {
            RestRequest request = new RestRequest($"{QUESTIONLIST_ROUTE}/{questionListId}");
            IRestResponse<QuestionList> response = client.Get<QuestionList>(request);
            CheckForError(response, "Something went wrong..");
            return response.Data;
        }

        List<QuestionList> IQuestionSetDao.GetQuestionLists()
        {
            RestRequest request = new RestRequest(QUESTIONLIST_ROUTE);
            IRestResponse<List<QuestionList>> response = client.Get<List<QuestionList>>(request);
            CheckForError(response, errorMessage: " Something went wrong..");
            return response.Data;
        }
    }
}