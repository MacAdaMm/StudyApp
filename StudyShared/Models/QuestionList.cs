using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyShared.Models
{
    public class QuestionList
    {
        public int QuestionListId { get; set; }
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public override string ToString()
        {
            return $"Id: {QuestionListId}, Name: {Name}, Questions: {Questions.Count}";
        }
    }
}
