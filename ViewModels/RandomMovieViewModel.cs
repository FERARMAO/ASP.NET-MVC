using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; } //Le Movie object et ses properties je veux les afficher comme un scalaire!
        public List<Customer> Customers { get; set; } //Le Customer object et ses properties je veux les afficher comme une liste!
    }
}