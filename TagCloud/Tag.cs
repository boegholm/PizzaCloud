using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagCloud
{
    class Tag
    {
        public Tag() { }                        // vi skal have en tom constructor

        public Tag(string tagName)
        {
            TagName = tagName.ToLower();
        }
        
        public int TagId { get; set; }          // unik identifikation af tag

        [Index(IsUnique = true)]                // vi vil kun have et tag!
        [StringLength(30)]                      // for at kunne bruge ovenst�ende constraint m� det max v�re 450 tegn
        public string TagName { get; set; }     // brug ikke dette som key! -- hvad hvis jeg vil �ndre v�rdien?
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}