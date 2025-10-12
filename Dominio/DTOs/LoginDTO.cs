namespace SWAPI_Minimal.Dominio.DTOs;

public record LoginDTO(string Email = default!, string Senha = default!, string Perfil = default!);