using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
 
namespace conexaoPostgre.Models
{
    public partial class Cargo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public float SalarioMinimo { get; set; }
        public float SalarioMaximo { get; set; }
        public bool? Excluido { get; set; }
 
        public IEnumerable<Colaborador>? Colaboradores { get; set; }
    }
}