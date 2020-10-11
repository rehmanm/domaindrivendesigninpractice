using System;
using System.Linq;
using static DdInPractice.Logic.Money;

namespace DdInPractice.Logic
{
    //Entity
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

        public void InsertMoney(Money money)
        {
            Money[] coinAndNotes = {Cent, TenCent, QuarterCent, Dollar, FiveDollar, TwentyDollar};
            if (!coinAndNotes.Contains(money))
            {
                throw new InvalidOperationException();
            }

            MoneyInTransaction += money;
        }
        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }
        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
    }
}
