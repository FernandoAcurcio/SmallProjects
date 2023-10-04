using System.Runtime.Intrinsics.X86;

namespace Calculator;

/* This project is a improvement from the previous project Math Game
*  the intention with this project is to build a simple calculator
*  this calculater can do operations with decimal numbers.
*/
internal class Program
{
    static void Main(string[] args)
    {
        CalculatorHandler();
    }

    private static void CalculatorHandler()
    {
        do
        {
            StartMenu();
            var auxOperation = int.TryParse(Console.ReadLine(), out int op);

            if (op == 0)
                break;

            if (op < 0 || op > 4)
            {
                Console.WriteLine("Choose a valid Option");
            }    
            else
            {
                var values = ValidateNumbers();
                if (values.Item1 == 0 && op == 4)
                {
                    Console.WriteLine("It's impossible to divide by Zero!!!");
                }
                else
                {
                    Console.WriteLine($"Result: {Operations.Operation(values.Item1, values.Item2, op)}");
                }
            }

            Console.ReadLine();
            Console.Clear();
        }
        while (true);
    }

    private static (float, float) ValidateNumbers()
    {
        float numbA, numbB;

        while (true)
        {
            Console.Write("Insert a valid number A: ");
            if (float.TryParse(Console.ReadLine(), out numbA))
                break; // Exit the loop if a valid number is entered.
        }

        while (true)
        {
            Console.Write("Insert a valid number B: ");
            if (float.TryParse(Console.ReadLine(), out numbB))
                break; // Exit the loop if a valid number is entered.
        }

        return (numbA, numbB);
    }

    private static void StartMenu()
    {
        Console.WriteLine("--- Welcome to the calculator ---");
        Console.WriteLine("Choose from the following options");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1- Addition;");
        Console.WriteLine("2- Subtraction;");
        Console.WriteLine("3- Multiplication;");
        Console.WriteLine("4- Division;");
        //Console.WriteLine("5- Game History;");
        Console.WriteLine("Click 0 to exit");
    }
}