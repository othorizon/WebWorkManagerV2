using Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WorkProgress : System.Web.UI.Page
{

    static string workid;
    static string username = "201207123";
    static string classid = "[ALL]";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //加载用户名
            //TODO  username = Request.Cookies["UserStatus"].Values["username"];

            workid = Request.QueryString["workid"];


            if (workid == null)
                Response.Redirect("error.aspx?msg=openwork工作信息错误");

            //加载 设置班级列表
            LoadClassList();
            if (Request.QueryString["classid"] != null)
                classid = Request.QueryString["classid"];
            ClassList.SelectedValue = classid;

            LoadInfo();
        }
    }
    private void LoadClassList()
    {
        Bean bean = new Bean();

        ArrayList arrays = bean.GetClassList(username);
        foreach (string[] str in arrays)
        {
            ClassList.Items.Add(new ListItem(str[0], str[1]));
        }
        bean = null;
    }
    private void LoadInfo()
    {
        workbody.InnerHtml = "";//清空
        Bean bean = new Bean();
        ArrayList arrays = bean.GetWorkProgress(workid, classid);
        for (int i = 0; i < arrays.Count; i++)
        {
            workbody.InnerHtml += arrays[i];
        }
    }
    protected void ClassList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("TeacherCenter.aspx?classid=" + ClassList.SelectedValue.ToString());
    }
    protected void PackDownload_ServerClick(object sender, EventArgs e)
    {
        Bean bean = new Bean();
        ArrayList arrays=bean.GetAttaches(workid, classid);
        ZipFiles(arrays);
    }
    public void ZipFiles(ArrayList arrays)
    {
        Response.Clear();
        string archiveName = String.Format(arrays[1].ToString()+"_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "inline; filename=\"" + archiveName + "\"");

        using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))//解决中文乱码问题  
        {
            zip.AddEntry("Readme.txt",arrays[0].ToString(), Encoding.Default);
            for (int i = 2; i < arrays.Count; i++)
            {
                string[] str = (string[])arrays[i];
                zip.AddFile(str[1],"\\");
            }

            zip.Save(Response.OutputStream);
        }

        Response.End();

    }
}