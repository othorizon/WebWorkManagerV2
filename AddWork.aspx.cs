using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddWork : System.Web.UI.Page
{
    static string username="201207123";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //TODO   username = Request.Cookies["UserStatus"].Values["username"];
            if (username == null)
                Response.Redirect("error.aspx?msg=openwork工作信息错误");



            //加载班级
            Bean bean = new Bean();
            ArrayList arrays = bean.GetClassList(username);
            arrays.RemoveAt(0);
            foreach (string[] str in arrays)
            {
                ListBox1.Items.Add(new ListItem(str[0], str[1]));
            }
        }
    }
    protected void bt_submit_Click(object sender, EventArgs e)
    {
        if (tbtitle.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('标题不能为空')", true);
            return;
        }
        if (ListBox1.SelectedIndex == -1)
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('还未选择班级')", true);
            return;
        }
        for (int i = 0; i < ListBox1.Items.Count; i++)
        {
            if (ListBox1.Items[i].Selected)
            {
                if (insertone(ListBox1.Items[i].Value) < 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('" + ListBox1.Items[i].Text + "的作业发布失败，终止发布')", true);
                    return;
                }

            }
        }

        Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('发布成功')", true);
        Response.Write("<script>window.parent.closeaddwork();</script>");
    }

    private int insertone(string classid)
    {


        string commitetime = "";
        if (tb_endtime.Text.Trim() != "")
        {
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(tb_endtime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('截止日期格式错误。格式：yyyy-MM-dd HH:mm:ss')", true);
                return -1;
            }
            commitetime = dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        string sql = "";
        List<SqlParameter> sqlparams = new List<SqlParameter>();

        if (commitetime != "")
        {
            sql = "insert into ReleaseWork (TeacherID,ClassID,Title,Content,ReleaseTime,EndTime)values(@TeacherID,@ClassID,@Title,@Content,@ReleaseTime,@EndTime)";
            sqlparams.Add(new SqlParameter("@CommitTime", commitetime));
        }
        else
            sql = "insert into ReleaseWork (TeacherID,ClassID,Title,Content,ReleaseTime)values(@TeacherID,@ClassID,@Title,@Content,@ReleaseTime)";

        sqlparams.Add(new SqlParameter("@ClassID", classid));
        sqlparams.Add(new SqlParameter("@TeacherID", username));
        sqlparams.Add(new SqlParameter("@Title", tbtitle.Text.Trim()));
        sqlparams.Add(new SqlParameter("@Content", tacontent.InnerText));
        sqlparams.Add(new SqlParameter("@ReleaseTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        DAO db = new DAO();
        return db.ExecuteNonQueryWithParam(sql, sqlparams);
    }


}