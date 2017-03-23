
using System.Collections.Generic;
using System.Linq;
using System;
using iRecruit.Models;
using iRecruit.Entity;

namespace iRecruit.Repository.Interfaces
{
    public interface IActivityLogRepository
    {
        ActivityLogModel GetActivityLogs(string indentNumber);
    }
}
