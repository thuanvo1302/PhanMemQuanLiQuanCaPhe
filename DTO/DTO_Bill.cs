using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Bill
    {
        private int id;
        private DateTime checkin;
        private DateTime checkout;
        private int table_ID;
        private string status;
        private int discount;
        private int total;

        public DTO_Bill(int id, DateTime checkin, DateTime checkout, int table_ID, string status, int discount)
        {
            
            Id = id;
            Checkin = checkin;
            Checkout = checkout;
            Table_ID = table_ID;
            Status = status;
            this.discount = discount;
            
        }
        
        public DTO_Bill() { }

        public int Id { get => id; set => id = value; }

        public DateTime Checkin { get => checkin; set => checkin = value; }

        public DateTime Checkout { get => checkout; set => checkout = value; }

        public int Table_ID { get => table_ID; set => table_ID = value; }

        public string Status { get => status; set => status = value; }

        public int Discount { get => discount; set => discount = value; }

        public int Total { get => total; set => total = value; }
    }
}
