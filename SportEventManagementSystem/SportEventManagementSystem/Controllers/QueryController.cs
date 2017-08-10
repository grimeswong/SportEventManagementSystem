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
                var Events = GetFullEventsInfo(context);
                return Events.First(o => o.id == id);
        }

        public static List<Event> GetUserParticipation(ApplicationDbContext context,ApplicationUser user)
        {
                var Events = GetFullEventsInfo(context);
                List<Event> q = (from e in Events
                        from c in e.Competitions
                        from t in c.Teams
                        where t.ManagerID == user.Id
                        select  e).ToList();

                
                return q;
        }
    }
}
