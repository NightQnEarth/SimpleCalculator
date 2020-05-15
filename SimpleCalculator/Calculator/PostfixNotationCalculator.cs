using System;
using System.Collections.Generic;

namespace SimpleCalculator.Calculator
{
    public static class PostfixNotationCalculator
    {
        private static readonly Dictionary<string, Func<double, double, double>> arithmeticOperations =
            new Dictionary<string, Func<double, double, double>>
            {
                ["+"] = (a, b) => a + b,
                ["-"] = (a, b) => a - b,
                ["*"] = (a, b) => a * b,
                ["/"] = (a, b) => a / b
            };

        public static double Calculate(IEnumerable<string> postfixNotationExpression)
        {
            var computeStack = new Stack<double>();

            foreach (var token in postfixNotationExpression)
                if (double.TryParse(token, out var operand))
                    computeStack.Push(operand);
                else if (arithmeticOperations.ContainsKey(token))
                {
                    var rightOperand = computeStack.Pop();
                    var leftOperand = computeStack.Pop();

                    computeStack.Push(arithmeticOperations[token](leftOperand, rightOperand));
                }
                else
                    throw new ArgumentException($"invalid token was detected \"{token}\".");

            return computeStack.Pop();
        }
    }
}