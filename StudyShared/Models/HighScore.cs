using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyShared.Models
{
    public class HighScore
    {
        public int HighScoreId { get; set; }
        public int QuestionSetId { get; set; }
        public int PlayerId { get; set; }
    }
}
