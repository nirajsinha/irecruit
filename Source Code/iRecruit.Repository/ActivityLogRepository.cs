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
    public class ActivityLogRepository : RepositoryBase<AppContext>, IActivityLogRepository
    {
        public ActivityLogModel GetActivityLogs(string indentNumber)
        {
            List<Tuple<string, string, string, string, DateTime>> objlist = this.DataContext.GetActivityLogs(indentNumber);
            ActivityLogModel model = new ActivityLogModel();
            DateTime? dt1 = null;

            foreach (Tuple<string, string, string, string, DateTime> obj in objlist)
            {
                if (! dt1.HasValue)
                {
                    dt1 = new DateTime(obj.Item5.Year, obj.Item5.Month, obj.Item5.Day, 0, 0, 0);
                    model.Items.Add(new Activity()
                    {
                        Label = true,
                        RecordDate = dt1.Value
                    });
                }
                DateTime dt2 = new DateTime(obj.Item5.Year, obj.Item5.Month, obj.Item5.Day, 0, 0, 0);
                if (! DateTime.Compare(dt1.Value, dt2).Equals(0))
                {
                    dt1 = new DateTime(obj.Item5.Year, obj.Item5.Month, obj.Item5.Day, 0, 0, 0);
                    model.Items.Add(new Activity()
                    {
                        Label = true,
                        RecordDate = dt1
                    });
                }
                model.Items.Add(new Activity()
                {
                    Label = false,
                    LogType = obj.Item1,
                    Header = obj.Item2,
                    Description = obj.Item3,
                    Comments = obj.Item4,
                    RecordDate = obj.Item5
                    
                });
            }

            return model;
        }
              
    }
}
