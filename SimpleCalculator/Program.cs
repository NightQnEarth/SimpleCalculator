using System;
using SimpleCalculator.Calculator;
using SimpleCalculator.CmdClient;

namespace SimpleCalculator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var calculatorOptions = CommandLineClient.GetOptions(args);
            var expressionResult = InfixNotationCalculator.Calculate(calculatorOptions.ArithmeticExpression);

            Console.WriteLine(expressionResult);
        }
    }
}