using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MMS.Data.Models;
using MMS.Data.Services;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMS.Web.Controllers
{
    [Authorize]
    public class ReviewController : BaseController
    {
        private readonly IMovieService svc;
        public ReviewController() 
        {
            svc = new MovieServiceDb();
        }

        // GET /review/index
        public IActionResult Index()
        {
            var reviews = svc.GetAllReviews();
            return View(reviews);
        }


        // GET /review/create
        //[Authorize(Roles= "Admin")]
        public IActionResult Create()
        {
            var movies = svc.GetAllMovies(); 
            var vm = new ReviewViewModel
            {
                Movies = new SelectList(movies, "Id", "Name")
            };

            // render blank form
            return View(vm);
        }

        // POST /review/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles= "Admin")]
        public IActionResult Create([Bind("MovieId,Comment")] ReviewViewModel vm)
        {
            if (ModelState.IsValid)
            {
                svc.AddReview(vm.MovieId, vm.Comment);

                Alert($"Review Created", AlertType.info);  
                return RedirectToAction(nameof(Index));
            }

            // redisplay the form for editing
            return View(vm);
        }

    }
}
