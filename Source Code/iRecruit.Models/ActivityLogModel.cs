using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class ActivityLogModel
    {
        public List<Activity> Items = new List<Activity>();
    }
    
    public class Activity
    {
        public bool Label { get; set; }
        public string LogType { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}