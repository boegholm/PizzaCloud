using System.Collections.Generic;

namespace TagCloud
{
    class Tag
    {
        private string _name;

        private Tag() { }                    // vi skal have en tom constructor, m� dog gerne v�re privat

        public Tag(string name)
        {
            Name = name;
        }

        public int Id { get; set; }          // unik identifikation af tag

        /*
         * nedenst�ende er flyttet til PizzaContext ved brug af Fluent-Api -- s� afkobler vi helt Tag fra EntityFramework!
         *  - det findes i PizzaContext:
         *    protected override void OnModelCreating(DbModelBuilder modelBuilder)
         *    
         */
        //        [Index(IsUnique = true)]                // vi vil kun have et tag!
        //        [StringLength(30)]                      // for at kunne bruge ovenst�ende constraint m� det max v�re 450 tegn
        public string Name                   // brug ikke dette som key! -- hvad hvis jeg vil �ndre v�rdien?
        {
            get { return _name; }
            set { _name = value?.ToLower(); }
        }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}