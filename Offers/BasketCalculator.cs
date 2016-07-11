using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Offers
{
    public class BasketCalculator
    {
        private const decimal BookCost = 8m;

        private readonly Action<Book[]>[] _offerFunctions;

        public BasketCalculator()
        {
            _offerFunctions = new Action<Book[]>[] {
                ApplyFiveDifferentItemDiscount,
                ApplyFourDifferentItemDiscount,
                ApplyThreeDifferentItemDiscount,
                ApplyTwoDifferentItemDiscount
            };
        }

        public decimal Calculate(IEnumerable<Book> books)
        {
            var booksArray = books as Book[] ?? books.ToArray();

            var cheapestPrice = decimal.MaxValue;

            var permutations = CreatePermutations();
            foreach (var permutation in permutations)
            {
                foreach (var offerFunction in permutation)
                {
                    offerFunction.Invoke(booksArray);
                }

                var total = Total(booksArray);

                if (total < cheapestPrice)
                {
                    cheapestPrice = total;
                }

                foreach (var book in booksArray)
                {
                    book.Applied = false;
                    book.DiscountPrice = 0; 
                }                
            }
            return cheapestPrice;
        }

        private static decimal Total(IEnumerable<Book> books) => books.Sum(x => x.Applied ? x.DiscountPrice : BookCost);

        public static void ApplyTwoDifferentItemDiscount(IEnumerable<Book> books)
        {            
            const int requiredDistinctItems = 2;
            const decimal rate = 0.05m;

            CalculateDiscount(books, requiredDistinctItems, rate);
        }

        public static void ApplyThreeDifferentItemDiscount(IEnumerable<Book> books)
        {            
            const int requiredDistinctItems = 3;
            const decimal rate = 0.10m;

            CalculateDiscount(books, requiredDistinctItems, rate);
        }
        public static void ApplyFourDifferentItemDiscount(IEnumerable<Book> books)
        {            
            const int requiredDistinctItems = 4;
            const decimal rate = 0.20m;

            CalculateDiscount(books, requiredDistinctItems, rate);
        }
        public static void ApplyFiveDifferentItemDiscount(IEnumerable<Book> books)
        {            
            const int requiredDistinctItems = 5;
            const decimal rate = 0.25m;

            CalculateDiscount(books, requiredDistinctItems, rate);
        }

        private static void CalculateDiscount(IEnumerable<Book> books, int requiredDistinctItems, decimal rate)
        {
            var distinctBooks = books.Where(x => !x.Applied).DistinctBy(x => x.Name).ToArray();

            if (distinctBooks.Count() >= requiredDistinctItems)
            {
                var itemsToApplyDiscountTo = distinctBooks.Take(requiredDistinctItems);
                foreach (var book in itemsToApplyDiscountTo)
                {
                    book.Applied = true;
                    book.DiscountPrice = BookCost - (BookCost*rate);
                }
            }
        }

        public IList<Action<Book[]>[]> CreatePermutations()
        {
            var permutations = new List<Action<Book[]>[]>();

            for (var i = 0; i < 64; i++)
            {
                var a = i%4;
                var b = i/4%4;
                var c = i/8%4 ;
                var d = i/16%4;

                permutations.Add(new[]
                {
                    _offerFunctions[a],
                    _offerFunctions[b],
                    _offerFunctions[c],
                    _offerFunctions[d]
                });                 
            }
            return permutations;
        }
    }
}
