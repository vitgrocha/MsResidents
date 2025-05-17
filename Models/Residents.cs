using System;
using System.ComponentModel.DataAnnotations;

namespace MSResidents.Models
{
    public class Resident
    {
        [Key]
        public int ResidentId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O número do apartamento é obrigatório.")]
        [Range(1, 9999, ErrorMessage = "O número do apartamento deve estar entre 1 e 9999.")]
        public required int ApartmentNumber { get; set; }

        [Required(ErrorMessage = "O número do bloco é obrigatório.")]
        [Range(1, 9999, ErrorMessage = "O número do bloco deve estar entre 1 e 9999.")]
        public required int BlockNumber { get; set; }

        [Required(ErrorMessage = "A data de entrada é obrigatória.")]
        public DateTime MoveInDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
