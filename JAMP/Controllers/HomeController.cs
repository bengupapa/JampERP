using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Added namespaces
using JAMP.Models;
using JAMP.ViewModels;
using JAMP.DAL;
using JAMP.Controllers;

namespace JAMP.Controllers
{
    public class HomeController : Controller
    {
        //Marketing home page
        public ActionResult Index()
        {
            EmployeeViewModel empView = new EmployeeViewModel();
            
            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        //About JAMP
        public ActionResult About()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        //Contact
        public ActionResult Contact()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        public ActionResult Help()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        public ActionResult started()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        public ActionResult features()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        public ActionResult Modules()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        public ActionResult support()
        {
            EmployeeViewModel empView = new EmployeeViewModel();

            Employee emp = findEmployee();
            empView.emp = emp;

            getUserIdent();
            return View(empView);
        }

        #region Helpper methods

        // Get the Employee
        private Employee findEmployee()
        {
            if (User.Identity.IsAuthenticated)
            {
                Employee emp = new Employee();

                try
                {
                    using (JampContext _context = new JampContext())
                    {
                        string email = HttpContext.User.Identity.Name;
                        emp = _context.Employees.FirstOrDefault(e => e.Email == email);

                        return emp;
                    }
                }
                catch
                {
                    return null;
                }

            }
            return null;
        }
        
        // Get the name of the current logged in user
        private ActionResult getUserIdent()
        {
            if (User.Identity.IsAuthenticated)
            {

                try
                {
                    Employee emp = findEmployee();
                    ViewBag.Name = emp.FirstName;
                    return View(emp);

                }
                catch
                {
                    return View();
                }

            }
            return View();
        }
        #endregion
    }
}
