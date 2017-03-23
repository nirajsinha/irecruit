using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{

    public class DepartmentRoles
    {
        [Key]
        public int DepartmentRoleID { get; set; }
        public int DepartmentID { get; set; }
        public int BranchID { get; set; }
        public string FunctionHead { get; set; }
        public string SVP { get; set; }
        public bool Active { get; set; }
    }
}
