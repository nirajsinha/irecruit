using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class CandidateModel
    {
        public Candidates Candidate { get; set; }
        public Resumes Resume { get; set; }
        public string ResumeVirtualPath { get; set; }
    }
    
}