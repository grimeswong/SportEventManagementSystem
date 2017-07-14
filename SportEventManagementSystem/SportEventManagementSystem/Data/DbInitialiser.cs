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

            /*Example to create an event (before comp re-added)
            List<TeamMember> _teamMembers = new List<TeamMember>();

            _teamMembers.Add(new TeamMember { MemberName = "Member1" });
            _teamMembers.Add(new TeamMember { MemberName = "Member2" });
            _teamMembers.Add(new TeamMember { MemberName = "Member3" });

            List<Team> _teams = new List<Team>();
            var team = new Team
            {
                TeamName = "Team1",
                ManagerID = "423ffb94-50f8-4f65-b306-e5c1faa997af",
                TeamMembers = _teamMembers,
            };
            _teams.Add(team);

            var _participants = new Participants
            {
                Teams = _teams
            };
            var _event = new Event
            {
                id = "tempEvent1",
                Name = "Test Event 1",
                Description = "Description event 1",
                Participants = _participants
            };

            context.Events.Add(_event); */

            //Looking into using GenFu to generate data
            //var events = GenFu.GenFu.ListOf<Event>(200);

            //context.Users.Add(new Models.ApplicationUser )
            context.SaveChanges();
        }
    }
}
