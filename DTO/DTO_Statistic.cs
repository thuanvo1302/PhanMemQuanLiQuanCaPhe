using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Statistic
    {
        private int id;
        private DateTime date_checkin;
        private DateTime date_checkout;
        private string drinkName;
        private int count;
        private int discount;
        private int total;
        
        public DTO_Statistic(DateTime date_checkin, DateTime date_checkout, int discount, int total, int id)
        {
            this.Date_checkin = date_checkin;
            this.Date_checkout = date_checkout;
            this.Discount = discount;
            this.Total = total;
            this.Id = id;
            
        }
        public DTO_Statistic(string name, int count) 
        {
            this.DrinkName = name;
            this.count = count;
        }  
        
        public DateTime Date_checkin { get => date_checkin; set => date_checkin = value; }

        public DateTime Date_checkout { get => date_checkout; set => date_checkout = value; }
        
        public int Discount { get => discount; set => discount = value; }

        public int Total { get => total; set => total = value; }

        public int Id { get => id; set => id = value; }
        
        public int Count { get => count; set => count = value; }

        public string DrinkName { get => drinkName; set => drinkName = value; }
    }
}
