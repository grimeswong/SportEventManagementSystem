using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportEventManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserDetails details { get; set; }
    }

    public class UserDetails
    {
        [Key]
        public string id { get; set; }
        public string FirstName { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContact { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public int Gender { get; set; }
        public string PostCode { get; set; }
    }

}

