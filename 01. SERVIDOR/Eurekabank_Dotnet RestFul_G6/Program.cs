using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Eurekabank_Dotnet_RestFul_G6.Models;
using Eurekabank_Dotnet_RestFul_G6.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<EurekabankContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("CadenaSQL"), new MySqlServerVersion(new Version(8, 0, 27))));
builder.Services.AddScoped<CuentaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eurekabank API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eurekabank API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();