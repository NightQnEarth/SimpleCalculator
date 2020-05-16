using System;
using FluentAssertions;
using NUnit.Framework;
using SimpleCalculator.Calculator;

namespace SimpleCalculator_Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("1+2", ExpectedResult = 3)]
        [TestCase("1 *30", ExpectedResult = 30)]
        [TestCase("24/ 2", ExpectedResult = 12)]
        [TestCase("1 - 2", ExpectedResult = -1)]
        [TestCase("10 *2 - 6/ 3", ExpectedResult = 18)]
        [TestCase("8/2/2", ExpectedResult = 2)]
        [TestCase("8/2/2*4", ExpectedResult = 8)]
        [TestCase("8*2*2/4", ExpectedResult = 8)]
        [TestCase("100 + 22", ExpectedResult = 122)]
        [TestCase("1/2", ExpectedResult = 0.5)]
        [TestCase("1/2-1", ExpectedResult = -0.5)]
        public double Calculate_OnValidIntegerExpression_ReturnsCorrectResult(string infixExpression) =>
            InfixNotationCalculator.Calculate(infixExpression);

        [TestCase("1.5*2", ExpectedResult = 3.0)]
        [TestCase("1.2+3.5", ExpectedResult = 4.7)]
        [TestCase("365.28 + 10.72", ExpectedResult = 376)]
        [TestCase(".4 + .6", ExpectedResult = 1)]
        [TestCase(".1 + 2", ExpectedResult = 2.1)]
        [TestCase("0. + 1", ExpectedResult = 1)]
        [TestCase("0 * .1324", ExpectedResult = 0)]
        public double Calculate_OnValidDoubleExpression_ReturnsCorrectResult(string infixExpression) =>
            InfixNotationCalculator.Calculate(infixExpression);

        [TestCase("((8*2)*2)/4", ExpectedResult = 8)]
        [TestCase("(8*2)*(2/4)", ExpectedResult = 8)]
        [TestCase("(10) + (2) - (3)", ExpectedResult = 9)]
        [TestCase("(5 + 10) / 5", ExpectedResult = 3)]
        [TestCase("1/(2-1)", ExpectedResult = 1)]
        [TestCase("(1+9)*(32-12)", ExpectedResult = 200)]
        [TestCase("50*(2/4)-25", ExpectedResult = 0)]
        public double Calculate_OnValidBracketExpression_ReturnsCorrectResult(string infixExpression) =>
            InfixNotationCalculator.Calculate(infixExpression);

        [Test]
        public void Calculate_OnDivideByZero_ThrowsDivideByZeroException()
        {
            Action calculator = () => InfixNotationCalculator.Calculate("5 / 0");
            calculator.Should().Throw<DivideByZeroException>().WithMessage("you cannot divide by zero.");
        }

        [Test]
        public void Calculate_OnNull_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => InfixNotationCalculator.Calculate(null));

        [TestCase("qwerty")]
        [TestCase("  \t\v")]
        [TestCase("1 + #")]
        public void Calculate_OnInvalidExpression_ThrowsArgumentException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("invalid token"));
        }

        [TestCase("123456")]
        [TestCase("10 2")]
        [TestCase("1 2 3")]
        public void Calculate_OnMissedOperator_ThrowsArgumentException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArgumentException>()
                .WithMessage("cannot found operator in passed expression.");
        }

        [TestCase("10 / 2 3")]
        [TestCase("10 2 - 3")]
        public void Calculate_OnExcessOperand_ThrowsArgumentException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArgumentException>()
                .WithMessage("was found excess operand.");
        }

        [TestCase("1 +")]
        [TestCase("*3")]
        [TestCase("4 * 5+")]
        [TestCase("4**5")]
        [TestCase("4//5")]
        [TestCase("-10 + 22")]
        [TestCase("4 + -5")]
        [TestCase("(-10 + 22) * (-2)")]
        public void Calculate_OnMissedOperand_ThrowsArgumentException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("cannot found operand"));
        }

        [TestCase("1,5*2")]
        [TestCase("1.2+3,5")]
        [TestCase("3, + 1")]
        [TestCase(",45 + 1")]
        [TestCase("2.,1 + 1")]
        [TestCase("32..13 + 1")]
        [TestCase("32.1.3 + 1")]
        [TestCase("3 ,0 + 1")]
        [TestCase("4 * ,5")]
        public void Calculate_OnInvalidDoubleExpression_ThrowsArgumentException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("invalid token"));
        }

        [TestCase(")5 * 4)")]
        [TestCase(")5 - 4(")]
        [TestCase("5 + 4)")]
        [TestCase("(5 + 3) * 4)")]
        public void Calculate_OnExcessCloseBracket_ThrowsArithmeticException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArithmeticException>()
                .WithMessage("was found excess close bracket.");
        }

        [TestCase("(5 + 4")]
        [TestCase("((5 + 3) * 4")]
        public void Calculate_OnExcessOpenBracket_ThrowsArithmeticException(string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArithmeticException>()
                .WithMessage("was found excess open bracket.");
        }

        [TestCase("3( - 2")]
        [TestCase("(5 / 4(")]
        [TestCase("5( + 3)")]
        [TestCase("()5 - 4")]
        [TestCase("5 (+) 3")]
        [TestCase("(5 + )3")]
        public void Calculate_WhenNoOperatorBetweenBracketAndOperand_ThrowsArgumentException(
            string invalidExpression)
        {
            Action calculator = () => InfixNotationCalculator.Calculate(invalidExpression);
            calculator.Should().Throw<ArgumentException>()
                .WithMessage("cannot found operator between bracket and operand.");
        }
    }
}