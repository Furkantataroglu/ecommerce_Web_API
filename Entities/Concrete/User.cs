using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Entities.Abstarct;
using Shared.Entities.Abstract_Base_;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User: IdentityUser<Guid>, IEntity
    {
        public override string? UserName { get; set; } // Make UserName nullable for identity
        //override örneği
        // public override DateTime CreatedDate { get => base.CreatedDate; set => base.CreatedDate = value; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
 

        //FK İçin lazım
        //public Guid? RoleId { get; set; }
        // public Role? Role { get; set; }

        public virtual DateTime? CreatedDate { get; set; } = DateTime.Now; //override etmek için override CreatedDate = new DateTime(2020/01/2020) gibi yapabiliriz virtual yapmamızın sebebi override edilebilir olması için
        public virtual DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool? IsDeleted { get; set; } = false;
        public virtual string? UpdatedBy { get; set; }
    }
}
