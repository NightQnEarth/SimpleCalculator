using System;
using System.Collections.Generic;

namespace SimpleCalculator.Calculator
{
    public static class PostfixNotationCalculator
    {
        private static readonly Dictionary<string, Func<int, int, int>> arithmeticOperations =
            new Dictionary<string, Func<int, int, int>>
            {
                ["+"] = (a, b) => a + b,
                ["-"] = (a, b) => a - b,
                ["*"] = (a, b) => a * b,
                ["/"] = (a, b) => a / b
            };

        public static int Calculate(IEnumerable<string> postfixNotationExpression)
        {
            var computeStack = new Stack<int>();

            foreach (var token in postfixNotationExpression)
                if (int.TryParse(token, out var operand))
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