using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class EmailNotifications
    {
        [Key]
        public int EmailNotificationID { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public string Subject { get; set; }
        public string BodyHtml { get; set; }
        public int Status { get; set; }
        public DateTime RecordDate { get; set; }
        
    }
    
}
