using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Description of the product
        public string Description { get; set; }

        // Price of the product
        public decimal Price { get; set; }

        // Available stock quantity of the product
        public int StockQuantity { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
