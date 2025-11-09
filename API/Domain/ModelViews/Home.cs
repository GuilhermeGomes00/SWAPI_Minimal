namespace SWAPI_Minimal.Dominio.ModelViews;

public struct Home
{
    public string Intro {
        get => "Bem-Vindo a minha SWAPI!";
    }

    public string Instrucao {
        get => "O Login funciona ao usar o Email 'admteste@teste.com' com a senha '123456' ";
    }
    
    public string Doc {
        get => "Acesse o swagger abaixo:";
    }
    
    public string API { get => "/swagger"; }
}