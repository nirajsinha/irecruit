using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class Candidates
    {
        [Key]
        public int CandidateID { get; set; }
        public string IndentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Skills { get; set; }
        public string CurrentTitle { get; set; }
        public string CurrentCompany { get; set; }
        public string CurrentLocation { get; set; }
        public decimal TotalExperience { get; set; }
        public bool Passport { get; set; }
        public bool Visa { get; set; }
        public bool TravelledOnsiteBefore { get; set; }
        public string Certifications { get; set; }
        public string AadhaarNumber { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Reference1Contact { get; set; }
        public string Reference2Contact { get; set; }
        public int ResumeSourceTypeID { get; set; }
        public string ResumeSourceDetail { get; set; }
        public decimal CurrentCTC { get; set; }
        public decimal ExpectedCTC { get; set; }
        public int CandidateStatusTypeID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
