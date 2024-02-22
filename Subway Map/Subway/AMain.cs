/*****************************************************************************************************
 * Subway
 * 
 * Name: AMain.cs
 * 
 * Written by: Athul Subash Marottikkal - February 2023
 * 
 * Description: Contains the main method and menu for SubwayMap.
 * ***************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Subway
{
    class AMain
    {
        static void Main()
        {
            SubwayMap map = new SubwayMap();

            int option;          

            Console.WriteLine("Subway Map Menu\n");
            Console.Write("Press '1' to create a custom subway map or '2' to use prebuild subway map: ");
            option = Convert.ToInt32(Console.ReadLine());

            // Prebuild subway map
            if (option == 2)
            {
                // Insert Station
                map.InsertStation("Dublin");
                map.InsertStation("Towerhill");
                map.InsertStation("Terminal");
                map.InsertStation("Peterborough");
                map.InsertStation("Trail");
                map.InsertStation("Otonabee");
                map.InsertStation("Toronto");
                map.InsertStation("Benson");
                map.InsertStation("Landsdowne");
                map.InsertStation("Reid");
                map.InsertStation("Niagra");
                map.InsertStation("Aylmer");
                map.InsertStation("Scarborough");
                map.InsertStation("Chemong");
                map.InsertStation("Saenz");
                map.InsertStation("Bethune");
                map.InsertStation("Hillard");
                map.InsertStation("Gotham");
                map.InsertStation("Raymond");
                map.InsertStation("Mcdonnel");
                map.InsertStation("Union");
                map.InsertStation("Hamilton");
                map.InsertStation("York");
                map.InsertStation("Sadleir");
                map.InsertStation("Waterloo");
                map.InsertStation("Water");

                // Insert Connection          
                map.InsertConnection("Dublin", "Towerhill", Colour.Red);
                map.InsertConnection("Towerhill", "Terminal", Colour.Red);
                map.InsertConnection("Terminal", "Peterborough", Colour.Red);
                map.InsertConnection("Peterborough", "Trail", Colour.Red);
                map.InsertConnection("Trail", "Otonabee", Colour.Red);
                map.InsertConnection("Otonabee", "Toronto", Colour.Red);
                map.InsertConnection("Gotham", "Raymond", Colour.Green);
                map.InsertConnection("Raymond", "Towerhill", Colour.Green);
                map.InsertConnection("Towerhill", "Terminal", Colour.Green);
                map.InsertConnection("Terminal", "Peterborough", Colour.Green);
                map.InsertConnection("Peterborough", "Mcdonnel", Colour.Green);
                map.InsertConnection("Mcdonnel", "Union", Colour.Green);
                map.InsertConnection("Union", "Hamilton", Colour.Green);
                map.InsertConnection("Hamilton", "York", Colour.Green);
                map.InsertConnection("Hamilton", "Sadleir", Colour.Green);
                map.InsertConnection("Sadleir", "Waterloo", Colour.Green);
                map.InsertConnection("Benson", "Terminal", Colour.Blue);
                map.InsertConnection("Terminal", "Landsdowne", Colour.Blue);
                map.InsertConnection("Landsdowne", "Reid", Colour.Blue);
                map.InsertConnection("Reid", "Niagra", Colour.Blue);
                map.InsertConnection("Niagra", "Aylmer", Colour.Blue);
                map.InsertConnection("Aylmer", "Scarborough", Colour.Blue);
                map.InsertConnection("Scarborough", "Chemong", Colour.Blue);
                map.InsertConnection("Chemong", "Saenz", Colour.Blue);
                map.InsertConnection("Hilliard", "Bethune", Colour.Purple);
                map.InsertConnection("Bethune", "Niagra", Colour.Purple);
                map.InsertConnection("Niagra", "Reid", Colour.Purple);
                map.InsertConnection("Reid", "Landsdowne", Colour.Purple);
                map.InsertConnection("Landsdowne", "Terminal", Colour.Purple);
                map.InsertConnection("Terminal", "Reid", Colour.Yellow);
                map.InsertConnection("Reid", "Union", Colour.Yellow);
                map.InsertConnection("Union", "Water", Colour.Yellow);
                map.InsertConnection("Water", "Trail", Colour.Yellow);
                map.InsertConnection("Trail", "Terminal", Colour.Yellow);

                option = 1;

            }

            // Subway Menu
            while (option == 1)
            {
                bool flag = false;
                string name1 = "", name2 = "";
                int colour = 0;

                Console.Write("\n\nPress '1' to add station \nPress '2' to remove station \nPress '3' to add connection \n" +
                    "Press '4' to remove connection \n Press '5' to find shortest route \nEnter input: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1: Console.Write("Enter the name of the station to be inserted: ");
                        map.InsertStation(Console.ReadLine());
                        break;

                    case 2: Console.Write("Enter the name of the station to be removed: ");
                        flag = map.RemoveStation(Console.ReadLine());
                        if (flag == true)
                            Console.WriteLine("\nStation removed!");
                        else
                            Console.WriteLine("\nStation does not exist!");
                        break;

                    case 3: Console.WriteLine("Enter the names of the stations to be inserted:");
                        Console.Write("Enter station 1: ");
                        name1 = Console.ReadLine();
                        Console.Write("Enter station 2: ");
                        name2 = Console.ReadLine();
                        Console.WriteLine("Enter colour of the connection\n '1' = Red, '2' = Yellow, '3' = Green, '4' = Purple, '5' = Orange");
                        Console.Write("Enter number: ");
                        colour = Convert.ToInt32(Console.ReadLine());

                        if(colour > 0 && colour < 6)
                        {
                            flag = map.InsertConnection(name1,name2, (Colour)colour);
                        }
                        if (flag == true)
                            Console.WriteLine("\nConnection successful!");
                        else
                            Console.WriteLine("\nConnection cannot be made!");
                        break;

                    case 4: Console.WriteLine("Enter the names of the stations to be who connection is to removed:");
                        Console.Write("Enter station 1: ");
                        name1 = Console.ReadLine();
                        Console.Write("Enter station 2: ");
                        name2 = Console.ReadLine();
                        Console.WriteLine("Enter colour of the connection\n '1' = Red, '2' = Yellow, '3' = Green, '4' = Purple, '5' = Orange");
                        Console.Write("Enter number: ");
                        colour = Convert.ToInt32(Console.ReadLine());

                        if (colour > 0 && colour < 6)
                        {
                            flag = map.RemoveConnection(name1, name2, (Colour)colour);
                        }
                        if (flag == true)
                            Console.WriteLine("\nConnection removed!");
                        else
                            Console.WriteLine("\nConnect does not exist!");
                        break;

                    case 5: Console.WriteLine("Enter name of the stations to find shortest path: ");
                        Console.Write("Enter station 1: ");
                        name1 = Console.ReadLine();
                        Console.Write("Enter station 2: ");
                        name2 = Console.ReadLine();
                        map.ShortestRoute(name1, name2);
                        break;

                    default: Console.WriteLine("\nInvalid Input!");
                        break;
                }
                option = 1;
                Console.Write("\nPress '1' to continue or '2' to exit: ");
                option = Convert.ToInt32(Console.ReadLine());
            } 
            Console.ReadLine();
        }
    }
}
