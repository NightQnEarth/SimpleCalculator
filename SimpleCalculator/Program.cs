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

            try
            {
                var expressionResult = InfixNotationCalculator.Calculate(calculatorOptions.ArithmeticExpression);

                Console.WriteLine(expressionResult);
            }
            catch (Exception exception)
            {
                var errorMessage = string.Concat("Exception was thrown:",
                                                 Environment.NewLine,
                                                 char.ToUpper(exception.Message[0]),
                                                 exception.Message.Substring(1));

                Console.WriteLine(errorMessage);
            }
        }
    }
}