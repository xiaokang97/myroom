using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAL
{
    class SplitString
    {

        public int [] GetAge(string s)
        {

           // string s = "1.2 - 5.5";

            Regex r = new Regex(" \\d + (\\.\\d+) ? ");
            int[] ageArr = { };
            MatchCollection matches = r.Matches(s);

            for (int i = 0; i < matches.Count; i++)

            {

                if (matches[i].Success)

                {

                 ageArr[i] =Int32.Parse(matches[i].Groups[0].Value);

                }

            }

            return ageArr;

        }

    }

}
    

