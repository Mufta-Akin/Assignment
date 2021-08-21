
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MMS.Data.Models;
using MMS.Data.Services;
using MMS.Web.Controllers;
using MMS.Web.Models;

namespace MMS.Web.Controllers
{
    public class MovieController : BaseController
    {
        private IMovieService svc;

        public MovieController()
        {
            svc = new MovieServiceDb();
        }

        // GET /movie/Index
        public IActionResult Index()
        {
            // complete this method
            var movies = svc.GetAllMovies();
            
            return View(movies);
        }

        // GET /movie/details/{id}
        public IActionResult Details(int id)
        {
            // -retrieve the movie with specified id from the service
            var m = svc.GetMovieById(id);

            // -check if s is null and return NotFound()
            if (m == null)
            {  
              //-return NotFound(); //return error 404
              Alert($"No such movie {id}", AlertType.warning);
              return RedirectToAction(nameof(Index));
              
            }

            // pass movie as parameter to the view
            return View(m);
        }

        // GET: /movie/create
        //[Authorize(Roles= "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            // display blank form to create a movie
            var m = new Movie();
            return View(m);
        }

       

        // POST /movie/create---Calls the form to process
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles= "Admin")]
        public IActionResult Create([Bind("Title, Director, Year, Duration, Budget, PosterUrl, Cast, Plot")] Movie m, Genre Genre)
        {
            
            // complete POST action to add movie
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                var added = svc.AddMovie(m);

               
                Alert("Movie has been added", AlertType.info);

                return RedirectToAction(nameof(Index));
            }
           
            // redisplay the form for editing as there are validation errors
            return View(m);
        }


        
        // GET /movie/edit/{id}
        //[Authorize(Roles= "Admin")]
        public IActionResult Edit(int id)
        {
            var m = svc.GetMovieById(id);

            // check if m is null and return NotFound()
            if (m == null)
            {
                Alert($"No such movie {id}", AlertType.warning); 
                return RedirectToAction(nameof(Index));
            }   

            // pass movie to view for editing
            return View(m);
        }

        // POST /movie/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles= "Admin")]
        public IActionResult Edit(int id, [Bind("Title, Director, Year, Duration, Budget, PosterUrl, Cast, Plot")] Movie m)
                    
        {
            // validate movie
            if (ModelState.IsValid)
            {
                // pass data to service to update
                svc.UpdateMovie(m);
                Alert("Movie details saved", AlertType.info);

                return RedirectToAction(nameof(Index));
            }

            // redisplay the form for editing as validation errors
            return View(m);
        }

        // GET / movie/delete/{id}
        //[Authorize(Roles= "Admin")]
        public IActionResult Delete(int id)
        {
            // load the movie using the service
            var m = svc.GetMovieById(id);
            // check the returned movie is not null and if so alert
            if (m == null)
            {
               Alert("Movie Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }     
            
            // pass movie to view for deletion confirmation
            return View(m);
        }

        // POST /movie/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles= "Admin")]
        public IActionResult DeleteConfirm(int id)
        {
            // delete movie via service
            svc.DeleteMovie(id);
            Alert($"Movie {id} deleted ", AlertType.success);

            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }

        
        // GET /movie/createReview
        //[Authorize(Roles= "Admin")]
        public IActionResult CreateReview(int id) //create a review for this movie(id)
        {
            var m = svc.GetMovieById(id);
             // check the returned movie is not null and if so alert
            if (m == null)
            {
                Alert($"movie {id} does not exist", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }  
            // create the review view model and populate the MovieId property
            var r = new Review {
                MovieId = id
                
            };
 
            return View("CreateReview", r); // to display to the user create review view
        }

        // POST /movie/createreview
        [HttpPost]
        //[Authorize(Roles= "Admin")]
        public IActionResult CreateReview(Review r)
        {
            var m = svc.GetMovieById(r.MovieId);
            // check the returned Movie is not null and if so alert
            if (m == null)
            {
                Alert($"No such movie {r.MovieId}", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            Alert($"Review created", AlertType.success);
            // create the review view model and populate the MovieId property
            svc.AddReview(r.MovieId, r);

            return RedirectToAction("Details", new { Id = r.MovieId });

        }
           
    }
}