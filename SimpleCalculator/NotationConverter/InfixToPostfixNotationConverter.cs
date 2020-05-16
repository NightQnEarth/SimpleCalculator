using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace SimpleCalculator.NotationConverter
{
    public static class InfixToPostfixNotationConverter
    {
        public static IEnumerable<string> Convert(IEnumerable<string> infixArithmeticExpression)
        {
            var operatorsStack = new Stack<string>();
            var postfixNotation = new List<string>();
            string previousToken = null;
            var tokenIndex = 0;

            foreach (var token in infixArithmeticExpression)
            {
                if (IsOperandAfterCloseBracket(token, previousToken) ||
                    IsOperandBeforeOpenBracket(token, previousToken))
                    throw new ArgumentException("cannot found operator between bracket and operand.");

                if (ConverterHelper.Operators.Contains(token))
                    ProcessOperator(token, operatorsStack, postfixNotation);
                else if (token is ConverterHelper.OpenBracket)
                    operatorsStack.Push(token);
                else if (token is ConverterHelper.CloseBracket)
                    ProcessCloseBracket(operatorsStack, postfixNotation);
                else if (!token.Contains(',') && double.TryParse(token, out _))
                    postfixNotation.Add(token);
                else
                    throw new ArgumentException($"invalid token \"{token}\" in [{tokenIndex}] position.");

                tokenIndex++;
                previousToken = token;
            }

            FlushStack(operatorsStack, postfixNotation);

            return postfixNotation;
        }

        private static bool IsOperandAfterCloseBracket(string token, string previousToken) =>
            double.TryParse(token, out _) &&
            previousToken is ConverterHelper.CloseBracket;

        private static bool IsOperandBeforeOpenBracket(string token, string previousToken) =>
            double.TryParse(previousToken, out _) &&
            token is ConverterHelper.OpenBracket;

        private static void ProcessOperator(string operatorSymbol,
                                            Stack<string> operatorsStack,
                                            ICollection<string> postfixNotation)
        {
            while (operatorsStack.TryPeek(out var peekedOperator) && peekedOperator != ConverterHelper.OpenBracket)
            {
                if (operatorSymbol.IsOperatorPriorityThan(peekedOperator))
                    break;

                postfixNotation.Add(operatorsStack.Pop());
            }

            operatorsStack.Push(operatorSymbol);
        }

        private static void ProcessCloseBracket(Stack<string> operatorsStack, ICollection<string> postfixNotation)
        {
            var wasFoundOpenBracket = false;

            while (operatorsStack.TryPop(out var poppedOperator))
            {
                if (poppedOperator == ConverterHelper.OpenBracket)
                {
                    wasFoundOpenBracket = true;
                    break;
                }

                postfixNotation.Add(poppedOperator);
            }

            if (!wasFoundOpenBracket)
                throw new ArithmeticException("was found excess close bracket.");
        }

        private static void FlushStack(Stack<string> operatorsStack, ICollection<string> postfixNotation)
        {
            while (operatorsStack.TryPop(out var poppedOperator))
            {
                if (poppedOperator is ConverterHelper.OpenBracket)
                    throw new ArithmeticException("was found excess open bracket.");

                postfixNotation.Add(poppedOperator);
            }
        }
    }
}