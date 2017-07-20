using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManagementSystem.Controllers;
using SportEventManagementSystem.Models;
//using GenFu;

namespace SportEventManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            /*** Grimes Database ***/
            /*** Create 3 Events and 3 team (one is Individual) data feeder

            /*** TeamMembers ***/
            List<TeamMember> _teamMembers = new List<TeamMember>(); //List Array
            _teamMembers.Add(new TeamMember { MemberName = "Team 1, Member1" });
            _teamMembers.Add(new TeamMember { MemberName = "Team 1, Member2" });
            _teamMembers.Add(new TeamMember { MemberName = "Team 1, Member3" });

            /*** Team or Individual Participant ***/
            List<Team> _teams = new List<Team>();
            var team = new Team
            {
                TeamName = "",
                ManagerID = "",
                TeamMembers = _teamMembers
            };
            _teams.Add(team);

   
            /*** division ***/
            var division = new Division
            {
                DivisionName = "division one",
                DivisionDescription = "division one description"
            };

            /*** sport type ***/
            var sportType = new SportType
            {
                Name = "Soccer",
                Description = "type 1 description"
            };

            /*** restriction ***/
            var _restrictions = new List<Restriction>();
            var restrictionMin18 = new Restriction
            {
                restrictionType = RestrictionTypes.MinimumAge,
                restrictionValue = 18
            };
            var restrictionMax60 = new Restriction
            {
                restrictionType = RestrictionTypes.MaximumAge,
                restrictionValue = 60
            };
            _restrictions.Add(restrictionMin18);   //add the Minimum age 18 restriction
            _restrictions.Add(restrictionMax60);   //add the Maximum age 60 restriction


            /*** Competition ***/
            var _competitions = new List<Competition>();
            var competition = new Competition
            {

                Name = "Test Comp 1",
                Division = division,
                Teams = _teams,
                SportType = sportType,
                Location = "Brisbane",
                TeamSizeMin = 1,
                TeamSizeMax = 10,
                Restrictions = _restrictions,
                StartTime = new DateTime(17, 10, 01, 0, 0, 0), // 1-Oct-17 00:00:00
                EndTime = new DateTime(17, 10, 04, 23, 59, 59), // 4-Oct-17 23:59:59
                EntryCapacity = 100
            };
            _competitions.Add(competition);   //Add the first set of competition


            /*** Event ***/
            var _event = new Event
            {
                Competitions = _competitions,
                Name = "Test Event 1",
                VenueName = "Sunrise Stadium",
                Description = "Description event 1",
                StreetAddress = "188 GoodView St",
                Suburb = "Good View",
                PostCode = "4000",
                StartTime = new DateTime(17, 10, 02, 15, 0, 0), // 1-Oct-17 15:00:00
                EndTime = new DateTime(17, 10, 02, 18, 0, 0), // 1-Oct-17 18:00:00
                RegStartTime = new DateTime(17, 08, 01, 0, 0, 0), // 1-Aug-17 00:00:00
                RegEndTime = new DateTime(17, 08, 10, 23, 59, 59), // 1-Aug-17 23:59:59
                EntryCapacity = 100,
                OrganiserName = "Alan Smith",
                OrganiserClub = "Aspley Rugby Club"
            };

            //context.Events.Add(_event);

            // Looking into using GenFu to generate Data
            // var evnets GenFu.GenFu.ListOf<Event>(200);

            //context.Users.Add(new Models.ApplicationUser);

            context.SaveChanges();
        }
    }
}
