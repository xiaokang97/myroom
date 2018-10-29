using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
   public class Users
    {
       public int user_id { set; get; }
       public string password { set; get; }
       public string name { set; get; }
       public int dep_id { set; get; }
       public string sex { set; get; }
       public int age { set; get; }
    }
    public class UserSearch : Users
    {
        public int page_number { set; get; }
        public int page_size { set; get; }
    }
}
