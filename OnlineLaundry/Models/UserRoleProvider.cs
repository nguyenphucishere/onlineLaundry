using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using OnlineLaundry.Models;

namespace OnlineLaundry.Models
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (OnlineLaundryEntities _Context = new OnlineLaundryEntities())
            {
                var userRoles = (from staff in _Context.staffs
                                 where staff.username == username
                                 select staff.role).ToArray();
                return userRoles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            using (OnlineLaundryEntities _Context = new OnlineLaundryEntities())
            {
                var userRoles = (from staff in _Context.staffs
                                 where staff.role == roleName
                                 select staff.role).First();
                return userRoles != null;
            }
        }
    }
}