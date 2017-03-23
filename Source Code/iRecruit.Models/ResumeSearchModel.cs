using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class ResumeSearchModel
    {
        public string SearchType { get; set; }
        public string search { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int MinExperience { get; set; }
        public int ResumeSourceTypeID { get; set; }
        public string CandidateStatus { get; set; }
        public string ResumeSourceDetail { get; set; }
        public string Skills { get; set; }
        public int Passport { get; set; }
        public int Visa { get; set; }
        public int TravelledOnsiteBefore { get; set; }
        public string Gender { get; set; }
        public string Certifications { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
    
    
}