

using Microsoft.VisualBasic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
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

            if (input.ToLower() == "exit")
                break;
            else if (input.ToLower() == "")
            {
                Console.WriteLine("nothing was entered.");                
            }
            
            else
            try
            {
                var tokens = Tokenize(input);
                double result = EvaluateTokens(tokens);
                Console.WriteLine($"The result is {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static List<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();
        int start;
        int i = 0;

        while (i < input.Length)
        {
            if (char.IsDigit(input[i]) || input[i] == '.')
            {
                start = i;
                while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.'))
                    i++;
                tokens.Add(new Token(TokenType.Number, input.AsMemory(start, i - start)));
            }
            else if (input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/')
            {
                tokens.Add(new Token(TokenType.Operator, input.AsMemory(i, 1)));
                i++;
            }
            else if (!char.IsWhiteSpace(input[i]))
            {
                throw new ArgumentException($"Invalid character in expression: '{input[i]}'");
            }
            else
            {
                i++;
            }
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
                case TokenType.Operator:
                case TokenType.EOF:
                    result = ApplyOperation(result, currentNumber, currentOperator);
                    currentOperator = token.Type == TokenType.Operator ? token.Value.Span[0] : '+';
                    break;
            }
        }
        return result;
    }

    static double ApplyOperation(double left, double right, char op)
    {
        switch (op)
        {
            case '+': return left + right;
            case '-': return left - right;
            case '*': return left * right;
            case '/':
                if (right == 0)
                    throw new DivideByZeroException("Cannot divide by zero");
                return left / right;
            default: throw new ArgumentException($"Invalid operator: '{op}'");
        }
    }
}

