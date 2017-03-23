using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRecruit.Models;
using iRecruit.Data.Contexts;
using iRecruit.Repository.Interfaces;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using iRecruit.Entity;

namespace iRecruit.Repository 
{
   public class MasterRepository : RepositoryBase<AppContext>, IMasterRepository
    {
       public Company GetCompanyInfo(int companyId)
       {
           return this.DataContext.GetCompany(companyId);
       }

       public List<Branches> GetBranches(int companyId)
       {
           return this.DataContext.GetBranches(companyId);
       }
       public List<Departments> GetDepartments(int companyId)
       {
           return this.DataContext.GetDepartments(companyId);
       }

       public List<InterviewPanel> GetInterviewPanel(int companyId)
       {
           return this.DataContext.GetInterviewPanel(companyId);
       }
       public List<Features> GetFeatures(int companyId)
       {
           return this.DataContext.GetFeatures(companyId);
       }
       public List<DepartmentRoles> GetDepartmentRoles()
       {
           return this.DataContext.GetDepartmentRoles();
       }
       public bool SaveDepartmentRoles(DepartmentRoles role)
       {
           return this.DataContext.SaveDepartmentRoles(role);
       }
       public bool SaveSkills(TechnologiesAndSkills skill)
       {
           return this.DataContext.SaveSkills(skill);
       }
       public List<TechnologiesAndSkills> GetTechnologyAndSkills(int companyId)
       {
           return this.DataContext.GetTechnologiesAndSkills(companyId);
       }
       public List<iRecruit.Entity.Type> GetTypes()
       {
           return this.DataContext.GetTypes();
       }
       public iRecruit.Entity.Type GetType(int typeClassCode, string typeCode)
       {
           return this.DataContext.GetType(typeClassCode, typeCode);
       }

       public MasterDataModel GetMasterModel(int companyId)
       {
           MasterDataModel model = new MasterDataModel();
           model.Company = GetCompanyInfo(companyId);
           model.Branches = GetBranches(companyId);
           model.Departments = GetDepartments(companyId);
           model.InterviewPanel = GetInterviewPanel(companyId);
           model.TechnologyAndSkills = GetTechnologyAndSkills(companyId);
           model.Types = GetTypes();
           return model;
       }
       public List<EmailNotifications> GetPendingEmailNotifications()
       {
           return this.DataContext.GetPendingEmailNotifications();
       }
       public int SaveEmailNotifications(EmailNotifications notification)
       {
           return this.DataContext.SaveEmailNotifications(notification);
       }
    }
}
