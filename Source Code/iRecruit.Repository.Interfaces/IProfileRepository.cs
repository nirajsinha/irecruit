
using System.Collections.Generic;
using System.Linq;
using System;
using iRecruit.Models;
using iRecruit.Entity;

namespace iRecruit.Repository.Interfaces
{
    public interface IProfileRepository
    {
        Candidates GetCandidate(int id);
        Resumes GetResume(int candId);
        InterviewFeedbackModel GetInterviewFeedbacks(int candidateId);
        List<ResumeSearchResult> SearchCandidates(ResumeSearchModel search);
        List<Consultancies> GetConsultancies();
        int SaveCandidate(CandidateModel candidate); 
        bool UpdateResumePath(int candId, string path, string fileType);
        int SaveInterviewFeedback(InterviewFeedbacks feedback);
        string ExecuteInterviewWorkflow(int candidateId);
        InterviewScheduleResult GetInterviewSchedule(int candidateId, int round);
        string SaveInterviewSchedule(InterviewSchedule schedule);
    }
}
