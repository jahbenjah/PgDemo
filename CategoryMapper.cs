using Npgsql;
using System.Data;

namespace Pagila;

public class CategoryMapper
{
    private static readonly string _connectionString = "Host=localhost:49153;Username=postgres;Password=postgrespw;Database=pagila";

    public static async Task<int> Create(Category category)
    {
        if (category.Id == 0)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            await using NpgsqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO public.category(name, last_update) VALUES (@Name, @LastUpdate) RETURNING category_id;";
            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@LastUpdate", category.LastUpdated);
            category.Id = (int)await command.ExecuteScalarAsync();
        }
        return category.Id;
    }
    public static async Task<Category> Read(int id)
    {
        await using NpgsqlConnection connection = new(_connectionString);
        connection.Open();
        await using NpgsqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT category_id, name, last_update FROM public.category WHERE category_id = @Id limit 1;";
        command.Parameters.AddWithValue("@Id", id);
        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            reader.Read();
            string name = (string)reader["name"];
            DateTime lastUpdate = (DateTime)reader["last_update"];
            int catId = (int)reader["category_id"];
            return new Category(catId, name, lastUpdate);
        }
        return null;
    }
    public static async Task<bool> Update(Category category)
    {
        int result = 0;
        if (category.Id > 0)
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            await using NpgsqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE public.category SET  name= @Name , last_update= @LastUpdate WHERE category_id = @Id;";
            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@LastUpdate", category.LastUpdated);
            command.Parameters.AddWithValue("@Id", category.Id);
            result = await command.ExecuteNonQueryAsync();
        }
        return result > 0;
    }
    public static async Task<bool> Delete(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        await using NpgsqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "DELETE FROM public.category WHERE category_id = @Id;";
        command.Parameters.AddWithValue("@Id", id);
        var result = await command.ExecuteNonQueryAsync();
        return result > 0;
    }
}
