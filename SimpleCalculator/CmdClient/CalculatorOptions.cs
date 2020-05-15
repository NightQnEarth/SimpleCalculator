using CommandLine;

namespace SimpleCalculator.CmdClient
{
    // TODO: check it
    // ReSharper disable once ClassNeverInstantiated.Global
    // CalculatorOptions class instantiated by reflection in CommandLineClient.GetOptions().
    public class CalculatorOptions
    {
        [Value(0, MetaName = "infix_notation_arithmetic_expression",
               Required = true,
               HelpText = "Type infix notation arithmetic expression you want to calculate." +
                          "\n\r" +
                          "For example: (1 + 9) * (32-12)")]
        public string ArithmeticExpression { get; set; }
    }
}