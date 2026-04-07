using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RotaCerta.Models;

namespace RotaCerta.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
}