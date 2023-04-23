using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Drink
    {
        DAL_Drink drink= new DAL_Drink();

        public List<DTO_Drink> LoadDrinkByCategory(int IdCategory)
        {
            return drink.GetDrink(IdCategory);

        }

        public List<DTO_Drink> LoadAllDrink()
        {
            return drink.GetAllDrink();
        }

        public bool InsertDrink(string name, int category, float price)
        {
            return drink.InsertDrink(name, category, price);
        }

        public void DeleteDrink(int id)
        {
            drink.DeleteDrink(id);
        }

        public bool UpdateDrink(int id, string name, int category, float price)
        {
            return drink.UpdateDrink(id, name, category, price);
        }

        public List<DTO_Drink> GetDrinkID(int bill_id)
        {
            return drink.GetDrinkIDFromBillID(bill_id);
        }
    }
}
