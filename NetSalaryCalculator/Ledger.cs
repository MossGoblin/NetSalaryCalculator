using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSalaryCalculator
{
    public class Ledger
    {
        public List<IncomeRecord> records;
        private Taxation taxMachine;

        public Ledger(Taxation taxMachine)
        {
            records = new List<IncomeRecord>();
            this.taxMachine = taxMachine;
        }

        public string AddNewRecord(string newName, decimal income)
        {
            decimal taxedAmount = taxMachine.ApplyTaxation(income);
            IncomeRecord newRecord = new IncomeRecord(newName, income, taxedAmount);
            records.Add(newRecord);
            return newRecord.ToString();
        }

        public string ReviewRecord(string name)
        {
            IncomeRecord record = records.FirstOrDefault(x => x.HolderName == name);
            string result = record.ToString();
            return result;
        }

        public string ExtractRecords(bool sortByName)
        {
            StringBuilder sb = new StringBuilder();
            if (sortByName)
            {
                foreach (var record in records.OrderBy(x => x.HolderName))
                {
                    sb.AppendLine(record.ToString());
                }
            }
            else
            {
                foreach (var record in records.OrderBy(x => x.NetAmount))
                {
                    sb.AppendLine(record.ToString());
                }
            }

            return sb.ToString();
        }

        public void UpdateRecord(string name)
        {
            IncomeRecord record = records.FirstOrDefault(x => x.HolderName == name);
            decimal oldIncome = record.Amount;
            string prompt = $"? Old Income: {oldIncome}; Enter new income ";
            string errorMessage = "! Invalid input - please enter income as a positive decimal value";
            decimal newAmount = Terminal.DecimalInput(prompt, errorMessage, false);
            record.UpdateAmount(newAmount);
            record.UpdateNetAmount(taxMachine.ApplyTaxation(record.Amount));
        }

        public bool RemoveRecord(string name)
        {
            IncomeRecord record = records.FirstOrDefault(x => x.HolderName == name);
            if (record != null)
            {
                records.Remove(record);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistingName(string name)
        {
            foreach (var record in records)
            {
                if (record.HolderName == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateAllRecords()
        {
            foreach (var record in records)
            {
                record.UpdateNetAmount(taxMachine.ApplyTaxation(record.Amount));
            }
        }

        public int RecordsCount()
        {
            return records.Count;
        }
    }
}
