using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSimulator
{
    //        Help us design a parking lot

    //Goals: Design a parking lot using object-oriented principles
    //Here are a few methods that you should be able to run:
    //Tell us how many spots are remaining
    //Tell us how many total spots are in the parking lot
    //Tell us when the parking lot is full
    //Tell us when the parking lot is empty
    //Tell us when certain spots are full e.g.when all motorcycle spots are taken
    //Tell us how many spots vans are taking up
    //Assumptions:

    //The parking lot can hold motorcycles, cars and vans
    //The parking lot has motorcycle spots, car spots and large spots
    //A motorcycle can park in any spot
    //A car can park in a single compact spot, or a regular spot
    //A van can park, but it will take up 3 regular spots
    //These are just a few assumptions.Feel free to ask your interviewer about more assumptions as needed

    public class ParkingLot
    {
        public int DisableSpace { get; set; }
        public int NormalSpace { get; set; }
        public int MotorcycleSpace { get; set; }


        public void ParkintLot()
        {
            DisableSpace = 10;
            NormalSpace = 70;
            MotorcycleSpace = 20;


        }

    }
}
