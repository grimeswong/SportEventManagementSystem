using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportEventManagementSystem.Models;
using SportEventManagementSystem.Models.AccountViewModels;
using SportEventManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using SportEventManagementSystem.Data;
using Microsoft.Extensions.Configuration;

namespace SportEventManagementSystem.Controllers
{
    public static class QueryController
    {
        //Static function to get current user from controllers
        public static ApplicationUser GetCurrentUserAsync(UserManager<ApplicationUser> _userManager, ClaimsPrincipal User)
        {
            return _userManager.Users.Include(x => x.details).FirstOrDefault(x => x.Id == _userManager.GetUserId(User));
        }

        public static ApplicationUser GetUserFromUserID(UserManager<ApplicationUser> _userManager, string userID)
        {
            return _userManager.Users.Include(x => x.details).FirstOrDefault(x => x.Id == userID);
        }

        //Static function to get current users events that were created by User
        public static List<Event> GetUserEvents(ApplicationDbContext context, ApplicationUser user)
        {
            var Events = GetFullEventsInfo(context);
            List<Event> q = (from e in Events
                             where e.ownerID == user.Id
                             select e).ToList();

            return q;
        }

        //Function to eager load all event data
        private static List<Event> GetFullEventsInfo(ApplicationDbContext context)
        {
            var events = context.Events
                .Include(e => e.Competitions)
                    .ThenInclude(c => c.DivisionType)
                .Include(e => e.Competitions)
                    .ThenInclude(c => c.Teams)
                .Include(e => e.Competitions)
                    .ThenInclude(c => c.SportType);

            return events.ToList();
        }

        public static Event GetEventFromId(ApplicationDbContext context, string id)
        {
            try
            {
                var evnt = GetFullEventsInfo(context).First(o => o.id == id);
                return evnt;
            } catch(InvalidOperationException)
            {
                return null;
            }            
        }

        public static Competition GetCompetitionFromId(Event evnt, string id)
        {
            try
            {
                return evnt.Competitions.First(c => c.id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Really generic searching algorithm, should be remade to search specific subsets of parameters to enhance user usability
        public static List<Event> ReturnMatchingEvents(ApplicationDbContext context, string param)
        {
            var events = GetFullEventsInfo(context);

            List<Event> q = (from e in events
                             from c in e.Competitions
                             where e.Description.Contains(param) ||
                             e.Name.Contains(param) ||
                             e.OrganiserClub.Contains(param) ||
                             e.OrganiserName.Contains(param) ||
                             e.PostCode.Contains(param) ||
                             e.Suburb.Contains(param) ||
                             e.VenueName.Contains(param) ||
                             c.SportType.Description.Contains(param) ||
                             c.SportType.Name.Contains(param)
                             select e).ToList();

            return events;

        }

        //Searches for competitions that a user has registered a team in and returns the relevant events
        public static List<Event> GetUserParticipation(ApplicationDbContext context,ApplicationUser user)
        {
                var events = GetFullEventsInfo(context);
                List<Event> q = (from e in events
                        from c in e.Competitions
                        from t in c.Teams
                        where t.ManagerID == user.Id
                        select  e).ToList();

                return q;
        }
    }
}
