using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Report
    {
        DAL_Report report = new DAL_Report();

        public BUS_Report() { }

        public List<DTO_Report> GetReport(int bill_id)
        {
            return report.Report(bill_id);
        }

        public int GetTotal(int bill_id)
        {
            return report.GetTotal(bill_id);
        }
        
        public int GetDiscount(int bill_id)
        {
            return report.GetDiscount(bill_id);
        }
        
        public string GetTableName(int bill_id)
        {
            return report.GetTableName(bill_id);
        }
    }
}
