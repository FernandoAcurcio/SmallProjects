﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSimulator
{
    
    /* TODO: need to implement lists, so I can know what type of cars are stored inside our parking lot
     * Example: When we store a van, she occupy one van space or three normal spaces, when  we withdraw a van we no longer know that a van was
     * stored in normal spaces
     */
    public class ParkingLot
    {
        private int _disableSpot;
        private int _disableSpotMax;
        private int _regularSpot;
        private int _regularSpotMax;
        private int _largeSpot;
        private int _largeSpotMax;
        private int _motorcycleSpot;
        private int _motorcycleSpotMax;

        public ParkingLot()
        {
            InitializeParkingSpots();
        }

        private void InitializeParkingSpots()
        {
            _disableSpot = 1;
            _regularSpot = 5;
            _largeSpot = 1;
            _motorcycleSpot = 2;

            _disableSpotMax = 1;
            _regularSpotMax = 5;
            _largeSpotMax = 1;
            _motorcycleSpotMax = 2;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                DisplayMenu();
                if (int.TryParse(Console.ReadLine(), out int selectionNumber))
                {
                    switch (selectionNumber)
                    {
                        case 1:
                            ParkVehicle();
                            break;
                        case 2:
                            WithdrawVehicle();
                            break;
                        case 3:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Select a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Select a valid option.");
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("- Hello to our Parking Lot program -");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Free spaces: {GetFreeSpaces()}");
            Console.WriteLine($"Disable spaces: {_disableSpot}");
            Console.WriteLine($"Regular spaces: {_regularSpot}");
            Console.WriteLine($"Large spaces: {_largeSpot}");
            Console.WriteLine($"Motorcycle spaces: {_motorcycleSpot}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("--- Choose an action             ---");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1- Park a vehicle");
            Console.WriteLine("2- Withdraw a car");
            Console.WriteLine("3- Exit");
            Console.WriteLine();
        }

        private int GetFreeSpaces()
        {
            return _disableSpot + _regularSpot + _largeSpot + _motorcycleSpot;
        }

        private void ParkVehicle()
        {
            DisplayVehicleMenu();
            if (int.TryParse(Console.ReadLine(), out int selectionNumber))
            {
                switch (selectionNumber)
                {
                    case 1:
                        CheckForAvailability(selectionNumber);
                        break;
                    case 2:
                        CheckForAvailability(selectionNumber);
                        break;
                    case 3:
                        CheckForAvailability(selectionNumber);
                        break;
                    case 4:
                        CheckForAvailability(selectionNumber);
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Choose a valid vehicle.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Choose a valid vehicle.");
            }
        }

        private void WithdrawVehicle()
        {
            DisplayVehicleMenu();
            if (int.TryParse(Console.ReadLine(), out int selectionNumber))
            {
                switch (selectionNumber)
                {
                    case 1:
                        WithdrawFromSpot(ref _regularSpot, _regularSpotMax);
                        break;
                    case 2:
                        WithdrawFromSpot(ref _disableSpot, _disableSpotMax);
                        break;
                    case 3:
                        WithdrawFromSpot(ref _largeSpot, _largeSpotMax);
                        break;
                    case 4:
                        WithdrawFromSpot(ref _motorcycleSpot, _motorcycleSpotMax);
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Choose a valid vehicle.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Choose a valid vehicle.");
            }
        }

        private void DisplayVehicleMenu()
        {
            Console.WriteLine("--- Choose a Vehicle Type        ---");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1- Car");
            Console.WriteLine("2- Disable Car");
            Console.WriteLine("3- Van");
            Console.WriteLine("4- Motorcycle");
            Console.WriteLine("5- Exit");
            Console.WriteLine();
        }

        private void CheckForAvailability(int vehicleType)
        {
            switch (vehicleType)
            {
                case 1: // Car
                    if (_regularSpot > 0)
                    {
                        _regularSpot--;
                        Console.WriteLine("Successfully Parked");
                    }
                    else
                    {
                        Console.WriteLine("There's no parking spot left for cars!");
                    }
                    break;
                case 2: // Disable Car
                    if (_disableSpot > 0)
                    {
                        _disableSpot--;
                        Console.WriteLine("Successfully Parked");
                    }
                    else
                    {
                        Console.WriteLine("There's no disable spot left!");
                    }
                    break;
                case 3: // Van
                    if (_largeSpot > 0)
                    {
                        _largeSpot--;
                        Console.WriteLine("Successfully Parked");
                    }
                    else if (_regularSpot >= 3)
                    {
                        _regularSpot -= 3;
                        Console.WriteLine("Successfully Parked");
                    }
                    else
                    {
                        Console.WriteLine("There's no van or regular spots left!");
                    }
                    break;
                case 4: // Motorcycle
                    if (_motorcycleSpot > 0)
                    {
                        _motorcycleSpot--;
                        Console.WriteLine("Successfully Parked");
                    }
                    else if (_regularSpot > 0)
                    {
                        _regularSpot--;
                        Console.WriteLine("Successfully Parked");
                    }
                    else
                    {
                        Console.WriteLine("There's no motorcycle or regular spots left!");
                    }
                    break;
                default:
                    Console.WriteLine("Choose a valid vehicle.");
                    break;
            }

            Console.ReadLine();
        }

        private void WithdrawFromSpot(ref int spot, int maxSpots)
        {
            if (spot < maxSpots)
            {
                spot++;
                Console.WriteLine("Successfully Withdraw");
            }
            else
            {
                Console.WriteLine("There's no vehicle to withdraw.");
            }
            Console.ReadLine();
        }
    }
}
