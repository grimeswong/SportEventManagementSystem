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
        public static List<Event> GetUserEvents(ApplicationUser user)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Startup.ConnectionString);
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var Events = context.Events;
                List<Event> q = (from e in Events
                                 where e.ownerID == user.Id
                                 select e).ToList();

                return q;
            }
        }

        public static Event GetEventFromId(string id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Startup.ConnectionString);
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var Events = context.Events;
                return Events.First(o => o.id == id);
            }
        }

        public static List<Event> GetUserParticipation(ApplicationUser user)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Startup.ConnectionString);
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var Events = context.Events;
                List<Event> q = (from e in Events
                        from c in e.Competitions
                        from t in c.Teams
                        where t.ManagerID == user.Id
                        select  e ).ToList();

                
                return q;
            }
        }
    }
}
