using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SportEventManagementSystem.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; internal set; }
        public string EmergencyContactNo { get; internal set; }
        public string EmergencyContact { get; internal set; }
        public string MobilePhone { get; internal set; }
        public string HomePhone { get; internal set; }
        public string MiddleName { get; internal set; }
        public string LastName { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string Street { get; internal set; }
        public string Suburb { get; internal set; }
        public int Gender { get; internal set; }
        public string PostCode { get; internal set; }
    }
}
