using IntelligencePipeline.Models.Enums;
using System;

namespace IntelligencePipeline.Validation
{
    public static class InputValidator
    {
        public static DateTime GetDateTime(string prompt)
        {
            Console.Write(prompt);

            bool isValid = false;
            DateTime result = default;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid datetime. Try again: ");
                }
            }

            return result;
        }

        public static double GetDouble(string prompt)
        {
            Console.Write(prompt);

            bool isValid = false;
            double result = default;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (double.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid number. Try again: ");
                }
            }

            return result;
        }

        public static int GetInt(string prompt)
        {
            Console.Write(prompt);

            bool isValid = false;
            int result = default;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid number. Try again: ");
                }
            }

            return result;
        }

        public static int GetEnum(string prompt, Type enumType)
        {
            Console.WriteLine("Choose option:");

            Array values = Enum.GetValues(enumType);

            foreach (var value in values)
            {
                Console.WriteLine($"{(int)value} - {value}");
            }

            Console.Write(prompt);

            bool isValid = false;
            int result = default;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out result) &&
                    Enum.IsDefined(enumType, result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid selection. Try again: ");
                }
            }

            return result;
        }
        // This validet priority non - case sensative
        public static Priority GetPriority(string prompt)
        {
            Console.Write(prompt);

            bool isValid = false;
            Priority result = default;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (Enum.TryParse(input, true, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid priority. Try again (Low / Medium / High / Critical): ");
                }
            }

            return result;
        }
        //VAlidate user status if its valid (one from the enum)
        public static ReportStatus GetStatus(string prompt)
        {
            Console.Write(prompt);

            bool isValid = false;
            ReportStatus result = default;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (Enum.TryParse(input, true, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid status. Try again (New / Validating / Validated / Rejected / InProgress / Completed): ");
                }
            }

            return result;
        }
        // ================= NEW: STRING BASED LANGUAGE INPUT =================
        public static Language GetLanguage(string prompt)
        {
            Console.Write(prompt);

            Language result = Language.English;
            bool isValid = false;

            while (!isValid)
            {
                string input = Console.ReadLine();

                if (Enum.TryParse(input, true, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.Write("Invalid language. Try again: ");
                }
            }

            return result;
        }
    }
}