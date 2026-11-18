using Microsoft.Extensions.Options;
using SaasLab.Web.Components;
using SaasLab.Web.Seo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

// Options pattern, validado na partida: se a URL estiver errada, o app nem sobe (fail-fast).
builder.Services.AddOptions<SiteOptions>()
    .Bind(builder.Configuration.GetSection(SiteOptions.Secao))
    .Validate(o => Uri.TryCreate(o.UrlBase, UriKind.Absolute, out _),
              "Site:UrlBase deve ser uma URL absoluta, por exemplo https://www.seudominio.com.br")
    .ValidateOnStart();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>();

app.MapGet("/robots.txt", (IOptions<SiteOptions> site) =>
    Results.Text(SeoArquivos.Robots(site.Value), "text/plain; charset=utf-8"));

app.MapGet("/sitemap.xml", (IOptions<SiteOptions> site) =>
    Results.Text(SeoArquivos.Sitemap(site.Value, SeoArquivos.PaginasPublicas),
                 "application/xml; charset=utf-8"));

app.Run();

// Torna a classe Program visível para o WebApplicationFactory nos testes de integração.
public partial class Program;
