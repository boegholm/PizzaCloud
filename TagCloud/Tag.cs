using System.Collections.Generic;

namespace TagCloud
{
    class Tag
    {
        private string _name;

        private Tag() { }                        // vi skal have en tom constructor, m� dog gerne v�re privat

        public Tag(string name)
        {
            Name = name;
        }

        public int Id { get; set; }          // unik identifikation af tag

        public string Name // brug ikke dette som key! -- hvad hvis jeg vil �ndre v�rdien?
        {
            get { return _name; }
            set { _name = value?.ToLower(); }
        }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}