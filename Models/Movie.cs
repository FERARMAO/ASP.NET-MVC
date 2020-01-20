using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        //Let's say we want to create a page where we randomly pick a movie and render its details, assuming that this page would be :
        // /movies/random  --> we need to create a controller called Moviescontroller with an action called random!
        [Display(Name = "Release Date")]
        public  DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Range(1,20)]
        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }

        public Genre Genre { get; set; }

       
        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; } //We placed the [Required] annotation here so solve the error. EF doesn't see any difference.

        public byte NumberAvailable { get; set; }
    }
}