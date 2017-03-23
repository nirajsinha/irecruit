
using System.Collections.Generic;
using System.Linq;
using System;
using iRecruit.Models;
using iRecruit.Entity;

namespace iRecruit.Repository.Interfaces
{
    public interface IDashboardRepository
    {
        OpenPositionsModel OpenPositionsDetails(int companyId);
        ChartDataModel OfferJoiningRatio(int companyId);
        ChartDataModel TopOpenings(int companyId);
        ChartDataModel ResumeSources(int companyId);
    }
}
