using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRecruit.Data.Contexts;
using System.Reflection;

namespace iRecruit.Repository
{
    public class RepositoryBase<TContext> : IDisposable where TContext : DbContext, new()
    {
        private AppContext _dataContext;
        public virtual AppContext DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new AppContext();
                    _dataContext.Configuration.LazyLoadingEnabled = true;
                }
                return _dataContext;
            }
        }
        public static void CopyPropertiesFrom (object to, object from, string[] excludedProperties)
        {
            Type targetType = to.GetType();
            Type sourceType = from.GetType();

            PropertyInfo[] sourceProps = sourceType.GetProperties();
            foreach (var propInfo in sourceProps)
            {
                //filter the properties
                if (excludedProperties != null
                  && excludedProperties.Contains(propInfo.Name))
                    continue;

                //Get the matching property from the target
                PropertyInfo toProp =
                  (targetType == sourceType) ? propInfo : targetType.GetProperty(propInfo.Name);

                //If it exists and it's writeable
                if (toProp != null && toProp.CanWrite)
                {
                    //Copy the value from the source to the target
                    Object value = propInfo.GetValue(from, null);
                    toProp.SetValue(to, value, null);
                }
            }
        }
        public void Dispose()         
        {
            if (_dataContext == null) return; 
            _dataContext.Dispose();    
            _dataContext = null;  
        }
    }
}
