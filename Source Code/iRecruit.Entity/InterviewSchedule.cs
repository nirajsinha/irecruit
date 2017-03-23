using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class InterviewSchedule
    {
        [Key]
        public int InterviewScheduleID { get; set; }
        public int CandidateID { get; set; } 
        public int InverviewRound { get; set; }
        public string ScheduledInterviewers { get; set; }
	    public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AttachResume { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class InterviewScheduleResult
    {

        public int InterviewScheduleID { get; set; }
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public string ContactNumber { get; set; }
        public int InverviewRound { get; set; }
        public string ScheduledInterviewers { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AttachResume { get; set; }
        public int Status { get; set; }
    }
}
