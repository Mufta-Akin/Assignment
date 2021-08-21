using Microsoft.AspNetCore.Mvc.Rendering;
using MMS.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MMS.Web.Models
    {
       public class AddedViewModel
       {
          [Required(ErrorMessage = "Title is required")] 
          [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
          public string Title { get; set; }
          public string Message { get; set; }
          public DateTime Formed { get; set; } = DateTime.Now;
          public string FormedString => Formed.ToLongDateString();
          public int Days => (DateTime.Now - Formed).Days;
          public Genre Genre { get; set; }
        public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>
          {
              new SelectListItem(Genre.Action.ToString(), ((int)Genre.Action).ToString()),
              new SelectListItem(Genre.Comedy.ToString(), ((int)Genre.Comedy).ToString()),
              new SelectListItem(Genre.Family.ToString(), ((int)Genre.Family).ToString()),
              new SelectListItem(Genre.Horror.ToString(), ((int)Genre.Horror).ToString()),
              new SelectListItem(Genre.Romance.ToString(), ((int)Genre.Romance).ToString()),
              new SelectListItem(Genre.SciFi.ToString(), ((int)Genre.SciFi).ToString()),
              new SelectListItem(Genre.Thriller.ToString(), ((int)Genre.Thriller).ToString()),
              new SelectListItem(Genre.Western.ToString(), ((int)Genre.Western).ToString()),
              new SelectListItem(Genre.War.ToString(), ((int)Genre.War).ToString())
          };

       }
    }
    