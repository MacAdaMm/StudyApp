using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyAPI.DAO;
using StudyShared.DAO;
using StudyShared.Models;
using Microsoft.Extensions.Configuration;

namespace StudyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionListController : ControllerBase
    {
        private static IQuestionListDao questionSetDao;

        public QuestionListController(IConfiguration configuration)
        {
            if (questionSetDao == null)
            {
                questionSetDao = new QuestionListSQLDao(configuration.GetConnectionString("PracticeQuestionDB"));
            }
        }

        [HttpGet]
        public IList<QuestionList> GetQuestionLists()
        {
            return questionSetDao.GetQuestionLists();
        }

        [HttpGet("{id}")]
        public QuestionList GetQuestionListById(int id)
        {
            return questionSetDao.GetQuestionList(id);
        }

        [HttpGet("names")]
        public IList<string> GetQuestionListNames()
        {
            return questionSetDao.GetQuestionListNames();
        }
    }
}
