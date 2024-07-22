using Shared.Entities.Abstarct;
using Shared.Entities.Abstarct_Base_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User:EntityBase<int>,IEntity
    {
        //override örneği
        // public override DateTime CreatedDate { get => base.CreatedDate; set => base.CreatedDate = value; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
