using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_BillInfo
    {
        DAL_BillInfo bi = new DAL_BillInfo();

        public List<DTO_BillInfo> LoadBillInfo(int bill_id)
        {
            return bi.GetBillInfo(bill_id);
        }

        public bool DeleteBillIn(int bill_id)
        {
            return bi.DeleteBillInfo(bill_id);
        }
    }
}
