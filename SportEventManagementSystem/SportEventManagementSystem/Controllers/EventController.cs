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

namespace SportEventManagementSystem.Controllers
{
    //[Authorize]
    public class EventController : Controller

    {
        private readonly Data.ApplicationDbContext _context;

        public EventController(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()    // Anyone can see this event page
        {
            ViewData["Message"] = "This is event page";
            return View();
        }

        [Authorize]
        public IActionResult CreateEvent()
        {
            ViewData["Message"] = "This is create event page";
            return View();
        }



        //GET: /Event/CreateEvent
        //[Authorize] // Only authorize user can create an event
        //[HttpGet]
        //[AllowAnonymous]
        // public async Task<IActionResult> CreateEvent(string returnUrl = null)
        // {
        //     ViewData["ReturnUrl"] = returnUrl;
        //     return View();
        // }

        ////
        //// post: /create/event
        //[httppost]
        //[allowanonymous]
        //[validateantiforgerytoken]
        //public async task<iactionresult> login(eventviewmodel model, string returnurl = null)
        //{
        //    viewdata["returnurl"] = returnurl;
        //    if (modelstate.isvalid)
        //    {
        //        // this doesn't count login failures towards account lockout
        //        // to enable password failures to trigger account lockout, set lockoutonfailure: true
        //        var result = await _signinmanager.passwordsigninasync(model.email, model.password, model.rememberme, lockoutonfailure: false);
        //        if (result.succeeded)
        //        {
        //            _logger.loginformation(1, "user logged in.");
        //            return redirecttolocal(returnurl);
        //        }
        //        if (result.islockedout)
        //        {
        //            _logger.logwarning(2, "user account locked out.");
        //            return view("lockout");
        //        }
        //        else
        //        {
        //            modelstate.addmodelerror(string.empty, "invalid login attempt.");
        //            return view(model);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

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
