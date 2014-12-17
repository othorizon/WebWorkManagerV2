<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReleaseWork.aspx.cs" Inherits="ReleaseWork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 200px;
            width: 336px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="text-align:left;width:340px">
        <div>
            <div>
                标题：<asp:TextBox ID="tbtitle" Style="background-color: transparent;" Width="221px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbtitle" Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="Red">不能为空</asp:RequiredFieldValidator>
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
        <div style="height:5px"></div>

    </form>

</body>
</html>
