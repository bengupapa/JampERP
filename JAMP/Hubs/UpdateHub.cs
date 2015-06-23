using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Added Namespaces
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using System.Diagnostics;
using JAMP.DAL;
using JAMP.Models;

namespace JAMP.Controllers
{
    [Authorize]
    [HubName("JampUpdateHub")]
    public class UpdateHub : Hub
    {
        private JampContext _context = new JampContext();

        #region Connect and disconnect

        public override Task OnConnected()
        {
            try
            {
                // Get user and set to online
                Employee user = _context.Employees.FirstOrDefault(u => u.Email.ToLower() == Context.User.Identity.Name);
                updateStatus(user, true);

                // Add to Business Group
                string groupName = user.BusinessID.ToString();
                Groups.Add(Context.ConnectionId, groupName);

                // Add to user Group
                Groups.Add(Context.ConnectionId, user.EmployeeID.ToString());

                // Add to Management Group if not cashier
                if (user.RoleName != "Cashier")
                {
                    Groups.Add(Context.ConnectionId, "Management" + groupName);
                }
                Debug.WriteLine(user.LoginCount + "[Connect]");
                // Tell group members of new arrival 
                if (user.LoginCount == 1)
                {
                    Clients.Group("Management" + groupName).access(true, user.EmployeeID);
                }
            }
            catch (Exception e) { Console.Write(e.Message); }
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            try
            {
                // Get user and set to offline
                Employee user = _context.Employees.FirstOrDefault(u => u.Email.ToLower() == Context.User.Identity.Name);
                updateStatus(user, false);

                // Remove from Business Group
                string groupName = user.BusinessID.ToString();
                Groups.Remove(Context.ConnectionId, groupName);

                // Add to user Group
                Groups.Remove(Context.ConnectionId, user.EmployeeID.ToString());

                // Remove from Management Group if not cashier
                if (user.RoleName != "Cashier")
                {
                    Groups.Remove(Context.ConnectionId, "Management" + groupName);
                }
                Debug.WriteLine(user.LoginCount + "[Disconnect]");
                // Tell group members of removal
                if (user.LoginCount == 0)
                {
                    Clients.Group("Management" + groupName, Context.ConnectionId).access(false, user.EmployeeID);
                }
            }
            catch (Exception e) { Console.Write(e.Message); }
            return base.OnDisconnected();
        }
        #endregion

