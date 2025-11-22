using Amazon.S3;
using Bella_Tian__Qiu_FullStackDeveloperWork.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "http://13.236.207.55:5173",
            "http://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddSingleton<CarsStore>();
builder.Services.AddHostedService<RegistrationCheckService>();
builder.Services.AddSignalR();
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();

app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CarStatusHub>("/carStatusHub");
app.MapMethods("{*path}", new[] { "OPTIONS" }, (context) =>
{
    context.Response.StatusCode = 200;
    return Task.CompletedTask;
});

app.Run("http://0.0.0.0:5000");
