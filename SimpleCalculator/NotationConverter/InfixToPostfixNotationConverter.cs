using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace SimpleCalculator.NotationConverter
{
    public class InfixToPostfixNotationConverter
    {
        private readonly IEnumerable<string> infixArithmeticExpression;
        private readonly Stack<string> operatorsStack = new Stack<string>();
        private readonly List<string> postfixNotation = new List<string>();

        public InfixToPostfixNotationConverter(IEnumerable<string> infixArithmeticExpression) =>
            this.infixArithmeticExpression = infixArithmeticExpression;

        public IEnumerable<string> Convert()
        {
            var tokenIndex = 0;
            string previousToken = null;

            foreach (var token in infixArithmeticExpression)
            {
                if (IsOperandAfterCloseBracket(token, previousToken) ||
                    IsOperandBeforeOpenBracket(token, previousToken))
                    throw new ArgumentException("cannot found operator between bracket and operand.");

                if (CalculatorHelper.Operators.Contains(token))
                    ProcessOperator(token);
                else if (token is CalculatorHelper.OpenBracket)
                    operatorsStack.Push(token);
                else if (token is CalculatorHelper.CloseBracket)
                    ProcessCloseBracket();
                else if (!token.Contains(',') && double.TryParse(token, out _))
                    postfixNotation.Add(token);
                else
                    throw new ArgumentException($"invalid token \"{token}\" in [{tokenIndex}] position.");

                tokenIndex++;
                previousToken = token;
            }

            while (operatorsStack.TryPop(out var poppedOperator))
            {
                if (poppedOperator is CalculatorHelper.OpenBracket)
                    throw new ArithmeticException("was found excess open bracket.");

                postfixNotation.Add(poppedOperator);
            }

            return postfixNotation;
        }

        private static bool IsOperandAfterCloseBracket(string token, string previousToken) =>
            double.TryParse(token, out _) &&
            previousToken is CalculatorHelper.CloseBracket;

        private static bool IsOperandBeforeOpenBracket(string token, string previousToken) =>
            double.TryParse(previousToken, out _) &&
            token is CalculatorHelper.OpenBracket;

        private void ProcessOperator(string operatorSymbol)
        {
            while (operatorsStack.TryPeek(out var peekedOperator) && peekedOperator != CalculatorHelper.OpenBracket)
            {
                if (operatorSymbol.IsOperatorPriorityThan(peekedOperator))
                    break;

                postfixNotation.Add(operatorsStack.Pop());
            }

            operatorsStack.Push(operatorSymbol);
        }

        private void ProcessCloseBracket()
        {
            var wasFoundOpenBracket = false;

            while (operatorsStack.TryPop(out var poppedOperator))
            {
                if (poppedOperator == CalculatorHelper.OpenBracket)
                {
                    wasFoundOpenBracket = true;
                    break;
                }

                postfixNotation.Add(poppedOperator);
            }

            if (!wasFoundOpenBracket)
                throw new ArithmeticException("was found excess close bracket.");
        }
    }
}