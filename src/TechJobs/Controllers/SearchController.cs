using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            //* refrences(?) the columns ViewBag from the ColumnChoices row from the List Controller
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        [HttpPost]
        
        public IActionResult Results(string searchType, string searchTerm)
        {

            // search the dictionary for the searchType (key) return all the values associated with this 
            // search the dictionary for the searchTerm (value) return all the values matching this term

            // send results to the search index  - viewbag.jobs
            ViewBag.jobs = jobs;
            return View("/Search/Results?" + );
        }
    }
}
