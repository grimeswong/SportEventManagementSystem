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
using SportEventManagementSystem.Data;

namespace SportEventManagementSystem.Controllers
{
    //[Authorize]
    public class EventController : Controller
    {
        #region Declaration
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;
        private readonly ApplicationDbContext _context;

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
        #endregion

        //
        // GET: /Event/
        [Authorize]
        public IActionResult Index()
        {
            ApplicationUser user = QueryController.GetCurrentUserAsync(_userManager, User);
            EventIndexViewModel indexModel = new EventIndexViewModel
            {
                CreatedEvents = QueryController.GetUserEvents(_context, user),
                ParticipatingEvents = QueryController.GetUserParticipation(_context, user)
            };
            ViewData["Message"] = "Event Dashboard";
            return View(indexModel);
        }

        //
        // GET: /Event/View
        [Authorize]
        public IActionResult ViewEvent(string eventID)
        {
            var model = QueryController.GetEventFromId(_context, eventID);

            if(model != null)
            {
                return View(model);
            } else
            {
                ViewData["error"] = "Invalid event.";
                return View("Error");
            }
            
        }

        //
        // POST: /Event/NewCompetition
        [Authorize]
        [HttpPost]
        public IActionResult NewCompetition(CreateEventViewModel model)
        {
            ViewData["compCount"] = model.CompetitionCount;
            return PartialView("CompetitionForm");
        }

        #region CreateEvent
        //
        // GET: /Event/CreateEvent
        [Authorize]
        public IActionResult CreateEvent()
        {
            ViewData["Message"] = "Create event page";
            ViewData["ReturnUrl"] = "/Event/";
            return View("CreateEvent");
        }

        //
        // POST: /Event/CreateEvent
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                List<Competition> competitions = new List<Competition>();
                if (model.Competitions != null)
                {
                    foreach (CompetitionValidationModel c in model.Competitions)
                    {
                        Sport sportType = new Sport
                        {
                            Name = c.SportName,
                            Description = c.SportDescription
                        };

                        Division div = new Division
                        {
                            DivisionName = c.DivisionName,
                            DivisionDescription = c.DivisionDescription
                        };

                        Competition comp = new Competition
                        {
                            Name = c.CompName,
                            DivisionType = div,
                            SportType = sportType,
                            Location = c.Location,
                            StartTime = c.CompStartTime,
                            EndTime = c.CompEndTime,
                            EntryCapacity = c.EntryCapacity,
                            TeamSizeMin = c.TeamSizeMin,
                            TeamSizeMax = c.TeamSizeMax,
                            MinimumAge = c.MinimumAge,
                            MaximumAge = c.MaximumAge,
                            GenderRestriction = c.GenderRestriction
                        };
                        competitions.Add(comp);
                    }
                }

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
                    OrganiserName = model.OrganiserName,
                    OrganiserClub = model.OrganiserClub,
                    IsPrivate = model.IsPrivate,
                    ownerID = QueryController.GetCurrentUserAsync(_userManager, User).Id,
                    Competitions = competitions
                };

