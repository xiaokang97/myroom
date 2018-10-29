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
    class UserDao
    {
        Common com = new Common();
        //用户登录
        public Users login(int username, string password)
        {

            Users user = null;
            string sql = "select * from xk.users where user_id = @username and password = @password";

            MySqlParameter[] pars = {
                                       new MySqlParameter("@username",MySqlDbType.Int32),
                                       new MySqlParameter("@password",MySqlDbType.VarChar)
                                   };
            pars[0].Value = username;
            pars[1].Value = password;
            //try   
            //{
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, pars);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                user = new Users();//实例化 ,并且将此条数据保存到user对象中
                user.user_id = com.ToInt(row["user_id"]);
                user.password = com.ToString(row["password"]);
                user.name = com.ToString(row["name"]);
                user.dep_id = com.ToInt(row["dep_id"]);
                user.sex = com.ToString(row["sex"]);
                user.age = com.ToInt(row["age"]);
                return user;
            }
            else
            {
                return user;
            }
        }
       
        //catch (Exception e)

        //{
        //    throw e;
        //}

        //   }
        //用户添加方法
        public int addUser(Users user)
        {
            int i = 0;
            string sql = "insert into xk.users values(@userID,@password,@name,@depID,@sex,@age)";
            MySqlParameter[] pars = {
                                    new MySqlParameter("@UserID", MySqlDbType.Int32),
                                    new MySqlParameter("@password", MySqlDbType.VarChar),
                                    new MySqlParameter("@name", MySqlDbType.VarChar),
                                    new MySqlParameter("@sex", MySqlDbType.VarChar),
                                    new MySqlParameter("@age", MySqlDbType.Int32),
                                    new MySqlParameter("@depID", MySqlDbType.Int32),
                                  };
            pars[0].Value = user.user_id;
            pars[1].Value = user.password;
            pars[2].Value = user.name;
            pars[3].Value = user.sex;
            pars[4].Value = user.age;
            pars[5].Value = user.dep_id;
            i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return i;
        }
        //表格中用户更新方法
        public int UpdateUser(Users user)
        {
            int i = 0;
            string sql = "update xk.users set name=@name,sex=@sex,age=@age,dep_id=@dep_id where user_id = @UserID";
            MySqlParameter[] pars = {
                                    new MySqlParameter("@UserID", MySqlDbType.Int32),
                                    new MySqlParameter("@name", MySqlDbType.VarChar),
                                    new MySqlParameter("@sex", MySqlDbType.VarChar),
                                    new MySqlParameter("@age", MySqlDbType.Int32),
                                    new MySqlParameter("@dep_id", MySqlDbType.Int32),
                                  };
            pars[0].Value = user.user_id;
            pars[1].Value = user.name;
            pars[2].Value = user.sex;
            pars[3].Value = user.age;
            pars[3].Value = user.dep_id;
            i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return i;
        }
        //删除用户
        public int deleteUser(int id)
        {
            int i = 0;
            string sql = "delete from xk.users where user_id = @id";
            MySqlParameter[] pars = { new MySqlParameter("@id", MySqlDbType.Int32) };
            pars[0].Value = id;
            try
            {
                i = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            }
            catch (MySqlException e)
            {
                i = -1;
            }
            return i;
        }

        //获取条件查询的总条数
        public int GetPageCount(Users user, int [] ages)
        {
            int i = 0;
            string sql = "select xk.users.user_id,xk.users.name,xk.users.sex,xk.users.age,xk.users.dep_id " +
                         " from xk.users  where 1=1 ";
            sql = QuerySum(sql,user, ages);
            //得到执行的条数，即条件查询的总数
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, null);
            return i = dt.Rows.Count;
           
        }
        //获取用户列表
        public List<Users> findAllUser(UserSearch userSearch, int[] ages)
        {
            Users user = userSearch;//子类到父类自动提升
            string sql = "select * from xk.users  where 1=1 ";
            sql = QuerySum(sql, user, ages);
            if (userSearch.page_number > 0 && userSearch.page_size > 0)
            {
                sql += "order by user_id LIMIT " + (userSearch.page_number - 1) * userSearch.page_size + "," + userSearch.page_size + " ";
            }
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, null);
            //创建一个List集合对象用于保存从数据库查询出来的结果集
            List<Users> userList = new UserDao().DataTableToList(dt);
            return userList;
        }
       

        public List<Users> findAllUserNoneQuery()
        {


            string sql = "select xk.users.user_id,xk.users.name,xk.users.sex,xk.users.age,xk.users.dep_id from xk.users";
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, null);
            //创建一个List集合对象用于保存从数据库查询出来的结果集
            List<Users> userList = new UserDao().DataTableToList(dt);
            return userList;
        }

        //将dt对象转换成List集合
        public List<Users> DataTableToList(DataTable dt)
        {
            List<Users> userList = new List<Users>();
            foreach (DataRow row in dt.Rows)
            {
                Users user = new Users();
                user.user_id = com.ToInt(row["user_id"]);
                user.name = com.ToString(row["name"]);
                user.dep_id = com.ToInt(row["dep_id"]);
                user.sex = com.ToString(row["sex"]);
                user.password = com.ToString(row["password"]);
                user.age = com.ToInt(row["age"]);
                userList.Add(user);
            }

            return userList;
        }
        //获取查询条件
        public string QuerySum(string sql,Users user,int [] ages)
        {
            if (user.name != null && !user.name.Equals(""))
            {
                sql += " and xk.users.name like '%" + user.name + "%'";
            }
            if (user.dep_id > 0)
            {
                sql += " and dep_id = " + user.dep_id + " ";
            }
            if (user.sex != null && !user.Equals(""))
            {
                sql += "  and sex like '%" + user.sex + "%'";
            }
            if (ages.Count() == 1)
            {

                sql += "  and age = " + ages[0] + " ";
            }
            else if (ages.Count() == 2)
            {
                sql += " and age between " + ages[0] + " and " + ages[1] + " ";
            }
            else {
                
            }
            return sql;
        }
        //获取datatable User集合
        //public DataTable GetAllUserDT(string name, int departID, string sex, int Minage, int Maxage)
        //{


        //    string sql = "select xk.users.user_id,xk.users.name,xk.users.sex,xk.users.age,xk.users.dep_id " +
        //                 "from xk.users where 1=1";
        //    if (name != null && !name.Equals(""))
        //    {
        //        sql += " and xk.users.name like '%" + name + "%'";
        //    }
        //    if (departID>0)
        //    {
        //        sql += " and dep_id = " + departID + " ";
        //    }
        //    if (sex != null && !sex.Equals(""))
        //    {
        //        sql += "  and sex like '%" + sex + "%'";
        //    }
        //    if (Minage<Maxage && Maxage>0)
        //    {
        //        sql += "  and age between " + Minage + " and " + Maxage + " ";
        //    }
        //    //CommandType.Text:sql语句
        //    DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, null);
        //    return dt;
        //}
    }
}
