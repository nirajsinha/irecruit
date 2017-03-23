
using System.Collections.Generic;
using System.Linq;
using System;
using iRecruit.Models;
using iRecruit.Entity;

namespace iRecruit.Repository.Interfaces
{
    public interface IIndentRepository
    {
        Indent GetIndent(string indentNumber);
        List<Indent> GetIndents(int companyId);
        List<IndentTrackerData> GetIndentTrackerData(int companyId, int page = 0, int pageSize = 0);
        int SaveIndent(Indent indent);
        bool UpdateJDFilePath(int indentId, string path);
        ExecuteIndentWorkFlowResult ExecuteIndentWorkflow(int indentId);
        IndentTrackerInfoModel GetIndentTrackerInfoModel(string indentNumber);
        
    }
}
