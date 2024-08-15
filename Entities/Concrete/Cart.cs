using Shared.Entities.Abstarct;
using Shared.Entities.Abstract_Base_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cart : EntityBase , IEntity
    {
        public Guid UserId { get; set; }
        public string Status { get; set; } = "Active";
        public decimal TotalPrice { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
