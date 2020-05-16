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
            var notationContainsOperator = false;

            foreach (var token in postfixNotationExpression)
                if (double.TryParse(token, out var operand))
                    computeStack.Push(operand);
                else if (arithmeticOperations.ContainsKey(token))
                {
                    notationContainsOperator = true;

                    if (computeStack.Count < 2)
                        throw new ArgumentException($"cannot found operand to process '{token}' operator.");

                    var rightOperand = computeStack.Pop();
                    var leftOperand = computeStack.Pop();

                    if (token is "/" && rightOperand is 0)
                        throw new DivideByZeroException("you cannot divide by zero.");

                    computeStack.Push(arithmeticOperations[token](leftOperand, rightOperand));
                }
                else
                    throw new ArgumentException($"invalid token was detected \"{token}\".");

            if (!notationContainsOperator)
                throw new ArgumentException("cannot found operator in passed expression.");

            if (computeStack.Count > 1)
                throw new ArgumentException("was found excess operand.");

            return computeStack.Pop();
        }
    }
}