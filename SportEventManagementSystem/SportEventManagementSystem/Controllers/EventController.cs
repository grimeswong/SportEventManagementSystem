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

        public EventController()
        {
        }

        public IActionResult Index()    // Anyone can see this event page
        {
            ViewData["Message"] = "This is event page";
            return View();
        }


        //
        // GET: /Event/CreateEvent
        [Authorize] // Only authorize user can create an event
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        ////
        //// POST: /Create/Event
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(EventViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation(1, "User logged in.");
        //            return RedirectToLocal(returnUrl);
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning(2, "User account locked out.");
        //            return View("Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return View(model);
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
