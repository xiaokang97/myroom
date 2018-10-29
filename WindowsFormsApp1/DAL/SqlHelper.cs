using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAL
{
   public class SqlHelper
    {
        //string connStr = "server=localhost;user id=root;persistsecurityinfo=True;database=xk;password=123456;Allow User Variables=True;SslMode=none";
        private readonly static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        /*
         * 此方法专门针对查询操作
         * sql:查询语句
         * type:表示你想使用什么方式出来查询.1.sql语句 2.存储过程 
         * pars:参数数组
         */
        public static DataTable GetTable(string sql, CommandType type, params MySqlParameter[] pars)
        {
            DataTable dt = null;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlDataAdapter adper = new MySqlDataAdapter(sql, conn))
                {
                    adper.SelectCommand.CommandType = type;//设置什么方式进行查询(sql语句,存储过程)
                    if (pars != null)
                    {
                        adper.SelectCommand.Parameters.AddRange(pars);
                    }
                    dt = new DataTable();
                    adper.Fill(dt);
                }
            }
            return dt;
        }
        /*
  * 此方法专门针对删除 , 修改 , 添加操作
  * sql:查询语句
  * type:表示你想使用什么方式出来查询.1.sql语句 2.存储过程 
  * pars:参数数组
  */
        public static int ExecuteNonQuery(string sql, CommandType type, params MySqlParameter[] pars)
        {
            int i = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = type; ;//设置什么方式进行操作(sql语句,存储过程)
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    i = cmd.ExecuteNonQuery();
                }
            }
            return i;
        }
        
        public static object ExecuteScalar(string sql, CommandType type, params MySqlParameter[] pars)
        {
           object obj = null ;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = type; ;//设置什么方式进行操作(sql语句,存储过程)
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    obj = cmd.ExecuteScalar();
                }
            }
            return obj;
        }


    }
}
