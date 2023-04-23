using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Menu
    {
        public DTO_Menu(string name,int count,float price, float totalprice ) 
        {
            this.name = name;
            this.count = count;
            this.price = price;
            this.totalPrice= totalprice;
        }

        private int count;
        private string name;
        private float price;
        private float totalPrice;

        public string Name { get => name; set => name = value; }

        public int Count { get => count; set => count = value; }

        public float Price { get => price; set => price = value; }

        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
