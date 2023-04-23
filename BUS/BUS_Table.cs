using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_Table
    {
        private DAL_Table table;
        
        public BUS_Table() { }

        public List<DTO_Table> LoadTable()
        {
            table= new DAL_Table();
            return table.GetTable();
        }

        public void AutoTable(int count)
        {
            table.AutoTable(count);
        }

        public bool InsertTable()
        {
            return table.InsertTable();
        }

        public bool DeleteTable(int id)
        {
            return table.DeleteTable(id);
        }
        
    }
}
