using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FWSync.Web
{
    public class MyIdentity: System.Security.Principal.IIdentity
    {


        public string AuthenticationType
        {
            get { return this.authenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return this.isAuthenticated; }
        }

        public string Name
        {
            get { return this.name; }
        }

        private string name;
        private bool isAuthenticated;
        private string authenticationType;

        public MyIdentity(string name,string authenticationType)
        {
            this.name = name;
            this.authenticationType = authenticationType;

            this.isAuthenticated = !string.IsNullOrEmpty(name);

        }

    }
}