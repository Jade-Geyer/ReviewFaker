using AmazonReviewFaker.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.\
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
