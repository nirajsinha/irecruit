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
    public class DashboardRepository : RepositoryBase<AppContext>, IDashboardRepository
    {
       public OpenPositionsModel OpenPositionsDetails(int companyId)
       {
           dynamic obj = this.DataContext.GetOpenPositionsDetails(companyId);
           return new OpenPositionsModel()
           {
               OpenPositions = obj.OpenPositions,
               OffersMade = obj.OffersMade,
               OnBoard = obj.OnBoard
           };
       }
       public ChartDataModel OfferJoiningRatio(int companyId)
       {
           List<Tuple<string, int, int>> objlist= this.DataContext.GetOfferJoiningRatio(companyId);
           ChartDataModel model = new ChartDataModel();
           model.Series = new List<string>() { "Offers", "Joining" };
           foreach (Tuple<string, int, int> obj in objlist)
           {
               model.Items.Add(new ChartData()
               {
                   x = obj.Item1,
                   y = new List<int>(){ obj.Item2, obj.Item3},
                   tooltip="Offers : "+ obj.Item2 + "<br />" + "Joining : "+ obj.Item3
               });
           }
           return model;
       }
       public ChartDataModel TopOpenings(int companyId)
       {
           List<Tuple<string, int>> objlist = this.DataContext.GetTopOpenings(companyId);
           ChartDataModel model = new ChartDataModel();
           foreach (Tuple<string, int> obj in objlist)
           {
               model.Items.Add(new ChartData()
               {
                   x = obj.Item1,
                   y = new List<int>() { obj.Item2},
                   tooltip=obj.Item1 + ": " + obj.Item2
               });
               model.Series.Add(obj.Item1);
           }

           return model;
       }
       public ChartDataModel ResumeSources(int companyId)
       {
           List<Tuple<string, int>> objlist = this.DataContext.GetResumeSources(companyId);
           ChartDataModel model = new ChartDataModel();
           foreach (Tuple<string, int> obj in objlist)
           {
               model.Items.Add(new ChartData()
               {
                   x = obj.Item1,
                   y = new List<int>() { obj.Item2 },
                   tooltip = obj.Item1 + ": " + obj.Item2
               });
               model.Series.Add(obj.Item1);
           }

           return model;
       }
    }
}
