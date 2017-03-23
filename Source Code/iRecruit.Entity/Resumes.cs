using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class Resumes
    {
        [Key]
        public int ResumeID { get; set; }
        public int CandidateID { get; set; }
        public string ResumePath { get; set; }
        public string FileType { get; set; }
        public string CandidatePhoto { get; set; }
    }
}
