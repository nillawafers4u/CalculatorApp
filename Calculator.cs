

using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Text.Json;

internal class Program
{
    new FormatException 
    private static void Main(string[] args)
    {

        while(true) 
        {
            
            Console.WriteLine("please input a simple math problem. Examples: 5*5, 124/76, 4784+272 etc.");

            //receives input from user
            var input = Console.ReadLine();
            var portion = input?[1..4];
            new Token (TokenType.Number, 1, 1, 1);
            //program shutdown
            if (input?.ToLower() is null or "exit")
                break;

            input = input.Trim();
            var numberLength = GetNumberLength(input);
            var result = double.Parse(input.Substring(0, numberLength));
            input = input.Substring(numberLength).Trim();
            while (input.Length > 0)
            {
                var op = input[0];
                input = input.Substring(1).Trim();
                numberLength = GetNumberLength(input);
                var operand = double.Parse(input.Substring(0, numberLength));
                input = input.Substring(numberLength).Trim();
                result = Calculate(result, op, operand);
            }

        }


        static double Calculate(double[] numbers, List<char> operators)
        {
            if (numbers.Length == 0)
                return 0;

            double result = numbers[0];

            for (int i = 0; i < operators.Count; i++)
            {
                result = operators[i] switch
                {
                    '+' => result += numbers[i + 1],
                    '-' => result -= numbers[i + 1],
                    '*' => result *= numbers[i + 1],
                    '/' => result /= numbers[i + 1],  
                };
            }

            return result;
        }
    }


        //extracts the numbers to an double array so that they can be worked
        static double[] ExtractNumbers(string input) => Regex.Split(input, @"[-+*/]").Select(double.Parse).ToArray();
        


        //produces list of operators from the users input
        static List<char> ExtractOperators(string input) => input.Where(c => "+-*/".Contains(c)).ToList();
        

    
}
