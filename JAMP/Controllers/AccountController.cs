using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
//Added Namespaces
using JAMP.Models;
using JAMP.DAL;
using JAMP.ViewModels;
using System.Globalization;
using System.Net.Mail;
using System.Net;


namespace JAMP.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private JampContext _context = new JampContext();

        #region Login & logoff
        //
        // GET: /Account/Login
        //
        [AllowAnonymous]
        public ActionResult Login()
        {
            AccountModels.LoginModel main = new AccountModels.LoginModel();
            getUserIdent();
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(main);
            }
        }

        //
        // POST: /Account/Login
        //
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountModels.LoginModel model, string returnUrl)
        {

            if ((ModelState.IsValid) && (Valid(model.Email, model.Password)))
            {
                Employee user = _context.Employees.FirstOrDefault(u => u.Email.ToLower() == model.Email.ToLower());

                if (!user.Archived)
                {
                    FormsAuthentication.SetAuthCookie(model.Email.ToLower(), createPersistentCookie: model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else {
                    ModelState.AddModelError("", "You no longer have access to this account.");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register Employee
        //
        // GET: /Account/Register (Employee)
        //
        public ActionResult Register()
        {
            EmployeeRegistrationViewModel main = new EmployeeRegistrationViewModel();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }

        //
        // POST: /Account/Register(Employee)
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(EmployeeRegistrationViewModel model, string returnUrl)
        {
            string email;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                    Employee user = _context.Employees.FirstOrDefault(u => u.Email.ToLower() == model.EmpRegister.EmployeeEmail.ToLower());

                    // Check if user already exists
                    if (user == null)
                    {
                        if (string.IsNullOrEmpty(model.EmpRegister.SelectedRole))
                        {
                            TempData["notice"] = "Please Select Role";
                        }
                        else
                        {
                            email = HttpContext.User.Identity.Name;
                            Employee newEmp = new Employee();
                            Employee oldEmp = new Employee();

                            // Insert employee into table
                            oldEmp = FindUser(email);
                            newEmp = _context.Employees.Add(new Employee
                            {
                                FirstName = ConvertToTitleCase(model.EmpRegister.UserName.ToLower()),
                                LastName = ConvertToTitleCase(model.EmpRegister.LastName.ToLower()),
                                Contacts = model.EmpRegister.Contacts,
                                Email = model.EmpRegister.EmployeeEmail,
                                Password = Encode(model.EmpRegister.Password.ToLower()),
                                RoleName = model.EmpRegister.SelectedRole,
                                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd, h:mm tt").ToLower(),
                                Settings = new SettingUser()
                            });

                            foreach (Business i in _context.Businesses.Where(n => n.BusinessID == oldEmp.Business.BusinessID))
                            {
                                i.Employees.Add(newEmp);
                            }
                            _context.SaveChanges();

                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email already exists. Please enter a different email.");
                    }

                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion 

        #region Register Business
        //
        // GET: /Account/Register (Business)
        //
        [AllowAnonymous]
        public ActionResult BusinessRegister()
        {
            RegistrationViewModel main = new RegistrationViewModel();
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(main);
            }
        }

        //
        // POST: /Account/Register(Business)
        //
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BusinessRegister(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the business
                try
                {
                    Business biz = _context.Businesses.FirstOrDefault(u => u.BusinessName.ToLower() == model.BusRegister.BusinessName.ToLower());

                    if (biz == null)
                    {
                        Business bus = new Business();
                        Employee emp = new Employee();
                        //add employee account
                        emp = _context.Employees.Add(new Employee
                        {
                            FirstName = ConvertToTitleCase(model.EmpRegister.UserName.ToLower()),
                            LastName = ConvertToTitleCase(model.EmpRegister.LastName.ToLower()),
                            Contacts = model.EmpRegister.Contacts,
                            Email = model.EmpRegister.EmployeeEmail.ToLower(),
                            RoleName = "Owner",
                            Password = Encode(model.EmpRegister.Password.ToLower()),
                            CreatedDate = DateTime.Now.ToString("yyyy-MM-dd, h:mm tt").ToLower(),
                            Settings = new SettingUser()
                        });
                        //add business account
                        bus = _context.Businesses.Add(new Business
                        {
                            BusinessName = ConvertToTitleCase(model.BusRegister.BusinessName),
                            BusinessStreet = model.BusRegister.BusinessStreet,
                            BusinessCity = model.BusRegister.BusinessCity,
                            BusinessPostalCode = model.BusRegister.BusinessPostalCode,
                            BusinessEmail = model.BusRegister.BusinessEmail,
                            BusinessContacts = model.BusRegister.BusTelephone,
                            CreatedDate = DateTime.Now.ToString("yyyy/MM/dd, h:mm tt").ToLower()
                        });
                        bus.Employees.Add(emp);

                        _context.SaveChanges();
                        AccountModels.LoginModel lg = new AccountModels.LoginModel();
                        lg.Email = emp.Email;
                        lg.Password = model.EmpRegister.Password;
                        Login(lg, null);

                        return RedirectToAction("Jamp", "App");
                    }

                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Manage Password
        //
        // GET: /Account/Manage
        //
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            AccountModels.LocalPasswordModel main = new AccountModels.LocalPasswordModel();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }

        //
        // GET: /Account/ManagedSuccess  [Password changed successfully]
        //
        public ActionResult ManagedSuccess()
        {
            ViewBag.message = "Your password has been changed successfully.";

            AccountModels.LocalPasswordModel main = new AccountModels.LocalPasswordModel();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }

        //
        // POST: /Account/Manage
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(AccountModels.LocalPasswordModel model)
        {
            bool changePasswordSucceeded = false;

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                if (Encode(model.OldPassword.ToLower()) == FindUser(HttpContext.User.Identity.Name).Password)
                {
                    try
                    {
                        changePasswordSucceeded = ChangePass(HttpContext.User.Identity.Name, model.NewPassword);
                    }
                    catch (Exception e)
                    {
                        e.Message.ToString();
                    }
                    if (changePasswordSucceeded == true)
                    {
                        return RedirectToAction("ManagedSuccess", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect");
                }
            }

            AccountModels.LocalPasswordModel main = new AccountModels.LocalPasswordModel();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }
        #endregion

        #region Manage Email
        //
        // GET: /Account/ManageEmail
        //
        public ActionResult ManageEmail()
        {
            AccountModels.ChangeEmail main = new AccountModels.ChangeEmail();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }

        //
        // POST: /Account/ManageEmail
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageEmail(AccountModels.ChangeEmail model)
        {
            bool updateSuccess = false;
            if (ModelState.IsValid)
            {
                if ((model.OldEmail) == FindUser(HttpContext.User.Identity.Name).Email)
                {
                    try
                    {
                        updateSuccess = ChangeEmail(HttpContext.User.Identity.Name, model.NewEmail);
                    }
                    catch (Exception e)
                    {
                        e.Message.ToString();
                    }
                    if (updateSuccess == true)
                    {
                        LogOff();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The current Email is incorrect");
                }
            }

            AccountModels.ChangeEmail main = new AccountModels.ChangeEmail();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }
        #endregion

        #region Manage Profile Picture
        //
        //GET: /Account/ManageProfilePicture
        //
        public ActionResult ManageProfilePicture()
        {
            EmployeeRegistrationViewModel main = new EmployeeRegistrationViewModel();
            string name = HttpContext.User.Identity.Name;
            Employee emp = FindUser(name);
            main.emp = emp;
            getUserIdent();
            return View(main);
        }

        //
        //POST: /Account/UploadPicture
        //
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPicture(AccountModels.EmployeeRegisterModel model)
        {
            bool updateSuccess = false;

            foreach (string file in Request.Files)
            {
                try
                {
                    var postedFile = Request.Files[file];
                    postedFile.SaveAs(Server.MapPath("~/Images/ProfilePictures/") + postedFile.FileName);
                    updateSuccess = ChangeProPic(HttpContext.User.Identity.Name, postedFile.FileName);
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Recover password
        //
        // GET: /Account/LoginRecovery
        //
        [AllowAnonymous]
        public ActionResult LoginRecovery()
        {
            AccountModels.LoginModelpasswordRecovery main = new AccountModels.LoginModelpasswordRecovery();
            return View(main);
        }

        //
        // POST: /Account/LoginRecovery
        //
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginRecovery(AccountModels.LoginModelpasswordRecovery model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var emp = FindUser(model.Email);
                if (emp != null)
                {
                    string msg = "Dear " + emp.FirstName.ToString() + " " + emp.LastName +
                        "<br/>" +
                        "<br/>" +
                        "Please receive here a temporary password: " + setTempPassword(emp) +
                        "<br/>" +
                        "We recommend that you reset your password." +
                        "<br/>" +
                        "Enjoy continue using Jamp ERP" +
                        "<br/>" +
                        "<br/>" +
                        "Regards" +
                        "<br/>" +
                        "Jamp support team";

                    SendEmail(model.Email.ToString().Trim(), "Password Recovery", msg);

                    return RedirectToAction("EmailSent", "Account");
                }
                else
                {
                    return RedirectToAction("EmailNotSent", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("", "The email provided is incorrect.");
            }
            return View(model);
        }

        //
        // GET: /Account/EmailSent
        //
        [AllowAnonymous]
        public ActionResult EmailSent()
        {
            ViewBag.message = "Your temporary password has been sent to your email address";
            return View();
        }

        //
        // GET: /Account/EmailNotSent
        //
        [AllowAnonymous]
        public ActionResult EmailNotSent()
        {
            ViewBag.message = "You appear to be not in the system. Please register.";
            return View();
        }
        #endregion

        #region Helper Methods

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Jamp", "App");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        //Converts text strings to title case
        private string ConvertToTitleCase(string value)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(value);
        }

        // Send email with temp password
        public void SendEmail(string address, string subject, string message)
        {
            try
            {
                string email = "ericashotel@gmail.com";
                string password = "b1e2n3g4u5";

                var loginInfo = new NetworkCredential(email, password);
                var msg = new System.Net.Mail.MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(address));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch
            {

            }
        }
        #endregion

        #region DbContext Methods

        // Encrypt string value
        private string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            string newpass;
            newpass = BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
            return newpass;
        }

        // Validate the users password
        private bool Valid(string _email, string _password)
        {
            var emp = FindUser(_email);
            if (emp != null)
            {
                if (emp.Password == Encode(_password.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Find user by username
        public Employee FindUser(string email)
        {
            try
            {
                var emp = _context.Employees.Single(e => e.Email == email);
                return emp;
            }
            catch
            {
                return null;
            }
        }

        // Update user password
        public bool ChangePass(string _email, string newpass)
        {
            bool result;

            try
            {
                Employee emp = FindUser(_email);
                emp.Password = Encode(newpass.ToLower());
                _context.Entry(emp).State = System.Data.EntityState.Modified;
                _context.SaveChanges();

                result = true;
            }
            catch
            {
                result = false;
            }


            return result;
        }

        // Change Email address
        public bool ChangeEmail(string _email, string newEmail)
        {
            bool result;

            try
            {
                Employee emp = FindUser(_email);
                emp.Email = newEmail;
                _context.Entry(emp).State = System.Data.EntityState.Modified;
                _context.SaveChanges();

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Change profile photo
        public bool ChangeProPic(string _email, string picName)
        {
            bool result;

            try
            {
                Employee emp = FindUser(_email);
                emp.ImageLocation = "../../Images/ProfilePictures/" + picName;
                _context.Entry(emp).State = System.Data.EntityState.Modified;
                _context.SaveChanges();

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Set a tempary password
        public string setTempPassword(Employee inEmp)
        {
            string newPassword = "Failed", tempPassword;

            Random num = new Random();
            double randomNum = num.Next(10000000, 999999999);
            tempPassword = randomNum.ToString();

            try
            {
                inEmp.Password = Encode(tempPassword);
                _context.Entry(inEmp).State = System.Data.EntityState.Modified;
                _context.SaveChanges();

                newPassword = tempPassword;
            }
            catch
            {
                newPassword = "";
            }

            return newPassword;
        }

        // Get the current logged in users name
        public ActionResult getUserIdent()
        {
            Employee emp = new Employee();
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    emp.FirstName = FindUser(HttpContext.User.Identity.Name).FirstName;
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
