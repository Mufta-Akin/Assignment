using System;
using System.Text;
using System.Collections.Generic;
using MMS.Data.Models;

namespace MMS.Data.Services
{
    public static class ServiceSeeder
    {
        // use this class to seed the database with dummy 
        // test data using an IMovieService 
        public static void Seed(IMovieService svc)
        {
            svc.Initialise();
           
        }
    }
}
