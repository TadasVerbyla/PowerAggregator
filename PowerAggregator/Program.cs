using Microsoft.EntityFrameworkCore;
using PowerAggregator.Data;
using PowerAggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PowerAggregationContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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

var aggregationService = new AggregationService();
aggregationService.ProcessStatisticUrl("https://data.gov.lt/media/filer_public/b2/3d/b23d5d9d-7f07-49a5-9ad8-8ec8917cdf82/2024-10.csv");
aggregationService.ProcessStatisticUrl("https://data.gov.lt/media/filer_public/be/39/be390ff0-8972-474e-a044-9f6f6f5f589a/2024-09.csv");

app.Run();
