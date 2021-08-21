
using System;
using System.Linq;
using Xunit;

using MMS.Data.Models;
using MMS.Data.Services;

namespace MMS.Test
{
    public class TestMovieService
    {
        private readonly IMovieService svc;
              
        public TestMovieService()
        {
            // create and initialise MovieServiceDb service
            svc = new MovieServiceDb();
            svc.Initialise();
                 
        }

        // add tests here - test should ensure your service implementation works
        [Fact]
        public void Test1() {
            
        }

        [Fact] 
        public void Movie_GetMoviesWhenNone_ShouldReturnNone()
        {
            // act 
            var movies = svc.GetAllMovies();
            var count = movies.Count;

            // assert
            Assert.Equal(0, count);
        }
    }
}
