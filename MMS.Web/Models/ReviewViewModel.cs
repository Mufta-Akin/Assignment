using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MMS.Web.Models
{
    public class ReviewViewModel
    {
        // selectlist of movies (id, name)       
        public SelectList Movies { set; get; }

        // Collecting MovieId and Comment in Form
        [Required]
        [Display(Name = "Select Movie")]
        public int MovieId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Comment { get; set; }
    }
}
