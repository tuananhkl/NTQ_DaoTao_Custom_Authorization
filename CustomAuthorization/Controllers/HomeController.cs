using System.Diagnostics;
using CustomAuthorization.Common.Constants;
using CustomAuthorization.CustomAuthorization;
using Microsoft.AspNetCore.Mvc;
using CustomAuthorization.Models;

namespace CustomAuthorization.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    // [CustomAuthorize(CustomAuthorizationConfig.HOME_PRIVACY)]
    [CustomAuthorize("ABc.xyz")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}