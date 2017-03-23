using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRecruit.Models;
using iRecruit.Data.Contexts;
using iRecruit.Repository.Interfaces;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using iRecruit.Entity;

namespace iRecruit.Repository 
{
   public class ProfileRepository : RepositoryBase<AppContext>, IProfileRepository
    {
        public Candidates GetCandidate(int id)
        {
            return this.DataContext.GetCandidate(id);
        }
        public Resumes GetResume(int candId)
        {
            return this.DataContext.GetResume(candId);
        }
        public InterviewFeedbackModel GetInterviewFeedbacks(int candidateId)
        {
            Candidates cand = GetCandidate(candidateId);
            InterviewFeedbackModel model = new InterviewFeedbackModel()
            {
                CandidateID = cand.CandidateID,
                CandidateName = cand.FirstName + " " + cand.LastName,
                TotalExperience = cand.TotalExperience,
                CurrentPosition = cand.CurrentTitle
            };
            model.FeedbackRound1 = this.DataContext.GetInterviewFeedback(candidateId, 1);
            model.FeedbackRound2 = this.DataContext.GetInterviewFeedback(candidateId, 2);
            return model;
        }
        public List<ResumeSearchResult> SearchCandidates(ResumeSearchModel model)
        {
            return this.DataContext.SearchCandidates(model.SearchType, model.search, model.FirstName, model.LastName,
                model.Email, model.ContactNumber, model.MinExperience, model.ResumeSourceTypeID, model.ResumeSourceDetail,
                model.Skills, model.Passport, model.Visa, model.TravelledOnsiteBefore, model.Gender, model.Certifications, model.CandidateStatus, model.PageNo, model.PageSize,
                model.SortColumn, model.SortDirection);
        }
       public List<Consultancies> GetConsultancies()
       {
           return this.DataContext.GetConsultancies();
       }
       public int SaveCandidate(CandidateModel model)
       {
           int candId = this.DataContext.SaveCandidate(model.Candidate);
           if (!string.IsNullOrWhiteSpace(model.Resume.ResumePath))
           {
               model.Resume.CandidateID = candId;
               return this.DataContext.SaveResume(model.Resume);
           }
           return candId;
       }
       public int SaveInterviewFeedback(InterviewFeedbacks feedback)
       {
           return this.DataContext.SaveInterviewFeedback(feedback);
       }
       public bool UpdateResumePath(int candId, string path, string fileType)
       {
           return this.DataContext.UpdateResumePath(candId, path,  fileType);
       }

       public string ExecuteInterviewWorkflow(int candidateId)
       {
           return this.DataContext.ExecuteInterviewWorkflow(candidateId);
       }

       public InterviewScheduleResult GetInterviewSchedule(int candidateId, int round)
       {
           return this.DataContext.GetInterviewSchedule(candidateId, round);
       }
       public string SaveInterviewSchedule(InterviewSchedule schedule)
       {
           List<string> emails = new List<string>();
           bool success = this.DataContext.SaveInterviewSchedule(schedule);
           if (success)
           {
               string[] users = schedule.ScheduledInterviewers.Split(',');
               foreach (string user in users)
               {
                   Users u = this.DataContext.GetUserDetailsByName(user);
                   if(u != null) emails.Add(u.Email);
               }
           }
           return string.Join("; ", emails.ToArray()); 
       }
    }
}
