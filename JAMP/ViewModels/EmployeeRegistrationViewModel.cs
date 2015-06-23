using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Added Namespaces
using JAMP.Models;
using JAMP.DAL;

namespace JAMP.ViewModels
{
    public class EmployeeRegistrationViewModel
    {
        public AccountModels.EmployeeRegisterModel EmpRegister { get; set; }
        public Employee emp { get; set; }

        //receives selected role id
        public List<Role> roles
        {
            get
            {
                using (JampContext db = new JampContext())
                {
                    List<Role> roleList = db.Roles.ToList();

                    int index = 0;
                    bool found = false;
                    while (!found && index < roleList.Count)
                    {
                        found = (roleList[index].RoleName == "Owner");
                        if (!found)
                        {
                            index++;
                        }
                        else 
                        {
                            roleList.RemoveAt(index);
                        }
                    }
                    return roleList;
                }
            }
        }
    }
}