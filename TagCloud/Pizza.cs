using System.Collections.Generic;

namespace TagCloud
{
    class Pizza
    {
        private Pizza() { } // den tomme ctor m� gerne v�re private, men de skal v�re der hvis der er andre
        public Pizza(string title, params Tag[] tags)
        {
            Title = title;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<Tag> Tags { get; set; }
    }
}