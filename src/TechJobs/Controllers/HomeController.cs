using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TechJobs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //* create dictionary called actionChoices with "Search" and "List" as its options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");
            
            //* add actionChoices dictionary to the actions ViewBag
            ViewBag.actions = actionChoices;

            return View();
        }
    }
}
