using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Calculate(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                return 0;

            List<string> delimiters = new List<string> { ",", "\n" };
            string numbersSection = arg;

            if (arg.StartsWith("//"))
            {
                int endFirstLine = arg.IndexOf("\n");
                string delimiterPart = arg.Substring(2, endFirstLine - 2);
                numbersSection = arg.Substring(endFirstLine + 1);

                delimiters.Clear();

                if (delimiterPart.Contains("["))
                {
                    string temp = delimiterPart;
                    while (temp.Contains("["))
                    {
                        int start = temp.IndexOf("[");
                        int end = temp.IndexOf("]");
                        string delimiter = temp.Substring(start + 1, end - start - 1);
                        delimiters.Add(delimiter);
                        temp = temp.Substring(end + 1);
                    }
                }
                else
                {
                    delimiters.Add(delimiterPart);
                }
            }

            string[] parts = numbersSection.Split(delimiters.ToArray(), StringSplitOptions.None);

            List<int> numbers = new List<int>();
            List<int> negatives = new List<int>();

            foreach (string part in parts)
            {
                int number = int.Parse(part);

                if (number < 0)
                {
                    negatives.Add(number);
                    continue;
                }

                if (number > 1000)
                    continue;

                numbers.Add(number);
            }
            
            if (negatives.Count > 0)
            {
                string negativesList = string.Join(", ", negatives);
                throw new ArgumentException($"Negatives not allowed: {negativesList}");
            }

            return numbers.Sum();
        }
    }
}