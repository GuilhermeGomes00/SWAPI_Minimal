using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAPI_Minimal.Dominio.Entidades;

public class Personagem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = default!;
    
    [Required]
    [StringLength(150)]
    public string Altura { get; set; }  = default!;
    
    [Required]
    [StringLength(150)]
    public string CorCabelo { get; set; }  = default!;
    
    [Required]
    [StringLength(150)]
    public string CorPele { get; set; }  = default!;
    
    [Required]
    [StringLength(150)]
    public string CorOlhos { get; set; }  = default!; 
    
    [Required]
    [StringLength(150)]
    public string DataNascimento { get; set; }  = default!;
    
    [Required]
    [StringLength(150)]
    public string Genero { get; set; } = default!;
    
    [ForeignKey(nameof(Planeta))]
    public int PlanetaID { get; set; } = default!;
    
    
    public Planetas Planeta { get; set; } = default!;
    
    
    public List<Filme> Filmes { get; set; } = new();
    
}