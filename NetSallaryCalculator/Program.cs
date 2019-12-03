using System;
using System.Collections.Generic;

namespace NetSallaryCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Taxation taxation = new Taxation();
            Ledger ledger = new Ledger(taxation);
            ProcessInput(ledger, taxation);
        }

        public static void ProcessInput(Ledger ledger, Taxation taxMachine)
        {
            // open terminal
            Terminal.Output("Wecome to..");
            Console.WriteLine();

            bool done = false;

            while (!done)
            {
                Terminal.Output("== Imaginaria Income Calculator ==");
                Console.WriteLine();
                Terminal.Output("> Please choose an action:");
                Terminal.Output(" (1 or i) New Income Record");
                Terminal.Output(" (2 or f) Find a particular income record");
                Terminal.Output(" (3 or r) Review saved income records");
                Terminal.Output(" (4 or t) Review and update taxation terms");
                Terminal.Output(" (0 or x) Exit");


                char nextKey = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine();
                switch (Char.ToLower(nextKey))
                {
                    case '1':
                    case 'i':
                        AddNewRecord(ledger);
                        break;
                    case '2':
                    case 'f':
                        FindRecord(ledger);
                        break;
                    case '3':
                    case 'r':
                        ReviewRecords(ledger);
                        break;
                    case '4':
                    case 't':
                        ReviewTerms(taxMachine, ledger);
                        break;
                    case '0':
                    case 'x':
                        done = true;
                        break;
                }
            }
        }

        private static void ReviewTerms(Taxation taxation, Ledger ledger)
        {
            Terminal.Output("> Current taxation terms are:");
            Terminal.Output($". Tax percentage: {taxation.TaxationPercentage}%");
            Terminal.Output($". Social contributions percentage: {taxation.SocialContributionsPercentage}%");
            Terminal.Output($". Minimum taxable amount: {taxation.MinimumTaxableAmount} IDR");
            Terminal.Output($". Maximum amount subject to social contributions: {taxation.MaximumAmountSubjectYoSC} IDR");

            bool update = Terminal.ConfirmationInput("? Would you like to update any of the values? (Y/y for 'yes'): ", 'y');
            Console.WriteLine();
            Console.WriteLine();
            if (update)
            {
                Terminal.Output("> Please select a value to update");
                Terminal.Output(" (1) Tax percentage");
                Terminal.Output(" (2) Social contributions percentage");
                Terminal.Output(" (3) Minimum taxable amount");
                Terminal.Output(" (4) Maximum amount subject to social contributions");
                Terminal.Output(" (0) Exit");

                bool done = false;

                while (!done)
                {
                    char nextKey = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    Console.WriteLine();
                    switch (Char.ToLower(nextKey))
                    {
                        case '1':
                            taxation.UpdateTaxationPercentge();
                            ledger.UpdateAllRecords();
                            break;
                        case '2':
                            taxation.UpdateSocialContributionPercentge();
                            ledger.UpdateAllRecords();
                            break;
                        case '3':
                            taxation.UpdateMinimumAmountPercentge();
                            ledger.UpdateAllRecords();
                            break;
                        case '4':
                            taxation.UpdateMaximumAmountSubjToSC();
                            ledger.UpdateAllRecords();
                            break;
                        case '0':
                            done = true;
                            ledger.UpdateAllRecords();
                            break;
                    }
                    done = true;
                }
            }

            Console.Clear();
        }

        private static void AddNewRecord(Ledger ledger) // TODO : send validation for existing name to the ledger
        {
            string name = Terminal.StringInput("? Enter the name of the record holder: ", false);
            bool existingname = false;

            if (ledger.ExistingName(name))
            {
                existingname = true;
                bool confirmation = Terminal.ConfirmationInput("? A record for this name already exists. Would you like to review it? (Y/y for 'yes'): ", 'y');
                Console.WriteLine();
                Console.WriteLine();
                if (confirmation)
                {
                    Terminal.Output(ledger.ReviewRecord(name));
                    confirmation = Terminal.ConfirmationInput("? Would you like to update the redord? (Y/y for 'yes'): ", 'y');
                    Console.WriteLine();
                    Console.WriteLine();
                    if (confirmation)
                    {
                        ledger.UpdateRecord(name);
                        Terminal.Output($"\n[Updated: '{name}]'\n");
                        Terminal.Output(ledger.ReviewRecord(name));
                        Terminal.AnyKey(true);
                    }
                }
            }

            if (!existingname)
            {
                // get income
                decimal income = 0;
                income = Terminal.DecimalInput("? Enter gross income: ", "! Invalid input - please enter income as a positive decimal value ", false);
                ledger.AddNewRecord(name, income);
                Terminal.Output($"\n[Added: {ledger.ReviewRecord(name)}]\n");
                Terminal.AnyKey(true);
            }

            Console.Clear();
        }

        public static void FindRecord(Ledger ledger)
        {
            string name = Terminal.StringInput("? Enter a name to search the ledger for: ", false);
            if (!ledger.ExistingName(name))
            {
                Terminal.Output("> This name is not present in the records");
            }
            else
            {

                Terminal.Output(ledger.ReviewRecord(name));

                bool update = Terminal.ConfirmationInput("? Would you like to update/delete the record? (Y/y for 'yes'): ", 'y');
                Console.WriteLine();
                Console.WriteLine();
                if (update)
                {
                    Console.WriteLine();
                    Terminal.Output($"> Actions for '{name}':");
                    Terminal.Output(" (1) Update");
                    Terminal.Output(" (2) Delete");
                    Terminal.Output(" (0) Exit");


                    char nextKey = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    switch (Char.ToLower(nextKey))
                    {
                        case '1':
                            ledger.UpdateRecord(name);
                            Terminal.Output($"\n[Updated: {name}]\n");
                            Terminal.Output(ledger.ReviewRecord(name));
                            break;
                        case '2':
                            bool confirmation = Terminal.ConfirmationInput($"? Are you certain you want to delete the record for '{name}'? (Y/y for 'yes'): ", 'y');
                            Console.WriteLine();
                            Console.WriteLine();
                            if (confirmation)
                            {
                                ledger.RemoveRecord(name);
                                Terminal.Output($"\n[Deleted: {name}]\n");
                            }
                            break;
                        case '0':
                            break;
                    }
                }
            }
            Terminal.AnyKey(true);
            Console.Clear();
        }

        public static void ReviewRecords(Ledger ledger)
        {
            // get sorting method preference
            // extract records
            string prompt = "? Would you like to review the recrods by name ('N') or by gross value ('G'): ";
            string errorMessage = "Unrecognized input - please press 'N/n' for ordering by name or 'G/g' for ordering by gross value ";
            bool sortByName = Terminal.BinaryChoiseInput(prompt, errorMessage, 'n', 'g');
            Console.WriteLine();
            Terminal.Output(ledger.ExtractRecords(sortByName));

            Terminal.AnyKey(true);
            Console.Clear();
        }
    }
}
