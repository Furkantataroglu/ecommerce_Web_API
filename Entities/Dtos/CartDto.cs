using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    //CartDto: Sepetin genel bilgilerini ve içerisindeki öğeleri temsil eder.
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; }
    }
}
