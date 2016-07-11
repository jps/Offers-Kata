using System.Collections.Generic;
using System.Linq;
using Offers;

namespace OfferTests
{
    public static class OfferBuilder
    {
        public static IEnumerable<Book> CreateBooks(params int[] bookQuantities) => CreateBooksInternal(bookQuantities);

        public static IEnumerable<Book> CreateUniqueBooks(int quantity) => CreateBooksInternal(
            Enumerable.Range(0, quantity)
            .Select(x => 1)
            .ToArray());

        private static IEnumerable<Book> CreateBooksInternal(params int[] bookQuantities)
        {
            for (var i = 0; i < bookQuantities.Length; i++)
            {
                for (var j = 0; j < bookQuantities[i]; j++)
                {
                    yield return new Book()
                    {
                        Name = $"Book {i + 1}",
                        Applied = false,
                    };
                }
            }
        }
    }
}