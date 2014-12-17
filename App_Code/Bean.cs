using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Bean 的摘要说明
/// </summary>
public class Bean
{
    DAO dao = null;
    public Bean()
    {
        dao = new DAO();
    }
    public ArrayList GetWorkInfo(string username, string classid)//制作工作信息
    {
        //TODO 关于teacherid 后期要根据是学生还是老师来改变
        string sql = "select ReleaseWork.*,ClassInfo.Name as claName from ReleaseWork,ClassInfo where TeacherID='" + username + "' and ClassInfo.ClassID=ReleaseWork.ClassID";
        if (classid != "[ALL]" && classid.Trim() != "")
            sql += " and  ReleaseWork.ClassID='" + classid + "' ";
        sql += " ORDER BY EndTime ASC,ReleaseTime DESC";
        DataTable dt = dao.GetDataTable(sql);
        ArrayList arrays = new ArrayList();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arrays.Add(MakeOneWorkInfo(dt.Rows[i], i));
            }
        }
        return arrays;
    }
    string MakeOneWorkInfo(DataRow dr, int num)//制作一条工作信息
    {
        string url = "";
        string id = "";
        string title = "";
        string claName = "";
        string type = "";
        string progress = "";
        string ReleaseTime = "";
        string CommitTime = "无";
        string Attach = "";
        string AttachName = "";
        id = dr["WorkID"].ToString();
        url = "WorkProgress.aspx?workid=" + id + "&classid=" + dr["ClassID"].ToString();//设置链接
        title = dr["Title"].ToString();
        claName = dr["claName"].ToString();
        type = "作业";//TODO:这个要改
        progress = GetWorkCount(id);
        ReleaseTime = Convert.ToDateTime(dr["ReleaseTime"]).ToShortDateString();
        try
        {
            CommitTime = Convert.ToDateTime(dr["EndTime"]).ToShortDateString();
        }
        catch { }
        Attach = dr["Attach"].ToString();
        string[] temp = Attach.Split('\\');
        if (temp.Length > 1)
            AttachName = temp[temp.Length - 1];
        else
            AttachName = "无";
        string html = "<tr data-am-collapse=\"{target: '#workinfo-hide-" + num + "'}\"><td><input type=\"checkbox\" /></td><td>" + id + "</td>"
             + "<td><a href='" + url + "' target='_blank'>" + title + "</a></td>"
             + "<td>" + claName + "</td>"
             + "<td>" + type + "</td>"
             + "<td>" + progress + "</td>"
             + "<td>" + ReleaseTime + "</td>"
             + "<td>" + CommitTime + "</td>"
             + "<td style='display:none' >" + Attach + "</td>"
             + "<td>" + AttachName + "</td>"
             + "<td><div class=\"am-btn-toolbar\"><div class=\"am-btn-group am-btn-group-xs\">"
             + "<button class=\"am-btn am-btn-default am-btn-xs am-text-secondary\"><span class=\"am-icon-pencil-square-o\"></span> 编辑</button>"
             + "<button class=\"am-btn am-btn-default am-btn-xs\"><span class=\"am-icon-copy\"></span> 复制</button>"
             + "<button class=\"am-btn am-btn-default am-btn-xs am-text-danger\"><span class=\"am-icon-trash-o\"></span> 删除</button>"
             + "</div></div></td></tr>";
        //折叠栏
        html += "<tr id='workinfo-hide-" + num + "' class='am-panel-collapse am-collapse am-success'>"
             + "<td colspan='2'>详情：</td>"
             + "<td colspan='17'>" + dr["Content"] + "</td>"
             + "</tr>";
        return html;
    }
    /// <summary>
    /// 获取某工作的所有完成者，作为表格的行项
    /// </summary>
    /// <param name="workid"></param>
    /// <param name="classid"></param>
    /// <returns></returns>
    public ArrayList GetWorkProgress(string workid, string classid)
    {
        string sql = "select distinct CommitID,StudentInfo.StudentID,StudentInfo.Name,Title,CommitTime,Content,Attach,ClassInfo.Name as claName from StudentInfo,CommitWork,ClassInfo where CommitWork.WorkID='" +
          workid + "' and StudentInfo.StudentID=CommitWork.StudentID and ClassInfo.ClassID=StudentInfo.ClassID";
        if (classid != "[ALL]" && classid.Trim() != "")
            sql += " and StudentInfo.ClassID='" + classid + "'";
        sql += " ORDER BY StudentInfo.StudentID ASC";
        DataTable dt = dao.GetDataTable(sql);
        ArrayList arrays = new ArrayList();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arrays.Add(MakeOneWorkProgress(dt.Rows[i], i));
            }
        }
        return arrays;
    }

    private string MakeOneWorkProgress(DataRow dr, int num)
    {
        string id = "";
        string title = "";
        string type = "";
        string author = "";
        string xuehao = "";
        string CommitTime = "";
        string Attach = "无";
        string AttachName = "";
        id = dr["CommitID"].ToString();
        title = dr["Title"].ToString();
        type = "作业";//TODO:这个要改
        author = dr["Name"].ToString() + "</br>" + dr["claName"].ToString();
        xuehao = dr["StudentID"].ToString();
        try
        {
            CommitTime = Convert.ToDateTime(dr["EndTime"]).ToShortDateString();
        }
        catch { }
        Attach = dr["Attach"].ToString();
        string[] temp = Attach.Split('\\');
        AttachName = temp[temp.Length - 1];
        string html = "<tr data-am-collapse=\"{target: '#workprogress-hide-" + num + "'}\"><td><input type=\"checkbox\" /></td><td>" + id + "</td>"
             + "<td><a href=\"#\">" + title + "</a></td>"
             + "<td>" + type + "</td>"
             + "<td>" + xuehao + "</td>"
             + "<td>" + author + "</td>"
             + "<td>" + CommitTime + "</td>"
             + "<td style='display:none' id='down_" + num + "' >" + Attach + "</td>"
             + "<td>" + AttachName + "</td>"
             + "<td><div class=\"am-btn-toolbar\"><div class=\"am-btn-group am-btn-group-xs\">"
             + "<button class=\"am-btn am-btn-default am-btn-xs am-text-secondary\"><span class=\"am-icon-pencil-square-o\"></span> 编辑</button>"
             + "<button class=\"am-btn am-btn-default am-btn-xs\"><span class=\"am-icon-copy\"></span> 复制</button>"
             + "<button class=\"am-btn am-btn-default am-btn-xs am-text-danger\"><span class=\"am-icon-trash-o\"></span> 删除</button>"
             + "</div></div></td></tr>";
        //折叠栏
        html += "<tr id='workprogress-hide-" + num + "' class='am-panel-collapse am-collapse am-success'>"
             + "<td colspan='2'>详情：</td>"
             + "<td colspan='17'>" + dr["Content"] + "</td>"
             + "</tr>";
        return html;
    }
    /// <summary>
    /// 获取教师职教班级列表,生成为Select选项
    /// </summary>
    /// <param name="username">TeacherID</param>
    /// <returns></returns>
    public ArrayList GetClassList(string username)
    {
        ArrayList arrays = new ArrayList();
        arrays.Add(new string[] { "所有班级", "[ALL]" });
        string sql = "select ClassInfo.ClassID,ClassInfo.Name from TeacherInfo,ClassInfo where TeacherID='" + username
                   + "' and ClassInfo.ClassID=TeacherInfo.ClassID";
        DataTable dt = dao.GetDataTable(sql);
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arrays.Add(new string[] { dt.Rows[i]["Name"].ToString(), dt.Rows[i]["ClassID"].ToString() });
            }
        }
        return arrays;

    }

    /// <summary>
    /// 获取工作完成人数及总人数 Finished,Total,Name,ClassID
    /// </summary>
    /// <param name="workid">工作id</param>
    /// <returns></returns>
    private string GetWorkCount(string workid)
    {
        string sql = "select (select count(*) from CommitWork where WorkID='" + workid + "' and StudentID in(select StudentID from StudentInfo where ClassID=cla.ClassID)) as Finished,count(StudentID) as Total,cla.Name,cla.ClassID from StudentInfo,ClassInfo cla where cla.ClassID=StudentInfo.ClassID group by StudentInfo.ClassID,cla.Name,cla.ClassID";
        DataTable dt = dao.GetDataTable(sql);
        string html = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += dt.Rows[i]["Finished"].ToString() + "/" + dt.Rows[i]["Total"].ToString() + " " + dt.Rows[i]["Name"] + "</br>";
        }
        return html;
    }


    /// <summary>
    /// 根据页码返回该页所有项目
    /// </summary>
    /// <param name="pagenum">页码</param>
    /// <param name="arrays">全部项目</param>
    /// <returns>该页项目集</returns>
    public ArrayList GetPageWorkInfo(int pagenum, ArrayList arrays)
    {
        int start = (pagenum - 1) * GlobalVar.ItemNumperPage ;//arrays 从0开始 第一页0-14
        ArrayList resultarrays = new ArrayList();
        //设置该页最大项的位置
        int end;
        if ((start + GlobalVar.ItemNumperPage - 1) < arrays.Count - 1)
            end = start + GlobalVar.ItemNumperPage - 1;
        else
            end = arrays.Count - 1;

        for (int i = start; i <= end; i++)
        {
            resultarrays.Add(arrays[i]);
        }
        return resultarrays;
    }
    /// <summary>
    /// 返回页码的html源码
    /// </summary>
    /// <param name="Sum">总页数</param>
    /// <param name="currentPage">当前页</param>
    /// <param name="Url">要跳转的网页</param>
    /// <returns></returns>
    public String setPageNum(int Sum, int currentPage, String Url)
    {
        int pagenum = GetPageNum(Sum);//页码总数
        String Html = "";
        if (currentPage == 1)
            Html += "<li class='am-disabled'><a href='#'>«</a></li>";
        else
            Html += "<li><a href='" + Url + "?page=" + (currentPage - 1) + "'>«</a></li>";

        for (int i = 1; i <= pagenum; i++)
        {
            if (i == currentPage)
                Html += "<li class='am-active'><a href='#'>" + i + "</a></li>";
            else
                Html += "<li><a href='" + Url + "?page=" + i + "'>" + i + "</a></li>";
        }

        if (currentPage < pagenum)
            Html += "<li><a href='" + Url + "?page=" + (currentPage + 1) + "'>»</a></li>";
        else
            Html += "<li class='am-disabled'><a href='#'>»</a></li>";

        return Html;
    }

    /// <summary>
    /// 获取页码总数
    /// </summary>
    /// <param name="Sum">总共的item数</param>
    /// <returns></returns>
    private int GetPageNum(int Sum)
    {
        int n = Sum / GlobalVar.ItemNumperPage;
        if (Sum % GlobalVar.ItemNumperPage != 0)
            n += 1;
        return n;
    }

    public ArrayList GetAttaches(string workid,string classid)
    {
        ArrayList arrays = new ArrayList();//第一个为readme.txt 第二个为班级名 后面为string[2] 0项：文件名 1项：文件路径
        string sql = "select distinct StudentInfo.Name,StudentInfo.StudentID,Attach from StudentInfo,CommitWork where CommitWork.WorkID='" +
          workid + "' and StudentInfo.StudentID=CommitWork.StudentID ";
        if (classid != "[ALL]" && classid.Trim() != "")
            sql += " and StudentInfo.ClassID='" + classid + "'";
        sql += " ORDER BY StudentInfo.StudentID ASC";
        DataTable dt = dao.GetDataTable(sql);

        string className=dao.GetDataRow("select Name from ClassInfo where classid='"+classid+"'")[0].ToString();


        int count = 0;
        string notfound = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            bool result = false;

            if (dt.Rows[i]["Attach"] != null)
            {
                string path = dt.Rows[i]["Attach"].ToString();
                if (File.Exists(path))
                {
                    arrays.Add(new string[] { dt.Rows[i]["StudentID"].ToString() + dt.Rows[i]["Name"].ToString(), dt.Rows[i]["Attach"].ToString() });
                    count++;
                    result = true;
                }
            }

            if (result==false)
            {
                notfound+= dt.Rows[i]["StudentID"].ToString() + "\t" + dt.Rows[i]["Name"].ToString() + "\n";
            }
        }
        String ReadmeText = String.Format("此压缩包为 {0} 班级的作业压缩包\n\n" +
                 "共包含{1}个文件\n" +
                 "以下学生的作业不存在：\n"+
                 notfound,className, count);
        arrays.Insert(0, ReadmeText);
        arrays.Insert(1, className);
        return arrays;
    }
}