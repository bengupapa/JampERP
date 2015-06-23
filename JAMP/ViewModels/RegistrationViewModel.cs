using System;
using System.Web;
//Added Namespaces
using JAMP.Models;

namespace JAMP.ViewModels
{
    public class RegistrationViewModel
    {
        public AccountModels.EmployeeRegisterModel EmpRegister { get; set; }
        public AccountModels.BusinessRegisterModel BusRegister { get; set; }      
    }
}