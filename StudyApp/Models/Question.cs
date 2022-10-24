using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public IList<Answer> Answers { get; set; }

        public override string ToString()
        {
            return $"Id: {QuestionId}, Type: {Type}, Text: {Text}";
        }
    }
}
