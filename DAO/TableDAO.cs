using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 90;
        public static int TableHeight = 90;

        private TableDAO() { }
        public void setStatus(int id)
        {
            DataAccess.Instance.ExecuteNonQuery("UPDATE dbo.TableFood SET status = N'Có Khách' WHERE id = " + id);
        }
        public void setEmpty(int id)
        {
            DataAccess.Instance.ExecuteNonQuery("UPDATE dbo.TableFood SET status = N'Trống' WHERE id = " + id);
        }
        public void SwitchTable(int id1, int id2)
        {
            DataAccess.Instance.ExecuteQuery("USP_SwitchTabel @idTable1 , @idTabel2", new object[]{id1, id2});
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataAccess.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
    }
}
