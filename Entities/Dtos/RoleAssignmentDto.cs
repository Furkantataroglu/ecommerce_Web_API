using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    /// <summary>
    /// Kullanıcıya rol atama işlemleri için DTO
    /// </summary>
    public class RoleAssignmentDto
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public string RoleName { get; set; }
    }

    /// <summary>
    /// Rol atama sonucu için DTO
    /// </summary>
    public class RoleAssignmentResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}
