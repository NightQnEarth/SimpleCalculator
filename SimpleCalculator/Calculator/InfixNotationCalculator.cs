using SimpleCalculator.NotationConverter;

namespace SimpleCalculator.Calculator
{
    public static class InfixNotationCalculator
    {
        public static double Calculate(string infixNotationExpression)
        {
            var infixExpressionTokens = InfixExpressionPreprocessor.Tokenize(infixNotationExpression);
            var postfixNotationExpression = InfixToPostfixNotationConverter.Convert(infixExpressionTokens);

            return PostfixNotationCalculator.Calculate(postfixNotationExpression);
        }
    }
}