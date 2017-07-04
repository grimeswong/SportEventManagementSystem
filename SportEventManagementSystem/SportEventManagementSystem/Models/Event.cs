﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManagementSystem.Models
{
    public class Event
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ID;

        public ICollection<Competition> Competitions { get; set; }
        public string Name { get; set; }
        public string VenueName { get; set; }
        public string Desciption { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RegStartTime { get; set; }
        public DateTime RegEndTime { get; set; }
        public int EntryCapacity { get; set; }
        public string OrginiserName { get; set; }
        public string OrginiserClub { get; set; }
    }

    public class Competition
    {
        public string Name { get; set; }
        public Division Division { get; set; }
        // TODO public List<Team,>Competition Participants = new List<Participants>();
        public SportType SportType { get; set; }
        public string Location { get; set; }
        public int TeamSizeMin { get; set; }
        public int TeamSizeMax { get; set; }
        public ICollection<Restriction> Restrictions { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EntryCapacity { get; set; }
    }

    public enum RestrictionTypes
    {
        Age,
        Gender
    }

    public class Restriction
    {
        public RestrictionTypes restrictionType { get; set; }
        public int restrictionValue { get; set; }


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
        public string DivisionName { get; set; }
        public string DivisionDescription { get; set; }
    }

    public class Participants
    {
        //TODO
    }

    public class SportType
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}