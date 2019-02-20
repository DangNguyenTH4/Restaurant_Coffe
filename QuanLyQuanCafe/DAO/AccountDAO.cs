using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

        string cryptoPassword(string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            hasPass.Reverse();
            return hasPass;
        }
        public string CryptoPassword(string password)
        {
            return cryptoPassword(password);
        }
        public bool Login(string username,string password)
        {
            string hasPass = CryptoPassword(password);
            string query = "USP_Login @username , @password";
            int result = 0;
            DataTable data;
            try
            {
                data = DataProvider.Instance.ExecuteQuery(query,new object[] { username, hasPass });
            }
            catch
            {
                return false;
            }
            result = data.Rows.Count;
            return result>0;
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
        public DataTable GetListAccount()
        {
            string query = "Select UserName, DisplayName, Type from dbo.Account";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string pass = cryptoPassword("1");
            string query = string.Format("insert dbo.Account (username, displayName, type, password) values(N'{0}','{1}',{2}, N'{3}')", name, displayName, type, pass);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateAccount(string name, string displayName, int type)
        {
            string query = string.Format("update dbo.Account set  displayName = '{1}', type ={2} where username = '{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            string query = string.Format("delete dbo.Account where username = '{0}'",name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool ResetPassword(string userName)
        {
            string pass = cryptoPassword("1");
            string query = string.Format("update dbo.Account set password = '{0}'", pass);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

