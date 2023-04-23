using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace BUS
{
    public class BUS_Category
    {
        DAL_Category category = new DAL_Category();

        public BUS_Category() { }

        public List<DTO_Category> LoadCategory()
        {
            return category.GetCategory();
        }

        public int GetIDCategoryByName(string name)
        {
            return category.GetIDCategoryByName(name);
        }

        public string GetNameCategoryByID(int id)
        {
            return category.GetNameCategoryByID(id);
        }

        public bool InsertCategory(string name)
        {
            return category.InsertCategory(name);
        }

        public void DeleteCaterogy(int id)
        {
            category.DeleteCategory(id);
        }

        public bool UpdateDrink(int id, string name)
        {
            return category.UpdateCategory(id, name);
        }
    }
}
