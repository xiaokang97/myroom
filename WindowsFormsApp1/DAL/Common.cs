using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAL
{
    public class Common
    {
        //字符类型转换
        public int ToInt(object obj)
        {
            int res = 0;
            if (obj == null && obj is DBNull)
            {
                return res;
            }
            else
            {
                if (Int32.TryParse(obj.ToString(), out res))
                {
                    return res;
                }
            }
            return res;
        }
        public string ToString(object obj)
        {
            string res = "null";
            if (obj == null && obj is DBNull)
            {
                return res;
            }
            else
                return res = obj.ToString();
        }
    }
}
