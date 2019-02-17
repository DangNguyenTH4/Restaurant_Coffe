using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static  AccountDAO instance;
        private AccountDAO() { }
        public static  AccountDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountDAO();
                return instance;
            }

            set => instance = value;
        }
        public bool Login(string username,string password)
        {
            string query = "USP_Login @username , @password";
            int result = 0;
            DataTable data;
            try
            {
                data = DataProvider.Instance.ExecuteQuery(query,new object[] { username, password });
            }
            catch
            {
                return false;
            }
            result = data.Rows.Count;
            return result>0;

            //object data1;
            //try
            //{
            //    data1 = DataProvider.Instance.ExecuteScala(query);
            //}
            //catch
            //{
            //    return false;
            //}
            //return data1 != null;
        }
        public bool UpdateAccount(string username, string displayname, string pass, string newpass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { username, displayname, pass, newpass });
            return result > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from account where userName = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
    }
}
