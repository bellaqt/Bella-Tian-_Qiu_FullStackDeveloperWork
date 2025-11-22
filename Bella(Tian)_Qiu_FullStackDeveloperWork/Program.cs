using Bella_Tian__Qiu_FullStackDeveloperWork.Services;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

// 控制器
builder.Services.AddControllers();

// ✅ 全局允许 CORS（彻底放开）
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true);
    });
});

// 注册服务
builder.Services.AddSingleton<CarsStore>();
builder.Services.AddHostedService<RegistrationCheckService>();
builder.Services.AddSignalR();
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();

// ======== 管道顺序非常重要 =========
app.UseRouting();

// ✅ 启用 CORS（必须在 Authorization 之前）
app.UseCors();

// ✅ 可选：授权中间件
app.UseAuthorization();

// ✅ 强制添加 CORS 响应头（确保跨域在 EC2 上生效）
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
    await next();
});

// ✅ 如果打包了 React 静态文件，可自动托管
app.UseStaticFiles();

// ✅ 映射 API 和 SignalR Hub
app.MapControllers();
app.MapHub<CarStatusHub>("/carStatusHub");

// ✅ 明确监听公网端口
app.Run("http://0.0.0.0:5000");
