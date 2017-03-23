using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using iRecruit.Entity;

namespace iRecruit.Models
{
    public class MasterDataModel
    {
        public Company Company { get; set; }
        public List<Branches> Branches = new List<Branches>();
        public List<Departments> Departments = new List<Departments>();
        public List<InterviewPanel> InterviewPanel = new List<InterviewPanel>();
        public List<Features> Features = new List<Features>();
        public List<DepartmentRoles> DepartmentRoles = new List<DepartmentRoles>();
        public List<TechnologiesAndSkills> TechnologyAndSkills = new List<TechnologiesAndSkills>();
        public List<iRecruit.Entity.Type> Types = new List<iRecruit.Entity.Type>();

    }
    public class AutoCompleteResults
    { 
        public string value { get; set; }
        public string label { get; set; }
    }
    public class MenuModel
    {
        public bool DashboardEnabled { get; set; }
        public bool IndentEnabled { get; set; }
        public bool ResumeManagementEnabled { get; set; }
        public bool SettingsEnabled { get; set; }
        public bool ReportsEnabled { get; set; }
        public bool InterviewsEnabled { get; set; }
        
    }
}