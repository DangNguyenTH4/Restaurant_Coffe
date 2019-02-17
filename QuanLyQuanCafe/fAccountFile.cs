using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountFile : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get
            {
                return loginAccount;
            }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }
        public fAccountFile(Account loginAccount)
        {
            InitializeComponent();
            LoginAccount = loginAccount;
        }

        #region method
        void ChangeAccount(Account loginAccount)
        {
            txtUsername.Text = LoginAccount.UserName;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }
        void UpdateAccount()
        {
            string username = txtUsername.Text;
            string displayName = txtDisplayName.Text;
            string password = txtPassWord.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtReEnterNewPass.Text;
            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Mật khẩu không trùng khớp.");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(username, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành không.");
                    if (updateAccountt != null)
                    {
                        updateAccountt(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(username)));
                    }
                }
                else MessageBox.Show("Nhập sai mật khẩu.");
            }

        }
        #endregion

        #region event

        private event EventHandler<AccountEvent> updateAccountt;
        public event EventHandler<AccountEvent> UpdateAccountt
        {
            add { updateAccountt += value; }
            remove { updateAccountt -= value; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
        #endregion

        private void fAccountFile_Load(object sender, EventArgs e)
        {

        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc { get => acc; set => acc = value; }

        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
