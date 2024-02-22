/*****************************************************************************************************
 * Point Quad Tree
 * 
 * Name: PointQuadTree.cs
 * 
 * Written by: Athul Subash Marottikkal - April 2023
 * 
 * Description: The program implements the Point Quad Tree data structure to store points.
 * ***************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3PointQuadTree
{
    /**
     * Class Name: Point
     * Variables: (double[]) coord - Array that stores each coordinate of the point.
     * Methods: public Point(double[]) - Constructor.
     */
    public class Point
    {
        public double[] coord;      // Each element of the array represents a coordinate of the point.
        public Point(double[] coord)
        {
            this.coord = coord;
        }
    }

    /**
     * Class Name: Node
     * Variables: (Point) pos - Stores the point.
     *            (Node[]) children - Array to store the children of the current node.
     * Methods: public Node(Point, int) - Constructor.
     */
    public class Node
    {
        public Point pos { get; set; }
        public Node[] children { get; set; }

        // Constructor.
        public Node(Point p, int dim)
        {
            pos = p;
            children = new Node[(int)Math.Pow(2, dim)];     // Sets the number of children to 2^(no of dimensions).
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = null;
            }
        }
    }

    /**
     * Class Name: PointQuadTree
     * Variables: (Node) root - Root of the tree.
     *            (int) dim - Stores the number of dimensions.
     * Methods: public PointQuadTree(int) - Constructor.
     *          public bool Insert(Point) - Calls private Insert() method to perform insertion.
     *          private bool Insert(Node, Point) - Performs insertion.
     *          public bool Remove(Point) - Calls private Remove() method to perform deletion.
     *          private void Remove(Node, PointQuadTree, Point) - Performs deletion.
     *          public bool Contains(Point) - Calls private Contains() method to check if the given point exists in the tree.
     *          private bool Contains(Node, Point)  - Checks if the given point exists in the tree.
     *          public void Print() - Calls private Print() method to print the tree.
     *          private void Print(Node, string) - Prints the tree.
     */
    public class PointQuadTree
    {
        private Node root;      // Root of the tree.
        private int dim;        // Number of dimensions.

        /**
         * Method Name: PointQuadTree(int)
         * Arguments: (Integer) dim: The number of dimensions.
         * Purpose: The constructor creates a Point Quad Tree with the given dimensions.
         * Time complexity: O(1)
         */
        public PointQuadTree(int dim)
        {
            root = null;
            this.dim = dim;
        }

        /**
         * Method Name: Insert(Point)
         * Arguments: (Point) p: The point to be inserted.
         * Purpose: Calls the private Insert() method to insert the Point p into the Point Quad Tree.
         * Returns: (bool) Returns true if the insertion is successful, false otherwise.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        public bool Insert(Point p)
        {
            // If the root is null, create a new node and make it the root.
            if (root == null)
            {
                root = new Node(p, dim);
                return true;
            }
            else if (Contains(p))   // Stops duplicates from being inserted.
            {
                return false;
            }
            // Otherwise, call the private Insert method to recursively insert the point.
            else
            {
                return Insert(root, p);
            }
        }

        /**
         * Method Name: Insert(Node, Point)
         * Arguments: (Node) curr: The current node being processed.
         *            (Point) p: The point to be inserted.
         * Purpose: Recursively inserts the given point into the Point Quad Tree.
         * Returns: (bool) Returns true if the insertion is successful, false otherwise.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        private bool Insert(Node curr, Point p)
        {
            int quadrant = 0;   // Variable to track the quadrant to be inserted into.

            // Determine which quadrant the point should be inserted into.
            for (int i = 0; i < dim; i++)
            {
                if (p.coord[i] > curr.pos.coord[i])
                {
                    quadrant += (int)Math.Pow(2, dim - i - 1);
                }
            }

            // If the quadrant's child node is null, create a new node and insert the point.
            if (curr.children[quadrant] == null)
            {
                curr.children[quadrant] = new Node(p, dim);
                return true;
            }
            // Otherwise, recursively insert the point into the quadrant's child node.
            else
            {
                return Insert(curr.children[quadrant], p);
            }
        }

        /**
         * Method Name: Remove(Point)
         * Arguments: (Point) p: The point to be removed.
         * Purpose: Calls the private Remove() method to remove the given point from the Point Quad Tree.
         * Returns: (bool) Returns true if the removal is successful, false otherwise.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        public bool Remove(Point p)
        {
            // Checks if the point exists in the tree.
            if (Contains(p))
            {
                PointQuadTree newTree = new PointQuadTree(dim);     // Creates a new tree.
                Remove(root, newTree, p);   // Calls the private Remove() method to remove the point.
                root = newTree.root;        // Sets the root of the current tree to the root of the new tree.
                return true;
            }
            return false;   // If the tree does not contain the point, return false.
        }

        /**
         * Method Name: Remove(Node, PointQuadTree, Point)
         * Arguments: (Node) curr: The current node being processed.
         *            (PointQuadTree) newTree: The new Point Quad Tree to hold the remaining points after removal.
         *            (Point) p: The point to be removed.
         * Purpose: Recursively adds all points in the orginal tree to a new tree, except the point to be removed.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        private void Remove(Node curr, PointQuadTree newTree, Point p)
        {
            // If the current node's position is not the point to be removed, insert it into the new tree.
            if (!curr.pos.coord.SequenceEqual(p.coord))
            {
                newTree.Insert(curr.pos);
            }

            // Recursively calls the Remove() method to insert the current node's children.
            for (int i = 0; i < Math.Pow(2, dim); i++)
            {
                if (curr.children[i] != null)
                {
                    Remove(curr.children[i], newTree, p);
                }
            }          
        }

        /**
         * Method Name: Contains(Point)
         * Arguments: (Point) p: The point to check if it is contained in the tree.
         * Purpose: Determines if the given point is contained in the tree by calling the private Contains() method.
         * Time complexity: O(log n)
         * Return: (bool) True if the point is in the tree, false otherwise.
         */
        public bool Contains(Point p)
        {
            return Contains(root, p);   // Calls the private Contains method().
        }

        /**
         * Method Name: Contains(Node, Point)
         * Arguments: (Node) curr: The current node being processed.
         *            (Point) p: The point to check if it is contained in the tree.
         * Purpose: Recursively traverses the tree to determine if the given point is contained in the tree.
         * Time complexity: O(log n)
         * Return: (bool) True if the point is in the tree, false otherwise.
         */
        private bool Contains(Node curr, Point p)
        {
            if (curr == null)   // Returns false if the point does not exist.
            {
                return false;
            }
            else if (curr.pos.coord.SequenceEqual(p.coord))     // Returns true if the points exists.
            {
                return true;
            }
            // Else checks the children of the current node.
            else
            {
                int quadrant = 0;
                for (int i = 0; i < dim; i++)
                {
                    if (p.coord[i] > curr.pos.coord[i])
                    {
                        quadrant += (int)Math.Pow(2, dim - i - 1);
                    }
                }
                return Contains(curr.children[quadrant], p);    // Recursively calls Contains() method with the children.
            }
        }

        /**
         * Method Name: Print
         * Purpose: Call the private Print() method to print the tree.
         * Time complexity: O(n log n)
         */
        public void Print()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty.");
                return;
            }

            Print(root, "");
        }

        /**
         * Method Name: Print
         * Arguments: (Node) curr: The current node being processed.
         *            (string) indent: The current indentation level used for printing.
         * Purpose: Recursively traverses the tree in a pre-order traversal and prints each node.
         * Time complexity: O(n log n)
         */
        private void Print(Node curr, string indent)
        {
            if (curr == null)
            {
                return;
            }

            Console.WriteLine(indent + string.Join(", ", curr.pos.coord));

            for (int i = 0; i < curr.children.Length; i++)
            {
                Print(curr.children[i], indent + "    ");
            }
        }

    }
}
