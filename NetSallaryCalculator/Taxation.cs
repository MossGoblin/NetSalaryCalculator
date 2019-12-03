using System;
using System.Collections.Generic;
using System.Text;

namespace NetSallaryCalculator
{
    public class Taxation
    {
        public decimal TaxationPercentage { get; private set; }
        public decimal SocialContributionsPercentage { get; private set; }

        public decimal MinimumTaxableAmount { get; private set; }

        public decimal MaximumAmountSubjectYoSC { get; private set; }

        public Taxation(decimal taxationP = 10, decimal socContribP = 15, decimal minTaxAmount = 1000, decimal maxAmSC = 3000)
        {
            this.TaxationPercentage = taxationP;
            this.SocialContributionsPercentage = socContribP;
            this.MinimumTaxableAmount = minTaxAmount;
            this.MaximumAmountSubjectYoSC = maxAmSC;
        }

        public void UpdateTaxationPercentge()
        {
           string prompt = "? Enter new value for taxation percentage: ";
           string errorMessage = "! Invalid inut - The value must be a non-negative decimal between 0 and 100\n";
           TaxationPercentage = Terminal.DecimalInputBound(prompt, errorMessage, 0, 100);
        }

        public void UpdateSocialContributionPercentge()
        {
            string prompt = "? Enter new value for social contribution percentage: ";
            string errorMessage = "! Invalid input - The value must be a non-negative decimal\n";
            SocialContributionsPercentage = Terminal.DecimalInput(prompt, errorMessage, true);
        }

        public void UpdateMinimumAmountPercentge()
        {
            string prompt = "? Enter new Minimum taxable amount: ";
            string errorMessage = "! Invalid input - The value must be a non-negative decimal..\n ..smaller than the maximum amount eligible for social contributions ({MaximumAmountSubjectYoSC})\n";
            MinimumTaxableAmount = Terminal.DecimalInputBound(prompt, errorMessage, 0, MaximumAmountSubjectYoSC);
        }

        public void UpdateMaximumAmountSubjToSC()
        {
            string prompt = "? Enter new maximum amount that can be subject to social contributions: ";
            string errorMessage = "! Invalid input - The value must be a a non-negative decimal..\n ..larger than the minimum taxable amount ({MinimumTaxableAmount})\n";
            MaximumAmountSubjectYoSC = Terminal.DecimalInputBound(prompt, errorMessage, MinimumTaxableAmount, Decimal.MaxValue);
        }

        private decimal CalculatedTax(decimal amount)
        {
            decimal applicableBase = Math.Max(0, amount - MinimumTaxableAmount);
            return applicableBase * TaxationPercentage / 100;
        }

        private decimal CalculatedSocialContributions(decimal amount)
        {
            decimal applicableBase = Math.Min(Math.Max(0, amount - 1000), 2000);
            return applicableBase * SocialContributionsPercentage / 100;
        }

        public decimal ApplyTaxation(decimal amount)
        {
            return amount - CalculatedTax(amount) - CalculatedSocialContributions(amount);
        }
    }
}
