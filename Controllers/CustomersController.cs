using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Runtime.Caching;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context; //To access DB & be able to query objects.

        public CustomersController()     //we need to initialize _context in the constructor.
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New() //To make the dropdown list. On a besoin que de cette liste. Celle de Customers on en a pas besoin car c à nous de l'entrer (pour le créer).
        {
            var membershiptypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), //the properties of this new customer will be initilized to their default values --> so customer.Id that we've put hidden, because it's an int, it will be initialized to it's default value which is 0. & then when the hidden field is rendered, it will have a value(0), so it will not be shown.
                MembershipTypes = membershiptypes
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) 
        {
           if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm",viewModel);
            }
            if(customer.Id == 0)
            _context.Customers.Add(customer); //To add this new customer to our DB, we need first to add it to the db context. Here it's not in the DB, it's just in the memory.
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id); //We get the customer in Db.
                //Mapper.Map(customer, customerInDb)
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate= customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges(); //At this point, our DB context goes through all modified objects and based on the kind of modification, it will generate SQL Statements at runtime & then it will run them in the DB.
            return RedirectToAction("Index", "Customers"); //Index action in Customers controller.
        }
        public ViewResult Index()
        {                                                       //PS: when var customers...is executed, EF is not going to query the DB! the query is actually executed when the iterate over the customers object(for example for the View y'a foreeach). By calling the ToList method it is executed immediatly.
           // var customers = _context.Customers.Include(c => c.MembershipType).ToList();        //initializing customers var. this customers property is a DBset defined in our DBcontext. So with this we can get all the customers in the DB.
            if (MemoryCache.Default["Genres"] == null) //To cache the Genres data.
            {
                MemoryCache.Default["Genres"] = _context.Genres.ToList();
            }
            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>; //This returns an object so we need to cast it to an IEnumerable of Genres.
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);         //Our query will be immediatly executed because we have SingleOrDefault extension.
            if (customer == null)
                return HttpNotFound();

            return View(customer);    
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id); //Ps : the id is read from the URL(In index view we can see it).C'est ActionResult qui assure ça.  
            if(customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList() //PS : ToList(),Single, ...All these are LINQ extension Methods! 
            };
            return View("CustomerForm", viewModel);
        }





    /*    private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith" },
                new Customer { Id = 2, Name = "Mary Williams" }
            };
        }*/
   
    }
}