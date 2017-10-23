using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumOfPowers
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<HashSet<int>> workingValues = ListOfInts(100, 4);
            printWorkingSets(workingValues);
            Console.Read();
        }
        /// <summary>
        /// helper print method.
        /// </summary>
        /// <param name="workingValues"></param>
        private static void printWorkingSets(HashSet<HashSet<int>> workingValues)
        {
            if (workingValues.Count == 0)
                Console.WriteLine("No Working Values");
            foreach (HashSet<int> intValues in workingValues)
            {
                foreach (int value in intValues)
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Returns the set of all sets of integers such that the sum of
        /// every integer in a set raised to a power p equals a total t
        /// </summary>
        /// <param name="total">total</param>
        /// <param name="power">power</param>
        /// <returns></returns>
        static HashSet<HashSet<int>> ListOfInts(int total, int power)
        {
            int maxVal = (int) Math.Floor(Math.Pow(total, (1d / power)));
            int[] possibleNumbers = new int[maxVal];
            for(int i = 0; i < maxVal; i++)
            {
                possibleNumbers[i] = i + 1;
            }
            int[] usedNumbers = new int[0];
            HashSet<HashSet<int>> setOfSetOfInts = new HashSet<HashSet<int>>();
            return ListOfInts(total, power, 0, usedNumbers, possibleNumbers, setOfSetOfInts);
        } 
        /// <summary>
        /// Recursive private helper method for ListOfInts(total, power)
        /// Will add a working set to the return, which gets passed through all 
        /// recursive calls. 
        /// I am defining working set to mean:
        ///     a set of integers such that the sum of every integer raised to the defined power
        ///     adds up to the defined total.
        ///     
        /// To avoid extra work, every number in possible numbers, must be less than every number in 
        /// used numbers. This keeps the algorithem from finding the same set more than once. 
        /// 
        /// </summary>
        /// <param name="total">total value that will be added to</param>
        /// <param name="power">power every int will be raised to</param>
        /// <param name="sum">sum of every used int so far raised to power</param>
        /// <param name="usedNumbers">all numbers that make up a list that could be a working set</param>
        /// <param name="possibleNumbers">numbers that haven't been added to the used numbers list yet</param>
        /// <param name="setOfSetOfInts">wet of all working sets of integers</param>
        /// <returns></returns>
        private static HashSet<HashSet<int>> ListOfInts(int total, int power, int sum
            , int[] usedNumbers, int[] possibleNumbers, HashSet<HashSet<int>> setOfSetOfInts)
        {
            for(int i=0; i<possibleNumbers.Length; i++)
            {
                if (sum + Math.Pow(possibleNumbers[i], power) > total)
                    possibleNumbers[i] = 0;
            }

            HashSet<int> used = new HashSet<int>(usedNumbers);
            HashSet<int> poss = toHashSetNoZero(possibleNumbers);

            foreach (int number in poss)
            {
                HashSet<int> potentialWorkingSet = new HashSet<int>(used);
                potentialWorkingSet.Add(number);
                if (sum + Math.Pow(number, power) == total)
                    setOfSetOfInts.Add(potentialWorkingSet); //add a working set

                else
                {
                    //calculate new values
                    int newSum = sum + (int)Math.Pow(number, power);
                    int[] newUsedNumbers = potentialWorkingSet.ToArray();
                    int[] newPossibleNumbers = removeGreaterThan(poss, number).ToArray();
                    //recursive call
                    ListOfInts(total, power ,newSum, newUsedNumbers, newPossibleNumbers, setOfSetOfInts);
                }

            }
            return setOfSetOfInts;
        }
        /// <summary>
        /// private helper
        /// returns the int array as a HashSet of ints, without a zero, if present.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static HashSet<int> toHashSetNoZero(int[] array)
        {
            HashSet<int> set = new HashSet<int>();
            foreach(int i in array)
            {
                if (i != 0) set.Add(i);
            }
            return set;
        }
        /// <summary>
        /// returns a new set, that has removed any value that was greater than the defined value.
        /// </summary>
        /// <param name="set"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private static HashSet<int> removeGreaterThan(HashSet<int> set, int val)
        {
            HashSet<int> newSet = new HashSet<int>();
            foreach(int i in set)
            {
                if (i < val) newSet.Add(i);
            }
            return newSet;
        }
    }
}
