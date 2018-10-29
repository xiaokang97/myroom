using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.BLL
{
    class UserBLL
    {
        UserDao userDao = new UserDao();
        //验证登录用户
        public Users ValidLogin(int username, string password)
        {
           
            Users user = userDao.login(username, password);
            return user;
        }
        //添加用户
        public int AddUser(Users user)
        {
            int res;
            return res = userDao.addUser(user);
        }
        //删除用户
        public int DeleteUser(int userID)
        {
            int res;
            return res = userDao.deleteUser(userID);
        }
        public int GetPageCount(Users user,int[] ages)
        {
            int res;
            return res = userDao.GetPageCount(user,ages);
        }
        public List<Users> GetAllUser(UserSearch userSearch,int[] ages)
        {
            List<Users> userList = new List<Users>();
           return userList = new UserDao().findAllUser(userSearch,ages);
        }
        public List<Users> GetAllUserNoQuery()
        {
            List<Users> userList = new List<Users>();
            return userList = new UserDao().findAllUserNoneQuery();
        }
        
        //表格中更新用户信息
        public int UpdateUser(Users user)
        {
            int res = 0;
            return res = userDao.UpdateUser(user);
        }
    }
}
