using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            //* refrences the columns ViewBag from the ColumnChoices row from the List Controller
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        
        [HttpGet]
        public IActionResult Results(string searchType, string searchTerm)
        { 
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            // use FindByColumnAndValue to complete your searches
            // search the dictionary for the searchType (column) return all the values associated with this 
            // search the dictionary for the searchTerm (value) return all the values matching this term
            // jobs comes back from the FBC&V send results to the search index  - viewbag.jobs

            if (searchType.Equals("all"))
            {
                ViewBag.jobs = JobData.FindByValue(searchTerm);
            }
            else
            {
                ViewBag.jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
            }

            return View("Index");
        }
    }
}
