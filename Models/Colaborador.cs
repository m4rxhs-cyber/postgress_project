using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
 
namespace conexaoPostgre.Models
{
    public partial class Colaborador
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public float Salario { get; set; }
        [ForeignKey("Cargo")]
        [Required]
        public int IdCargo { get; set; }
        public Cargo? Cargo { get; set; }
    }
}