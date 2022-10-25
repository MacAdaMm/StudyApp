﻿using System;
using System.Collections.Generic;
using StudyShared.Models;

namespace StudyShared.DAO
{
    public interface IQuestionSetDao
    {
        public IList<string> GetQuestionListNames();
        public QuestionList GetQuestionList(int questionSetId);
        public QuestionList GetQuestionList(string questionListName);
        public IList<QuestionList> GetQuestionLists();
    }
}