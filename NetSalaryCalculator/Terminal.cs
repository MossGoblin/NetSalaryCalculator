using System;
using System.Collections.Generic;
using System.Text;

namespace NetSalaryCalculator
{
    public static class Terminal
    {

        public static bool BinaryChoiseInput(string prompt, string errorMessage, char optionOne, char optionTwo)
        {
            // check for proper arguments
            if ((Char.ToLower(optionOne) != optionOne) || (Char.ToLower(optionTwo) != optionTwo))
            {
                throw new ArgumentException("The two options must be represented by variables of type 'char'");
            }


            Console.Write(prompt);
            char input;
            bool validInput = false;
            while (!validInput)
            {
                input = Console.ReadKey().KeyChar;
                if (Char.ToLower(input) == optionOne)
                {
                    return true;
                }
                else if (Char.ToLower(input) == optionTwo)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
            return true;
        }

        public static decimal DecimalInput(string prompt, string errorMessage, bool zeroAllowed)
        {
            bool validInput = false;
            decimal result = 0;
            Console.Write(prompt);
            while (!validInput)
            {
                string inputString = Console.ReadLine();
                validInput = Decimal.TryParse(inputString, out result);
                if (!validInput || (result <= 0 && zeroAllowed) || (result == 0 && !zeroAllowed))
                {
                    Console.WriteLine(errorMessage);
                    Console.Write(prompt);
                }
            }

            return result;
        }

        public static decimal DecimalInputBound(string prompt, string errorMessage, decimal minimum, decimal maximum)
        {
            bool validInput = false;
            decimal result = 0;
            Console.Write(prompt);
            while (!validInput)
            {
                string inputString = Console.ReadLine();
                validInput = Decimal.TryParse(inputString, out result);
                if (!validInput || result < minimum || result > maximum)
                {
                    Console.WriteLine(errorMessage);
                    Console.Write(prompt);
                }
            }

            return result;
        }

        public static string StringInput(string prompt, bool emptyAllowed)
        {
            Console.Write(prompt);
            if (emptyAllowed)
            {
                return Console.ReadLine();
            }
            else
            {
                string result = "";
                bool validInput = false;
                while (!validInput)
                {
                    Console.Write("Please enter a non-empty string: ");
                    result = Console.ReadLine();
                    validInput = (result != String.Empty);
                }
                return result;
            }
        }

        public static bool ConfirmationInput(string prompt, char confirmationChar)
        {
            Console.Write(prompt);
            char nextKey = Console.ReadKey().KeyChar;
            if (Char.ToLower(nextKey) == confirmationChar)
            {
                return true;
            }
            return false;
        }

        public static void Output(string output)
        {
            Console.WriteLine(output);
        }

        public static void Line(int lenght)
        {
            Console.WriteLine(new String('-', lenght));
        }

        internal static void AnyKey(bool text)
        {
            if (text)
            {
                Console.WriteLine("? Press any key to return to the previous menu");
            }
            Console.ReadKey();
        }
    }
}
