using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCalculator.NotationConverter
{
    public static class InfixExpressionPreprocessor
    {
        public static IEnumerable<string> Tokenize(string infixExpression)
        {
            var builder = new StringBuilder();

            foreach (var symbol in infixExpression)
                if (IsSpecialSymbol(symbol.ToString()))
                {
                    builder.Append(' ');
                    builder.Append(symbol);
                    builder.Append(' ');
                }
                else
                    builder.Append(symbol);

            return builder.ToString().Split(' ').Where(token => !string.IsNullOrEmpty(token));
        }

        private static bool IsSpecialSymbol(string symbol) =>
            CalculatorHelper.Operators.Contains(symbol) ||
            symbol is CalculatorHelper.OpenBracket ||
            symbol is CalculatorHelper.CloseBracket;
    }
}