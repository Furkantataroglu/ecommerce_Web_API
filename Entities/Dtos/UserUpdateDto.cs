using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(50)]
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
