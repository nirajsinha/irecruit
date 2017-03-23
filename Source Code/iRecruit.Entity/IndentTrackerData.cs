using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace iRecruit.Entity
{
    public class IndentTrackerData
    {
        public int TotalCount { get; set; }
        public int ROWNUM { get; set; }
        public string IndentNumber { get; set; }
        public DateTime? IndentDate { get; set; }
        public int NoOfPositions { get; set; }
        public string PositionTitle { get; set; }
        public string Client_Domain { get; set; }
        public string AssignedTo { get; set; }
        public string Indent_Status { get; set; }
        public string Technologies { get; set; }

    }
    
}