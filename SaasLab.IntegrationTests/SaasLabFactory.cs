using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace SaasLab.IntegrationTests
{
    /// <summary>Sobe a aplicação inteira em memória, com a configuração que o teste controla.</summary>
    public sealed class SaasLabFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
            => builder.ConfigureAppConfiguration((_, config) =>
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Site:UrlBase"] = "https://exemplo.com.br",
                }));
    }
}
