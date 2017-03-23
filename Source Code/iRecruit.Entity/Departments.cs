using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class Departments
    {
        [Key]
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
