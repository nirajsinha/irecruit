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
   public class IndentRepository : RepositoryBase<AppContext>, IIndentRepository
    {
       public Indent GetIndent(string indentNumber)
       {
           return this.DataContext.GetIndent(indentNumber);
           //IndentModel model = new IndentModel();
           //CopyPropertiesFrom(model, indent, new string[] { "ProjectStatusID", "ReasonID", "LocationTypeID", "EmploymentTypeID", "StaffingModeID", "FunctionHeadStatusTypeID", "SeniorVicePresidentStatusTypeID" });
           //model.ProjectStatusType =  this.DataContext.GetType(indent.ProjectStatusID);
           //model.ReasonType  =  this.DataContext.GetType(indent.ReasonID);
           //model.LocationType =  this.DataContext.GetType(indent.LocationTypeID);
           //model.EmploymentType =  this.DataContext.GetType(indent.EmploymentTypeID);
           //model.StaffingMode =  this.DataContext.GetType(indent.StaffingModeID);
           //model.FunctionHeadStatusType =  this.DataContext.GetType(indent.FunctionHeadStatusTypeID);
           //model.SeniorVicePresidentStatusType = this.DataContext.GetType(indent.SeniorVicePresidentStatusTypeID);
       }
       public List<Indent> GetIndents(int companyId)
       {
           return this.DataContext.GetIndents(companyId);
       }
       public List<IndentTrackerData> GetIndentTrackerData(int companyId, int page = 1, int pageSize=0)
       {
           return this.DataContext.GetIndentTrackerData(companyId, page, pageSize);
       }
       public IndentTrackerInfoModel GetIndentTrackerInfoModel(string indentNumber)
       {
           dynamic obj = this.DataContext.GetIndentTrackerInfo(indentNumber);
           return new IndentTrackerInfoModel()
           {
               NoOfPositions = obj.NoOfPositions,
               OfferedMade = obj.OfferedMade,
               OnBoard = obj.OnBoard,
               Rejected = obj.Rejected,
               OfferDenied = obj.OfferDenied
           };
       }
       public int SaveIndent(Indent indent)
       {
           return this.DataContext.SaveIndent(indent);
       }
       public bool UpdateJDFilePath(int indentId, string path)
       {
           return this.DataContext.UpdateJDFilePath(indentId, path);
       }
       public ExecuteIndentWorkFlowResult ExecuteIndentWorkflow(int indentId)
       {
           return this.DataContext.ExecuteIndentWorkflow(indentId);
       }
    }
}
