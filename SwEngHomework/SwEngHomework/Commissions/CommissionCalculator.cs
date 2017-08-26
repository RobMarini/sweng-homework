using System;
using System.Collections.Generic;
using System.Linq;

namespace SwEngHomework.Commissions
{
    public class CommissionCalculator : ICommissionCalculator
    {
        public IDictionary<string, double> CalculateCommissionsByAdvisor(string jsonInput)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ComissionData>(jsonInput);
            Dictionary<string, double> returnVal = new Dictionary<string, double>();
            foreach(var employee in data.Advisors)
            {
                double totalFees = data.Accounts
                    .Where(s => s.Advisor == employee.Name)
                    .Select(s => ComputeAccountFee(s.PresentValue))
                    .Sum(s => s);
                returnVal.Add(employee.Name, Math.Round(totalFees * employee.CompensationRate, 2));
            }
            return returnVal;
        }
        private double ComputeAccountFee(double totalAccValue)
        {
            //define magic numbers
            const double firstTierBps = .0005;
            const double secondTierBps = .0006;
            const double thirdTierBps = .0007;
            const int firstTierLimit = 50000;
            const int secondTierLimit = 100000;
            double accountFee = 0;
            if (totalAccValue > secondTierLimit)
            {
                accountFee = totalAccValue * thirdTierBps;
            }
            else if (totalAccValue > firstTierLimit)
            {
                accountFee = totalAccValue * secondTierBps;
            }
            else
            {
                accountFee = totalAccValue * firstTierBps;
            }
            return accountFee;
        }
    }
}
