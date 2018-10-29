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
    class DepartBLL
    {
        public List<Depart> GetALLDepart()
        {
            List<Depart> departlist = new DepartDao().findAll();
            return departlist;
        }
        public DataTable GetAllDepartTable()
        {
            DataTable dt = new DepartDao().findAllDepTable();
            return dt;
        }
    }
}
