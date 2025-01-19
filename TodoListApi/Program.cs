using Microsoft.Data.Sqlite;
using TodoListApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 添加 CORS 服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // 允许所有来源
            .AllowAnyMethod() // 允许所有 HTTP 方法
            .AllowAnyHeader(); // 允许所有请求头
    });
});

// 添加 Repository 服务
builder.Services.AddScoped<TodoRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("TodoListDb");
    return new TodoRepository(connectionString);
});

// 添加控制器
builder.Services.AddControllers();

var app = builder.Build();

// 使用 CORS 中间件
app.UseCors("AllowAllOrigins");

// 配置 HTTP 请求管道
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run("http://0.0.0.0:5032");