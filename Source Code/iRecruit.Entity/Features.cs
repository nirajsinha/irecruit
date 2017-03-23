using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRecruit.Entity
{
    public class Features
    {
        [Key]
        public int FeatureID { get; set; }
        public string Code { get; set; }
        public int CompanyID { get; set; }
        public string Description { get; set; }
    }

    
    

    

    
    








    
}
