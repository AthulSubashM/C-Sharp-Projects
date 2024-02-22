using System;
using System.Collections.Generic;
using System.Linq;
/*****************************************************************************************************
 * The Perfect Maze
 * 
 * Name: BMain.cs
 * 
 * Written by: Athul Subash Marottikkal - February 2023
 * 
 * Description: Contains the Main() method for Maze.
 * Subroutines: Maze.cs - Generates the maze.
 * ***************************************************************************************************/
using System.Text;
using System.Threading.Tasks;

namespace PerfectMaze
{
    class BMain
    {
        static void Main()
        {
            Console.Write("Enter size of the maze: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Maze maze = new Maze(size);
            maze.Print();
            Console.ReadLine();
        }
    }
}
