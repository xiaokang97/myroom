using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.DAL
{
   public class DepartDao
    {
        //根据部门名称获取部门ID
        public Depart findDpartByName(string DepartName)
        {

            Depart depart= null;
            string sql = "select * from xk.depart where name = @departname";

            MySqlParameter[] pars = {
                                       new MySqlParameter("@departname",MySqlDbType.VarChar)

                                   };
            pars[0].Value = DepartName;

            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                depart = new Depart();//实例化 ,并且将此条数据保存到depart对象中
                depart.id = Int32.Parse(row["id"].ToString());
                depart.name = row["name"].ToString();
                depart.desc = row["desc"].ToString();
                return depart;
            }
            else
            {
                return depart;
            }

        }
        //获取部门列表，返回Datatable对象
        public DataTable findAllDepTable()
        {
            string sql = "select * from xk.depart";
            //CommandType.Text:sql语句
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, null);
            return dt;
        }
            //获取部门列表
            public List<Depart> findAll()
        {
            string sql = "select * from xk.department";
            //CommandType.Text:sql语句
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, null);
            //创建一个List集合对象用于保存从数据库查询出来的结果
            List<Depart> departInfoList = new List<Depart>();
            foreach (DataRow row in dt.Rows)
            {
                Depart depart= new Depart();
                depart.id = Int32.Parse(row["id"].ToString());
                depart.name = row["name"].ToString();
                depart.desc = row["desc"].ToString();
                departInfoList.Add(depart);
            }

            return departInfoList;
        }
    }
}