        #region Accessible Server Methods
        // Send updates list to client
        public void GetUpdates(string aDevice)
        {
            try
            {
                // Get list of updates
                List<EntityUpdate> _updates = _context.Devices
                    .Where(d => d.Number == aDevice)
                    .SelectMany(eu => eu.Updates)
                    .ToList();
                if (_updates.Count() > 0)
                {
                    foreach (EntityUpdate aUpdate in _updates)
                    {
                        aUpdate.Device = null;
                        aUpdate.DeviceID = 0;
                    }


                    Clients.Client(Context.ConnectionId).entitesToUpdate(_updates);
                }
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        // Broadcast update available 
        public void UpdateNotification(string aDevice, List<EntityUpdate> UpdateList)
        {
            // Check bools
            bool allLevel = false;
            bool managementLevel = false;

            try
            {
                // Get the user and there business group
                Employee user = _context.Employees
                    .FirstOrDefault(u => u.Email.ToLower() == Context.User.Identity.Name);
                string groupName = user.BusinessID.ToString();

                // Get a list of all devices in that business group
                List<Device> listDevice = _context.Employees.Where(e => e.BusinessID == user.BusinessID)
                    .SelectMany(d => d.Devices)
                    .ToList();

                // Assign update based on device user role level
                foreach (Device _device in listDevice)
                {
                    // Check not the caller device
                    if (_device.Number != aDevice)
                    {
                        // Assign the updates
                        foreach (EntityUpdate _update in UpdateList)
                        {
                            EntityUpdate _New = new EntityUpdate();
                            _New.Entity = _update.Entity;
                            _New.UpdateNumber = _update.UpdateNumber;

                            switch (_update.Entity)
                            {
                                // All employee case
                                case "Product":
                                case "Category":
                                case "Customer":
                                case "CustomerAccount":
                                case "CustomerPayment":
                                case "Business":
                                    _device.Updates.Add(_New);
                                    allLevel = true;
                                    break;
                                // User specific case
                                case "SettingUser":
                                    if (_device.EmployeeID == user.EmployeeID) 
                                    {
                                        _device.Updates.Add(_New);
                                    }
                                    break;
                                // Manager and owner case
                                default:
                                    if (_device.Employee.RoleName != "Cashier")
                                    {
                                        _device.Updates.Add(_New);
                                        managementLevel = true;
                                    }
                                    break;
                            }
                        }
                    }

                }
                _context.SaveChanges(); //Save update list to the DB

                // Broadcast available update
                if (allLevel)
                {
                    Clients.Group(groupName, Context.ConnectionId).updateMessage();
                }
                else if (managementLevel) 
                {
                    Clients.Group("Management" + groupName, Context.ConnectionId).updateMessage();
                } 
                else // user level
                {
                    Clients.Group(user.EmployeeID.ToString(), Context.ConnectionId).updateMessage();
                }
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        // Unlink the users Device
        public void UnlinkDevice(string aDevice)
        {
            try
            {
                Device device = _context.Devices.FirstOrDefault(d => d.Number == aDevice);
                _context.Devices.Remove(device);
                _context.SaveChanges();
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        // Remove completed Updates
        public void RemoveUpdates(string aDevice)
        {
            try
            {
                // Get list of updates
                List<EntityUpdate> _updates = _context.Devices
                    .Where(d => d.Number == aDevice)
                    .SelectMany(eu => eu.Updates)
                    .ToList();

                foreach (EntityUpdate aUpdate in _updates)
                {
                    _context.Updates.Remove(aUpdate);
                }

                _context.SaveChanges();

            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        // Broadcast stock changes
        //public void stockChanges(List<StockMessage> messages)
        //{
        //    try
        //    {
        //        // Get the user and there business group
        //        Employee user = _context.Employees
        //            .FirstOrDefault(u => u.Email.ToLower() == Context.User.Identity.Name);
        //        string groupName = user.BusinessID.ToString();
        //        Clients.Group(groupName, Context.ConnectionId).stockChangeMessage(messages);
        //    }
        //    catch (Exception e) { Console.Write(e.Message); }
        //}
        #endregion

        #region Device Setup Methods

        // Assign a unique ID to a client
        public void AssignUniqueID()
        {
            try
            {
                Employee user = _context.Employees
                    .FirstOrDefault(u => u.Email.ToLower() == Context.User.Identity.Name);

                Device aDevice = new Device();
                aDevice.EmployeeID = user.EmployeeID;
                aDevice.Number = Guid.NewGuid().ToString("N");
                _context.Devices.Add(aDevice);
                _context.SaveChanges();
                Clients.Client(Context.ConnectionId).storgeLocally(aDevice.Number);
            }
            catch (Exception e) { Console.Write(e.Message); }
        }
        #endregion

        #region Helper Methods
        // Set user to online or offline
        private void updateStatus(Employee emp, bool status)
        {
            try
            {
                if (status) // If online
                {
                    emp.Online = status;
                    emp.LastSeen = "Now";
                    emp.LoginCount += 1;
                }
                else
                {
                    emp.LoginCount -= 1;
                    if (emp.LoginCount < 0)
                    {
                        emp.LoginCount = 0;
                    }
                    else if (emp.LoginCount == 0) // If last offline
                    {
                        emp.Online = status;
                        emp.LastSeen = DateTime.Now.ToString("h:mm tt, yyyy'/'MM'/'dd").ToLower();
                    }
                }
                _context.Entry(emp).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        // Stock change items
        public class StockMessage
        {
            public int ProductID { get; set; }
            public int OutReorder { get; set; } // 0 = out, 1 = reorder
        }
        #endregion
    }
}