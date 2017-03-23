using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class OpenPositionsModel
    {
        public int OpenPositions { get; set; }
        public int OffersMade { get; set; }
        public int OnBoard { get; set; }
        public int RejectedDenied { get; set; }
        public int InProcess { get; set; }
    }
    public class ChartDataModel
    {
        public List<string> Series = new List<string>();
        public List<ChartData> Items = new List<ChartData>();
    }
    public class ChartData
    {
        public string x { get; set; }
        public List<int> y { get; set; }
        public string tooltip { get; set; }
    }
}