using System;
using System.Collections.Generic;
using MMS.Data.Models;

namespace MMS.Data.Services
{
    public interface IMovieService
    {void Initialise();
        IList<Movie> GetAllMovies(string order=null);
        Movie GetMovieById(int id);
        bool DeleteMovie(int id);
        bool UpdateMovie(Movie m);
        Movie AddMovie(Movie m);

        // Review      

        Review AddReview(int movieId, Review r);
        Review GetReviewById(int id);
        bool DeleteReview(int id);
        
        IList<Review> GetAllReviews();

        // User
        User Authenticate(string email, string password);
        User Register(string name, string email, string password, Role role);
        User GetUserByEmail(string email);
        void AddReview(int movieId, string comment);
        //void AddReview(int movieId, string comment);
    }
}