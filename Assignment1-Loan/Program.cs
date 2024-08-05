using Assignment1_Loan.CheckValidity;
using Spectre.Console;

namespace Assignment1_Loan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Console.WriteLine("Enter Principal Amount : ");
            user.PrincipalAmount = GetInputInt(user);

            Console.WriteLine("Enter Interest Percentage : ");
            user.InterestPerAnnum = GetInputDouble(user);

            Console.WriteLine("Enter Duration in Months : ");
            user.DurationInMonths = GetInputInt(user);

            double yearlyInterestAmount = Utility.GetYearlyInterestAmount(user);
            double monthlyInterestAmount = Utility.GetMonthlyInterestAmount(yearlyInterestAmount);
            double totalInterest = Utility.GetTotalInterest(user, monthlyInterestAmount);

            DisplayOutput(user, totalInterest, monthlyInterestAmount);
            Console.ReadLine();
        }

        private static void DisplayOutput(User user, double totalInterest, double interestPerMonth)
        {
            Table table = new Table();
            table.Border = TableBorder.Square;
            table.ShowRowSeparators = true;
            table.Alignment(Justify.Center);

            table.AddColumn("[green]Month No.[/]");
            table.AddColumn("[green]Principal.[/]");
            table.AddColumn("[green]Interest Amount[/]");

            for (int i = 1; i < user.DurationInMonths; ++i)
            {
                table.AddRow(i.ToString(), 0.ToString(), interestPerMonth.ToString());
            }
            table.AddRow(user.DurationInMonths.ToString(), user.PrincipalAmount.ToString(), interestPerMonth.ToString());
            AnsiConsole.Write(table);


            Console.WriteLine("Total Payable : " + Convert.ToDouble(totalInterest + user.PrincipalAmount));
        }

        public static int GetInputInt(User user)
        {
            var isValid = false;
            int inputNumber = default(int);
            while (!isValid)
            {
                try
                {
                    bool isParsed = int.TryParse(Console.ReadLine(), out inputNumber) && inputNumber > 0 && inputNumber < int.MaxValue;
                    isValid = isParsed == true;
                    if (isValid) return inputNumber;
                    else Console.WriteLine("** Invalid Input **");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return inputNumber;
        }

        public static double GetInputDouble(User user)
        {
            double inputNumber = default(double);
            var isValid = false;
            while (!isValid)
            {
                try
                {
                    bool isParsed = double.TryParse(Console.ReadLine(), out inputNumber) && inputNumber > 0 && inputNumber < double.MaxValue;
                    isValid = isParsed == true;
                    if (isValid) user.InterestPerAnnum = inputNumber;
                    else Console.WriteLine("** Invalid Input **");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return inputNumber;
        }
    }

    public class User
    {
        public int PrincipalAmount { get; set; }
        public double InterestPerAnnum { get; set; }
        public int DurationInMonths { get; set; }
    }

    public static class Utility
    {
        public static double GetYearlyInterestAmount(User user)
        {
            return (double)(user.PrincipalAmount * (double)user.InterestPerAnnum * 1) / 100;
        }

        public static double GetMonthlyInterestAmount(double yearlyInterestAmount)
        {
            return yearlyInterestAmount / (double)12;
        }

        public static double GetTotalInterest(User user, double monthlyInterestAmount)
        {
            return (double)monthlyInterestAmount * user.DurationInMonths;
        }
    }
}
