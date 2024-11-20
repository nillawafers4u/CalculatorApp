

using Microsoft.VisualBasic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Net.Security;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace Calculator;



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
                var ast = ParseExpression(tokens);
                var result = Evaluate(ast);
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
        int parenthesisBalance = 0;

        while (!input.IsEmpty)
        {
            var span = input.Span;

            if (char.IsWhiteSpace(span[0]))
            {
                input = input[1..];
                continue;
            }

            if (span[0] is '+' or '-' or '*' or '/' or '(' or ')')
            {
                //Makes sure that we actually have the correct amount of open and closes.
                if (span[0] == '(')
                    parenthesisBalance++;
                else if (span[0] == ')')
                {
                    parenthesisBalance--;
                    if (parenthesisBalance < 0)
                        throw new ArgumentException("Unbalanced parentheses. You have closing parenthesis without matching opening parenthesis");
                }


                tokens.Add(new Token(TokenType.Operator, input[..1]));
                input = input[1..];
                continue;
            }

            if (char.IsDigit(span[0]) || span[0] == '.')
            {
                int length = 1;
                var hasDecimal = false;

                if (span[0] == '.')
                {
                    hasDecimal = true;
                    //must have at least one digit after decimal point..

                    if (length + 1 >= input.Length || !char.IsDigit(span[length + 1]))
                        throw new ArgumentException("Invalid decimal format: missing digits after decimal point");
                }

                while (length < input.Length &&
                    (char.IsDigit(span[length]) ||
                    (span[length] == '.' && !hasDecimal)))
                {

                    if (span[length] == '.')
                    {
                        if (hasDecimal)
                            throw new ArgumentException("Invalid number format: multiple decimal points detected");


                        hasDecimal = true;
                    }

                    length++;
                }

                var numberSlice = input[..length];
                if (!double.TryParse(numberSlice.Span, out _))
                    throw new ArgumentException($"Invalid number format: '{numberSlice}'");


                tokens.Add(new Token(TokenType.Number, numberSlice));
                input = input[length..];
                continue;
            }

            throw new ArgumentException($"Invalid Character in expression: '{span[0]}'");

        }

        tokens.Add(new Token(TokenType.EOF, ReadOnlyMemory<char>.Empty));
        return tokens;
    }


    static AstNode ParseExpression(List<Token> tokens)
    {
        AstNode expression = ParseTerm(tokens);
        while (tokens.Count > 0 && tokens[0].Type == TokenType.Operator &&
            (tokens[0].Value.Span[0] == '+' || tokens[0].Value.Span[0] == '-')) /* <== this only works while there's another operator */
        {
            char op = tokens[0].Value.Span[0];
            tokens.RemoveAt(0);
            expression = new BinaryOperationNode(op, expression, ParseTerm(tokens));
        }

        return expression;
    }


    static AstNode ParseTerm(List<Token> tokens)
    {
        AstNode term = ParseFactor(tokens);

        while (tokens.Count > 0 && tokens[0].Type == TokenType.Operator && 
            (tokens[0].Value.Span[0] == '*' || tokens[0].Value.Span[0] == '/'))
        {
            char op = tokens[0].Value.Span[0];
            tokens.RemoveAt(0);
            term = new BinaryOperationNode(op, term, ParseFactor(tokens));
        }

        return term;
        //Term getting passed back up to ParseExpression ^^

    }

    //and ParseFactor finally get our first thing and it starts working it's way back up! ^^
    static AstNode ParseFactor(List<Token> tokens)
    {

        // Handles parentheses
        if (tokens[0].Type == TokenType.Operator && tokens[0].Value.Span[0] == '(')
        {
            tokens.RemoveAt(0); // Remove the '(' token
            AstNode expression = ParseExpression(tokens);

            //ensure we see a closing parentheses and remove it
            if (tokens.Count == 0 || tokens[0].Type != TokenType.Operator || tokens[0].Value.Span[0] != ')')
                throw new ArgumentException("Mismatched parentheses");


            tokens.RemoveAt(0); // Remove the ')' token
            return expression;
        }

        if (tokens[0].Type == TokenType.Number)
        {
            double value = double.Parse(tokens[0].Value.Span);

            tokens.RemoveAt(0);

            return new NumberNode(value); 
            //NumberNode getting passed back up to the ParseTerm Function ^^
        }

        throw new ArgumentException("Invalid syntax");
    }


    static double Evaluate(AstNode node) => node.Evaluate();

}

