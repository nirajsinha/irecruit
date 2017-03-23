using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class InterviewFeedbackModel
    {
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public decimal TotalExperience { get; set; }
        public string CurrentPosition { get; set; }
        public InterviewFeedbacks FeedbackRound1 { get; set; }
        public InterviewFeedbacks FeedbackRound2 { get; set; }
    }
    
    
}