using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnregister_Click(object sender, EventArgs e)
    {
        string username = this.tbxusername.Text;
        string password = this.tbxpassword.Text;

        System.Web.Security.MembershipCreateStatus status;

        //本来那两个"haha"是string.Empty，但是那么写会报错，所以瞎写个东西好了
        System.Web.Security.MembershipUser user =
            System.Web.Security.Membership.CreateUser(
            username,
            password,
            string.Empty,
            "haha",
            "haha",
            true,
            out status
            );

        switch (status)
        {
            case MembershipCreateStatus.Success:
                this.Response.Redirect("~/Default.aspx");
                break;
            case MembershipCreateStatus.InvalidUserName:
                this.lblWarn.Text = "用户名错误";
                break;
            case MembershipCreateStatus.DuplicateUserName:
                this.lblWarn.Text = "用户名已经存在";
                break;
            default:
                this.lblWarn.Text = "未知错误";
                break;
        }
      
    }
}