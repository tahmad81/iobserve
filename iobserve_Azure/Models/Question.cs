using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iobserve_Azure.Models
{
    public class Question
    {
        public Question()
        {
            this.AnswerOptionList = new List<AnswerOptions>();
        }
        public string Id { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public List<AnswerOptions> AnswerOptionList { get; set; }
        public  int ?  SequenceNo { get; set; }
    }
    public class AnswerOptions
    {

        public string OptionText { get; set; }
        public bool IsSelected { get; set; }
        public int SequenceNo { get; set; }

    }
}