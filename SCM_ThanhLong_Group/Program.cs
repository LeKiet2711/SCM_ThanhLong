using Microsoft.Extensions.Configuration;
using System.IO;
using Blazored.SessionStorage;
using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SCM_ThanhLong_Group.Components;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Service;
using Telerik.Reporting.Services;
using Telerik.Reporting.Cache.File;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp => new ReportServiceConfiguration
{
    ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
    HostAppId = "SCM_ThanhLong_Group",
    Storage = new FileStorage(),
    ReportSourceResolver = new UriReportSourceResolver(
        System.IO.Path.Combine(GetReportsDir(sp)))
});
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<ConnectionStringManager>();
builder.Services.AddScoped<OracleDbConnection>();
builder.Services.AddControllers();

builder.Services.AddScoped<Users_Service>();
builder.Services.AddScoped<Users>();
builder.Services.AddScoped<ChucNang_Service>();
builder.Services.AddScoped<KhuVucTrong_Service>();
builder.Services.AddScoped<HoTrong_Service>();
builder.Services.AddScoped<Kho_Service>();
builder.Services.AddScoped<PhieuNhap_Service>();
builder.Services.AddScoped<PhieuXuat_Service>();
builder.Services.AddScoped<ChiTietPhieuNhap_Service>();
builder.Services.AddScoped<ChiTietPhieuXuat_Service>();
builder.Services.AddScoped<Profile_Service>();
builder.Services.AddScoped<LoThanhLong_Service>();
builder.Services.AddScoped<XacThucService>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddTelerikBlazor();

var app = builder.Build();
app.UseRouting();
app.UseAntiforgery();

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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers(); // Use top-level route registration

app.Run();

static string GetReportsDir(IServiceProvider sp)
{
    var env = sp.GetService<IWebHostEnvironment>();
    if (env == null)
    {
        throw new InvalidOperationException("IWebHostEnvironment is not available.");
    }
    return Path.Combine(env.ContentRootPath, "Reports");
}
