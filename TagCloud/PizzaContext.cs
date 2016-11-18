using System.Data.Entity;

namespace TagCloud
{
    class PizzaContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
    }
}