using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UserBase.UserLogic;

namespace UserBase.WebUI.Models
{
    public class LoginRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string login)
        {
            return new[] { LoginLinker.Instance.GetRole(login) };
        }

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
            foreach(var username in usernames)
            {
                foreach(var rolename in roleNames)
                {
                    LoginLinker.Instance.ChangeRole(username, rolename);
                }
            }
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
            return LoginLinker.Instance.GetAllRoles();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return LoginLinker.Instance.GetRole(username) == roleName;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}