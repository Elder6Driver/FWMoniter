using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FWSync.Web
{
    public class MyPrinciple:System.Security.Principal.IPrincipal
    {

        public System.Security.Principal.IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string role)
        {
            //遍历用户拥有的所有角色
            foreach (string r in this.roles)
            {
                if (r == role)
                    return true;
            }

            return false;
        }

        private System.Security.Principal.IIdentity identity;
        private string[] roles;

        public MyPrinciple(System.Security.Principal.IIdentity identity, string[] roles)
        {
            this.identity = identity;
            this.roles = roles;
        }


    }
}