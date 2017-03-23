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
using System.Configuration;


namespace iRecruit.Repository 
{
   public class UsersRepository : RepositoryBase<AppContext>, IUsersRepository
   {
       public bool ValidateCredentials(string userName, string password, string domain)
       {
           bool authentic = false;
           try
           {
               using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, userName, password))
               {
                   object nativeObject = entry.NativeObject;
                   authentic = true;
               }
           }
           catch (DirectoryServicesCOMException) { throw; }
           return authentic;
       }
       
       private List<string> GetDepartmentsFromAD(string userName, string password, string domain)
       {
           try
           {
               List<string> depts = new List<string>();
               DirectoryEntry connection = new DirectoryEntry("LDAP://" + domain, userName, password);
               DirectorySearcher ds = new DirectorySearcher(connection);
               ds.Filter = "(objectClass=user)";
               ds.PropertiesToLoad.Add("department");
               ds.PageSize = 1;  // put any non zero value, else it will default to 1000
               SearchResultCollection results = ds.FindAll();
               foreach (SearchResult result in results)
               {
                   string dept = String.Empty;
                   DirectoryEntry de = result.GetDirectoryEntry();
                   if (de.Properties.Contains("department"))
                   {
                       dept = de.Properties["department"][0].ToString();
                       if (!depts.Contains(dept) && !string.IsNullOrWhiteSpace(dept))
                       {
                           depts.Add(result.Properties["department"][0].ToString());
                       }
                   }
               }
               return depts;
           }
           catch (DirectoryServicesCOMException) { throw; }

       }
       public List<AutoCompleteResults> GetUsersFromAD(string partialSearch)
       {
           try
           {

               List<AutoCompleteResults> dic = new List<AutoCompleteResults>();
               string Domain = ConfigurationManager.AppSettings["Domain"];
               string DomainUser = ConfigurationManager.AppSettings["DomainUser"];
               string DomainPassword = ConfigurationManager.AppSettings["DomainPassword"];

               using (DirectoryEntry ent = new DirectoryEntry("LDAP://" + Domain, DomainUser, DomainPassword))
               {
                   if (ent != null)
                   {
                       DirectorySearcher search = new DirectorySearcher(ent);
                       search.Filter = "(&(samAccountType=805306368)(SAMAccountName=*" + partialSearch + "*))";
                       foreach (SearchResult result in search.FindAll())
                       {
                           dic.Add(new AutoCompleteResults() { value = GetProperty(result, "samaccountname"), label = GetProperty(result, "displayname") });
                       }
                   }
               }
               return dic;
           }
           catch (DirectoryServicesCOMException) { throw; }

       }
       public List<AutoCompleteResults> GetUsersFromDatabase(int companyid, string partialSearch)
       {
           try
           {

               List<AutoCompleteResults> dic = new List<AutoCompleteResults>();
               List<Users> users = this.DataContext.GetUsers(companyid, partialSearch);
               foreach (Users result in users)
                {
                    dic.Add(new AutoCompleteResults() { value = result.UserID, label = result.Name });
                }
               
               return dic;
           }
           catch (DirectoryServicesCOMException) { throw; }
       }
       public Dictionary<string, string> GetUserDetails(string userName)
       {
           string Domain = ConfigurationManager.AppSettings["Domain"];
           string DomainUser = ConfigurationManager.AppSettings["DomainUser"];
           string DomainPassword = ConfigurationManager.AppSettings["DomainPassword"];

           Dictionary<string, string> dic = new Dictionary<string, string>();
           using (DirectoryEntry ent = new DirectoryEntry("LDAP://" + Domain, DomainUser, DomainPassword))
           {
               if (ent != null)
               {
                   DirectorySearcher search = new DirectorySearcher(ent);
                   //search.Filter = "(&(objectClass=user)(objectCategory=person)(SAMAccountName=" + userName + "))";
                   //search.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
                   search.Filter = "(&(samAccountType=805306368)(SAMAccountName=" + userName + "))";
                   
                    foreach (SearchResult result in search.FindAll()) 
                    {
                        dic.Add("mail", GetProperty(result,"mail"));
                        dic.Add("samaccountname", GetProperty(result, "samaccountname"));
                        //dic.Add("displayname", GetProperty(result, "displayname"));
                        dic.Add("cn", GetProperty(result, "cn"));
                        dic.Add("title", GetProperty(result, "title"));
                        string doj = GetProperty(result, "extensionAttribute3"); // date of joining
                        dic.Add("sn", GetProperty(result, "sn")); // last name
                        dic.Add("givenName", GetProperty(result, "givenName")); // first name
                        dic.Add("thumbnailPhoto", GetProperty(result, "thumbnailPhoto")); // thumbnail
                        if(!string.IsNullOrWhiteSpace(doj))
                        {
                            dic.Add("doj", string.Format("{0:MMM, yyyy}", Convert.ToDateTime(doj)));
                        }
                    }
               }
           }
           return dic;
       }
       public Dictionary<string, string> GetIndenterAndApprovers(string userName, string password, string domain)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           using (DirectoryEntry ent = new DirectoryEntry("LDAP://" + domain, userName, password))
           {
               if (ent != null)
               {
                   DirectorySearcher search = new DirectorySearcher(ent);
                   //search.Filter = "(&(objectCategory=person)(objectClass=contact)(|(title=Senior Project Manager)(title=Delivery Manager)(title=Vice President)(title=Senior Vice President)))";
                   search.Filter = "(&(objectCategory=person)(objectClass=user)(|(title=Senior Project Manager)(title=Delivery Manager)(title=Senior Delivery Manager)(title=Vice President)(title=Senior Vice President)))";
                   //search.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
                    foreach (SearchResult result in search.FindAll()) 
                    {
                        dic.Add(GetProperty(result, "samaccountname"), GetProperty(result, "cn"));
                    }
               }
           }
           return dic;
       }
       private string GetProperty(SearchResult searchResult, string propertyName)
      {
          if (searchResult.Properties.Contains(propertyName))
            {
                if ("thumbnailPhoto".Equals(propertyName))
                {
                    byte[] bb = (byte[])searchResult.Properties[propertyName][0];
                    return Convert.ToBase64String(bb);
                }
                return searchResult.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
      }
       public Users GetUserDetailFromDatabase(string userid)
       {
          return this.DataContext.GetUserDetails(userid);
       }
       public bool SaveUser(Users user)
       {
          return this.DataContext.SaveUser(user);
       }
   }
}
