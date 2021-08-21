using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MMS.Data.Models
{
    public class Review
    { 
        public int Id { get; set; }      

        // name of reviewer
        [Required]
        public string Name { get; set; }   

        // date review was made        
        public DateTime CreatedOn { get; set; }

        // reviewer comments
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Comment { get; set; }

        // value between 1-10
        public int Rating { get; set; }
    
        // EF Dependant Relationship Review belongs to a Movie
        public int MovieId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Movie Movie { get; set; }
        //public bool Active { get; internal set; }
    }
}