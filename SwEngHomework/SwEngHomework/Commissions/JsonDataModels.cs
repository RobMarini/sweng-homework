using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwEngHomework.Commissions
{
    public class ComissionData
    {
        public List<Advisor> Advisors { get; set; }
        public List<Account> Accounts { get; set; }
    }
    public class Advisor
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public double CompensationRate {
            get
            {
                switch (this.Level.ToLower())
                {
                    case "senior":
                        return 1;
                    case "experienced":
                        return .5;
                    case "junior":
                        return .25;
                    default:
                        return 0;
                }
            }
        }
    }
    public class Account
    {
        public string Advisor { get; set; }
        public int PresentValue { get; set; }
    }
}
