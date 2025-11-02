using SWAPI_Minimal.Dominio.ENUMs;

namespace SWAPI_Minimal.Dominio.DTOs;

public record AdminDTOs(string Email, string Senha, Perfil? Perfil);