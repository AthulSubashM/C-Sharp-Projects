/*****************************************************************************************************
 * The Perfect Maze
 * 
 * Name: Maze.cs
 * 
 * Written by: Athul Subash Marottikkal - February 2023
 * 
 * Description: The program generates a random maze of size 'n' with Walls represented by '|' 
 *              and a path represented by ' '. Maze is generated implementing a Depth First Search 
 *              on an adjacent matrix.
 * Note: Inaccessible paths are repesented by '-'.
 * ***************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectMaze
{
    /**
     * Class Name: Maze
     * Variables: (char[,]) M - represents the maze.
     *            (int) size - size of the maze.
     * Methods: public Maze(int) - Constructor
     *          private void Initialize() - Initializes maze.
     *          Create() - Creates maze.
     *          DepthFirstSearch(int, int, bool[,]) - Creates path in the maze.
     *          Print() - Prints maze.
     */
    public class Maze
    {      
        // The variable 'M' is a two dimensional array of type char representing the Maze.
        private char[,] M;
        // Size of the Maze.
        private int size;

        /**
         * Method Name: Maze()
         * Arguments: (Integer) n: represents the size of the maze.
         * Purpose: The constructor initializes the two dimensional array M, with the n+2 rows and coloumns. 
         *          The constructor then calls the methods Initialize() and Create().
         * Time complexity: O(n^2)
         */
        public Maze(int n)
        {
            // Sets the variable 'size' to n+2 to accomodate the walls of the maze.
            size = n + 2;
            // Initializes 'M' with 'size' rows and coloumns.
            M = new char[size, size];

            // Calls the Initialize() method to populate the maze with '|' and '-' characters.
            Initialize();

            // Calls the Create() method to create the maze.
            Create();
        }

        /**
         * Method Name: Initialize()
         * Purpose: Populates 'M' with '|' and '-' characters, representing walls 
         *          and black spaces in the maze, respectively.
         * Time complexity: O(n^2)
         */
        private void Initialize()
        {
            // Loop to iterate through the rows of 'M'.
            for (int i = 0; i < size; i++)
            {
                // Loops to iterate through the columns of 'M'.
                for (int j = 0; j < size; j++)
                {
                    // The if statement populates the edges of 'M' with the '|' character.
                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                    {
                        M[i, j] = '|';
                    }

                    // The else statement populates the inner portions of 'M' with the '-' character.
                    else
                    {
                        M[i, j] = '-';
                    }
                }
            }
        }


        /*
         * Method Name: Create()
         * Purpose: The method creates a two dimensional array of type bool with 'size' number of rows and columns
         *          and sets the value at the indices representing the edge of the maze to true. The method then
         *          selects a random index from the inner portions of the maze and invokes the DepthFirstSearch() method
         *          with the selected index and the created bool array.
         * Time complexity: O(n^2)
         */
        private void Create()
        {
            // Creates a random variable called rand.
            Random rand = new Random();

            // The variables i and j stores a randomly selected index from the inner portions of the maze.
            int i = rand.Next(1, size - 1);
            int j = rand.Next(1, size - 1);

            // Creates a two dimensional array of type bool called 'visited', with 'size' number of rows and columns.
            bool[,] visited = new bool[size, size];

            // Sets the value of 'visited' at the indices representing the edge of the maze to true.
            for (int k = 0; k < size; k++)
            {
                visited[0,k] = true;
                visited[size - 1, k] = true;
                visited[k, size - 1] = true;
                visited[k, 0] = true;
            }

            // Calls the DepthFirstSearch() method with the arguments, i, j and visited.
            DepthFirstSearch(i,j,visited);
        }

        /**
         * Method name: DepthFirstSearch()
         * Arguments: (Integer) i & j: Randomly selected indices of the maze.
         *            (bool[,]) visited: Two dimensional array to indicate if the index if visited. 
         * Purpose: Recursive method that generates the paths in the maze represented by the ' ' character.
         * 
         * Note: The position (i,j) and all of its adjacent indices of M[i,j], passed to this recursive method is set to ' ' 
         *       which represents a possible path, i.e, the adjacent positions are marked as seen. The value at visited[i,j] 
         *       is set to true only when the path is visited. If the position is already adjacent to a path, i.e, M[i,j] = ' ', 
         *       but not visited and is adjacent to the current position, then it is set to '|' represeting a wall.
         * Time complexity: O(n^2)
         */
        private void DepthFirstSearch(int i, int j, bool[,] visited)
        {
            // Creates a random variable called rand.
            Random rand = new Random();

            // Stops the method if size is equal to 2, i.e, n is equal to 0 (empty maze). 
            if (size == 2)
                return;

            // The variable x and y stores the index values from i and j, respectively. Used inside the loops.
            int x = i, y = j;
            // The two dimensional array, 'dir' stores values which can be added to an index position to move
            // one position in any of the four directions.
            int[,] dir = new int[4, 2] { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 } };

            // Sets value at index i,j of the visited to true, i.e. marks the current position as visited. 
            visited[i, j] = true;
            // Sets value at index i,j of the M to ' ' to represent a path.
            M[i, j] = ' ';

            // The Loop sets values of all possible paths from a current index, (i,j) of 'M' to ' ' if the adjacent
            // paths are not visited and to '|' is they are visited.
            for (int m = 0; m < 4; m++)
            {
                // x and y stores each of the four possible path from the index (i,j) on each interation of the loop.
                x = i + dir[m, 0];
                y = j + dir[m, 1];

                // If the path is adjacent to another path, makes that path into a wall and marks it as visited.
                if (M[x, y] == ' ' && !visited[x, y])
                {
                    M[x, y] = '|';
                    visited[x, y] = true;
                }
                // Else makes the path traversable by setting the position at M as ' '.
                else if (M[x, y] == '-')
                {
                    M[x, y] = ' ';
                }
            }

            // Loop to select a random path unvisited from the current position. Exits loop if all possible paths
            // from the current position are all visited. Implements recursion of the DepthFirstSearch() method.
            while (!(visited[i, j - 1] && visited[i, j + 1] && visited[i + 1, j] && visited[i - 1, j]))
            {
                // The variable stores a randomly selected number between 0 and 3, inclusive.
                int index = rand.Next(4);

                // x and y stores each of the four possible path from the index (i,j) on each interation of the loop.
                x = i + dir[index, 0];
                y = j + dir[index, 1];

                // if the path (x,y) is not visited, then the DepthFirstSearch() method is invoke with the arugments
                // x, y and visited.
                if (!visited[x, y])
                {
                    DepthFirstSearch(x, y, visited);    // Recursion
                } 
            }
        }

        /**
         * Method Name: Print()
         * Purpose: Prints the maze.
         * Time complexity: O(n^2)
         */
        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0} ",M[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}