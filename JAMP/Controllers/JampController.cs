using System;
using System.Linq;
using System.Web.Http;
//Added Namespaces
using Breeze.WebApi;
using Breeze.WebApi.EF;
using JAMP.DAL;
using JAMP.Models;
using Newtonsoft.Json.Linq;
using System.Web;

namespace JAMP.Controllers
{
    [BreezeController]
    [Authorize]
    public class JampController : ApiController
    {
        readonly EFContextProvider<JampContext> _contextProvider = new EFContextProvider<JampContext>();

        #region Primary API's
        // /breeze/jamp/Metadata 
        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        // Post save changes
        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        // /breeze/jamp/UserInformation
        [HttpGet]
        public IQueryable<Employee> UserInformation()
        {
            string name = HttpContext.Current.User.Identity.Name;
            return _contextProvider.Context.Employees.Where(n => n.Email == name);
        }

        // /breeze/jamp/BusinessInformation
        [HttpGet]
        public IQueryable<Business> BusinessInformation()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Businesses.Where(e => e.BusinessID == user.BusinessID);
        }
        #endregion

        #region Backup API's
        // /breeze/jamp/Employees
        [HttpGet]
        public IQueryable<Employee> Employees()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Employees.Where(e => e.BusinessID == user.BusinessID);
        }

        // /breeze/jamp/Products
        [HttpGet]
        public IQueryable<Product> Products()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Products.Where(e => e.BusinessID == user.BusinessID);
        }

        // /breeze/jamp/Categories
        [HttpGet]
        public IQueryable<Category> Categories()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Categories.Where(e => e.BusinessID == user.BusinessID); 
        }

        // /breeze/jamp/Orders
        [HttpGet]
        public IQueryable<Order> Orders()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Orders.Where(e => e.BusinessID == user.BusinessID);
        }

        // /breeze/jamp/Suppliers
        [HttpGet]
        public IQueryable<Supplier> Suppliers()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Suppliers.Where(e => e.BusinessID == user.BusinessID);
        }

        // /breeze/jamp/Customers
        [HttpGet]
        public IQueryable<Customer> Customers()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Customers.Where(e => e.BusinessID == user.BusinessID);
        }

        // /breeze/jamp/Sales
        [HttpGet]
        public IQueryable<Sale> Sales()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Sales.Where(e => e.BusinessID == user.BusinessID);
        }

        // /breeze/jamp/Incidents
        [HttpGet]
        public IQueryable<Incident> Incidents()
        {
            string email = HttpContext.Current.User.Identity.Name;
            Employee user = _contextProvider.Context.Employees.FirstOrDefault(u => u.Email.ToLower() == email);
            return _contextProvider.Context.Incidents.Where(e => e.BusinessID == user.BusinessID); 
        }
        #endregion

    }
}
