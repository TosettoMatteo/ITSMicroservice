using BookService.Data;
using BookService.Repository;
using BookServiceITS.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBooksContext, BookContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
// Add services to the container.

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
