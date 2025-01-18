using Microsoft.Data.Sqlite;
using TodoListApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 添加 Repository 服务
builder.Services.AddScoped<TodoRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("TodoListDb");
    return new TodoRepository(connectionString);
});

// 添加控制器
builder.Services.AddControllers();

var app = builder.Build();

// 配置 HTTP 请求管道
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();