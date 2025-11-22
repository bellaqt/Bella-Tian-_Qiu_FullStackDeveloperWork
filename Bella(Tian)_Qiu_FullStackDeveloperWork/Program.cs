using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CarStatusHub>("/carStatusHub");

app.Run("http://0.0.0.0:5000");
