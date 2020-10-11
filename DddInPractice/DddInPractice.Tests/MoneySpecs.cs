using System;
using DdInPractice.Logic;
using FluentAssertions;
using Xunit;

namespace DddInPractice.Core.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result()
        {

            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCentCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Two_money_instances_equal_if_contain_the_same_money_amount()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Two_money_instancs_does_not_equal_if_contain_different_amount()
        {
            Money dollar = new Money(0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money(100, 0, 0, 0,0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -1, 0, 0, 0, 0)]
        [InlineData(0, 0, -1, 0, 0, 0)]
        [InlineData(0, 0, 0, -1, 0, 0)]
        [InlineData(0, 0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0, -1)]

        public void Cannot_Create_money_with_negative_value(
            int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount
        )
        {
            Action action = () =>  new Money(oneCentCount, tenCentCount, quarterCentCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            action.Should().ThrowExactly<InvalidOperationException>();
        }

        [Theory]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 1, 0, 0, 0, 0, 0.11)]
        [InlineData(1, 1, 1, 0, 0, 0, 0.36)]
        [InlineData(1, 1, 1, 1, 0, 0, 1.36)]
        [InlineData(1, 1, 1, 1, 1, 0, 6.36)]
        [InlineData(1, 1, 1, 1, 1, 1, 26.36)]

        public void Amount_is_calculated_correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount
        )
        {
            Money m = new Money(oneCentCount, tenCentCount, quarterCentCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            m.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCentCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);

            result.Amount.Should().Be(113.64M);

        }
    }
}
