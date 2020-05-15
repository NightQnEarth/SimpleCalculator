using System;
using SimpleCalculator.NotationConverter;

namespace SimpleCalculator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var infixExpressionTokens = InfixExpressionPreprocessor.Tokenize("(10 +2) *40 + 4");
            var infixToPostfixConverter = new InfixToPostfixNotationConverter(infixExpressionTokens);

            foreach (var word in infixToPostfixConverter.Convert())
                Console.Write(word + ' ');

            Console.WriteLine();
        }
    }
}