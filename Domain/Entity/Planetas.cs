using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAPI_Minimal.Dominio.Entidades;

public class Planetas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = default!;

    [Required]
    [StringLength(150)]
    public string Terreno { get; set; } =  default!;
    
    [Required]
    [StringLength(150)]
    public string Clima { get; set; }  = default!;
    
    public List<Personagem> Personagens { get; set; } = new();
}