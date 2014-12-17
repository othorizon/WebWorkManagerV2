using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// DAO 的摘要说明
/// </summary>
public class DAO
{
    SqlConnection con = null;
    string strcon;
    SqlTransaction tran;
	public DAO()
	{
       // strcon = WebConfigurationManager.ConnectionStrings["sqlservercon"].ConnectionString;
         strcon = WebConfigurationManager.ConnectionStrings["WebWorkManagerConnectionString"].ConnectionString; 
	}
    public void open()
    {
        if (con == null)
            con = new SqlConnection(strcon);

        if (con.State.Equals(ConnectionState.Closed))
            con.Open();
    }
    public void close()
    {
        if (con == null) return;
        if (con.State.Equals(ConnectionState.Open))
        {
            con.Close();
            con.Dispose();
            con = null;
        }
        else
        {
            con.Dispose();
            con = null;
        }
    }
    public int ExecuteNonQuery(string sql)//执行SQL
    {
        try
        {
            this.open();
            SqlCommand cmd = new SqlCommand(sql, con);
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        finally
        {
            close();
        }

    }
    //执行SQL 不关闭连接 事务处理使用
    public int ExecuteNonQueryWithTran(string sql)
    {
        int ret = 0;
        try
        {
            this.open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Transaction = tran;
            ret = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ret;
    }
    public void BeginTrans()
    {
        tran = con.BeginTransaction();
    }
    public void Commit()
    {
        tran.Commit();
    }
    public void Rollback()
    {
        tran.Rollback();
    }
    public int ExecuteNonQueryWithParam(string sql, List<SqlParameter> sqlParams)//执行SQL 传递参数
    {
        try
        {
            this.open();
            SqlCommand cmd = new SqlCommand(sql, con);
            foreach (SqlParameter p in sqlParams)
                cmd.Parameters.Add(p);
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        finally
        {
            this.close();
        }
    }
    public DataTable GetDataTable(string sql)//执行SQL 返回查询的表 失败返回null
    {
        DataTable dt;
        try
        {
            this.open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dt = ds.Tables[0];
        }
        catch
        {
            dt = null;
        }
        finally
        {
            this.close();
        }
        return dt;
    }
    public DataRow GetDataRow(string sql)//执行SQL返回DataRow 失败返回null
    {
        DataRow dr;
        try
        {
            dr = GetDataTable(sql).Rows[0];
        }
        catch
        {
            dr = null;
        }
        finally
        {
            close();
        }
        return dr;
    }
}