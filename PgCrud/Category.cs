namespace Pagila;

public class Category
{
    public Category()
    {
    }
    public Category(int id, string name, DateTime lastUpdate)
    {
        this.Id = id;
        this.Name = name;
        this.LastUpdated = lastUpdate;
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime LastUpdated { get; set; }
}
