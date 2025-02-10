// This code is data model.
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public class Pizza{
        public int Id { get; set;}
        public string ? Name { get; set;}
        public string ? Description { get; set;}
    }

    // DbContext for interacting with data by creating Context object.
    class PizzaDb : DbContext{
        public PizzaDb(DbContextOptions options) : base(options) { }
        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}