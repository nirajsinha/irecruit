using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class InterviewPanel
    {
        [Key]
        public int InterviewPanelID { get; set; }
        public string Name { get; set; }
        public int CompanyID { get; set; }
        public string Departments { get; set; }
        public string Technologies { get; set; }
        public string Level { get; set; }
    }

}
