using Pagila;

//Create
var category = new Category { Name = "Benjamin Category 2022", LastUpdated = DateTime.Now };
var categoryId = await CategoryMapper.Create(category);

//Read
var cat = await CategoryMapper.Read(categoryId);
Console.WriteLine($"{cat.Name} ,{cat.Id},{cat.LastUpdated} ");

//Update
cat.Name += "Actualizada";
cat.LastUpdated = DateTime.Now;
await CategoryMapper.Update(cat);
var cat2 = await CategoryMapper.Read(cat.Id);
Console.WriteLine($"{cat2.Name} {cat2.Id},{cat2.LastUpdated} ");

//Delete Demo
//await CategoryMapper.Delete(30);