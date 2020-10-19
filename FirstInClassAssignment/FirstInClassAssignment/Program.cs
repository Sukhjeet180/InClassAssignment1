using System;

namespace FirstInClassAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter numbers:");
            string input = Console.ReadLine();
            string[] arr = input.Split('+');
            int sum = 0;

            try
            {
                foreach (string x in arr)
                    sum += int.Parse(x);

                Console.WriteLine("Sum of the input elements is {0}", sum);
            }
            catch (Exception e)
            {
                Console.WriteLine("Incorrect Input");
            }
        }
    }
}
