using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class Indent
    {
        [Key]
        public int IndentID { get; set; }
        public string IndentNumber { get; set; }
        public DateTime? IndentDate { get; set; }
        public int BranchID { get; set; }
        public int DepartmentID { get; set; }
        public string Client_Domain { get; set; }
        public int ProjectStatusID { get; set; }
        public int ReasonID { get; set; }
        public int LocationTypeID { get; set; }
        public int EmploymentTypeID { get; set; }
        public int StaffingModeID { get; set; }
        public int? ContractMonths { get; set; }
        public string Technologies { get; set; }
        public string TechnicalSkills { get; set; }
        public string BehaviouralSkills { get; set; }
        public string PositionTitle { get; set; }
        public int NoOfPositions { get; set; }
        public int MinExperiance { get; set; }
        public int MaxExperiance { get; set; }
        public string VisaType { get; set; }
        public DateTime? TargetJoinDate { get; set; }
        public string InterviewPanel1 { get; set; }
        public string InterviewPanel2 { get; set; }
        public string InterviewPanel3 { get; set; }
        public string InterviewPanel4 { get; set; }
        public string InterviewPanel5 { get; set; }
        public string ReportingManager { get; set; }
        public string DeploymentLocation { get; set; }
        public string Qualification { get; set; }
        public string Others { get; set; }
        public string Indentor { get; set; }
        public int Indent_Status { get; set; }
        public string StatusChangedBy { get; set; }
        public string IndentorRemarks { get; set; }
        public string FunctionHead { get; set; }
        public int FunctionHeadStatusTypeID { get; set; }
        public DateTime? FunctionHeadStatusDate { get; set; }
        public string FunctionHeadRemarks { get; set; }
        public string SeniorVicePresident { get; set; }
        public int SeniorVicePresidentStatusTypeID { get; set; }
        public DateTime? SeniorVicePresidentStatusDate { get; set; }
        public string SeniorVicePresidentRemarks { get; set; }
        public string JobDescription { get; set; }
        public string UploadFile_Indents { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

}
