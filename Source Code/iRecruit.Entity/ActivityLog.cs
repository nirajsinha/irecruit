using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class ActivityLog
    {
        [Key]
        public int ActivityLogID { get; set; }
        public int IndentID { get; set; }
        public string UserID { get; set; }
        public int LogTypeID { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime RecordDate { get; set; }
    }
    
}
