﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportEventManagementSystem.Models
{
    //Data model used to store events
    public class Event
    {
        [Key]
        public string id { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }
        public List<Competition> Competitions { get; set; }
        public string Name { get; set; }
        public string VenueName { get; set; }
        public string Description { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RegStartTime { get; set; }
        public DateTime RegEndTime { get; set; }
        public string OrganiserName { get; set; }
        public string OrganiserClub { get; set; }
        public string ownerID { get; set; }
    }

    public class Competition
    {
        [Key]
        public string id { get; set; }

        public string Name { get; set; }
        public Division DivisionType { get; set; }
        public List<Team> Teams { get; set; }
        public Sport SportType { get; set; }
        public string Location { get; set; }
        public int TeamSizeMin { get; set; }
        public int TeamSizeMax { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public int GenderRestriction { get; set; }
        //public List<Restriction> Restrictions { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EntryCapacity { get; set; }

        public bool userParticipating(string userID)
        {
            if (this.Teams != null)
            {
                foreach (Team t in this.Teams)
                {
                    if(t.ManagerID == userID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int getParticipants()
        {
            int r = 0;
            if(this.Teams != null)
            {
                foreach (Team t in this.Teams)
                {
                    if (t.ManagerParticipation == true)
                    {
                        r++;
                    }
                    if (t.TeamMembers != null)
                    {
                        r += t.TeamMembers.Count;
                    }
                }
            }
            return r;
        }
    }

    public class Division
    {
        [Key]
        public string id { get; set; }

        public string DivisionName { get; set; }
        public string DivisionDescription { get; set; }
    }

    public class Team
    {
        [Key]
        public string id { get; set; }

        public string TeamName { get; set; }
        public string ManagerID { get; set; }
        public bool ManagerParticipation { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        public bool IsIndividual()
        {
            return this.TeamMembers.Count > 0;
        }
    }

    public class TeamMember
    {
        [Key]
        public string id { get; set; }

        public string MemberName { get; set; }
    }

    public class Sport
    {
        [Key]
        public string id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    //Unused code for having an extensible list of restrictions - non hard coded.. May be used in the future
    //public enum RestrictionTypes
    //{
    //    MinimumAge,
    //    MaximumAge,
    //    Gender
    //}

    //public class Restriction
    //{
    //    [Key]
    //    public string id { get; set; }

    //    public RestrictionTypes restrictionType { get; set; }
    //    public int restrictionValue { get; set; }
    //}
}