using EfcRepo;
using FileMemoryRepo;
using RepositoryContracts;
using AppContext = EfcRepo.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepo, EfcUserRepo>();
builder.Services.AddScoped<ICommentRepo, EfcCommentRepo>();
builder.Services.AddScoped<IPostRepo, EfcPostRepo>();
builder.Services.AddDbContext<AppContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();