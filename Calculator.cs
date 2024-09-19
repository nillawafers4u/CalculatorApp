

using Microsoft.VisualBasic;
using System.Diagnostics.SymbolStore;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace Calculator;



internal class Program
{
    

    

   

    private static void Main(string[] args)
    {


        while (true)
        {

            Console.WriteLine("please input a simple math problem. Examples: 5*5, 124/76, 4784+272 etc.");

            // receives input from user
            var inputString = Console.ReadLine();


            //program shutdown
            if (inputString?.ToLower() is null or "exit")
                break;

            inputString = inputString.Trim();




            for (int i = 0; i < inputString.Length; i++)

            {
                var numberLength = GetNumberLength(inputString);

                var result = double.Parse(inputString.Substring(0, numberLength));
                inputString = inputString.Substring(numberLength).Trim();
                while (inputString.Length > 0)
                {
                    char op = 'a';
                    if (numberLength >= 1)
                    {
                        
                        op = char.Parse(inputString.Substring(numberLength + 1, 1)) switch
                        {
                            '+' => op = '+',
                            '-' => op = '-',
                            '*' => op = '*',
                            '/' => op = '/',
                        };
                    }

                    inputString = inputString.Substring(1).Trim();
                    numberLength = GetNumberLength(inputString);
                    var operand = double.Parse(inputString.Substring(0, numberLength));
                    inputString = inputString.Substring(numberLength).Trim();
                
                    result = Calculate(result, op, operand);
                    
                }

            }


            static double Calculate(double result, char op, double operand)
            {



                result = op switch
                {
                    '+' => result += operand,
                    '-' => result -= operand,
                    '*' => result *= operand,
                    '/' => result /= operand,
                };


                return result;
            }





            //extracts the numbers to an double array so that they can be worked
            static double[] ExtractNumbers(string input) => Regex.Split(input, @"[-+*/]").Select(double.Parse).ToArray();



            //produces list of operators from the users input
            static List<char> ExtractOperators(string input) => input.Where(c => "+-*/".Contains(c)).ToList();

            
            
            static int GetNumberLength(string input)
            {
                int result = 0;


                foreach (char c in input)
                {
                    if (c is '+' or '-' or '/' or '*' or ' ')
                    {
                        break;
                    }

                    result += 1;
                }

                return result;


            }

        }
    }
}
