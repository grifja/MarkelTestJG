using WebApi.Repositories;
using WebApi.Services;
using WebApi.Services.Claim;
using WebApi.Services.Claim.Models;
using WebApi.Services.Company;
using WebApi.Services.Company.Models;
using WebApi.ViewModels;
using WebApi.ViewModels.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<CompanyDbContext>();
builder.Services.AddSingleton<ClaimDbContext>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IResponseBuilder, ResponseBuilder>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IMapper<CompanyViewModel, CompanyModel>, CompanyMapper>();
builder.Services.AddScoped<IMapper<ClaimViewModel, ClaimModel>, ClaimMapper>();
builder.Services.AddScoped<IMapper<UpdateClaimViewModel, ClaimModel>, UpdateClaimMapper>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
