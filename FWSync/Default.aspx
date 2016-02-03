<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>首页</title>
    
    
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=A5hDmYyGr7ObVNKHoH7RzYnU"></script>


    <script type="text/javascript" src="./JS/echarts.common.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LoginName ID="LoginName1" runat="server" />
        <asp:LoginStatus ID="LoginStatus1" runat="server" 
            LogoutAction="RedirectToLoginPage" />
    </div>
        
    
        这里是选择哪个设备，以后需要换为地图的api
        <br />
        <br />
        <%--<asp:Button ID="btnDevice1" runat="server" Text="设备1" 
        onclick="btnDevice1_Click" />
        <asp:Button ID="btnDevice2" runat="server" Text="设备2" 
        onclick="btnDevice2_Click" />--%>
        <div id="allmap" style="width: 600px;height:400px;"></div>
        <br />
        <br />
        <br />
        这里需要根据列表选择哪个参数，通过repeater点击某一行然后刷新并绑定数据来实现
     
        <br />
        <asp:Repeater ID="rpt" runat="server" >
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            设备名称
                        </td>
                        <td>
                            设备值
                        </td>
                        <td>
                            采集时间
                        </td>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td >
                        <asp:Label ID = "lblName" runat ="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParamDesc") %>'></asp:Label>

                    </td>
                    <td >
                        <asp:Label ID = "lblValue" runat ="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParamValue") %>'></asp:Label>

                    </td>
                     <td >
                        <asp:Label ID = "lblTime" runat ="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsertTime") %>'></asp:Label>

                    </td>
                </tr>
                

            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        
        
        </asp:Repeater>
        <br />
        <br />
        这里需要有图像，折线图什么的
        <div id="container" style="width: 600px;height:400px;">
        </div>

        <script type="text/javascript">
            // 基于准备好的dom，初始化echarts实例
            var myChart = echarts.init(document.getElementById('container'));

            // 指定图表的配置项和数据
            option = 
            {
               tooltip: {
                    trigger: 'item',
                    formatter: function (params) {

                            var date = new Date(params.value[0]);
                            date = date.getFullYear() + '-'
                                   + (date.getMonth() + 1) + '-'
                                   + date.getDate() + ' '
                                   + date.getHours() + ':'
                                   + date.getMinutes();
                            
                            return   '时间:'
                                   + date +  '<br/>'+ '参数值: '
                                   + params.value[1] ;
                    }
                },
                legend: {
                    data: ['温度','一氧化碳']
                },
                grid: {

                    containLabel: true
                },

                xAxis: 
                [
                    {
                        type: 'time'
                        //boundaryGap: false,
                        //data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
                    }
                ],
                    yAxis: 
                [
                    {
                        type: 'value',
                        scale:true
                    }
                ],
                    series: 
                [
                    {
                        name:'温度',
                        type:'line',
                        smooth: true,
                        data:(function (){
                            var res = [];
                            var jsonData = <%= jsonstr %>;
                            var i;
                            var now = new Date();
                            for (i = 0; i <  jsonData.j; i++) 
                           {
                               res.push([
                                        now.setTime(jsonData.rows[ jsonData.j - i-1].time1),
                                        jsonData.rows[ jsonData.j - i-1].price
                                        ]);
                               
                           }
                            return res;
                        })()
                    },
                    {
                        name:'一氧化碳',
                        type:'line',
                        smooth: true,
                        data:(function (){
                            var res = [];
                            var jsonData = <%= jsonstr2 %>;
                            var i;
                            var now = new Date();
                            for (i = 0; i <  jsonData.j; i++) 
                           {
                               res.push([
                                        now.setTime(jsonData.rows[ jsonData.j - i-1].time1),
                                        jsonData.rows[ jsonData.j - i-1].price
                                        ]);
                               
                           }
                            return res;
                        })()
                    }
                ]
            };//end option

            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
    </script>

    </form>
</body>
</html>
<script type="text/javascript">
    // 百度地图API功能
    var map = new BMap.Map("allmap");
    var point = new BMap.Point(120.277515, 31.490646);
    map.centerAndZoom(point, 18);
    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放

    var json_data = <%= mapdata %>;
    var pointArray = new Array();
    for (var i = 0; i < json_data.length; i++) 
    {
        var marker = new BMap.Marker(new BMap.Point(json_data[i][0], json_data[i][1])); // 创建点
        map.addOverlay(marker);    //增加点
        //这个是给点加标签，但是单击事件还是在点上面
        var strdesc = "设备" + json_data[i][2] + ":" + json_data[i][3]
        var label = new BMap.Label(strdesc, { offset: new BMap.Size(20, -10) });
        marker.setTitle(json_data[i][2]);
        marker.setLabel(label);
        pointArray[i] = new BMap.Point(json_data[i][0], json_data[i][1]);
        marker.addEventListener("click", function(e){ChangeLabel(e)});
    }
    //让所有点在视野范围内
    map.setViewport(pointArray);

    //点击marker之后调用的函数，初步理解为改变隐藏的label里面的值，然后在刷新本页的方法
    function ChangeLabel(e) 
    {
        var p = e.target;
        var devicenum = p.getTitle();
        
        // 这里设置了转向页面，使用了get请求，如果没有这个的话，那么设定一个默认值好了
        window.location.href = 'Default.aspx?devicenum='+devicenum;
    }
</script>


