<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaiduMapClick.aspx.cs" Inherits="BaiduMapClick" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
	<style type="text/css">
		body, html,#allmap {width: 800px;height: 600px;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=A5hDmYyGr7ObVNKHoH7RzYnU"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    
    </div>
    <div id="allmap"></div>
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
        window.location.href = 'BaiduMapClick.aspx?devicenum='+devicenum;
    }
</script>

