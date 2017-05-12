using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SportEventManagementSystem.Models
{
    public class ApplicationEvent : IdentityUser<int>
    {
        //INSERT STUFF HERE
        public List<Competition> Competitions = new List<Competition>();
        
    }

    public class Competition
    {
        public Division Division;
       // public List<Team,>Competition Participants = new List<Participants>();
    }

    public class Division
    {
        public string DivisionName;
        public string DivisionDescription;
    }

    public class Participants
    {
        //public
    }

    public class SportType
    {

    }
}
