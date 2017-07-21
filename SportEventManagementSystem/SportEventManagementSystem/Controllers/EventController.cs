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
using SportEventManagementSystem.Models.EventViewModels;
using SportEventManagementSystem.Services;

namespace SportEventManagementSystem.Controllers
{
    //[Authorize]
    public class EventController : Controller

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;
        private readonly Data.ApplicationDbContext _context;

        public EventController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory, Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _logger = loggerFactory.CreateLogger<EventController>();
            _context = context;
        }
       
        public IActionResult Index()    // Anyone can see this event page
        {
            ViewData["Message"] = "This is event page";
            return View();
        }

        //
        // GET: /Event/Create
        [Authorize]
        public IActionResult CreateEvent()
        {
            ViewData["Message"] = "Create event page";
            return View();
        }

        //
        // POST: /Event/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var e = new Event
                {
                    Name = model.Name,
                    VenueName = model.VenueName,
                    Description = model.Description,
                    StreetAddress = model.StreetAddress,
                    Suburb = model.Suburb,
                    PostCode = model.PostCode,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    RegStartTime = model.RegStartTime,
                    RegEndTime = model.RegEndTime,
                    EntryCapacity = model.EntryCapacity,
                    OrganiserName = model.OrganiserName,
                    OrganiserClub = model.OrganiserClub,
                    ownerID = QueryController.GetCurrentUserAsync(_userManager, User).Id
                };

               _context.Events.Add(e);
                await _context.SaveChangesAsync();
            } else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }

            return RedirectToLocal(returnUrl);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
