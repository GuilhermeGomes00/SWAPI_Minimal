using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Infra.DB;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Testes.Endpoints;

[TestClass]
public class AutenticacaoEndpointsTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;
    private SqliteConnection _connection = null!;

    private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    [TestInitialize]
    public void TestInitialize()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<DbContexto>));
                    
                    if (dbContextDescriptor != null)
                    {
                        services.Remove(dbContextDescriptor);
                    }

                    services.AddDbContext<DbContexto>(options =>
                    {
                        options.UseSqlite(_connection);
                    });
                });
            });

        _client = _factory.CreateClient();

        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var _ctx = scopedServices.GetRequiredService<DbContexto>();
            _ctx.Database.EnsureCreated();
        }
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _client.Dispose();
        _factory.Dispose();
        _connection.Dispose();
    }

    [TestMethod]
    public async Task PostLogin_DeveRetornarToken()
    {
        var loginDto = new LoginDTO
        {
            Email = "admteste@teste.com",
            Senha = "123456"
        };
        
        var jsonBody = JsonSerializer.Serialize(loginDto);
        var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/Login", httpContent);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        var responseBody = await response.Content.ReadAsStringAsync();
        var loginResult = JsonSerializer.Deserialize<LoginResponse>(responseBody, _jsonOptions);

        Assert.IsNotNull(loginResult);
        Assert.IsNotNull(loginResult.Token);
        Assert.IsFalse(string.IsNullOrEmpty(loginResult.Token));
        Assert.AreEqual("Adm", loginResult.Perfil);
    }
}

file record LoginResponse(string Email, string Perfil, string Token);