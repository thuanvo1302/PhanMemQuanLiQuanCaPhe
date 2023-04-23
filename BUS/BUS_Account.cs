using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_Account
    {
        public DAL_Account account = new DAL_Account();
        
        public BUS_Account() { }

        public bool Login(string tk, string mk)
        {
            
            if (account.GetAccounts(tk, mk).MatKhau != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckAdmin(string tk, string mk)
        {
            
            if (account.GetAccounts(tk, mk).QuyenHan == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void InsertAccount(string tk, string mk)
        {
            account.InsertAccount(tk, mk);
        }

        public bool CheckExistsAccount(string tk)
        {
            if (account.CheckExistsAccount(tk))
            {
                return true;
            }
            return false;
        }

        public DTO_Account LoadAccount(string tk)
        {
            return account.GetAccount(tk);
        }

        public void UpdateAccount(string tk, string new_mk)
        {
            account.UpdateAccount(tk,new_mk);
        }

        public bool CheckLengthPassword(string password)
        {
            if (password.Length <= 8)
            {
                return false;
            }
            return true;
        }

        public bool CheckConfirmPassword(string password, string repassword)
        {
            if (password == repassword)
            {
                return true;
            }
            return false;
        }

        public List<DTO_Account> LoadAllAccount()
        {
            return account.GetAllAccount();
        }

        public bool DeleteAccount(string tk)
        {
            return account.DeleteAccount(tk);
        }

        public bool InsertAccountAdmin(string tk, string mk)
        {
            return account.InsertAccountAdmin(tk, mk);
        }
    }
}
