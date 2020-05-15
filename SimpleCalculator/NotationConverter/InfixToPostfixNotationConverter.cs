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
        private int tokenIndex;

        public InfixToPostfixNotationConverter(IEnumerable<string> infixArithmeticExpression) =>
            this.infixArithmeticExpression = infixArithmeticExpression;

        public IEnumerable<string> Convert()
        {
            foreach (var token in infixArithmeticExpression)
            {
                if (CalculatorHelper.Operators.Contains(token))
                    ProcessOperator(token);
                else if (token is CalculatorHelper.OpenBracket)
                    operatorsStack.Push(token);
                else if (token is CalculatorHelper.CloseBracket)
                    ProcessCloseBracket();
                else if (double.TryParse(token, out _))
                    postfixNotation.Add(token);
                else
                    throw new ArgumentException($"Invalid token \"{token}\" in [{tokenIndex}] position.");

                tokenIndex++;
            }

            while (operatorsStack.TryPop(out var poppedOperator))
                postfixNotation.Add(poppedOperator);

            return postfixNotation;
        }

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
            while (operatorsStack.TryPop(out var poppedOperator) && poppedOperator != CalculatorHelper.OpenBracket)
                postfixNotation.Add(poppedOperator);
        }
    }
}