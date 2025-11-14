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
    public DateTime Ano { get; set; } = default!;
    public List<Personagem> Personagens { get; set; } = new();

    public Filme() { }
    public Filme(string titulo, string tipo, DateTime ano)
    {
        Titulo = titulo;
        Tipo = tipo;
        Ano = ano;
    }
}