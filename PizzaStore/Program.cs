using Microsoft.OpenApi.Models;
using PizzaStore.DB;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddEndpointsApiExplorer();
// Add Service of DbContext
builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "PizzaStore API", 
        Description = "Making the Pizzas you love", 
        Version = "v1" 
    });
});
    
var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
   });
}
    
app.MapGet("/", () => "Hello World!");
// app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
// app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
// app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
// app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
// app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

// To get the list of Pizzas
app.MapGet("/pizzas", async(PizzaDb db) => await db.Pizzas.ToListAsync());

// Create new items.
app.MapPost("/pizza", async (PizzaDb db, PizzaStore.Models.Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

// Get a single item.
app.MapGet("/pizza/{id}", async(PizzaDb db, int id) =>
await db.Pizzas.FindAsync(id));

app.Run();