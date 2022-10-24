using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public override string ToString()
        {
            return $"Id: {AnswerId}, IsCorrect: {IsCorrect}, Text: {Text}";
        }
    }
    
}
