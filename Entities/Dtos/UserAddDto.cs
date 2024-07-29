using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SADECE FRONT END KISMINDAN GELEN BİLGİLERLE USER OLUŞTURMAMIZI SAĞLAR MESELA CREATE DATE FALAN OTOMATİK GELSİN DİYE BU CLASSI KULLANIYORUZ
namespace Entities.Dtos
{
    public class UserAddDto
    {
        [Required(ErrorMessage = "Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(50)]
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
