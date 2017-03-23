using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class ResumeSearchResult
    {

        public int TotalCount { get; set; }
        public int ROWNUM { get; set; }
        public int CandidateID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HireStatus { get; set; }
        public int TotalExperience { get; set; }
        public string Remarks { get; set; }
        public string Reference1 { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string ResumeVirtualPath { get; set; }
        public string ResumeFileName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
