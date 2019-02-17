using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        private int type;
        private string userName;
        private string displayName;
        private string password;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Type { get => type; set => type = value; }
        public string Password { get => password; set => password = value; }

        public Account(string userName, string displayName, int type, string password = null)
        {
            this.userName = userName;
            this.displayName = displayName;
            this.type = type;
            this.password = password;
        }
        public Account(DataRow row)
        {
            this.userName = row["userName"].ToString();
            displayName = row["displayName"].ToString();
            type = (int)row["type"];
            password = row["password"].ToString();

        }
    }
}
