using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeacherCenter : System.Web.UI.Page
{
    static string classid = "[ALL]";
    static string username = "201207123";
    int page = 1;
    int itemNum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //加载用户名
            //TODO  username = Request.Cookies["UserStatus"].Values["username"];

            if (Request.QueryString["classid"] != null)
            {
                classid = Request.QueryString["classid"];
            }
            if (Request.QueryString["page"] != null)
            {
                try
                {
                    page = Convert.ToInt32(Request.QueryString["page"]);
                }
                catch { }
            }
            LoadClassInfo();
            ClassList.SelectedValue = classid;
            LoadWorkInfo();
        }

    }

    private void LoadClassInfo()
    {
        Bean bean = new Bean();

        ArrayList arrays = bean.GetClassList(username);
        foreach (string[] str in arrays)
        {
            ClassList.Items.Add(new ListItem(str[0], str[1]));
        }
        bean = null;
    }

    private void LoadWorkInfo()
    {

        workbody.InnerHtml = "";//清空
        Bean bean = new Bean();
        ArrayList arrays = bean.GetWorkInfo(username, classid);


        //先设置页码
        SetPageNum(arrays.Count);
        arrays = bean.GetPageWorkInfo(page, arrays);
        for (int i = 0; i < arrays.Count; i++)
        {
            workbody.InnerHtml += arrays[i];
        }
    }
    private void SetPageNum(int sum)
    {
        Bean bean = new Bean();
        pageul.InnerHtml += bean.setPageNum(sum, page, "TeacherCenter.aspx");
        dvpage.InnerHtml="共"+sum+"条记录";
        itemNum = sum;
    }
    protected void ClassList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("TeacherCenter.aspx?classid=" + ClassList.SelectedValue.ToString());
    }
    protected void Allselect_ServerChange(object sender, EventArgs e)
    {
    }
}