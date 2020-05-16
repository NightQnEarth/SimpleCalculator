using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// CalculatorOptions class and it properties instantiated by reflection in CommandLineClient.GetOptions().

namespace SimpleCalculator.CmdClient
{
    public class CalculatorOptions
    {
        [Value(0, MetaName = "infix_notation_arithmetic_expression",
               Required = true,
               HelpText = "Type infix notation arithmetic expression you want to calculate." +
                          "\r\n" +
                          "For example: (1.5 + 8.5) * (32-12) / 2")]
        public string ArithmeticExpression { get; set; }
    }
}