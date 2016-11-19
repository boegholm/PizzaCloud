using System.Collections.Generic;

namespace TagCloud
{
    class Tag
    {
        private string _name;

        private Tag() { }                    // vi skal have en tom constructor, må dog gerne være privat

        public Tag(string name)
        {
            Name = name;
        }

        public int Id { get; set; }          // unik identifikation af tag

        /*
         * nedenstående er flyttet til PizzaContext ved brug af Fluent-Api -- så afkobler vi helt Tag fra EntityFramework!
         *  - det findes i PizzaContext:
         *    protected override void OnModelCreating(DbModelBuilder modelBuilder)
         *    
         */
        //        [Index(IsUnique = true)]                // vi vil kun have et tag!
        //        [StringLength(30)]                      // for at kunne bruge ovenstående constraint må det max være 450 tegn
        public string Name                   // brug ikke dette som key! -- hvad hvis jeg vil ændre værdien?
        {
            get { return _name; }
            set { _name = value?.ToLower(); }
        }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}