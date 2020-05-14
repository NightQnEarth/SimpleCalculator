using System;
using CommandLine;

namespace SimpleCalculator.CmdClient
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // CalculatorOptions class instantiated by reflection in CommandLineClient.GetOptions().
    public class CalculatorOptions
    {
        private string arithmeticExpression;

        [Value(0, MetaName = "arithmetic_expression",
               Required = true,
               HelpText = "Type arithmetic expression you want to calculate. For example: 1 + (3 - 5) * 8 / 7")]
        public string ArithmeticExpression
        {
            get => arithmeticExpression;
            set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Was passed invalid arithmetic expression.");

                arithmeticExpression = value;
            }
        }
    }
}