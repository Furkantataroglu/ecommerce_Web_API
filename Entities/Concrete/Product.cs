using Microsoft.AspNetCore.Mvc.ViewEngines;
using Shared.Entities.Abstarct;
using Shared.Entities.Abstract_Base_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product:EntityBase, IEntity
    {
        public string Name { get; set; }

        // Description of the product
        public string Description { get; set; }

        // Price of the product
        public decimal? Price { get; set; }

        // Available stock quantity of the product
        public int? StockQuantity { get; set; }
        public List<string>? ImageUrls { get; set; }

        // Average customer rating of the product
        public double? AverageRating { get; set; }
        //public List<Review> Reviews { get; set; }
        public bool IsActive { get; set; }
        public Product()
        {
            ImageUrls = new List<string>();
            //Reviews = new List<Review>();
            IsActive = true;
        }
    }
}
