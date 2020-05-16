using System;
using System.Collections.Generic;
using CommandLine;

namespace SimpleCalculator.CmdClient
{
    public static class CommandLineClient
    {
        // ReSharper disable once ParameterTypeCanBeEnumerable.Global
        // As an input parameter can only be passed command-line arguments from Main(string[] args).
        public static CalculatorOptions GetOptions(string[] args)
        {
            CalculatorOptions calculatorOptions = null;

            Parser.Default.ParseArguments<CalculatorOptions>(args)
                .WithParsed(inputOptions => calculatorOptions = inputOptions)
                .WithNotParsed(HandleParseErrors);

            return calculatorOptions;
        }

        private static void HandleParseErrors(IEnumerable<Error> errors)
        {
            var exitCode = 0;

            foreach (var error in errors)
            {
                Console.Error.WriteLine(error);
                exitCode = (int)error.Tag;
            }

            Environment.Exit(exitCode);
        }
    }
}