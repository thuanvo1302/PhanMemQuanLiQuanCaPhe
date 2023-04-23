using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Drink
    {
        private int iD;
        private string drinkName;
        private int categoryID;
        private float price;

        public DTO_Drink() { }

        public int ID { get => iD; set => iD = value; }

        public string DrinkName { get => drinkName; set => drinkName = value; }

        public int CategoryID { get => categoryID; set => categoryID = value; }

        public float Price { get => price; set => price = value; }
    }
}
