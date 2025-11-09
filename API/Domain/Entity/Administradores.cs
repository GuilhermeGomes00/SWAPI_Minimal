using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAPI_Minimal.Dominio.Entidades;

public class Administradores
{
    [Key]
    public Guid Id { get; init; }
    
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = default!;
    
    [Required]
    [StringLength(100)]
    public string Senha { get; set; } = default!;
    
    [Required]
    [StringLength(20)]
    public string Perfil { get; set; } = default!;

    public Administradores()
    {
        
    }
    public Administradores(string email, string senha, string perfil)
    {
        Id = Guid.NewGuid();
        Email = email;
        Senha = senha;
        Perfil = perfil;
    }
    
}