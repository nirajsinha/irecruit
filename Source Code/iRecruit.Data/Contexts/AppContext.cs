using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using iRecruit.Entity;
using Omu.ValueInjecter;
using System.Data.Entity.ModelConfiguration.Conventions;
using iRecruit.Data.Helpers;
using System.Dynamic;


namespace iRecruit.Data.Contexts
{
    public class AppContext: DbContext
    {
        public DbSet<Features> Features { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Indent> Indents { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<CandidatesHistory> CandidatesHistory { get; set; }
        public DbSet<DepartmentRoles> DepartmentRoles { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<InterviewPanel> InterviewPanels { get; set; }
        public DbSet<Resumes> Resumes { get; set; }
        public DbSet<InterviewFeedbacks> InterviewFeedbacks { get; set; }
        public DbSet<InterviewSchedule> InterviewSchedule { get; set; }
        public DbSet<Consultancies> Consultancies { get; set; }
        public DbSet<TechnologiesAndSkills> TechnologiesAndSkills { get; set; }
        public DbSet<TypeClass> TypeClass { get; set; }
        public DbSet<iRecruit.Entity.Type> Type { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<EmailNotifications> EmailNotifications { get; set; }

        public AppContext(): base ("Name=iRecruit")
        {
            var _ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention, the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public Company GetCompany(int companyId)
        {
            return (from f in this.Company 
                    where f.CompanyID == companyId
                    select f).FirstOrDefault();
        }
        public List<Branches> GetBranches(int companyId)
        {
            return (from f in this.Branches 
                    where f.CompanyID == companyId
                    select f).ToList();
        }
        public List<Departments> GetDepartments(int companyId)
        {
            return (from f in this.Departments 
                    where f.CompanyID == companyId && f.Active 
                    select f).ToList();
        }
        public List<DepartmentRoles> GetDepartmentRoles()
        {
            return (from f in this.DepartmentRoles where f.Active
                    select f).ToList();
        }
        public bool SaveDepartmentRoles(DepartmentRoles role)
        {
            try
            {
                var existingUserCount = this.DepartmentRoles.Count(a => a.BranchID.Equals(role.BranchID) && a.DepartmentID.Equals(role.DepartmentID));
                if (existingUserCount == 0)
                {
                    // insert user
                    role.Active = true;
                    DepartmentRoles.Add(role);
                }
                else
                {
                    DepartmentRoles u = this.DepartmentRoles.Where(a => a.BranchID.Equals(role.BranchID) && a.DepartmentID.Equals(role.DepartmentID)).FirstOrDefault<DepartmentRoles>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        u.BranchID = role.BranchID;
                        u.DepartmentID = role.DepartmentID;
                        u.FunctionHead = role.FunctionHead;
                        u.SVP = role.SVP;
                        u.Active = true;
                    }
                    this.Entry(u).State = EntityState.Modified;
                }
                this.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool SaveSkills(TechnologiesAndSkills skill)
        {
            try
            {
                var existingCount = this.TechnologiesAndSkills.Count(a => a.TechnologyAndSkillID.Equals(skill.TechnologyAndSkillID));
                if (existingCount == 0)
                {
                    // insert user
                    skill.Active = true;
                    TechnologiesAndSkills.Add(skill);
                }
                else
                {
                    TechnologiesAndSkills u = this.TechnologiesAndSkills.Where(s => s.TechnologyAndSkillID == skill.TechnologyAndSkillID).FirstOrDefault<TechnologiesAndSkills>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        u.CompanyID = skill.CompanyID;
                        u.Code = skill.Code;
                        u.Name = skill.Name;
                        u.SkillType = skill.SkillType;
                        u.Active = true;
                    }
                    this.Entry(u).State = EntityState.Modified;
                }
                this.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public List<Features> GetFeatures(int companyId)
        {
            return (from f in this.Features
                    where f.CompanyID == companyId 
                    select f).ToList();
        }
        public List<TechnologiesAndSkills> GetTechnologiesAndSkills(int companyId)
        {
            return (from f in this.TechnologiesAndSkills
                    where f.CompanyID == companyId && f.Active
                    select f).ToList();
        }
        public List<InterviewPanel> GetInterviewPanel(int companyId)
        {
            return (from f in this.InterviewPanels
                    where f.CompanyID == companyId
                    select f).ToList();
        }
        public List<Users> GetUsers(int companyId, string partialSearch)
        {
            if (string.IsNullOrWhiteSpace(partialSearch))
            {
                return (from f in this.Users
                        where f.CompanyID == companyId
                        select f).ToList();
            }
            
            return (from f in this.Users
                    where f.CompanyID == companyId && (f.UserID.Contains(partialSearch) || f.Name.Contains(partialSearch))
                    select f).ToList();
        }
        public Users GetUserDetails(string userid)
        {
            return (from f in this.Users
                   where f.UserID.Equals(userid)
                   select f).FirstOrDefault();
        }
        public Users GetUserDetailsByName(string user)
        {
            return (from f in this.Users
                    where f.Name.Equals(user)
                    select f).FirstOrDefault();
        }
        public List<iRecruit.Entity.Type> GetTypes()
        {
            return (from t in this.Type
                    join tc in this.TypeClass on t.TypeClassID equals tc.TypeClassID
                    select t).ToList();
        }
        public iRecruit.Entity.Type GetType(int id)
        {
            return (from t in this.Type
                    where t.TypeID == id
                    select t).FirstOrDefault();
        }
        public iRecruit.Entity.Type GetType(int typeClassCode, string typeCode)
        {
            return (from t in this.Type
                    join tc in this.TypeClass on t.TypeClassID equals tc.TypeClassID
                    where tc.Code.Equals(typeClassCode.ToString()) && t.Code.Equals(typeCode)
                    select t).FirstOrDefault();
            
        }
        public bool SaveUser(Users user)
        {
            try
            {
                var existingUserCount = this.Users.Count(a => a.UserID.Equals(user.UserID));
                if (existingUserCount == 0)
                {
                    // insert user
                    Users.Add(user);
                }
                else
                {
                    Users u = this.Users.Where(s => s.UserID == user.UserID).FirstOrDefault<Users>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        u.Title = user.Title;
                        u.Email = user.Email;
                        u.Branches = user.Branches;
                        u.AccessFeatures = user.AccessFeatures;
                        u.Photo = user.Photo;
                    }
                    this.Entry(u).State = EntityState.Modified;
                }
                this.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }

            
        }
        public dynamic GetOpenPositionsDetails(int companyId)
        {

            try
            {
                StoredProcedureParameter OpenPositionsParam = new StoredProcedureParameter("@OpenPositions", 0, DbType.Int32, ParameterDirection.Output);
                StoredProcedureParameter OffersMadeParam = new StoredProcedureParameter("@OffersMade", 0, DbType.Int32, ParameterDirection.Output);
                StoredProcedureParameter OnBoardParam = new StoredProcedureParameter("@OnBoard", 0, DbType.Int32, ParameterDirection.Output);
                StoredProcedureParameter RejectedDeniedParam = new StoredProcedureParameter("@RejectedDenied", 0, DbType.Int32, ParameterDirection.Output);
                StoredProcedureParameter InProcessParam = new StoredProcedureParameter("@InProcess", 0, DbType.Int32, ParameterDirection.Output);
                
                StoredProcedureHelper helper = new StoredProcedureHelper();
                List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
                {
                    new StoredProcedureParameter("@CompanyID", companyId, DbType.Int32, ParameterDirection.Input),
                    OpenPositionsParam,
                    OffersMadeParam,
                    OnBoardParam,
                    RejectedDeniedParam,
                    InProcessParam
                };
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.OpenPositions, parameters));
                dynamic d = new ExpandoObject();
                d.OpenPositions = OpenPositionsParam.Value;
                d.OffersMade = OffersMadeParam.Value;
                d.OnBoard = OnBoardParam.Value;
                d.RejectedDenied = RejectedDeniedParam.Value;
                d.InProcess = InProcessParam.Value;
                return d;
            }
            catch
            {
                throw;
            }
            
        }
        public dynamic GetIndentTrackerInfo(string indentNumber){

            StoredProcedureParameter NoOfPositionsParam = new StoredProcedureParameter("@NoOfPositions", 0, DbType.Int32, ParameterDirection.Output);
            StoredProcedureParameter OffersMadeParam = new StoredProcedureParameter("@OffersMade", 0, DbType.Int32, ParameterDirection.Output);
            StoredProcedureParameter OnBoardParam = new StoredProcedureParameter("@OnBoard", 0, DbType.Int32, ParameterDirection.Output);
            StoredProcedureParameter RejectedParam = new StoredProcedureParameter("@Rejected", 0, DbType.Int32, ParameterDirection.Output);
            StoredProcedureParameter OfferDeniedParam = new StoredProcedureParameter("@OfferDenied", 0, DbType.Int32, ParameterDirection.Output);
            StoredProcedureParameter InProcessParam = new StoredProcedureParameter("@InProcess", 0, DbType.Int32, ParameterDirection.Output);
   
            StoredProcedureHelper helper = new StoredProcedureHelper();
            List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
            {
                new StoredProcedureParameter("@IndentNumber", indentNumber, DbType.String, ParameterDirection.Input),
                NoOfPositionsParam,
                OffersMadeParam,
                OnBoardParam,
                RejectedParam,
                OfferDeniedParam,
                InProcessParam
            };
            var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.IndentTrackerInfo, parameters));
            dynamic d = new ExpandoObject();
            d.NoOfPositions  = NoOfPositionsParam.Value;
            d.OfferedMade = OffersMadeParam.Value;
            d.OnBoard = OnBoardParam.Value;
            d.Rejected = RejectedParam.Value;
            d.OfferDenied = OfferDeniedParam.Value;
            d.InProcess = InProcessParam.Value;
            return d;
            
        }
        public List<Tuple<string, int>> GetResumeSources(int companyId)
        {

            StoredProcedureHelper helper = new StoredProcedureHelper();
            List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
            {
                new StoredProcedureParameter("@CompanyID", companyId, DbType.Int32, ParameterDirection.Input)
            };
            var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.ResumeSources, parameters));
            //return helper.SerializeToResultObject<IndentTrackerData>(results.First()).ToList();
            List<Tuple<string, int>> sources = new List<Tuple<string,int>>();
            foreach(DataRow dr in results.First().Rows)
            {
                sources.Add(Tuple.Create(dr["ResumeSource"].ToString(), Convert.ToInt32(dr["CandidatesCount"])));
            }
            return sources;

        }
        public List<Tuple<string, int, int>> GetOfferJoiningRatio(int companyId)
        {
            StoredProcedureHelper helper = new StoredProcedureHelper();
            List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
            {
                new StoredProcedureParameter("@CompanyID", companyId, DbType.Int32, ParameterDirection.Input)
            };
            var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.OfferJoiningRatio, parameters));
            //return helper.SerializeToResultObject<IndentTrackerData>(results.First()).ToList();
            List<Tuple<string, int, int>> sources = new List<Tuple<string, int, int>>();
            foreach (DataRow dr in results.First().Rows)
            {
                sources.Add(Tuple.Create(dr["DepartmentName"].ToString(), Convert.ToInt32(dr["OffersMadeCount"]), Convert.ToInt32(dr["OnBoardCount"])));
            }
            return sources;
        }
        public List<Tuple<string, string, string, string, DateTime>> GetActivityLogs(string indentNumber)
        {
            var logs = (
                        from i in this.Indents
                        join t in this.ActivityLogs on i.IndentID equals t.IndentID
                        join tt in this.Type on t.LogTypeID equals tt.TypeID
                        where i.IndentNumber == indentNumber
                        select new { tt.Code, t.Header, t.Description, t.Comments, t.RecordDate }).ToList();

            return logs.OrderByDescending(x => x.RecordDate).Select( y => Tuple.Create(y.Code, y.Header, y.Description,y.Comments, y.RecordDate)).ToList();
        }
        public List<Tuple<string, int>> GetTopOpenings(int companyId)
        {
            List<Tuple<string, int>> returnVal = new List<Tuple<string, int>>();

            List<Indent> indents = GetIndents(companyId);
            // convert data into array
            string[] techs = indents.Select(l => l.Technologies).ToArray();
            // concat array into string
            string tech = techs.Aggregate((current, next) => current + "; " + next);
            // remove all junk and make array
            string[] source = tech.Split(new char[] { '.', '?', ' ', '!', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
            // remove duplicates
            var distinctWords = new List<string>(source.Distinct());
            //loop to get word by count
            foreach (string word in distinctWords)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    // Create the query.  Use ToLowerInvariant to match "data" and "Data"  
                    var matchQuery = from w in source
                                     where w.ToLowerInvariant() == word.ToLowerInvariant()
                                     select word;

                    // Count the matches, which executes the query. 
                    int wordCount = matchQuery.Count();
                    returnVal.Add(Tuple.Create(word, matchQuery.Count()));
                }
            }
            return returnVal
                .OrderByDescending(g => g.Item2)  
                .Take(5).ToList();
        }
        public Indent GetIndent(string indentNumber)
        {
            return (from t in this.Indents
                    where t.IndentNumber.Equals(indentNumber)
                    select t).FirstOrDefault<Indent>();
        }
        public List<Indent> GetIndents(int companyId)
        {
            try
            {
                StoredProcedureHelper helper = new StoredProcedureHelper();
                List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
                {
                    new StoredProcedureParameter("@CompanyID", companyId, DbType.Int32, ParameterDirection.Input)
                };
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.Indents, parameters));
                return helper.SerializeToResultObject<Indent>(results.First()).ToList();
            }
            catch
            {
                throw;
            }
        }
        public List<IndentTrackerData> GetIndentTrackerData(int companyId, int page = 0, int pageSize = 0)
        {
            try
            {
                StoredProcedureHelper helper = new StoredProcedureHelper();
                List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
                {
                    new StoredProcedureParameter("@PageNo", page, DbType.Int32, ParameterDirection.Input),
                    new StoredProcedureParameter("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input),
                    new StoredProcedureParameter("@CompanyID", companyId, DbType.Int32, ParameterDirection.Input)
                };
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.IndentTrackerData, parameters));
                return helper.SerializeToResultObject<IndentTrackerData>(results.First()).ToList();
            }
            catch
            {
                throw;
            }
       
        }
        public List<Candidates> GetCandidates()
        {
            return (from t in this.Candidates select t).ToList();
        }
        public List<Consultancies> GetConsultancies()
        {
            return (from t in this.Consultancies select t).ToList();
        }
        public int SaveCandidate(Candidates candidate) 
        {
            try
            {
                // add/update indent
                var count = this.Candidates.Count(a => a.CandidateID.Equals(candidate.CandidateID));
                if (count == 0)
                {
                    candidate.CreatedDate = DateTime.UtcNow;
                    Candidates.Add(candidate);
                }
                else
                {
                    Candidates u = this.Candidates.Where(s => s.CandidateID == candidate.CandidateID).FirstOrDefault<Candidates>();
                    // insert into candidate history before update
                    CandidatesHistory ch = new CandidatesHistory();
                    ch.CandidateID = u.CandidateID;
                    ch.IndentNumber = u.IndentNumber;
                    ch.FirstName = u.FirstName;
                    ch.LastName = u.LastName;
                    ch.Gender = u.Gender;
                    ch.DOB = u.DOB.HasValue ? u.DOB.Value : (DateTime?)null;
                    ch.Email = u.Email;
                    ch.ContactNumber = u.ContactNumber;
                    ch.Skills = u.Skills;
                    ch.CurrentTitle = u.CurrentTitle;
                    ch.CurrentCompany = u.CurrentCompany;
                    ch.CurrentLocation = u.CurrentLocation;
                    ch.Certifications = u.Certifications;
                    ch.TotalExperience = u.TotalExperience;
                    ch.Passport = u.Passport;
                    ch.Visa = u.Visa;
                    ch.AadhaarNumber = u.AadhaarNumber;
                    ch.TravelledOnsiteBefore = u.TravelledOnsiteBefore;
                    ch.Reference1 = u.Reference1;
                    ch.Reference1Contact = u.Reference1Contact;
                    ch.Reference2 = u.Reference2;
                    ch.Reference2Contact = u.Reference2Contact;
                    ch.ResumeSourceTypeID = u.ResumeSourceTypeID;
                    ch.ResumeSourceDetail = u.ResumeSourceDetail;
                    ch.CurrentCTC = u.CurrentCTC;
                    ch.ExpectedCTC = u.ExpectedCTC;
                    ch.CandidateStatusTypeID = u.CandidateStatusTypeID;
                    ch.Remarks = u.Remarks;
                    ch.CreatedBy = candidate.ModifiedBy;
                    ch.CreatedDate = DateTime.UtcNow;
                    CandidatesHistory.Add(ch);
                    // update candidate
                    if (u != null)
                    {
                        u.CandidateID = candidate.CandidateID;
                        u.IndentNumber = candidate.IndentNumber;
                        u.FirstName = candidate.FirstName;
                        u.LastName = candidate.LastName;
                        u.Gender = candidate.Gender;
                        u.DOB = candidate.DOB.HasValue ? candidate.DOB.Value : (DateTime?)null;
                        u.Email = candidate.Email;
                        u.ContactNumber = candidate.ContactNumber;
                        u.Skills = candidate.Skills;
                        u.CurrentTitle = candidate.CurrentTitle;
                        u.CurrentCompany = candidate.CurrentCompany;
                        u.CurrentLocation = candidate.CurrentLocation;
                        u.Certifications = candidate.Certifications;
                        u.TotalExperience = candidate.TotalExperience;
                        u.Passport = candidate.Passport;
                        u.Visa = candidate.Visa;
                        u.AadhaarNumber = candidate.AadhaarNumber;
                        u.TravelledOnsiteBefore = candidate.TravelledOnsiteBefore;
                        u.Reference1 = candidate.Reference1;
                        u.Reference1Contact = candidate.Reference1Contact;
                        u.Reference2 = candidate.Reference2;
                        u.Reference2Contact = candidate.Reference2Contact;
                        u.ResumeSourceTypeID = candidate.ResumeSourceTypeID;
                        u.ResumeSourceDetail = candidate.ResumeSourceDetail;
                        u.CurrentCTC = candidate.CurrentCTC;
                        u.ExpectedCTC = candidate.ExpectedCTC;
                        u.CandidateStatusTypeID = candidate.CandidateStatusTypeID;
                        u.Remarks = candidate.Remarks;
                        u.ModifiedBy = candidate.ModifiedBy;
                        u.ModifiedDate = DateTime.UtcNow;
                    }
                    this.Entry(u).State = EntityState.Modified;
                }

                this.SaveChanges();

                return candidate.CandidateID;
            }
            catch
            {
                throw;
            }
        }
        public Candidates GetCandidate(int id)
        {
            return (from t in this.Candidates
                    where t.CandidateID == id
                    select t).FirstOrDefault<Candidates>();
        }
        public Resumes GetResume(int candId)
        {
            return (from t in this.Resumes
                    where t.CandidateID == candId
                    select t).FirstOrDefault<Resumes>();
        }
        public InterviewFeedbacks GetInterviewFeedback(int candId, int round)
        {
            return (from t in this.InterviewFeedbacks
                    where t.CandidateID == candId && t.InterviewRound == round
                    select t).FirstOrDefault<InterviewFeedbacks>();
        }
        public int SaveResume(Resumes resume)
        {
            try
            {
                // add/update indent
                var count = this.Resumes.Count(a => a.CandidateID.Equals(resume.CandidateID));
                if (count == 0)
                {
                    Resumes.Add(resume);
                }
                else
                {
                    Resumes u = this.Resumes.Where(s => s.CandidateID == resume.CandidateID).FirstOrDefault<Resumes>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        u.CandidateID = resume.CandidateID;
                        u.CandidatePhoto = resume.CandidatePhoto;
                        u.FileType = resume.FileType;
                        u.ResumePath = resume.ResumePath;
                        
                    }
                    this.Entry(u).State = EntityState.Modified;
                }

                this.SaveChanges();

                return resume.CandidateID;
            }
            catch
            {
                throw;
            }
        }
        public int SaveInterviewFeedback(InterviewFeedbacks feedback)
        {
            try
            {
                // add/update feedback
                var count = this.InterviewFeedbacks.Count(a => a.InterviewFeedbacksID == feedback.InterviewFeedbacksID);
                if (count == 0)
                {
                    
                    feedback.CreatedDate = DateTime.UtcNow;
                    InterviewFeedbacks.Add(feedback);
                }
                else
                {
                    InterviewFeedbacks u = this.InterviewFeedbacks.Where(s => s.InterviewFeedbacksID == feedback.InterviewFeedbacksID).FirstOrDefault<InterviewFeedbacks>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        //u.IndentDate = indent.IndentDate.HasValue ? indent.IndentDate.Value : (DateTime?) null;
                        u.InterviewerName = feedback.InterviewerName;
                        u.PositionFor = feedback.PositionFor;
                        u.ReleventExperience = feedback.ReleventExperience;
                        u.ReleventExperienceDiscountReason = feedback.ReleventExperienceDiscountReason;
                        u.TechKnowledgeAreas = feedback.TechKnowledgeAreas;
                        u.TechKnowledgeAreasLevel = feedback.TechKnowledgeAreasLevel;
                        u.TechKnowledgeAreasComments = feedback.TechKnowledgeAreasComments;
                        u.AnalysisAreas = feedback.AnalysisAreas;
                        u.AnalysisAreasLevel = feedback.AnalysisAreasLevel;
                        u.AnalysisAreasComments = feedback.AnalysisAreasComments;
                        u.DesignAreas = feedback.DesignAreas;
                        u.DesignAreasLevel = feedback.DesignAreasLevel;
                        u.DesignAreasComments = feedback.DesignAreasComments;
                        u.CodingAreas = feedback.CodingAreas;
                        u.CodingAreasLevel = feedback.CodingAreasLevel;
                        u.CodingAreasComments = feedback.CodingAreasComments;
                        u.DatabaseAreas = feedback.DatabaseAreas;
                        u.DatabaseAreasLevel = feedback.DatabaseAreasLevel;
                        u.DatabaseAreasComments = feedback.DatabaseAreasComments;
                        u.TestingAreas = feedback.TestingAreas;
                        u.TestingAreasLevel = feedback.TestingAreasLevel;
                        u.TestingAreasComments = feedback.TestingAreasComments;
                        u.ResultOrientationLevel = feedback.ResultOrientationLevel;
                        u.ResultOrientationComments = feedback.ResultOrientationComments;
                        u.CommunicationSkillsLevel = feedback.CommunicationSkillsLevel;
                        u.CommunicationSkillsComments = feedback.CommunicationSkillsComments;
                        u.TeamWorkingLevel = feedback.TeamWorkingLevel;
                        u.TeamWorkingComments = feedback.TeamWorkingComments;
                        u.LeadershipCapabilityLevel = feedback.LeadershipCapabilityLevel;
                        u.LeadershipCapabilityComments = feedback.LeadershipCapabilityComments;
                        u.AttitudeLevel = feedback.AttitudeLevel;
                        u.AttitudeComments = feedback.AttitudeComments;
                        u.OverallRatingLevel = feedback.OverallRatingLevel;
                        u.OverallRatingComments = feedback.OverallRatingComments;
                        u.SelectionReason = feedback.SelectionReason;
                        u.PositivesRemarks = feedback.PositivesRemarks;
                        u.ConcernsGaps = feedback.ConcernsGaps;
                        u.PositionRecomended = feedback.PositionRecomended;
                        u.PositionSuggested = feedback.PositionSuggested;
                        u.AlternaticeCompetancy = feedback.AlternaticeCompetancy;
                        u.TrainingsNeededTechnical = feedback.TrainingsNeededTechnical;
                        u.TrainingNeededBehavioral = feedback.TrainingNeededBehavioral;
                        u.Status = feedback.Status;
                        u.ModifiedBy = feedback.ModifiedBy;
                        u.ModifiedDate = DateTime.UtcNow;

                    }
                    this.Entry(u).State = EntityState.Modified;
                }

                this.SaveChanges();

                return feedback.CandidateID;
            }
            catch
            {
                throw;
            }
        }
        public InterviewScheduleResult GetInterviewSchedule(int candidateId, int round)
        {
            try
            {
                StoredProcedureHelper helper = new StoredProcedureHelper();
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(
                            StoredProcedureNames.InterviewSchedule,
                            new List<StoredProcedureParameter>
                            {
                                new StoredProcedureParameter("@CandidateID", candidateId, DbType.Int32, ParameterDirection.Input),
                                new StoredProcedureParameter("@InverviewRound", round, DbType.Int32, ParameterDirection.Input)
                                
                            }));
                return helper.SerializeToResultObject<InterviewScheduleResult>(results.First()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public bool SaveInterviewSchedule(InterviewSchedule schedule)
        {
            try
            {
                var existingCount = this.InterviewSchedule.Count(a => a.InterviewScheduleID.Equals(schedule.InterviewScheduleID));
                if (existingCount == 0)
                {
                    // insert user
                    schedule.CreatedDate = DateTime.UtcNow;
                    InterviewSchedule.Add(schedule);
                }
                else
                {
                    InterviewSchedule u = this.InterviewSchedule.Where(a => a.InterviewScheduleID.Equals(schedule.InterviewScheduleID)).FirstOrDefault<InterviewSchedule>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        
                        u.InverviewRound = schedule.InverviewRound;
                        u.ScheduledInterviewers = schedule.ScheduledInterviewers;
                        u.Subject = schedule.Subject;
                        u.Description = schedule.Description;
                        u.StartTime = schedule.StartTime;
                        u.EndTime = schedule.EndTime;
                        u.AttachResume = schedule.AttachResume;
                        u.ModifiedDate  = DateTime.UtcNow;
                    }
                    this.Entry(u).State = EntityState.Modified;
                }
                this.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public List<EmailNotifications> GetPendingEmailNotifications()
        {
            try
            {
                return (from t in this.EmailNotifications
                        where t.Status == 0
                        select t).ToList();
            }
            catch
            {
                throw;
            }
        }
        public int SaveEmailNotifications(EmailNotifications notification)
        {
            try
            {
                notification.RecordDate = DateTime.UtcNow;
                EmailNotifications.Add(notification);
                this.SaveChanges();
                return notification.EmailNotificationID;
            }
            catch
            {
                throw;
            }
        }
        public int SaveIndent(Indent indent)
        {
            try
            {
                // add into technologies if not found
                int compid = (from t in this.Departments
                            where t.DepartmentID == indent.DepartmentID
                            select t.CompanyID).FirstOrDefault();
                if (indent.Technologies != null)
                {
                    string[] techs = indent.Technologies.Split(';');
                    if (techs.Length > 0)
                    {
                        foreach (string tech in techs)
                        {
                            if (!string.IsNullOrWhiteSpace(tech))
                            {
                                string t = tech.Trim();
                                var cnt = this.TechnologiesAndSkills.Count(a => a.Code.Trim().Equals(t));
                                if (cnt == 0)
                                {
                                    TechnologiesAndSkills a = new Entity.TechnologiesAndSkills()
                                    {
                                        Code = t,
                                        Name = t,
                                        Active = true,
                                        CompanyID = compid,
                                        SkillType = 1
                                    };
                                    TechnologiesAndSkills.Add(a);
                                }
                            }
                        }
                    }
                }
                // add/update indent
                var count = this.Indents.Count(a => a.IndentID.Equals(indent.IndentID));
                if (count == 0)
                {
                    var code = (from t in this.Departments
                            join c in this.Company on t.CompanyID equals c.CompanyID
                            where t.DepartmentID == indent.DepartmentID
                            select new { CompanyCode = c.Code , DepartmentCode = t.Code }).FirstOrDefault();
                    
                    //Tuple<string, string> d = code.Select(x => new Tuple<string, string>(x.CompanyCode, x.DepartmentCode)).FirstOrDefault();
                    //int max = from t in this.Indents
                    int maxValue = 1;
                    try
                    {
                        var i = this.Indents.Count(x => x.DepartmentID.Equals(indent.DepartmentID) && x.CreatedDate.Value.Year.Equals(DateTime.Now.Year));
                        maxValue += i;
                    }
                    catch { }
                    indent.IndentNumber = code.CompanyCode + "-" + code.DepartmentCode + "-" + DateTime.UtcNow.Year + "-" + maxValue.ToString().PadLeft(4, '0');
                    var dr = (from t in this.DepartmentRoles
                              join u in this.Users on t.FunctionHead equals u.UserID
                              join u1 in this.Users on t.SVP equals u1.UserID
                                where t.DepartmentID == indent.DepartmentID && t.BranchID == indent.BranchID
                              select new { FunctionHead = u.Name, SVP = u1.Name }).FirstOrDefault();
                    
                    indent.CreatedDate = DateTime.UtcNow;
                    indent.FunctionHead = dr.FunctionHead;
                    indent.SeniorVicePresident = dr.SVP;
                    Indents.Add(indent);
                }
                else
                {
                    Indent u = this.Indents.Where(s => s.IndentID == indent.IndentID).FirstOrDefault<Indent>();
                    // change contact in disconnected mode (out of DBContext scope)
                    if (u != null)
                    {
                        //u.IndentDate = indent.IndentDate.HasValue ? indent.IndentDate.Value : (DateTime?) null;
                        u.Indentor = indent.Indentor;
                        u.IndentorRemarks = indent.IndentorRemarks;
                        u.BranchID = indent.BranchID;
                        u.DepartmentID = indent.DepartmentID;
                        u.Client_Domain = indent.Client_Domain;
                        u.ProjectStatusID = indent.ProjectStatusID;
                        u.ReasonID = indent.ReasonID;
                        u.LocationTypeID = indent.LocationTypeID;
                        u.EmploymentTypeID = indent.EmploymentTypeID;
                        u.ContractMonths = indent.ContractMonths;
                        u.StaffingModeID = indent.StaffingModeID;
                        u.Technologies = indent.Technologies;
                        u.TechnicalSkills = indent.TechnicalSkills;
                        u.BehaviouralSkills = indent.BehaviouralSkills;
                        u.PositionTitle = indent.PositionTitle;
                        u.NoOfPositions = indent.NoOfPositions;
                        u.MinExperiance = indent.MinExperiance;
                        u.MaxExperiance = indent.MaxExperiance;
                        u.VisaType = indent.VisaType;
                        u.TargetJoinDate = indent.TargetJoinDate.HasValue ? indent.TargetJoinDate.Value : (DateTime?)null; 
                        u.InterviewPanel1 = indent.InterviewPanel1;
                        u.InterviewPanel2 = indent.InterviewPanel2;
                        u.ReportingManager = indent.ReportingManager;
                        u.Qualification = indent.Qualification;
                        u.Others = indent.Others;
                        u.FunctionHead = indent.FunctionHead;
                        //u.FunctionHeadStatusDate = indent.FunctionHeadStatusDate.HasValue ? indent.FunctionHeadStatusDate.Value : (DateTime?)null; ;
                        u.FunctionHeadRemarks = indent.FunctionHeadRemarks;
                        u.SeniorVicePresident = indent.SeniorVicePresident;
                        //u.SeniorVicePresidentStatusDate = indent.SeniorVicePresidentStatusDate.HasValue ? indent.SeniorVicePresidentStatusDate.Value : (DateTime?)null; ;
                        u.SeniorVicePresidentRemarks = indent.SeniorVicePresidentRemarks;
                        u.JobDescription = indent.JobDescription;
                        u.UploadFile_Indents = indent.UploadFile_Indents;
                        u.DeploymentLocation = indent.DeploymentLocation;
                        u.Indent_Status = indent.Indent_Status;
                        u.StatusChangedBy = indent.StatusChangedBy;
                        u.ModifiedBy = indent.ModifiedBy;
                        u.ModifiedDate = DateTime.UtcNow; 

                    }
                    this.Entry(u).State = EntityState.Modified;
                }

                this.SaveChanges();
                
                return indent.IndentID;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateJDFilePath(int indentId, string path)
        {
            try
            {
                Indent indent = this.Indents.Where(a => a.IndentID.Equals(indentId)).FirstOrDefault<Indent>();
                // change contact in disconnected mode (out of DBContext scope)
                if (indent != null)
                {
                    indent.UploadFile_Indents = path;
                }
                this.Entry(indent).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateResumePath(int candId, string path, string fileType)
        {
            try
            {
                Resumes res = this.Resumes.Where(a => a.CandidateID.Equals(candId)).FirstOrDefault<Resumes>();
                // change contact in disconnected mode (out of DBContext scope)
                if (res != null)
                {
                    res.ResumePath = path;
                    res.FileType = fileType;
                }
                this.Entry(res).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public ExecuteIndentWorkFlowResult ExecuteIndentWorkflow(int indentId)
        {
            try
            {
                StoredProcedureHelper helper = new StoredProcedureHelper();
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(
                            StoredProcedureNames.IndentWorkflow,
                            new List<StoredProcedureParameter>
                            {
                                new StoredProcedureParameter("@IndentID", indentId, DbType.Int32, ParameterDirection.Input)
                            }));
                return helper.SerializeToResultObject<ExecuteIndentWorkFlowResult>(results.First()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public string ExecuteInterviewWorkflow(int candidateId)
        {

            try
            {
                StoredProcedureHelper helper = new StoredProcedureHelper();
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(
                            StoredProcedureNames.InterviewWorkflow,
                            new List<StoredProcedureParameter>
                            {
                                new StoredProcedureParameter("@CandidateID", candidateId, DbType.Int32, ParameterDirection.Input)
                            }));
                if(results.First() != null && results.First().Rows.Count > 0 && results.First().Rows[0][0] != null)
                {
                    return results.First().Rows[0][0].ToString();
                }
                return "";
            }
            catch
            {
                throw;
            }
        }
        public List<ResumeSearchResult> SearchCandidates(string SearchType, string search, string FirstName, string LastName, string Email, string ContactNumber, int MinExperience, int ResumeSourceTypeID, string ResumeSourceDetail, string Skills, int Passport, int Visa, int TravelledOnsiteBefore, string Gender, string Certifications, string CandidateStatus, int pageNo, int pageSize, string sortColumn, string sortDirection)
        {
            try
            {
                StoredProcedureHelper helper = new StoredProcedureHelper();
                List<StoredProcedureParameter> parameters = new List<StoredProcedureParameter>()
                {
                    new StoredProcedureParameter("@PageNo", pageNo, DbType.Int32, ParameterDirection.Input),
                    new StoredProcedureParameter("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input),
                    new StoredProcedureParameter("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
                    new StoredProcedureParameter("@SortOrder", sortDirection, DbType.String, ParameterDirection.Input)
                };
                if (SearchType.Equals("Simple"))
                {
                    parameters.Add(new StoredProcedureParameter("@SearchString", search, DbType.String, ParameterDirection.Input));
                }
                else {
    
                    parameters.Add(new StoredProcedureParameter("@FirstName", FirstName, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@LastName", LastName, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@Email", Email, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@ContactNumber", ContactNumber, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@MinExperience", MinExperience, DbType.Int32, ParameterDirection.Input));
                    if (ResumeSourceTypeID > 0) parameters.Add(new StoredProcedureParameter("@ResumeSourceTypeID", ResumeSourceTypeID, DbType.Int32, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@ResumeSourceDetail", ResumeSourceDetail, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@Skills", Skills, DbType.String, ParameterDirection.Input));
                    if (Passport > 0) parameters.Add(new StoredProcedureParameter("@Passport", Passport, DbType.Boolean, ParameterDirection.Input));
                    if (Visa > 0) parameters.Add(new StoredProcedureParameter("@Visa", Visa, DbType.Boolean, ParameterDirection.Input));
                    if (TravelledOnsiteBefore > 0) parameters.Add(new StoredProcedureParameter("@TravelledOnsiteBefore", TravelledOnsiteBefore, DbType.Boolean, ParameterDirection.Input));
                    if (!string.IsNullOrWhiteSpace(Gender)) parameters.Add(new StoredProcedureParameter("@Gender", Gender, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@Certifications", Certifications, DbType.String, ParameterDirection.Input));
                    parameters.Add(new StoredProcedureParameter("@CandidateStatus", CandidateStatus, DbType.String, ParameterDirection.Input));
                }
                var results = helper.MultiResultStoredProcedure(new StoredProcedureSettings(StoredProcedureNames.ResumeSearch, parameters));
                return helper.SerializeToResultObject<ResumeSearchResult>(results.First()).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
