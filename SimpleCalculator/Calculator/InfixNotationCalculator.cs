using SimpleCalculator.NotationConverter;

namespace SimpleCalculator.Calculator
{
    public static class InfixNotationCalculator
    {
        public static double Calculate(string infixNotationExpression)
        {
            var infixExpressionTokens = InfixExpressionPreprocessor.Tokenize(infixNotationExpression);
            var infixToPostfixConverter = new InfixToPostfixNotationConverter(infixExpressionTokens);
            var postfixNotationExpression = infixToPostfixConverter.Convert();

            return PostfixNotationCalculator.Calculate(postfixNotationExpression);
        }
    }
}