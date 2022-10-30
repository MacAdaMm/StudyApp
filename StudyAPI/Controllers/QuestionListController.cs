using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyShared.DAO;
using StudyShared.Models;
using Microsoft.Extensions.Configuration;

namespace StudyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionListController : ControllerBase
    {
        private static IQuestionListDao questionListDao;

        public QuestionListController(IQuestionListDao questionListDao)
        {
            if (QuestionListController.questionListDao == null)
            {
                QuestionListController.questionListDao = questionListDao;
            }
        }

        [HttpGet]
        public IList<QuestionList> GetQuestionLists()
        {
            return questionListDao.GetQuestionLists();
        }

        [HttpGet("{id}")]
        public QuestionList GetQuestionListById(int id)
        {
            return questionListDao.GetQuestionList(id);
        }

        [HttpGet("names")]
        public IList<string> GetQuestionListNames()
        {
            return questionListDao.GetQuestionListNames();
        }
    }
}
