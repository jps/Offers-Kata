using System;

namespace Offers
{
    public class Book : IComparable<Book>
    {
        public string Name { get; set; }

        public bool Applied { get; set; }

        public decimal DiscountPrice { get; set; }
        
        public int CompareTo(Book other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}