using Npgsql;

NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
builder.Host = "localhost";
builder.Database = "Pagila";
Console.WriteLine(builder.ConnectionString);