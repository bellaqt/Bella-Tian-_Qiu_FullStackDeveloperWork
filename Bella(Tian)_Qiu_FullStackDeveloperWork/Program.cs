using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://13.236.207.55:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<CarsStore>();
builder.Services.AddHostedService<RegistrationCheckService>();
builder.Services.AddSignalR();
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        context.Response.Headers.Add("Access-Control-Allow-Origin", "http://13.236.207.55:5173");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        await context.Response.CompleteAsync();
        return;
    }

    await next();
});

app.MapControllers();
app.MapHub<CarStatusHub>("/carStatusHub");

app.Run("http://0.0.0.0:5000");
