using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAPI_Minimal.Dominio.Entidades;

public class Filme
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Titulo { get; set; } = default!;
    
    [Required]
    [StringLength(150)]
    public string Tipo { get; set; } = default!;
    
    [Required]
    [StringLength(150)]
    public string Ano { get; set; } = default!;
    public List<Personagem> Personagens { get; set; } = new();
}