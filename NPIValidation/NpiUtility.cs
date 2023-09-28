using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPIValidation
{
    public static class NpiUtility
    {
        /// <summary>
        /// Validates an NPI
        /// </summary>
        /// <param name="npi"></param>
        /// <returns></returns>
        public static bool Validate(string npi)
        {
            // Valid NPI's are either 10 digits, or 15 digits starting with '80840'
            if (npi.Length != 10 && !(npi.Length == 15 && npi.StartsWith("80840")))
            {
                return false;
            }

            // 24 is luhn algorithm result of the first 5 digits of the NPI when the card issuer prefix is present '80840'
            // Since we're stripping off the first five digits, we need to add 24 to the sum
            int sum = 24;

            if (npi.Length == 15)
            {
                npi = npi.Substring(5);
            }

            // Implement the Luhn algorithm (the last digit is the check digit so we don't need to include it)
            for (int i = 0; i < npi.Length - 1; i++)
            {
                int digit = int.Parse(npi[i].ToString());
                if (i % 2 != 0)
                {
                    sum += digit;
                }
                else
                {
                    sum += GetDouble(digit);
                }
            }

            //Subtract the sum from the next higher number ending in zero. This should match the check digit
            if (sum % 10 == 0)
            {
                sum = 0;
            }
            else
            {
                sum = 10 - (sum % 10);
            }

            var lastDigit = int.Parse(npi[npi.Length - 1].ToString());
            return sum == lastDigit;
        }

        /// <summary>
        /// Doubles a number and returns the sum of the digits
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int GetDouble(int i)
        {
            switch (i)
            {
                case 0: return 0;
                case 1: return 2;
                case 2: return 4;
                case 3: return 6;
                case 4: return 8;
                case 5: return 1;
                case 6: return 3;
                case 7: return 5;
                case 8: return 7;
                case 9: return 9;
                default: return 0;
            }
        }
    }
}
