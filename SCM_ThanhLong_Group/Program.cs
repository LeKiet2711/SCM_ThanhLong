﻿using Microsoft.Extensions.Configuration;
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
using BlazorDownloadFile;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Repository;


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
builder.Services.AddScoped<OracleFgaService>();

builder.Services.AddScoped<ICheckEmail, CheckEmailRepository>();
builder.Services.AddScoped<checkEmail>();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazorDownloadFile();
builder.Services.AddScoped<OracleAuditService>();
builder.Services.AddSingleton<ConnectionStringManager>();
builder.Services.AddScoped<OracleDbConnection>();
builder.Services.AddControllers();

builder.Services.AddScoped<IKeyValidationRepository, KeyValidationRepository>();
builder.Services.AddScoped<KeyValidationService>();
builder.Services.AddScoped<VerificationService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<Users_Service>();
builder.Services.AddScoped<Users>();

builder.Services.AddScoped<IChucNangRepository, ChucNangRepository>();
builder.Services.AddScoped<ChucNang_Service>();

builder.Services.AddScoped<IKhuVucTrongRepository, KVTRepository>();
builder.Services.AddScoped<KhuVucTrong_Service>();

builder.Services.AddScoped<IHoTrongRepository, HoTrongRepository>();
builder.Services.AddScoped<HoTrong_Service>();

builder.Services.AddScoped<IKhoRepository, KhoRepository>();
builder.Services.AddScoped<Kho_Service>();

builder.Services.AddScoped<IKhoUserRepository, KhoUserRepository>();
builder.Services.AddScoped<KhoUser_Service>();

builder.Services.AddScoped<IPhieuNhapRepository, PhieuNhapRepository>();
builder.Services.AddScoped<PhieuNhap_Service>();

builder.Services.AddScoped<IPhieuXuatRepository, PhieuXuatRepository>();
builder.Services.AddScoped<PhieuXuat_Service>();

builder.Services.AddScoped<ICTPNRepository, CTPNRepository>();
builder.Services.AddScoped<ChiTietPhieuNhap_Service>();

builder.Services.AddScoped<ICTPXRepository, CTPXRepository>();
builder.Services.AddScoped<ChiTietPhieuXuat_Service>();

builder.Services.AddScoped<ITonKhoRepository, TonKhoRepository>();
builder.Services.AddScoped<TonKho_Service>();
builder.Services.AddScoped<Profile_Service>();

builder.Services.AddScoped<ILoThanhLongRepository, LoThanhLongRepository>();
builder.Services.AddScoped<LoThanhLong_Service>();

builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<Audit_Service>();

builder.Services.AddScoped<IDBA_UserRepository, DBA_UserRepository>();
builder.Services.AddScoped<DBA_UserService>();

builder.Services.AddScoped<INhomQuyenRepository, NhomQuyenRepository>();
builder.Services.AddScoped<NhomQuyen_Service>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddTelerikBlazor();

var app = builder.Build();
app.UseRouting();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	// ... 
});
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
