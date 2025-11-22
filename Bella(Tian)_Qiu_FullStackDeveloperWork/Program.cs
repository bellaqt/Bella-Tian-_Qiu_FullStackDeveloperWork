using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Amazon.S3;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<CarsStore>();
builder.Services.AddHostedService<RegistrationCheckService>();
builder.Services.AddSignalR();
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();

// ✅ 放在最前
app.UseCors();

app.UseRouting();
app.UseAuthorization();

// ✅ 额外中间件强制返回 CORS 响应头
app.Use(async (context, next) =>
{
    context.Response.Headers["Access-Control-Allow-Origin"] = "*";
    context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
    context.Response.Headers["Access-Control-Allow-Headers"] = "Origin, X-Requested-With, Content-Type, Accept, Authorization";
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});

app.MapControllers();
app.MapHub<CarStatusHub>("/carStatusHub");

app.Run("http://0.0.0.0:5000");
