using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TodoListApi.Models;

namespace TodoListApi.Repositories
{
    public class TodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        // 查询所有 TodoItem
        public IEnumerable<TodoItem> GetAll()
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<TodoItem>("SELECT * FROM TodoItem");
            }
        }

        // 添加新的 TodoItem
        public int Add(TodoItem item)
        {
            using (var connection = CreateConnection())
            {
                var sql =
                    "INSERT INTO TodoItem (Title, Description, IsCompleted) VALUES (@Title, @Description, @IsCompleted); SELECT last_insert_rowid();";
                return connection.ExecuteScalar<int>(sql, item);
            }
        }

        // 更新 TodoItem
        public int Update(TodoItem item)
        {
            using (var connection = CreateConnection())
            {
                var sql =
                    "UPDATE TodoItem SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted WHERE Id = @Id";
                return connection.Execute(sql, item);
            }
        }
        // 删除 TodoItem
        public int Delete(int id)
        {
            using (var connection = CreateConnection())
            {
                return connection.Execute("DELETE FROM TodoItem WHERE Id = @Id", new { Id = id });
            }
        }
        
    }
}