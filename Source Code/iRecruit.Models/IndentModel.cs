using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class IndentModel
    {
        public Indent Indent { get; set; }
        public int DisableIndenterAccess { get; set; }
        public int DisableFHAccess { get; set; }
        public int DisableSVPAccess { get; set; }
        List<string> Files = new List<string>();
        public string JDUrl { get; set; }
    }
    
}