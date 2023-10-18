using System;
using System.ComponentModel.Design;

namespace MathGame
{
    internal class Program
    {
        // A list to store the results of arithmetic operations.
        private static List<int> _resultOperationsList = new List<int>();

        static void Main(string[] args)
        {
            GameMenu();
        }

        // The main game menu where the user selects operations.
        private static void GameMenu()
        {
            int selectionNumber;
            do
            {
                selectionNumber = MenuManager();
                Console.ReadLine();
                Console.Clear();
            } while (selectionNumber != 0);
        }

        // Manages the menus, user input, and operation selection.
        private static int MenuManager()
        {
            DisplayMenu();
            if (int.TryParse(Console.ReadLine(), out int selectionNumber))
            {
                switch (selectionNumber)
                {
                    case 0:
                        return 0; // Exits the game if the user selects "0".
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        Console.Clear();
                        PerformOperation(selectionNumber);
                        return selectionNumber;
                    case 5:
                        Console.Clear();
                        return DisplayHistoryList();
                    default:
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        return 6;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return 6;
            }
        }

        // Displays the main menu options to the user.
        private static void DisplayMenu()
        {
            Console.WriteLine("Please choose an operation?");
            Console.WriteLine("1- Addition;");
            Console.WriteLine("2- Subtraction;");
            Console.WriteLine("3- Multiplication;");
            Console.WriteLine("4- Division;");
            Console.WriteLine("5- Game History;");
            Console.WriteLine("Click 0 to exit");
        }

        // Performs arithmetic operations based on user input.
        private static void PerformOperation(int operation)
        {
            string operationName = "";
            switch (operation)
            {
                case 1:
                    operationName = "add";
                    break;
                case 2:
                    operationName = "subtract";
                    break;
                case 3:
                    operationName = "multiply";
                    break;
                case 4:
                    operationName = "divide";
                    break;
            }

            Console.WriteLine($"Please insert the two numbers that you want to {operationName}.");
            if (int.TryParse(Console.ReadLine(), out int a) && int.TryParse(Console.ReadLine(), out int b))
            {
                switch (operation)
                {
                    case 1:
                        Console.WriteLine($"Result: {a + b}");
                        _resultOperationsList.Add(a + b);
                        break;
                    case 2:
                        Console.WriteLine($"Result: {a - b}");
                        _resultOperationsList.Add(a - b);
                        break;
                    case 3:
                        Console.WriteLine($"Result: {a * b}");
                        _resultOperationsList.Add(a * b);
                        break;
                    case 4:
                        if (b == 0)
                            Console.WriteLine("Division by zero is not allowed.");
                        else
                            Console.WriteLine($"Result: {a / b}");
                            _resultOperationsList.Add(a / b);
                        break;
                }
            }
            else
            {
                Console.WriteLine("You need to insert two integer numbers");
            }
        }

        // Displays the history of operations.
        private static int DisplayHistoryList()
        {
            for (int i = 0; i < _resultOperationsList.Count; i++)
                Console.WriteLine($"Operation {i+1}: {_resultOperationsList[i].ToString()}");
            return 5;
        }

        //  TODO: ADD CHALLENGES
        //  Implement levels of difficulty.
        //  Add a timer to track how long the user takes to finish the game.
        //  Add a function that let's the user pick the number of questions.
        //  Create a 'Random Game' option where the players will be presented with questions from random operations
    }
}