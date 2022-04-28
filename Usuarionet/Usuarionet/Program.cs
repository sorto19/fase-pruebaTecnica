var builder = WebApplication.CreateBuilder(args);


var misReglasCors = "ReglaCors";

builder.Services.AddCors(option =>
 option.AddPolicy(name: misReglasCors,
     builder =>
 {
     builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
 }

 )
    );

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
