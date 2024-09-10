using Blazored.SessionStorage;
using Blazored.Toast;
using SCM_ThanhLong_Group.Components;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Service;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

string oracleConnectionString = "User Id=C##ADMIN;Password=oracle;Data Source=localhost:1521/orcl1;";
builder.Services.AddScoped<OracleDbConnection>(sp => new OracleDbConnection(oracleConnectionString));

builder.Services.AddScoped<Users_Service>();
builder.Services.AddScoped<ChucNang_Service>();
builder.Services.AddScoped<KhuVucTrong_Service>();
builder.Services.AddScoped<NhaPhanPhoi_Service>();
builder.Services.AddScoped<HoTrong_Service>();
builder.Services.AddScoped<Kho_Service>();


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddTelerikBlazor();

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
