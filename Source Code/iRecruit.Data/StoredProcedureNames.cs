using System.Collections.Generic;
using System.Data;

namespace iRecruit.Data
{
    public static class StoredProcedureNames
    {
        public const string IndentWorkflow = "[dbo].[prcExecuteIndentWorkflow]";
        public const string ResumeSearch = "[dbo].[prcResumeSearch]";
        public const string InterviewWorkflow = "[dbo].[prcExcuteInterviewWorkflow]";
        public const string IndentTrackerData = "[dbo].[prcGetIndentTrackerData]";
        public const string Indents = "[dbo].[prcGetIndents]";
        public const string OpenPositions = "[dbo].[prcGetOpenPositions]";
        public const string IndentTrackerInfo = "[dbo].[prcGetIndentTrackerInfo]";
        public const string ResumeSources = "[dbo].[prcGetResumeSources]";
        public const string OfferJoiningRatio = "[dbo].[prcGetOfferJoiningRatio]";
        public const string InterviewSchedule = "[dbo].[prcGetInterviewSchedule]";

    }

    
}
