using System;
using System.Collections.Generic;
using System.Text;

namespace NetSallaryCalculator
{
    public class IncomeRecord
    {
        public string HolderName { get; private set; }
        public decimal Amount { get; private set; }

        public decimal NetAmount { get; private set; }

        public IncomeRecord(string holdeName, decimal amount, decimal netAmount)
        {
            this.HolderName = holdeName;
            this.Amount = amount;
            this.NetAmount = ValidateNetAmount(netAmount);
        }

        public void UpdateAmount(decimal newAmount)
        {
            this.Amount = newAmount;
        }

        private decimal ValidateNetAmount(decimal netAmount)
        {
            if (netAmount > this.Amount)
            {
                throw new ArgumentOutOfRangeException("Net amount must be equal or lower than the base gross amount");
            }
            return netAmount;
        }

        public void UpdateNetAmount(decimal amount)
        {
            this.NetAmount = ValidateNetAmount(amount);
        }

        public override string ToString()
        {
            return $"'{HolderName}': {Amount} IDR ({NetAmount} IDR after tax)";
        }
    }
}
