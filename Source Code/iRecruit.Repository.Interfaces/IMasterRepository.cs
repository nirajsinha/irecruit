
using System.Collections.Generic;
using System.Linq;
using System;
using iRecruit.Models;
using iRecruit.Entity;

namespace iRecruit.Repository.Interfaces
{
    public interface IMasterRepository
    {
        Company GetCompanyInfo(int companyId);
        List<Branches> GetBranches(int companyId);
        List<Departments> GetDepartments(int companyId);
        List<InterviewPanel> GetInterviewPanel(int companyId);
        List<Features> GetFeatures(int companyId);
        List<DepartmentRoles> GetDepartmentRoles();
        bool SaveDepartmentRoles(DepartmentRoles role);
        bool SaveSkills(TechnologiesAndSkills skill);
        List<TechnologiesAndSkills> GetTechnologyAndSkills(int companyId);
        List<iRecruit.Entity.Type> GetTypes();
        iRecruit.Entity.Type GetType(int typeClassCode, string typeCode);
        MasterDataModel GetMasterModel(int companyId);
        List<EmailNotifications> GetPendingEmailNotifications();
        int SaveEmailNotifications(EmailNotifications notification);
    }
}
