using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Domain;

namespace BussinesLayer
{
    public class ProductBL
    {
        public List<ProductDOM> GetProducts()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            return Mapper.convertToList(dc.Products.ToList<Product>());
        }
    }
}
