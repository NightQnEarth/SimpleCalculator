using System;
using System.Linq;

namespace SimpleCalculator.NotationConverter
{
    public static class CharExtensions
    {
        public static bool IsOperatorPriorityThan(this string firstOperator, string secondOperator)
        {
            if (!CalculatorHelper.Operators.Contains(firstOperator) &&
                !CalculatorHelper.Operators.Contains(secondOperator))
                throw new ArgumentException("at less one of passed operators is not operator.");

            return (firstOperator is "*" || firstOperator is "/") && (secondOperator is "+" || secondOperator is "-");
        }
    }
}