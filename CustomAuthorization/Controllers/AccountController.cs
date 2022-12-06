using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CustomAuthorization.Common.Constants;
using CustomAuthorization.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorization.Controllers
{
     public class AccountController : Controller
    {
        private readonly IAuthManager _authManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAuthManager authManager, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _authManager = authManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null && httpContext.Session.GetString(UserConfigurations.USER_NAME) is not null)
            {
                return RedirectToAction("Success");
            }
            
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
        
        public IActionResult Details()
        {
            return View();
        }
        
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null && httpContext.Session.GetString(UserConfigurations.USER_NAME) is not null)
            {
                return RedirectToAction("Success");
            }
            
            if (username is not null && password is not null)
            {
                var result = await _authManager.ValidateUser(username, password);
                if (result == true)
                {
                    var user = await _userRepository.GetByUserName(username);
                    if (user is null)
                    {
                        return NotFound("User is not found");
                    }
                    if (httpContext != null)
                    {
                        httpContext.Session.SetString(UserConfigurations.USER_NAME, username);
                        httpContext.Session.SetString(UserConfigurations.PASSWORD, password);
                        if (user.DateOfBirth != null)
                            httpContext.Session.SetString(UserConfigurations.DOB, user.DateOfBirth.Value.Date.ToString(CultureInfo.InvariantCulture));
                        httpContext.Session.SetString(UserConfigurations.ADDRESS, user.Address);
                        httpContext.Session.SetString(UserConfigurations.EMAIL, user.Email);
                        httpContext.Session.SetString(UserConfigurations.GENDER, user.Age.ToString());
                        if (user.Gender == true)
                        {
                            httpContext.Session.SetString(UserConfigurations.GENDER, UserConfigurations.GENDER_NAM);
                        }
                        else
                        {
                            httpContext.Session.SetString(UserConfigurations.GENDER, UserConfigurations.GENDER_NU);
                        }
                    }
                    
                    return View("Success");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.error = "User Name or Password can't be empty";
                return View("Index");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(UserConfigurations.USER_NAME);
            return RedirectToAction("Index");
        }
    }
}