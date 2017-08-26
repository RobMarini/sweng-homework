using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SwEngHomework.DescriptiveStatistics
{
    public class StatsCalculator : IStatsCalculator
    {
        public Stats Calculate(string semicolonDelimitedContributions)
        {
            const int DECIMALPLACES = 2;
            //base case is to return 0 for empty, which is handled by the constructor
            var returnVal = new Stats();

            //using chaining here to combine multiple operations that map our data to a reasonable structure (a sorted IEnumerable implementation)
            var contributions = semicolonDelimitedContributions
                .Split(';')
                .Select(s => Sanitize(s))
                .Where(s => s != null)
                .Select(s => (double)s)
                .OrderBy(s => s)
                .ToArray(); //Choosing array for access performance and for indexing later on, since we will not mutate this data going forward
            //guard against divide by zero
            if (contributions.Count() > 0)
            {
                returnVal.Average = Math.Round(contributions.Sum() / contributions.Count(), DECIMALPLACES);
                if(contributions.Count() % 2 == 0)
                {
                    returnVal.Median = Math.Round((contributions[(int)(contributions.Count() / 2)] + contributions[(int)((contributions.Count() / 2) - 1)])/2, DECIMALPLACES);
                }
                else
                {
                    returnVal.Median = Math.Round(contributions[(int)(contributions.Count() / 2)], DECIMALPLACES);
                }
                //we could use indexing here, but the linq extension makes the formula we are using more obvious
                returnVal.Range = Math.Round(contributions.Last() - contributions.First(), DECIMALPLACES); 
            }
            return returnVal;
        }

        private static double? Sanitize(string input)
        {
            Regex numberPattern = new Regex(@"(\d|\.|,)+");
            if (numberPattern.IsMatch(input))
            {
                return Convert.ToDouble(numberPattern.Match(input).Value);
            }
            else
            {
                return null;
            }
        }
    }
}
