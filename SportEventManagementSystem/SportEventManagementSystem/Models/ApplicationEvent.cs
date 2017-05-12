using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SportEventManagementSystem.Models
{
    public class ApplicationEvent : IdentityUser<int>
    {
        public List<Competition> Competitions = new List<Competition>();
        public string Name;
        public string VenueName;
        public string Desciption;
        public string StreetAddress;
        public string Suburb;
        public string Postcode;
        public DateTime StartTime;
        public DateTime EndTime;
        public DateTime RegStartTime;
        public DateTime RegEndTime;
        public int EntryCapacity;
        public string OrginiserName;
        public string OrginiserClub;
    }

    public class Competition
    {
        public string Name;
        public Division Division;
        // TODO public List<Team,>Competition Participants = new List<Participants>();
        public SportType SportType;
        public string Location;
        public int TeamSizeMin;
        public int TeamSizeMax;
        public List<Restriction> Restrictions = new List<Restriction>();
        public DateTime StartTime;
        public DateTime EndTime;
        public int EntryCapacity;
    }

    public class Restriction
    {
        public Restriction restrictionType;
        public int restrictionValue;

        public enum RestrictionTypes
        {
            Age,
            Gender
        }
        //for (Restriction restriction in Restriction)
        //{
        //    if(restriction.restrictionType == RestrictionTypes.Age)
        //    {
        //        return <currentValue>.restrictionValue <= restrictionValue;
        //    } else if(restriction.restrictionType == RestrictionTypes.Gender)
        //    {
        //        return < currentValue >.restrictionValue == restrictionValue;
        //    }
        //}
    }

    public class Division
    {
        public string DivisionName;
        public string DivisionDescription;
    }

    public class Participants
    {
        //TODO
    }

    public class SportType
    {
        public string Name;
        public string Description;
    }
}