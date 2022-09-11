using Npgsql;
using Dapper;
namespace Pagila;

public class CategoryMapperDapper
{
    private static readonly string _connectionString = "Host=localhost:49153;Username=postgres;Password=postgrespw;Database=pagila";

    public static async Task<int> Create(Category category)
    {
        if (category.Id == 0)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            string sql = @"INSERT INTO public.category(name, last_update) 
                           VALUES (@Name, @LastUpdate) 
                           RETURNING category_id;";
            category.Id = (int)await connection.ExecuteScalarAsync(sql, new { Name = category.Name, LastUpdate = category.LastUpdated });
        }
        return category.Id;
    }
    public static async Task<Category> Read(int id)
    {
        await using NpgsqlConnection connection = new(_connectionString);
        string sql = @"SELECT category_id,
                              name, 
                              last_update 
                      FROM public.category 
                      WHERE category_id = @Id 
                      limit 1;";
        return connection.QueryFirstOrDefault<Category>(sql, new { Id = id });
    }
    public static async Task<bool> Update(Category category)
    {
        int result = 0;
        if (category.Id > 0)
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            string sql = @"UPDATE public.category 
                          SET  name= @Name , 
                               last_update= @LastUpdate
                          WHERE category_id = @Id;";
            result = await connection.ExecuteAsync(sql, new { Name = category.Name, LastUpdate = category.LastUpdated, Id = category.Id });
        }
        return result > 0;
    }
    public static async Task<bool> Delete(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        string sql = @"DELETE FROM public.category WHERE category_id = @Id;";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}
