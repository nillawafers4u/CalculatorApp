


using System.Diagnostics.Metrics;

internal class Program
{
    private static void Main(string[] args)
    {

        while(true) 
        {
        string answer;
        Console.WriteLine("please input a simple math problem. Examples: 5*5, 124/76, 4784+272 etc.");

        //receives input from user
        string input = Console.ReadLine();

            if (input == "exit") break;

        //Looks for an operator inside of the input
        char i = operatorDetector(input);
        if (i == 'n')
        {
            Console.WriteLine("sorry, you must've not input an operator. Please try again.");
        }
        else
        {
        // calculates the answer based on the input being split into an array by the operator.
        // Then uses the result from operatorDetector to decide what it will do with the numbers.
            Console.WriteLine(calculation(input, i));
        }
        }







        int calculation(string input, char i)
        {
            string[] values = input.Split('/', '-', '+', '*');
            int bigAnswer;
            int[] numbers = new int[values.Length];

            for (int counter = 0; counter >= values.Length; counter++)
            {

                int.TryParse(values[counter], out numbers[counter]);
                int.TryParse(values[1], out int second);
                int answer;


                switch (i)
                {
                    case '-':
                        answer = numbers[counter] - numbers[counter+1]; break;
                    case '+':
                        answer = numbers[counter] - numbers[counter + 1]; break;
                    case '/':
                        answer = numbers[counter] - numbers[counter + 1]; break;
                    case '*':
                        answer = numbers[counter] - numbers[counter + 1]; break;
                }
            }
            return bigAnswer;

        }





        //searches for operator in the input
        char operatorDetector(string input)
        {
            char c = '';
            foreach (char i in input)
            {
                switch (i)
                {
                    case '-':
                        c = i; break;
                    case '+':
                        c = i; break;
                    case '/':
                        c = i; break;
                    case '*':
                        c = i; break;
                }
            }
            return c;
        }
    }
}