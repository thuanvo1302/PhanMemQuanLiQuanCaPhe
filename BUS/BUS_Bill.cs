using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_Bill
    {
        DAL_Bill bill = new DAL_Bill();

        public DTO_Bill LoadBill(int table_id)
        {
            return bill.GetBill(table_id);
        }

        public List<DTO_Menu> LoadBillInfo(int table_id)
        {
            return bill.GetBillIn(table_id);
        }
        
        public void InsertBill(int table_id)
        {
            bill.InsertBill(table_id);
        }

        public void InsertBillInfo(int Bill_id,int Drink_id, int Count)
        {
            bill.InsertBillInfo(Bill_id, Drink_id, Count);
        }

        public int CheckIdBill(int table_id)
        {
            if (bill.GetBill(table_id).Id != 0)
            {
                return bill.GetBill(table_id).Id;
            }
            else
            {
                return -1;
            }
        }

        public bool CheckDrinkIdFromBillIn(int drink_id,int bill_id)
        {
            if (bill.GetDrinkIdFromBillIn(drink_id,bill_id))
            {
                return true;
            }
            return false;
        }

        public void UpdateBillInfo(int bill_id, int drink_id, int count)
        {
            bill.UpdateBillInfo(bill_id, drink_id, count);
        }

        public void UpdateStatusTable(int table_id, string status)
        {
            bill.UpdateStatusTable(table_id,status);
        }

        public void UpdateBill(int bill_id, float discount, float total)
        {
            bill.UpdateBill(bill_id, discount, total);
        }

        public List<DTO_Statistic> LoadAllBill(string from, string to)
        {
            return bill.GetAllBill(from,to);
        }

        public List<DTO_Statistic> GetDrinkNameAndCount(int bill_id)
        {
            return bill.GetDrinkNameAndCount(bill_id);
        }

        public bool DeleteBillInfo(int billinfo_id)
        {
            return bill.DeleteBillIn(billinfo_id);
        }

        public int GetCount(string from, string to)
        {
            return bill.GetCount(from, to);
        }

        public int GetIDBill(int table_id)
        {
            return bill.GetIDBill(table_id);
        }

        public bool DeleteBill(int bill_id)
        {
            return bill.DeleteBill(bill_id);
        }

        public bool Delete()
        {
            return bill.Delete();
        }
    }
}
