using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagCloud
{
    class Tag
    {
        private Tag() { }                        // vi skal have en tom constructor, må dog gerne være privat

        public Tag(string tagName)
        {
            TagName = tagName.ToLower();
        }
        
        public int TagId { get; set; }          // unik identifikation af tag

        [Index(IsUnique = true)]                // vi vil kun have et tag!
        [StringLength(30)]                      // for at kunne bruge ovenstående constraint må det max være 450 tegn
        public string TagName { get; set; }     // brug ikke dette som key! -- hvad hvis jeg vil ændre værdien?
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}