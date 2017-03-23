
-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcInterviewFeedback   24 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
prcInterviewFeedback 24
*/
CREATE PROCEDURE prcInterviewFeedback
(
   @CandidateID INT
)
AS
BEGIN
    
	Select CandidateID,
		FirstName,
		LastName,
		CurrentTitle,
		TotalExperience
	from Candidates where CandidateID = @CandidateID
	
	select CandidateID,
			InterviewRound,
			InterviewerName,
			PositionFor,
			ReleventExperience,
			ReleventExperienceDiscountReason,
			TechKnowledgeAreas,
			TechKnowledgeAreasLevel,
			TechKnowledgeAreasComments,
			AnalysisAreas,
			AnalysisAreasLevel,
			AnalysisAreasComments,
			DesignAreas,
			DesignAreasLevel,
			DesignAreasComments,
			CodingAreas,
			CodingAreasLevel,
			CodingAreasComments,
			DatabaseAreas,
			DatabaseAreasLevel,
			DatabaseAreasComments,
			TestingAreas,
			TestingAreasLevel,
			TestingAreasComments,
			ResultOrientationLevel,
			ResultOrientationComments,
			CommunicationSkillsLevel,
			CommunicationSkillsComments,
			TeamWorkingLevel,
			TeamWorkingComments,
			LeadershipCapabilityLevel,
			LeadershipCapabilityComments,
			AttitudeLevel,
			AttitudeComments,
			OverallRatingLevel,
			OverallRatingComments,
			SelectionReason,
			PositivesRemarks,
			ConcernsGaps,
			PositionRecomended,
			PositionSuggested,
			AlternaticeCompetancy,
			TrainingsNeededTechnical,
			TrainingNeededBehavioral,
			CreatedBy,
			CreatedDate
	from InterviewFeedbacks
	where InterviewRound = 1
	and CandidateID = @CandidateID
	
	-- round 2 feedback
	select CandidateID,
			InterviewRound,
			InterviewerName,
			PositionFor,
			ReleventExperience,
			ReleventExperienceDiscountReason,
			TechKnowledgeAreas,
			TechKnowledgeAreasLevel,
			TechKnowledgeAreasComments,
			AnalysisAreas,
			AnalysisAreasLevel,
			AnalysisAreasComments,
			DesignAreas,
			DesignAreasLevel,
			DesignAreasComments,
			CodingAreas,
			CodingAreasLevel,
			CodingAreasComments,
			DatabaseAreas,
			DatabaseAreasLevel,
			DatabaseAreasComments,
			TestingAreas,
			TestingAreasLevel,
			TestingAreasComments,
			ResultOrientationLevel,
			ResultOrientationComments,
			CommunicationSkillsLevel,
			CommunicationSkillsComments,
			TeamWorkingLevel,
			TeamWorkingComments,
			LeadershipCapabilityLevel,
			LeadershipCapabilityComments,
			AttitudeLevel,
			AttitudeComments,
			OverallRatingLevel,
			OverallRatingComments,
			SelectionReason,
			PositivesRemarks,
			ConcernsGaps,
			PositionRecomended,
			PositionSuggested,
			AlternaticeCompetancy,
			TrainingsNeededTechnical,
			TrainingNeededBehavioral,
			CreatedBy,
			CreatedDate
	from InterviewFeedbacks
	where InterviewRound = 2
	and CandidateID = @CandidateID

END
