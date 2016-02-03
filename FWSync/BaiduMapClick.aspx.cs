using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FWSync.BLL;
using FWSync.Model;
using FWSync.Web;

public partial class BaiduMapClick : System.Web.UI.Page
{

    public int devicenum = 1;
    public string mapdata = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            //如果是第一次进入
            //加载地图，并且传输相应的设备信息在地图上
            


        }


        IList<DeviceInfo> devicedata = DeviceDataProxy.GetDevices();
        mapdata = "[";

        if (devicedata.Count > 0)
        {
            for(int i = 0 ; i < devicedata.Count ; i++)
            {
                mapdata += "[" + devicedata[i].DeviceX + "," + devicedata[i].DeviceY + "," + devicedata[i].DeviceID + "," + "\"" + devicedata[i].DeviceDesc + "\"],";
            }
        }
        mapdata = mapdata.TrimEnd(',');
        mapdata += "]";


        if (this.Request["devicenum"] != null)
        {
            int devicenum = int.Parse(HttpUtility.HtmlEncode(this.Request["devicenum"]));
        }

        //这里需要修改相应的菜单栏显示了，相当于BindAll这个函数需要做得了

        


       
    }
}