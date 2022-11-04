using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurfsUp_API.Models;
using SurfsUp_API.Areas.Identity.Data;

namespace SurfsUp.Controllers
{
    public class SurfboardsController : Controller
    {
        private readonly UserManager<SurfsUpUser> userManager;
        private readonly string mainUrl = Environment.GetEnvironmentVariable("SURFSUP_API_URL");

        public SurfboardsController(UserManager<SurfsUpUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: Surfboards
        [HttpGet, ActionName("Index")]
        public async Task<ActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber,string? error)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "Length_desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "Width_desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "Thickness_desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "Volume_desc" : "Volume";

            if (error != null)
                ViewData["Error"] = error;

            HttpClient httpClient = new();

            string url = mainUrl + "/Surfboards/Read?";
            if (sortOrder != null)
                url += "&sortOrder=" + sortOrder;
            if (currentFilter != null)
                url += "&currentFilter=" + currentFilter;
            if (searchString != null)
                url += "&searchString=" + searchString;
            if (pageNumber != 0)
                url += "&pageNumber=" + pageNumber;

            var result = await httpClient.GetFromJsonAsync<SurfboardsList>(url);
            Console.WriteLine(ViewData["CurrentSort"]);
            IQueryable<Surfboard> queryable = result.Surfboards.AsQueryable().AsNoTracking();
            return View(PaginatedList<Surfboard>.Create(queryable, result.PageNumber, result.PageSize));
        }


        
        // GET: Surfboards/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            return View();
        }

        // GET: Surfboards/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Surfboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create([Bind("Image,Id,Title,Length,Width,Volume,Thickness,Type,Price,Equipment")] Surfboard surfboard)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            HttpClient httpClient = new();
            Surfboard surfboard = await httpClient.GetFromJsonAsync<Surfboard>(mainUrl + "/Surfboards/Single?id=" + id);
            return View(surfboard);
        }

        // POST: Surfboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int id, [Bind("Image,Id,Title,Length,Width,Volume,Thickness,Type,Price,Equipment")] Surfboard surfboard)
        {
            if (id != surfboard.Id)
                return NotFound();
            if (!ModelState.IsValid)
                return View(surfboard);
            HttpClient httpClient = new();
            var request = await httpClient.PostAsJsonAsync(mainUrl + "/Surfboards/Edit?id="+id, surfboard);
            Console.WriteLine(request.StatusCode);
            if(request.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(surfboard);
        }

        // GET: Surfboards/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            HttpClient httpClient = new();
            Surfboard surfboard = await httpClient.GetFromJsonAsync<Surfboard>(mainUrl + "/Surfboards/Single?id=" + id);
            return View(surfboard);
        }

        // POST: Surfboards/Delete/5
        [HttpPost, ActionName("ConfirmDeletetion")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ConfirmDeletetion(int id)
        {
            var httpClient = new HttpClient();
            var request = await httpClient.DeleteAsync(mainUrl + "/Surfboards/Delete?id=" + id.ToString());
            if (request.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", new { error = "Delete failed with the status code: " + request.StatusCode });
        }

        private bool SurfboardExists(int id)
        {
          return false;
        }

       
    }
}
