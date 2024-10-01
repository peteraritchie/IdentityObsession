using Microsoft.EntityFrameworkCore;

using Pri.IdentityObsession.Domain;
using Pri.IdentityObsession.Web.Components;
using Pri.IdentityObsession.Web.Infrastructure;
using Pri.IdentityObsession.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<ISsnRegistry, SsnRegistry>();
//builder.Services.AddSingleton<ClientService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddDbContext<DatabaseContext>();
#if true
//builder.Services.AddDbContextFactory<DatabaseContext>();
#else
// figure out why this doesn't initialize the provider.
builder.Services.AddDbContextFactory<DatabaseContext>(opt =>
	opt.UseSqlite($"Data Source={DatabaseContext.DataSourcePath}"));
#endif
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
public partial class Program { }
