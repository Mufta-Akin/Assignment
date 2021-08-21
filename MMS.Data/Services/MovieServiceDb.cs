using Microsoft.EntityFrameworkCore;
using MMS.Data.Models;
using MMS.Data.Repositories;
using MMS.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMS.Data.Services
{
    // create IMovieService implementation called MovieServiceDb
    // using the provided Entityframework Repository (MovieDbContext)
    public class MovieServiceDb : IMovieService
    {
        private readonly MovieDbContext db;
       

        public MovieServiceDb()
        {
            db = new MovieDbContext();
        }

        public void Initialise()
        {
            db.Initialise();           
        }

        // ------------------ Movie Related Operations ------------------------

        // retrieve list of Movies
        public IList<Movie> GetAllMovies(string order=null)
        {
            return db.Movies.ToList();
        }

        // Retrive movies by Id 
        public Movie GetMovieById(int id)
        {
            return db.Movies.FirstOrDefault(m => m.Id == id);
        }

        public bool DeleteMovie(int id)
        {
            var m = GetMovieById(id);
            if (m == null)
            {
                return false;
            }
            db.Movies.Remove(m);
            db.SaveChanges(); // write to database
            return true;
        }

         // Update the movie with the details in updated 
         bool IMovieService.UpdateMovie(Movie m)
        {
            // verify the movie exists
            var movie = GetMovieById(m.Id);
            if (movie == null)
            {
                return false;
            }
            // update the details of the movie retrieved and save

            movie.Title = m.Title;
            movie.Director = m.Director;
            movie.Year = m.Year;
            movie.Duration = m.Duration;
            movie.Budget = m.Budget;
            movie.PosterUrl = m.PosterUrl;
            movie.Genre = m.Genre;
            movie.Cast = m.Cast;
            movie.Plot = m.Plot;

            db.SaveChanges(); // write to database
            return true;
        }

        
        // Add a new movie checking a movie with same title does not exist
       public Movie AddMovie(Movie m)
        {
            // check if movie title already exists
            var exists = GetMovieById(m.Id);

            if (exists != null)
            {
                return null; // title already exists, cannot create movie
            } 
            // title is ok so go ahead an create movie
            var mov = new Movie
            {
                //  Id is set by Db              
                Title =m.Title,
                Director = m.Director,
                Year = m.Year,
                Duration = m.Duration,
                Budget = m.Budget,
                PosterUrl = m.PosterUrl,
                Genre =m.Genre,
                Cast = m.Cast,
                Plot = m.Plot
                
            };
            db.Movies.Add(mov);
            db.SaveChanges(); // write to database
            return mov; // return newly added movie
        }
                         

        public IList<Movie> GetMoviesQuery(Func<Movie,bool> q)
        {
            return db.Movies.Include(m => m.Reviews).Where(q).ToList();
        }
       
      
         // =================== Review Management ===================
         public Review AddReview(int movieId, Review r)
        {
            var movie = GetMovieById(movieId);
            if (movie == null) return null;
             var review = new Review
             {
              //Comment = comment,
              MovieId = movieId,
            ///////////////////////////////////
              CreatedOn = DateTime.Now,
            /////////////////////////////////

            };
 
            // add a review
           movie.Reviews.Add(review);
            db.SaveChanges(); // write to database
            return review;         
        }


         public Review GetReviewById(int id)
        {
            return db.Reviews.Include(r => r.Movie).FirstOrDefault(r => r.Id == id);

            // var review = db.Movies.SelectMany(m => m.Reviews).FirstOrDefault(m => m.Id == id);
            // return review;
        }
        
        public bool DeleteReview(int id)
        {
            // find review
            var review = GetReviewById(id);
            if (review == null) return false;
            
            // remove review 
            var result = review.Movie.Reviews.Remove(review);
            
            db.SaveChanges();
            return result;
        }

        // Retrieve all reviews and the associated movies 
        public IList<Review> GetAllReviews()
        {
            return db.Reviews.Include(r => r.Movie).ToList();
        }

        // --------------- User Management / Authentication -----------------

        /// <returns>The user if authenticated, otherwise null</returns>
        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="u">User to register</param>
        /// <returns>The user if registered, otherewise null</returns>
        public User Register(string name, string email, string password, Role role)
        {
            
            // call service to retrieve a user by their email address (GetUserByEmail)
            var exists =  GetUserByEmail(email);

            // return null if user found as you cannot register two users with same email address
            if (exists != null)
            {
                return null;
            }

            // create a new user object and populate with method  parameters
            var user = new User
            {
                Name = name,
                Email = email,
                // call Hasher to encrypt the password before storing in database
                Password = Hasher.CalculateHash(password),
                Role = role
            };
            

            // add user to database and save changes 
            db.Add(user);
            db.SaveChanges();  

            // return the newly created user    
            return user;
        }

        /// <summary>
        /// Find a user by EmailAddress (name should be unique)
        /// </summary>
        /// <param name="email">user EmailAddress</param>
        /// <returns>The user if found, otherewise null</returns>
        public User GetUserByEmail(string email)
        {
            var U = db.Users.FirstOrDefault(u => u.Email == email);
            return U;
        }

        void IMovieService.AddReview(int movieId, string comment)
        {
            throw new NotImplementedException();
        }
    }
    
}