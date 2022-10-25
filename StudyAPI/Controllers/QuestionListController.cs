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
        private static IQuestionSetDao questionSetDao;

        public QuestionListController(IConfiguration configuration)
        {
            if(questionSetDao == null)
            {
                questionSetDao = new QuestionSetSQLDao(configuration.GetConnectionString("PracticeQuestionDB"));
            }
        }

        [HttpGet]
        public IList<QuestionList> GetQuestionLists()
        {
            return questionSetDao.GetQuestionLists();
        }

        [HttpGet("{Id}")]
        public QuestionList GetQuestionList(int Id)
        {
            return questionSetDao.GetQuestionList(Id);
        }

        [HttpGet("names")]
        public IList<string> GetQuestionListNames()
        {
            return questionSetDao.GetQuestionListNames();
        }
    }
}
