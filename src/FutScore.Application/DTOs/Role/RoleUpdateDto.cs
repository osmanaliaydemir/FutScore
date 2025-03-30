using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.Role
{
    public class RoleUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rol adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Rol adı en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string Description { get; set; }
    }
} 