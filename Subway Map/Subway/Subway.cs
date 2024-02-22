/*****************************************************************************************************
 * Subway
 * 
 * Name: Subway.cs
 * 
 * Written by: Athul Subash Marottikkal - February 2023
 * 
 * Description: The program allows users to add and remove stations and the connections between them,
 *              and to find the shortest path between two stations using breath-first search.
 *              The subway map is implemented using an adjacency list.
 * ***************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Subway
{
    // The enum Colour stores the name of the colour of the subway line.
    public enum Colour
    {
        NotSet, Red, Yellow, Green, Blue, Purple, Orange
    }

    /**
         * Class Name: SubwayMap
         * Purpose: The class SubwayMap represents subway connections using an adjaceny list.
         * Sub-classes: Node - Represents the edge of the adjacency list.
         *              Station - Represents the vertex of the adjacent list.
         * Variables: (Dictionary) S - Represents a dictionary of Station and their connections.
         * Methods: SubwayMap() - Default constructor.
         *          InsertStation(string) - Inserts station.
         *          RemoveStation(string) - Removes station.
         *          InsertConnection(string, string, Colour) - Inserts connection.
         *          RemoveConnection(string, string, Colour) - Removes connection.
         *          ShortestRoute(string, string) - Finds shortest route.
         */
    public class SubwayMap
    {
        /**
         * Class Name: Node
         * Purpose: The class Node repesents a connection between two stations. It acts as the edge
         *          in the SubwayMap adjacency list.
         * Variables: Connection - Represents the adjacent station.
         *            Line - Repesents the colour of the subway connection.
         *            Next - Points to the next Node.
         * Methods: Node() - Default constructor.
         *          Node(Station, Colour, Node) - Overloaded constructor.
         */
        private class Node
        {
            public Station Connection { get; set; }      // Adjacent station (connection)
            public Colour Line { get; set; }             // Colour of its subway line
            public Node Next { get; set; }               // Link to the next adjacent station (Node)

            /**
             * Method Name: Node()
             * Purpose: Default constructor.
             * Time complexity: O(1)
             */
            public Node()
            {
                Connection = null;
                Line = Colour.NotSet;
                Next = null;
            }

            /**
             * Method Name: Node(Station connection, Colour c, Node next)
             * Purpose: Constructor that initializes the Node with the arguments of type Station, Colour and Node.
             * Time complexity: O(1)
             */
            public Node(Station connection, Colour c, Node next)
            {
                Connection = connection;
                Line = c;
                Next = next;
            }
        }
        /**
         * Class Name: Station
         * Purpose: The class Station repesents a station in the subway map. It acts as the vertex
         *          in the SubwayMap adjacency list.
         * Variables: Name - Name of the subway station
         *            Visited - bool variable for the breadth-first search
         *            E - Header node for a linked list of adjacent stations
         * Methods: Station() - Default constructor.
         *          Station(string) - Overloaded constructor.
         */
        private class Station 
        {
            public string Name { get; set; }             // Name of the subway station
            public bool Visited { get; set; }            // Used for the breadth-first search
            public Node E { get; set; }                  // Header node for a linked list of adjacent stations

            /**
             * Method Name: Station()
             * Purpose: Default constructor.
             * Time complexity: O(1)
             */
            public Station()
            {
                Name = null;
                Visited = false;
                E = null;
            }

            /**
             * Method Name: Station(string name)
             * Purpose: Constructor that initializes objects of the class Subway with argument name and initializes
             *          a new Node, E.
             * Time complexity: O(1)
             */
            public Station(string name)
            {
                Name = name;
                Visited = false;
                E = new Node();
            }
        }

        // Dictionary of stations
        private Dictionary<string, Station> S;

        /*
         * Method Name: SubwayMap()
         * Purpose: Default constructor. Initializes the Dictionary S. 
         * Time complexity: O(1)
         */
        public SubwayMap()
        {
            S = new Dictionary<string, Station>();
        }

        /*
         * Method Name: InsertStation()
         * Arguments: (string) name - Name of the station.
         * Purpose: Inserts a station to the dictionary with the name, 'name' if it does not exist.
         * Time complexity: O(1)
         */
        public void InsertStation(string name)
        {
            if (!S.ContainsKey(name))
            {
                Station s = new Station(name);
                S.Add(name, s);
                Console.WriteLine("{0} Station inserted!", name);
            }
            else
            {
                Console.WriteLine("{0} Station already exists!", name);
            }

        }

        /*
         * Method Name: RemoveStation()
         * Arguments: (string) name - Name of the station.
         * Purpose: Removes a station from the dictionary with the name, 'name' if it exists and
         *          removes all connections to the station.
         * Time complexity: O(n^2)
         */
        public bool RemoveStation(string name)
        {
            if (S.ContainsKey(name))
            {
                // If the station is connected to other stations, then the connections are removed.
                if (S[name].E.Connection.Name != null)
                {
                    Node current = new Node();
                    current = S[name].E;

                    // Calls the RemoveConnection() method for each connected station to remove the connection.
                    while (current.Next != null)
                    {
                        RemoveConnection(name, current.Connection.Name, current.Line);
                        current = current.Next;
                    }
                }
                // Removes station from the dictionary.
                S.Remove(name);
                return true;
            }
            return false;
        }

        /*
         * Method Name: InsertConnection()
         * Arguments: (string) name1, name2 - Names of the station to be connected.
         *            (Colour) c - Colour of the connection to be inserted.
         * Purpose: Inserts a connection between two stations.
         * Time complexity: O(n)
         */
        public bool InsertConnection(string name1, string name2, Colour c)
        {
            // Checks if the stations exist and the colour is valid.
            if (S.ContainsKey(name1) && S.ContainsKey(name2) && Enum.IsDefined(typeof(Colour), c) == true)
            {
                // If the first station has connections to other stations, and then loops through the list if it does.
                if (S[name1].E != null)
                {
                    Node current = new Node();
                    current = S[name1].E;
                    while (current.Next != null)
                    {
                        // Exits the method if the connection already exits.
                        if (current.Connection.Name == name2 && current.Line == c)
                        {
                            return false;
                        }
                        current = current.Next;
                    }
                }

                // Adds connection from the first station to the second.
                Node con1 = new Node(S[name2], c, S[name1].E);
                S[name1].E = con1;

                // Adds connection from the second station to the first.
                Node con2 = new Node(S[name1], c, S[name2].E);
                S[name2].E = con2;

                return true;
            }

            // Returns false if the connection cannot be completed or if it already exists.
            return false;
        }

        /*
         * Method Name: RemoveConnection()
         * Arguments: (string) name1, name2 - Names of the station to be disconnected.
         *            (Colour) c - Colour of the connection to be removed.
         * Purpose: Removes a connection between two stations.
         * Time complexity: O(n)
         */
        public bool RemoveConnection(string name1, string name2, Colour c)
        {
            // Variable to indicate if the connection is found.
            bool found = false;

            // Checks if the stations exist and the colour is valid.
            if (S.ContainsKey(name1) && S.ContainsKey(name2) && Enum.IsDefined(typeof(Colour), c) == true)
            {
                // If the first Node of the first station's list is the second station, sets found to true.
                if (S[name1].E.Connection.Name == name2 && S[name1].E.Next == null)
                {
                    if (S[name1].E.Line == c)
                    {
                        // Removes the second station from the list of the first station, by replace it with the next value on the list.
                        S[name1].E = S[name1].E.Next;
                        found = true;
                    }
                }
                // Else loops through the first station's list to find the connection to the second station.
                else
                {
                    Node current = new Node();
                    current = S[name1].E;
                    while (current.Next != null)
                    {
                        // If the connection is found, sets found to true and exits loop.
                        if (current.Connection.Name == name2 && current.Line == c)
                        {
                            current = current.Next.Next;
                            found = true;
                            break;
                        }
                        current = current.Next;
                    }
                }

                // If the first Node of the second station's list is the first station, removes the connection.
                if (S[name2].E.Connection.Name == name1 && S[name2].E.Next == null)
                {
                    if (S[name2].E.Line == c)
                    {
                        S[name2].E = S[name2].E.Next;
                    }
                }
                // Else loops through the second station's list to find the connection to the first station.
                else
                {
                    Node current = new Node();
                    current = S[name2].E;
                    while (current.Next != null)
                    {
                        // If the connection is found and exits loop.
                        if (current.Connection.Name == name1 && current.Line == c)
                        {
                            current = current.Next.Next;
                            break;
                        }
                        current = current.Next;
                    }
                }
            }

            // The method returns true if the connection is found and removed, else false.
            if (found)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Method Name: ShortestRoute()
         * Arguments: (string) name1, name2 - Names of the station.
         * Purpose: Finds the shortest route between two stations.
         * Time complexity: O(n)
         */
        public void ShortestRoute(string name1, string name2)
        {
            // Checks if stations exist
            if (! (S.ContainsKey(name1) && S.ContainsKey(name2)))
            {
                Console.WriteLine("Route cannot be found");
                return;
            }

            // Sets all stations in the dictionary as unvisited.
            foreach (Station station in S.Values)
            {
                station.Visited = false;
            }

            // Dictionary used to find the previous connection from each station.
            Dictionary<string, string> path = new Dictionary<string, string>();
            // Queue to implement breadth-first search.
            Queue<Station> Q = new Queue<Station>();
            // Stack to store the shortest path between the two stations.
            Stack<Station> shortestPath = new Stack<Station>();

            // First station in the queue.
            Station prev = S[name1];

            Node current = new Node();
            bool flag = false;

            // Sets the first station as visited.
            prev.Visited = true;
            Q.Enqueue(prev);

            while (Q.Count != 0 && flag == false)
            {
                prev = Q.Dequeue();
                current = prev.E;

                // Loop to implement breath-first search.
                while (current.Next != null)
                {
                    // If the station is not visited, adds it to the queue.
                    if (!current.Connection.Visited)
                    {
                        current.Connection.Visited = true;
                        Q.Enqueue(current.Connection);
                        // Links the previous station to the current station using the dictionary, 'path'.
                        path[current.Connection.Name] = prev.Name;

                        // Stops loop if target station is reached through the breadth-first search.
                        if (current.Connection.Name == name2)
                        {
                            flag = true;
                            break;
                        }
                    }
                    current = current.Next;
                }
            }

            if(flag == false)
            {
                Console.WriteLine("Connection does not exist!");
                return;
            }

            // Used to back track from station with name2 to station with name1.
            Station target = S[name2];

            // Loop to push the previous station of station in reverse order (finds the shortest path).
            while (target.Name != name1)
            {
                shortestPath.Push(target);
                // Gets the name of the previous station and stores it in target.
                target = S[path[target.Name]];
            }
            shortestPath.Push(target);

            // Statements to print the connection.
            // Prints in the format ("Station")|"Line Colour" -> ("Station")

            // Used to format.
            char divider = '|';

            Console.Write("Shortest path from {0} to {1}:",name1, name2);
            while(shortestPath.Count != 0) 
            {
                target = shortestPath.Pop();
                Console.Write(" ({0})", target.Name);

                current = target.E;
                if (shortestPath.Count != 0)
                {
                    // Loop to find the colour of the subway line.
                    while (current.Next != null)
                    {
                        if (shortestPath.Peek().Name == current.Connection.Name)
                        {
                            Console.Write("{0}{1}", divider,current.Line);
                            divider='/';    // formatting. If there are mulitple connections between the same adjacent station.
                        }
                        current = current.Next;
                    }
                    divider = '|';
                    Console.Write(" ->");
                }
            }
        }
    }
}
