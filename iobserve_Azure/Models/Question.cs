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
        public AnswerOptions()
        {

        }
        public string OptionText { get; set; }
        public bool IsSelected { get; set; }
        public int SequenceNo { get; set; }

    }
    public class QuestionDetails
    {
        public QuestionDetails()
        {

        }
        public string Image  { get; set; }
        public List<Question> Questions { get; set; }
        public string Id { get; set; }
        string _workStopped;
        public string WorkStopped
        {

            get {return this._workStopped; }
            set {_workStopped = string.IsNullOrEmpty(value) ? @"N\A" : value; }
        }
        string _riskLevel;
        public string RiskLevel
        {
            get { return this._riskLevel; }

            set {_riskLevel = string.IsNullOrEmpty(value)? @"N\A" : value; }
        }
        string _customerInfo;
        public string CustomerInfo 
        {
            get{return _customerInfo;}
            set{ _customerInfo = string.IsNullOrEmpty(value) ? @"N\A" : value; }

        }
        string _actionTaken;
        public string ActionTaken
        {
            get { return _actionTaken; }
            set { _actionTaken = string.IsNullOrEmpty(value) ? @"N\A" : value; }

        }

        string _supnotified;
        public string SupervisorNotified
        {
            get { return _supnotified; }
            set { _supnotified = string.IsNullOrEmpty(value) ? @"N\A" : value; }

        }

        string _riskEliminated;
        public string RiskEliminated
        {
            get { return _riskEliminated; }
            set { _riskEliminated = string.IsNullOrEmpty(value) ? @"N\A" : value; }

        }
        public string SelectedStatus { get; set; }
        public string Comments { get; set; }
    }
}