using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace TagCloud
{
    class PizzaContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().Property(x => x.Name)
                // for at kunne bruge ovenstående constraint må det max være 450 tegn
                .HasMaxLength(30)
                // vi vil kun have et tag!
                // Øv, den har de ikke fået lavet endnu:
                // http://entityframework.codeplex.com/workitem/299
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}