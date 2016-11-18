using System;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeDb();

            using (var ctx = new PizzaContext())
            {
                //ctx.Database.Log = Console.WriteLine; // Du kan se den generede SQL med denne linie

                // vis tag-popularitet
                PrintIngredients(ctx);
                Prompt();

                // i stedet for query-tilgang, brug method-chaining
                PrintIngredientsMethodChaining(ctx);
                Prompt();

                // lidt mere information
                IncludePizzasPrIngredient(ctx);
                Prompt();

                // find pizzaer ud fra tags
                PotentialPizzas(ctx); // nomnom
            }
            Prompt("Tryk en tast for at afslutte");
        }

        private static void PotentialPizzas(PizzaContext ctx)
        {
            Console.WriteLine("Her er en tilfældig yndlings-ingrediens");
            int count = ctx.Tags.ToList().Count;
            Random r = new Random();

            Tag nomnomTag = ctx.Tags.ToList()[r.Next(count)];


            var nomnom = ctx.Pizzas.Where(p => p.Tags.Any(t => t.TagId == nomnomTag.TagId));
            Console.WriteLine($"Pizzaer med {nomnomTag.TagName}");
            foreach (Pizza pizza in nomnom)
            {
                Console.WriteLine($"  {pizza.Title}");
            }
            Console.WriteLine(" ...nom nom");
        }

        private static void IncludePizzasPrIngredient(PizzaContext ctx)
        {
            // vi vil gemme pizzaerne i vores grouping
            Console.WriteLine("Lidt mere information:");
            var tagCount =
                (from pizza in ctx.Pizzas
                    from tag in pizza.Tags
                    group pizza by tag.TagName
                    into g
                    select new
                    {
                        Name = g.Key,
                        Count = g.Count(),
                        Pizzas = g.ToList(),
                    })
                    .OrderByDescending(g => g.Count);
            foreach (var v in tagCount)
            {
                Console.WriteLine($"  {v.Name} er brugt {v.Count} gange i følgende pizzaer");
                Console.WriteLine("  " + string.Join(",", v.Pizzas.Select(e => e.Title)));
                Console.WriteLine();
            }
        }

        private static void PrintIngredientsMethodChaining(PizzaContext ctx)
        {
            // man kan gøre det samme med method-chaining -- resharper kan ofte convertere
            var tagCount =
                (ctx.Pizzas.SelectMany(pizza => pizza.Tags, (pizza, tag) => new {pizza, tag})
                    .GroupBy(t => t.tag.TagName, t => t.tag)
                    .Select(g => new
                    {
                        Name = g.Key,
                        Count = g.Count(),
                    }))
                    .OrderByDescending(g => g.Count);
            Console.WriteLine("List over ingrediens-popularitet: (tag-cloud?) -- method-chaining");
            foreach (var count in tagCount)
            {
                Console.WriteLine($"  {count.Name} er brugt {count.Count} gange!");
            }
        }

        private static void PrintIngredients(PizzaContext ctx)
        {
            var tagCount =
                (from pizza in ctx.Pizzas
                    from tag in pizza.Tags
                    group tag by tag.TagName
                    into g
                    select new
                    {
                        Name = g.Key,
                        Count = g.Count(),
                    })
                    .OrderByDescending(g => g.Count);

            Console.WriteLine("List over ingrediens-popularitet: (tag-cloud?)");
            foreach (var count in tagCount)
            {
                Console.WriteLine($"  {count.Name} {count.Count}");
            }
        }

        private static void InitializeDb()
        {
            new PizzaContext().Database.Delete();
            Tag tomat = new Tag("tomat");
            Tag ost = new Tag("ost");
            Tag ananas = new Tag("Ananas");
            Tag skinke = new Tag("Skinke");
            Tag champignon = new Tag("Champignon");
            Tag rejer = new Tag("rejer");
            Tag paprika = new Tag("Paprika");
            Tag kødfars = new Tag("kødfars");
            Tag bacon = new Tag("bacon");
            Tag pepperoni = new Tag("pepperoni");

            using (var ctx = new PizzaContext())
            {
                ctx.Pizzas.Add(new Pizza("Margherita", tomat, ost));
                ctx.Pizzas.Add(new Pizza("Hawaii", tomat, ost, skinke, ananas));
                ctx.Pizzas.Add(new Pizza("Capricciosa", tomat, ost, skinke, champignon));
                ctx.Pizzas.Add(new Pizza("Quattro Stagioni", tomat, ost, rejer, champignon));
                ctx.Pizzas.Add(new Pizza("Vesuvio", tomat, ost, skinke, paprika));
                ctx.Pizzas.Add(new Pizza("Venezia", tomat, ost, kødfars, bacon, pepperoni));
                ctx.Pizzas.Add(new Pizza("Cannibale", tomat, ost, kødfars));
                ctx.Pizzas.Add(new Pizza("Pepperoni", tomat, ost, pepperoni));

                ctx.SaveChanges();
            }
        }

        private static void Prompt(string txt = "Tryk en tast for at fortsætte")
        {
            Console.WriteLine(txt);
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine();
        }

    }
}
