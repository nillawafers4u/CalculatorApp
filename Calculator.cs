﻿

using Microsoft.VisualBasic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Net.Security;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace ConsoleApp1;



internal class Program
{

    private static void Main(string[] args)
    {

        while (true)
        {
            Console.Write("Enter a math problem (or 'exit' to quit): ");
            var input = Console.ReadLine();

            if (input == "exit")
                break;
            else if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("nothing was entered.");
                continue;
            }
            
            try
            {
                var tokens = Tokenize(input.AsMemory());
                double result = EvaluateTokens(tokens);
                Console.WriteLine($"The result is {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static List<Token> Tokenize(ReadOnlyMemory<char> input)
    {
        var tokens = new List<Token>();

        while (!input.IsEmpty)
        {
            var span = input.Span;

            if (char.IsWhiteSpace(span[0]))
            {
                input = input[1..];
                continue;
            }

            if (span[0] is '+' or '-' or '*' or '/')
            {
                tokens.Add(new Token(TokenType.Operator, input[..1]));
                input = input[1..];
                continue;
            }

            if (char.IsDigit(span[0]) || span[0] == '.')
            {
                int length = 1;
                bool hasDecimal = span[0] == '.';

                while (length < input.Length &&
                    (char.IsDigit(span[length]) ||
                    (span[length] == '.' && !hasDecimal)))
                {
                    if (span[length] == '.')
                        hasDecimal = true;
                    length++;
                }

                var numberSlice = input[..length];
                if (double.TryParse(numberSlice.Span, out _))
                {
                    tokens.Add(new Token(TokenType.Number, numberSlice));
                }
                else
                {
                    throw new ArgumentException($"Invalid number format: '{numberSlice}'");
                }

                input = input[length..];
                continue;
            }

            throw new ArgumentException($"Invalid Character in expression: '{span[0]}'");

        }

        tokens.Add(new Token(TokenType.EOF, ReadOnlyMemory<char>.Empty));
        return tokens;
    }

    static double EvaluateTokens(List<Token> tokens)
    {
        double result = 0;
        double currentNumber = 0;
        char currentOperator = '+';

        foreach (var token in tokens)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                    currentNumber = double.Parse(token.Value.Span);
                    break;
                case TokenType.Operator or TokenType.EOF:
                    result = ApplyOperation(result, currentNumber, currentOperator);
                    currentOperator = token.Type == TokenType.Operator ? token.Value.Span[0] : '+';
                    break;
            }
        }
        return result;
    }

    static double ApplyOperation(double left, double right, char op)
    {
        return op switch
        {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => right != 0 ? left / right :
            throw new ArgumentException($"Invalid operator: '{op}'"), _ => throw new ArgumentException()
        };
    }
}

