namespace SWAPI_Minimal.Controllers;

public static class checagem
{
    public static void Login(this WebApplication app)
    {
        var loginEndPoint = app.MapGroup("Login");
        
        app.MapGet("/", () =>
        {
            var xD = "Tenta o /Login ai pls";
            return Results.Ok(xD);
        });

        

        

    } 
}