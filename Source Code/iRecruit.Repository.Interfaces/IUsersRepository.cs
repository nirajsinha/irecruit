
using System.Collections.Generic;
using System.Linq;
using System;
using iRecruit.Models;
using iRecruit.Entity;

namespace iRecruit.Repository.Interfaces
{
    public interface IUsersRepository
    {
        bool ValidateCredentials(string id, string pwd, string domain);
        Dictionary<string, string> GetUserDetails(string userName);
        Users GetUserDetailFromDatabase(string userid);
        List<AutoCompleteResults> GetUsersFromAD(string partialSearch);
        List<AutoCompleteResults> GetUsersFromDatabase(int companyId, string partialSearch);
        bool SaveUser(Users user);
    }
}
