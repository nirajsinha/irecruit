using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace iRecruit.Models
{
    public class IndentTrackerInfoModel
    {
        public int NoOfPositions { get; set; }
        public int InProcess { get; set; }
        public int OfferedMade {get; set; }
        public int OnBoard { get; set; }
        public int Rejected { get; set; }
        public int OfferDenied { get; set; }
    }
}