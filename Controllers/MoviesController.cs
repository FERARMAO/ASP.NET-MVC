using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models; //ReSharper put this using automatically!!
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Authorize(Roles = RoleName.CanManageMovies)] //Now even if we use the link movies/new we cannot access the page without loggin with manager account.
        public ActionResult New()
        {
            var genre = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genre
            };
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock; //I added this to have the NumberAvailable when creating a new movie.
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.NumberAvailable = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ViewResult Index()
        {
            // var movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies)) 
                return View("List");

            return View("ReadOnlyList"); //Here we have only the list to read without New button!
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id) 
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie) //To make the code cleaner. It will take the value from Db here for example, and thanks to the constroctor, we pass those values to the properties of Movie NON NULLABLE we just declared in the MovieFormViewModel.
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }



     /*   private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
                {
                    new Movie {Id =1, Name="Shrek"},
                    new Movie {Id =2, Name="Batman"},
                };

        }*/
    }
}






// GET: Movies/
/* public ActionResult Random()            //In this method let's create an instance of our Movie model
 {
     var movie = new Movie() { Name = "Shrek!" };
     var customers = new List<Customer>
{
 new Customer { Name = "Customer1"},    //Here we create a liste of two customers.
 new Customer { Name = "Customer2"}
};

     var viewModel = new RandomMovieViewModel     //Here we create a view model object.
     {
         Movie = movie,
         Customers = customers
     };

     return View(viewModel); */


/* [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
 public ActionResult ByRealeaseDate(int year, int month)
 {
     return Content(year + "/" + month);
 } */

//return Content("Hello world!");
//return HttpNotFound();
//return new EmptyResult();
//return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" }); //Index is the action, Home is the controller.    