using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FWSync.Web
{
    public class MyModule:System.Web.IHttpModule
    {

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            application.AuthenticateRequest += 
                new EventHandler(Application_AuthenticateRequest);


        }

        //针对所有请求，就会到这里
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;

            //找请求的cookie里面是否有用户票据
            HttpCookie cookie = request.Cookies["Ticket"];

            string name = string.Empty;

            if (cookie != null)
            {
                string ticketstring = cookie.Value;

                //解密

                System.Web.Security.FormsAuthenticationTicket ticket
                    = System.Web.Security.FormsAuthentication.Decrypt(ticketstring);

                name = ticket.Name;


            }

            //上面是教学实践，下面是微软写好的
            //MyIdentity identity = new MyIdentity(name, "Type");
            System.Security.Principal.GenericIdentity identity
                = new System.Security.Principal.GenericIdentity(name, "Type");

            //MyPrinciple user = new MyPrinciple(identity, new string[] { });
            System.Security.Principal.GenericPrincipal user
                = new System.Security.Principal.GenericPrincipal(identity,new string[] { } );

            HttpContext context = HttpContext.Current;
            context.Items.Add("User", user);


        }

    }
}