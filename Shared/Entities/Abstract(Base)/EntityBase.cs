using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities.Abstarct
{
    public abstract class EntityBase
    {
 
        public virtual int Id { get; set; }
        public virtual DateTime? CreatedDate { get; set; } = DateTime.Now; //override etmek için override CreatedDate = new DateTime(2020/01/2020) gibi yapabiliriz virtual yapmamızın sebebi override edilebilir olması için
        public virtual DateTime? ModifiedDate { get; set;} = DateTime.Now;
        public virtual bool? IsDeleted { get; set; } = false;
        public virtual string? UpdatedBy {get; set;}
    }
    
}
