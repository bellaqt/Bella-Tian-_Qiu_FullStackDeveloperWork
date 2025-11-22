using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://13.236.207.55:5173", "http://localhost:5173")
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
app.UseRouting();
app.UseCors("AllowFrontend"); 
// app.UseHttpsRedirection();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CarStatusHub>("/carStatusHub");
    endpoints.MapMethods("{*path}", new[] { "OPTIONS" }, context =>
    {
        context.Response.StatusCode = 200;
        context.Response.Headers.Add("Access-Control-Allow-Origin", "http://13.236.207.55:5173");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
        return Task.CompletedTask;
    });
});

app.Run("http://0.0.0.0:5000");
