using APIBooksCRUD.Automappers;
using APIBooksCRUD.DTOs;
using APIBooksCRUD.Models;
using APIBooksCRUD.Repository;
using APIBooksCRUD.Services;
using APIBooksCRUD.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedScoped<ICommonService<GenreDto, GenreInsertDto, GenreUpdateDto>, GenreService>("genreService");
builder.Services.AddKeyedScoped<ICommonService<BookDto, BookInsertDto, BookUpdateDto>, BookService>("bookService");

//Repository
builder.Services.AddScoped<IRepository<Genre>, GenreRepository>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();

//EntityFramework
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));

//Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Validators
builder.Services.AddScoped<IValidator<GenreInsertDto>, GenreInsertValidator>();
builder.Services.AddScoped<IValidator<GenreUpdateDto>, GenreUpdateValidator>();
builder.Services.AddScoped<IValidator<BookInsertDto>, BookInsertValidator>();
builder.Services.AddScoped<IValidator<BookUpdateDto>, BookUpdateValidator>();

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
