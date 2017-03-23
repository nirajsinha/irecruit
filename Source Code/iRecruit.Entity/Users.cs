using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{

    public class Users
    {
        [Key]
        public string UserID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Branches { get; set; }
        public string AccessFeatures { get; set; }
        public string Photo { get; set; }
    }
}
