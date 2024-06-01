using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePlugin.Manager
{
    public class DebtManager
    {
        public static int interestRate = 0;
        public static int gilTarget = 0;
        public static DateTime endDate;
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

        public void generateKey(int interest, DateTime enddate, int gilamount)
        {
            string prelimKey = interest.ToString() + "|" + enddate.ToString() + "|" + gilamount.ToString();
        }
    }
}
