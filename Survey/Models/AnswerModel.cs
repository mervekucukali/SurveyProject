using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class AnswerModel
    {
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public string QuestionType { get; set; }
        public string NameSurname { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}