                _context.Events.Add(e);
                await _context.SaveChangesAsync();
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }
        #endregion

        #region EditEvent
        //
        // GET: /Event/EditEvent?eventID=
        [Authorize]
        public IActionResult EditEvent(string eventID)
        {
            ViewData["Message"] = "Edit event page";
            ViewData["ReturnUrl"] = "/Event/";
            var evnt = QueryController.GetEventFromId(_context, eventID);
            if(evnt != null)
            {
                if(evnt.ownerID ==  _userManager.GetUserId(User))
                {
                    List<CompetitionValidationModel> competitions = new List<CompetitionValidationModel>();
                    foreach (Competition c in evnt.Competitions)
                    {
                        competitions.Add(new CompetitionValidationModel
                        {
                            CompName = c.Name,
                            DivisionName = c.DivisionType.DivisionName,
                            DivisionDescription = c.DivisionType.DivisionDescription,
                            SportName = c.SportType.Name,
                            SportDescription = c.SportType.Description,
                            Location = c.Location,
                            CompStartTime = c.StartTime,
                            CompEndTime = c.EndTime,
                            EntryCapacity = c.EntryCapacity,
                            TeamSizeMin = c.TeamSizeMin,
                            TeamSizeMax = c.TeamSizeMax,
                            MinimumAge = c.MinimumAge,
                            MaximumAge = c.MaximumAge,
                            GenderRestriction = c.GenderRestriction
                        });
                    }

                    var model = new CreateEventViewModel
                    {
                        Name = evnt.Name,
                        VenueName = evnt.VenueName,
                        Description = evnt.Description,
                        StreetAddress = evnt.StreetAddress,
                        Suburb = evnt.Suburb,
                        PostCode = evnt.PostCode,
                        StartTime = evnt.StartTime,
                        EndTime = evnt.EndTime,
                        RegStartTime = evnt.RegStartTime,
                        RegEndTime = evnt.RegEndTime,
                        OrganiserName = evnt.OrganiserName,
                        OrganiserClub = evnt.OrganiserClub,
                        IsPrivate = evnt.IsPrivate,
                        Competitions = competitions
                    };
                    ViewData["eventID"] = eventID;
                    return View("EditEvent", model);
                } else
                { //User trying to modify an event they didn't create
                    ViewData["error"] = "You do not have permissions to edit this event.";
                    return View("Error");
                }
              
            }
            else
            { //If event is null
                ViewData["error"] = "Not a valid event.";
                return View("Error");
            }
            
        }

        //
        // POST: /Event/EditEvent?eventID=
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(CreateEventViewModel model,string eventID,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                List<Competition> competitions = new List<Competition>();
                if (model.Competitions != null)
                {
                    foreach (CompetitionValidationModel c in model.Competitions)
                    {
                        Sport sportType = new Sport
                        {
                            Name = c.SportName,
                            Description = c.SportDescription
                        };

                        Division div = new Division
                        {
                            DivisionName = c.DivisionName,
                            DivisionDescription = c.DivisionDescription
                        };

                        Competition comp = new Competition
                        {
                            Name = c.CompName,
                            DivisionType = div,
                            SportType = sportType,
                            Location = c.Location,
                            StartTime = c.CompStartTime,
                            EndTime = c.CompEndTime,
                            EntryCapacity = c.EntryCapacity,
                            TeamSizeMin = c.TeamSizeMin,
                            TeamSizeMax = c.TeamSizeMax,
                            MinimumAge = c.MinimumAge,
                            MaximumAge = c.MaximumAge,
                            GenderRestriction = c.GenderRestriction
                        };
                        competitions.Add(comp);
                    }
                }
                
                var e = QueryController.GetEventFromId(_context,eventID);
                e.Name = model.Name;
                e.VenueName = model.VenueName;
                e.Description = model.Description;
                e.StreetAddress = model.StreetAddress;
                e.Suburb = model.Suburb;
                e.PostCode = model.PostCode;
                e.StartTime = model.StartTime;
                e.EndTime = model.EndTime;
                e.RegStartTime = model.RegStartTime;
                e.RegEndTime = model.RegEndTime;
                e.OrganiserName = model.OrganiserName;
                e.OrganiserClub = model.OrganiserClub;
                e.IsPrivate = model.IsPrivate;
                e.ownerID = QueryController.GetCurrentUserAsync(_userManager, User).Id;
                e.Competitions = competitions;

                _context.Events.Update(e);
                await _context.SaveChangesAsync();

                //Loop over each competition, notify the participating users that the events changed and remove them from the event.
                foreach(Competition c in e.Competitions)
                {
                    if(c.Teams != null)
                    {
                        foreach (Team t in c.Teams)
                        {
                            ApplicationUser manager = QueryController.GetUserFromUserID(_userManager, t.ManagerID);
                            await _emailSender.SendEmailAsync(manager.Email, "Event details changed notice!", "Dear, " + manager.details.FirstName + " " + manager.details.LastName +
                                                        ", \n this email is to notify you that the event you are participating in has had a change of details and thus you have been removed from the competition you registered in." +
                                                        " Please rejoin if you wish by visiting this link: http://localhost:49494/Event/JoinCompetition?eventID=" + e.id + "&competitionID=" + c.id);
                        }
                    }
                   
                }
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }

        #endregion Event

        //
        // GET: /Event/JoinCompetition?eventID=&competitionID=
        [Authorize]
        public IActionResult JoinCompetition(string eventID, string competitionID)
        {
            ViewData["ReturnUrl"] = "/Event/";
            var competition = QueryController.GetCompetitionFromId(QueryController.GetEventFromId(_context, eventID), competitionID);

            if(competition != null)
            {
                JoinCompetitionViewModel model = new JoinCompetitionViewModel
                {
                    Competition = competition
                };
                return View(model);
            }
            ViewData["error"] = "Invalid event or competition.";
            return View("Error");
        }

        //
        // POST: /Event/JoinCompetition?eventID=&competitionID=
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinCompetition(string eventId, string competitionId, JoinCompetitionViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                List<TeamMember> members = new List<TeamMember>();
                foreach (TeamMemberValidationModel m in model.members)
                {
                    members.Add(new TeamMember { MemberName = m.MemberName });
                }
                ApplicationUser user = QueryController.GetCurrentUserAsync(_userManager, User);
                Team team = new Team
                {
                    ManagerID = user.Id,
                    TeamName = model.TeamName,
                    TeamMembers = members
                };
                Event evnt = QueryController.GetEventFromId(_context, eventId);
                Competition comp = evnt.Competitions.Where(o => o.id == competitionId).First();

                comp.Teams.Add(team);
                await _context.SaveChangesAsync();
            }
            else
            {
                return View(model);
            }
            return RedirectToLocal(returnUrl);
        }

        //
        // POST: /Event/Leave
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(string eventId, string competitionId, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            try
            {
                Event evnt = QueryController.GetEventFromId(_context, eventId);

                Competition comp = evnt.Competitions.Where(o => o.id == competitionId).First();
                Team team = comp.Teams.First(o => o.ManagerID == QueryController.GetCurrentUserAsync(_userManager, User).Id);
                comp.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Invalid" + e.Message);
            }

            return RedirectToLocal(returnUrl);
        }

        //
        // Get: /Event/Search?param=
        [Authorize]
        public IActionResult Search(string param = null)
        {
            ViewData["param"] = param;
            ViewData["Message"] = "Search for events.";

            //If user entered param in url fetch results and display
            if (param != null)
            {
                SearchViewModel model = new SearchViewModel
                {
                    results = QueryController.ReturnMatchingEvents(_context, param)
                };
            return View("Search",model);
            }
            //Else no param just display form
            return View("Search");
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
