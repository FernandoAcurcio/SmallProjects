using System.Numerics;
using System.Threading.Tasks;
using System;

namespace CarParkingSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parkinglot = new ParkingLot();
            parkinglot.Run();
        }
    }
}