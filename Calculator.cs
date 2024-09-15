


using System.Diagnostics.Metrics;

internal class Program
{
    private static void Main(string[] args)
    {

        while(true) 
        {
            
            Console.WriteLine("please input a simple math problem. Examples: 5*5, 124/76, 4784+272 etc.");

            //receives input from user
            string input = Console.ReadLine();

            //program shutdown
            if (input?.ToLower() == "exit")
                break;

            //creating the int array that will hold all the numbers.
            double [] numbers = extractNumbers(input);

            //creating a list to hold all of the operators
            List<char> operators = extractOperators(input);

            //checks to ensure that the user followed the normal math format
            if (numbers.Length != operators.Count + 1)
            {
                Console.WriteLine("Invalid input. Please ensure the format is correct.");
                continue;
            }

            //calculates the answer
            double result = calculate(numbers, operators);

            //Then uses the result from operatorDetector to actually do math ad print the result to the user.
            Console.WriteLine(result);
            

        }


        static double calculate(double[] numbers, List<char> operators)
        {
            if (numbers.Length == 0)
                return 0;

            double result = numbers[0];

            for (int i = 0; i < operators.Count; i++)
            {
                switch (operators[i])
                {
                    case '+':
                        result += numbers[i + 1];
                        break;
                    case '-':
                        result -= numbers[i + 1];
                        break;
                    case '*':
                        result *= numbers[i + 1];
                        break;
                    case '/':
                        if (numbers[i + 1] == 0)
                            throw new DivideByZeroException("Cannot divide by zero");
                        result /= numbers[i + 1];
                        break;
                    default:
                        throw new ArgumentException($"Invalid operator: {operators[i]}");
                }
            }

            return result;
        }
    }



        static double[] extractNumbers(string input)
        {
            string[] numbers = System.Text.RegularExpressions.Regex.Split(input, @"[-+*/]");
            return Array.ConvertAll(numbers, double.Parse);
        }


        //produces list of operators from the users input
        static List<char> extractOperators(string input)
        {
            return new List<char>(input.Where(c => "+-*/".Contains(c)));
        }
    
}
