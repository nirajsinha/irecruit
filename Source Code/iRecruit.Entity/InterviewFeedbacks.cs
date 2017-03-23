using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class InterviewFeedbacks
    {
        [Key]
        public int InterviewFeedbacksID { get; set;}
        public int CandidateID { get; set; }
        public int InterviewRound { get; set; }
        public string InterviewerName { get; set; }
        public string PositionFor { get; set; }
        public decimal ReleventExperience { get; set; }
        public string ReleventExperienceDiscountReason { get; set; }
        public string TechKnowledgeAreas { get; set; }
        public string TechKnowledgeAreasLevel { get; set; }
        public string TechKnowledgeAreasComments { get; set; }
        public string AnalysisAreas { get; set; }
        public string AnalysisAreasLevel { get; set; }
        public string AnalysisAreasComments { get; set; }
        public string DesignAreas { get; set; }
        public string DesignAreasLevel { get; set; }
        public string DesignAreasComments { get; set; }
        public string CodingAreas { get; set; }
        public string CodingAreasLevel { get; set; }
        public string CodingAreasComments { get; set; }
        public string DatabaseAreas { get; set; }
        public string DatabaseAreasLevel { get; set; }
        public string DatabaseAreasComments { get; set; }
        public string TestingAreas { get; set; }
        public string TestingAreasLevel { get; set; }
        public string TestingAreasComments { get; set; }
        public string ResultOrientationLevel { get; set; }
        public string ResultOrientationComments { get; set; }
        public string CommunicationSkillsLevel { get; set; }
        public string CommunicationSkillsComments { get; set; }
        public string TeamWorkingLevel { get; set; }
        public string TeamWorkingComments { get; set; }
        public string LeadershipCapabilityLevel { get; set; }
        public string LeadershipCapabilityComments { get; set; }
        public string AttitudeLevel { get; set; }
        public string AttitudeComments { get; set; }
        public string OverallRatingLevel { get; set; }
        public string OverallRatingComments { get; set; }
        public string SelectionReason { get; set; }
        public string PositivesRemarks { get; set; }
        public string ConcernsGaps { get; set; }
        public string PositionRecomended { get; set; }
        public string PositionSuggested { get; set; }
        public string AlternaticeCompetancy { get; set; }
        public string TrainingsNeededTechnical { get; set; }
        public string TrainingNeededBehavioral { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

}
