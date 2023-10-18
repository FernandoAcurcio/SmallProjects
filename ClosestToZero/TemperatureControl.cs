using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestToZero
{
    public class TemperatureControl
    {
        private List<float> _temperatures;

        // constructor
        public TemperatureControl()
        {
            _temperatures = new List<float>();
        }

        public void Run()
        {
            var tempAux = true;
            do
            {
                DisplayMenu();
                tempAux = float.TryParse(Console.ReadLine(), out float temp);

                if (tempAux)
                    _temperatures.Add(temp);
            } while (tempAux);
            Console.WriteLine($"The closest number to zero is: {CalculateClosest()}");
            Console.ReadLine();
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Insert a new temperature, use ',' as dot sign.");
            Console.WriteLine("If you want to stop press other key.");
        }

        /// <summary>
        /// This code sort the list of numbers by their math absolute value in ascending order,
        /// then if there is two equal numbers by is descending order,
        /// then returns the first number of the list, favoring positive numbers
        /// </summary>
        /// <returns></returns>
        private float CalculateClosest()
        {
            return _temperatures.OrderBy(x => Math.Abs(x)).ThenByDescending(x => x).First();
        }
    }

}
