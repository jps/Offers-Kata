using System.Linq;
using FluentAssertions;
using Offers;
using Xunit;

namespace OfferTests
{
    public class OfferCalculatorDiscount
    {
        private readonly BasketCalculator _basketCalculator;

        public OfferCalculatorDiscount()
        {
            _basketCalculator = new BasketCalculator();
        }

        [Fact]
        public void OneBook()
        {
            var books = OfferBuilder.CreateUniqueBooks(1);

            var basketTotal = _basketCalculator.Calculate(books);

            basketTotal.Should().Be(8m);
        }

        [Fact]
        public void TwoSameNoDiscount()
        {
            var books = OfferBuilder.CreateBooks(2);

            var basketTotal = _basketCalculator.Calculate(books);

            basketTotal.Should().Be(16m);
        }

        [Fact]
        public void TwoDifferentItemOfferCorrectDiscount()
        {
            var books = OfferBuilder.CreateUniqueBooks(2);

            var basketTotal = _basketCalculator.Calculate(books.ToList());

            basketTotal.Should().Be(15.2m);
        }

        [Fact]
        public void ThreeDifferentItemOfferCorrectDiscount()
        {
            var books = OfferBuilder.CreateUniqueBooks(3);

            var basketTotal = _basketCalculator.Calculate(books.ToList());

            basketTotal.Should().Be(21.6m);
        }

        [Fact]
        public void FourDifferentItemOfferCorrectDiscount()
        {
            var books = OfferBuilder.CreateUniqueBooks(4);

            var basketTotal = _basketCalculator.Calculate(books.ToList());

            basketTotal.Should().Be(25.6m);
        }

        [Fact]
        public void FiveDifferentItemOfferCorrectDiscount()
        {
            var books = OfferBuilder.CreateUniqueBooks(5);

            var basketTotal = _basketCalculator.Calculate(books.ToList());

            basketTotal.Should().Be(30m);
        }

        [Fact]
        public void BackPageDifferentItemOfferCorrectDiscount()
        {
            var books = OfferBuilder.CreateBooks(2,2,2,1,1);

            var basketTotal = _basketCalculator.Calculate(books.ToList());

            basketTotal.Should().Be(51.20m);
        }
    }
}
