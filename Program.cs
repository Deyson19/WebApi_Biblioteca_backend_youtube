using Microsoft.EntityFrameworkCore;
using WebApp_SistemaBiblioteca.DataAccess;
using WebApp_SistemaBiblioteca.Services.Contrato;
using WebApp_SistemaBiblioteca.Services.Implementacion;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SQLServer");
// Add services to the container.

//*Agregar SQL Server para el DbContext
builder.Services.AddDbContext<ApplicationDbContext>(op =>
{
    op.UseSqlServer(connectionString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//*My Services
builder.Services.AddScoped<ITipoUsuarioService,TipoUsuarioService>();

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
