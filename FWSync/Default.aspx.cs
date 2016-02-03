using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FWSync.BLL;
using FWSync.Model;
using FWSync.Web;

public partial class _Default : System.Web.UI.Page
{
    //地图要用到的内容
    public int deviceid = 1;
    public string mapdata = "";
    //js作图用这个串来传递数据
    public string jsonstr = "";
    public string jsonstr2 = "";
    //public int devicenum = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次进入，给按钮和repeater赋初始值，并完成一次数据绑定

            //这里使用viewstate对两个参数进行全局存储
            //ViewState["DeviceID"] = 1;
            //ViewState["ParamID"] = 1;

            //如果登录，则显示用户名
            HttpContext context = this.Context;
            //MyIdentity identity = context.Items["User"] as MyIdentity;
            //System.Security.Principal.IPrincipal user = context.Items["User"] as System.Security.Principal.IPrincipal;

            System.Security.Principal.IPrincipal user = this.User;

            //if (user.Identity.IsAuthenticated)
            //{
            //    this.lblLogSta.Text = string.Format("欢迎 {0}", user.Identity.Name);
            //}
            //else
            //{
            //    this.lblLogSta.Text = "未登录";
            //}

           

        }

        //这里应该是每次进来都做得事情，即对数据进行绑定
        //首先对表格进行调整
        BindAll();


    }

    private void BindAll()
    {

        IList<DeviceInfo> devicedata = DeviceDataProxy.GetDevices();
        mapdata = "[";

        if (devicedata.Count > 0)
        {
            for (int i = 0; i < devicedata.Count; i++)
            {
                mapdata += "[" + devicedata[i].DeviceX + "," + devicedata[i].DeviceY + "," + devicedata[i].DeviceID + "," + "\"" + devicedata[i].DeviceDesc + "\"],";
            }
        }
        mapdata = mapdata.TrimEnd(',');
        mapdata += "]";


        if (this.Request["devicenum"] != null)
        {
            deviceid = int.Parse(HttpUtility.HtmlEncode(this.Request["devicenum"]));
        }


        //int deviceid = (int)ViewState["DeviceID"];
        //int paramid = (int)ViewState["ParamID"];
        IList<ParamAndOneDataInfo> tempdata = WebUtility.GetParamAndOneData(deviceid);
        rpt.DataSource = tempdata;
        rpt.DataBind();

        //接下来对折线进行调整
        jsonstr = GetLine(deviceid, 1);
        jsonstr2 = GetLine(deviceid, 2);

        
            

    }

    private string GetLine(int deviceid,int paramid)
    {
        IList<OriginalDataInfo> orilist = new List<OriginalDataInfo>();
        OriginalData ori = new OriginalData();

        IList<ParamInfo> paramdata = ParamDataProxy.GetParams();


        orilist = ori.GetTopNDatasByDeviceIDAndParamID(20, deviceid, paramid);
        int j = orilist.Count;
        if (j > 0)
        {
            string json = "{\"j\":" + j + ",\"rows\":[";
            double maxitem = 1000;//最多放1000个点在图像上面
            int step = j > maxitem ? (int)(maxitem / j) : 1;

            for (int i = 0; i < orilist.Count; i += step)
            {
                json += "{\"time1\":\"" + (Convert.ToDateTime(orilist[i].InsertTime).AddHours(-8) - new DateTime(1970, 1, 1)).TotalMilliseconds + "\",\"price\":\"" + Convert.ToDecimal(orilist[i].ParamValue) + "\"},";
            }

            json = json.TrimEnd(',');
            json += "]}";
            return json;

        }
        else
        {
            throw new Exception("无数据");
        }
    }

    //protected void btnDevice1_Click(object sender, EventArgs e)
    //{
    //    ViewState["DeviceID"] = 1;
    //    BindAll();
    //}

    //protected void btnDevice2_Click(object sender, EventArgs e)
    //{
    //    ViewState["DeviceID"] = 2;
    //    BindAll();
    //}


    //protected void btnLogout_Click(object sender, EventArgs e)
    //{
    //    HttpCookie cookie = new HttpCookie("Ticket");
    //    cookie.Expires = new DateTime(1999, 9, 9);

    //    this.Response.Cookies.Add(cookie);
    //    this.Response.Redirect("~/Login.aspx");
    //}
}