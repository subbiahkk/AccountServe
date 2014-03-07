using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountsDAL;
using AccountsDAL.Common.cs;
namespace AccountsBAL.Common
{
    public static class Common
    {
        public static void SetConnectionString(string connString)
        {
            Constants.ConnString = connString;
        }
    }
}
