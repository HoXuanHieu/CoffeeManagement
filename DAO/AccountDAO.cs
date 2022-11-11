using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() {}
      

        //Login with userName and password
        public bool Login(string userName, string passWord)
        {
            Account account = GetAccountByUserName(userName);
            if (account == null)
            {
                return false;
            }
            else
            {
                if (account.Password == passWord)
                {
                    return true;
                }
            }
            return false;
        }
        
        
        
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataAccess.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });

            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataAccess.Instance.ExecuteQuery("SELECT * FROM dbo.Account");
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataAccess.Instance.ExecuteQuery("Select * from account where userName = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool InsertAccount(string name, string displayName, int type, string pass)
        {
            string query = string.Format("INSERT dbo.Account ( UserName, DisplayName, Type, password )VALUES  ( N'{0}', N'{1}', {2}, N'{3}')", name, displayName, type, pass);
            int result = DataAccess.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, int type, string pass)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type, pass);
            int result = DataAccess.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string name)
        {
            string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataAccess.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPassword(string name, string pass)
        {
            string query = string.Format("update account set password = N'{0}' where UserName = N'{1}'",pass, name);
            int result = DataAccess.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
