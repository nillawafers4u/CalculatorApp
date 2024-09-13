


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

                
            // program shutdown
            if (input == "exit") break;





            //creating the int array that will hold all the numbers.
            string[] values = input.Split('/', '-', '+', '*');
            int [] numbers = Array.ConvertAll(values, int.Parse);



            //creates list of operators
            List<char> operators = operatorDetector(input);


            //calculation answer
            int result = calculation(numbers, operators);

            //Then uses the result from operatorDetector to actually do math ad print the result to the user.
            Console.WriteLine(result);
            

        }
            







        int calculation(int[] numbers, List<char> operators)
        {
            int answer = numbers[0];
            int secondCounter = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                char c = operators.ElementAt(secondCounter);
                if(c != '\u0000' && i < operators.Count)
                {
                    switch (c)
                    {
                        case '-':
                            answer -= numbers[i + 1]; break;
                        case '+':
                            answer -= numbers[i + 1]; break;
                        case '/':
                            answer -= numbers[i + 1]; break;
                        case '*':
                            answer -= numbers[i + 1]; break;
                    }     

                }
                    
                                        
            }
            return answer;
        }





        //produces list of operators from the users input
        List<char> operatorDetector(string input)
        {

            List<char> list = new List<char>();
            foreach (char i in input)
            {
                switch (i)
                {
                    case '-':
                        list.Add(i); break;
                    case '+':
                        list.Add(i); break;
                    case '/':
                        list.Add(i); break;
                    case '*':
                        list.Add(i); break;
                }
            }
            return list;
            
        }
    }
}

