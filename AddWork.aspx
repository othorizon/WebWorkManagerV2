<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddWork.aspx.cs" Inherits="AddWork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                标题：<asp:TextBox ID="tbtitle" Style="background-color: transparent;" Width="221px" runat="server"></asp:TextBox>
            </div>
            <div id="dvmycontent">
                <div>作业要求：</div>
                <textarea id="tacontent" style="height: 200px; width: 336px" runat="server" name="S1"></textarea>
            </div>
            <div id="dvattach" style="width: 336px" runat="server">
                截止日期:
                <asp:TextBox ID="tb_endtime" runat="server"></asp:TextBox>
                <br />
                发布班级:<br />
                <asp:ListBox ID="ListBox1" runat="server" Font-Size="Large" Height="108px" SelectionMode="Multiple" Width="237px"></asp:ListBox>
            </div>
            <div>
                <asp:Button ID="bt_submit" runat="server" Text="提交作业" OnClick="bt_submit_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div style="height: 5px"></div>
    </form>
</body>
</html>
