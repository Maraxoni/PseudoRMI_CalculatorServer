using CoreWCF;
using System.Data;

namespace PseudoRMI_CalculatorServer
{
    public class CalculatorService : ICalculatorService
    {
        public double Calculate(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input shouldn't be empty.");

            input = input.Trim();

            char[] operators = { '+', '-', '*', '/' };

            // Find operator
            foreach (char op in operators)
            {
                int index = input.IndexOf(op);
                if (index != -1)
                {
                    // Extract numbers
                    string leftPart = input.Substring(0, index).Trim();
                    string rightPart = input.Substring(index + 1).Trim();

                    // Parse
                    if (!double.TryParse(leftPart, out double leftValue))
                        throw new ArgumentException("Wrong left number.");

                    if (!double.TryParse(rightPart, out double rightValue))
                        throw new ArgumentException("Wrong right number.");

                    // Calculate
                    switch (op)
                    {
                        case '+':
                            return Add(leftValue, rightValue);
                        case '-':
                            return Subtract(leftValue, rightValue);
                        case '*':
                            return Multiply(leftValue, rightValue);
                        case '/':
                            return Divide(leftValue, rightValue);
                        default:
                            throw new InvalidOperationException("Invalid operator.");
                    }
                }
            }

            throw new ArgumentException("Couldn't find operator.");
        }
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b)
        {
            if (b == 0) throw new FaultException("Can't divide by zero.");
            return (double)a / b;
        }
    }
}
