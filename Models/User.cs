using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Models
{
    public class User
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Password { get; set; }

      
    }
}
