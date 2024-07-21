using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePlugin.Manager
{
    public class DebtManager
    {
        public static int interestRate = Math.Clamp(0, 0, 999);
        public static DateTime endDate;
        public static int gilTarget = 0;
        public void incrementInterest(int i)
        {
            interestRate += i;
        }

        public bool dateValid(string dateinput)
        {
            DateTime temp;
            if (DateTime.TryParse(dateinput, out temp))
            {
                return true;
            }
            return false;
        }

        public static string generateKey(int interest, DateTime enddate, int gilamount)
        {
            var prelimKey = System.Text.Encoding.UTF8.GetBytes(interest.ToString() + "|" + enddate.ToString() + "|" + gilamount.ToString());
            return System.Convert.ToBase64String(prelimKey);
        }

        public static string decodeKey(string key)
        {
            var encodedKey = System.Convert.FromBase64String(key);
            return System.Text.Encoding.UTF8.GetString(encodedKey);
        }

        public static void setKeyValues(string key)
        {
            foreach (char c in key) 
            {

            }
        }
    }
}
