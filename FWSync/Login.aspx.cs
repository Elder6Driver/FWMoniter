using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string name = this.tbxName.Text;
        string password = this.tbxPassword.Text;

        bool isexist = System.Web.Security.Membership.ValidateUser(name, password);

        if (isexist)
        {

            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(name, true);
            ////生成了票据
            //System.Web.Security.FormsAuthenticationTicket ticket
            //    = new System.Web.Security.FormsAuthenticationTicket(
            //        2,
            //        name,
            //        DateTime.Now,
            //        DateTime.Now.AddHours(12),
            //        true,
            //        string.Empty
            //        );

            ////这里需要存储产生的票据
            //HttpCookie cookie = new HttpCookie("Ticket");

            ////序列化并加密
            //string ticketstring =
            //    System.Web.Security.FormsAuthentication.Encrypt(ticket);

            //cookie.Value = ticketstring;
            //cookie.Expires = DateTime.Now.AddHours(12);

            //this.Response.Cookies.Add(cookie);

            ////这里应该跳转到登录成功的界面之类的地方，先转向主页也一样
            //this.Response.Redirect("~/Default.aspx");

        }
    }
}