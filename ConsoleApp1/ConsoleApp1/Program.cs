using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string formula = "2.200 3.14159 1e10";
            int count = 0;
            string val = "0";
            foreach(string t in GetTokens(formula))
            {
                Console.WriteLine(isVar(t));
            }
            Console.WriteLine(val);

            Console.Read();
        }

        private static void parenCount(string t, ref int count)
        {
            if (t == "(") count++;
            if (t == ")") count--;
        }

        private static void formatDouble(ref string t)
        {
            if (double.TryParse(t, out double val))
                t = val.ToString();
        }

        private static bool tokenIs(string s, params string[] t)
        {
            foreach(string el in t)
            {
                if (el == s) return true;
            }
            return false;
        }

        private static bool isNum(string t)
        {
            return double.TryParse(t, out double val);
        }

        private static bool isVar(string t)
        {
            return Regex.IsMatch(t, "^[a - zA - Z_] +[a - zA - Z_0 - 9] *$");
        }

        private static bool isOp(string t)
        {
            return Regex.IsMatch(t, @"[\+\-*/]");
        }

        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }
}
