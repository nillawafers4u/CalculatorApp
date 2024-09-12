


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







        int calculation(string input, char[] operators)
        {
            int answer = 0;
            string[] values = input.Split('/', '-', '+', '*');
            int bigAnswer = 0;
            int[] numbers = new int[values.Length];

            for (int counter = 0; counter >= values.Length; counter+=2)
            {

                int.TryParse(values[counter], out numbers[counter]);

                char c = operators[counter];
                switch (c)
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
                bigAnswer += answer;
            }
            return bigAnswer;

        }





        //searches for operator in the input
        char operatorDetector(string input)
        {
            char c = 'n';
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