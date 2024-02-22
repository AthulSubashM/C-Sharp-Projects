/*****************************************************************************************************
 * KD Tree
 * 
 * Name: KDTree.cs
 * 
 * Written by: Athul Subash Marottikkal April 2023
 * 
 * Description: The program implements the KD Tree data structure to store points.
 * ***************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KDTree
{
    /**
     * Class Name: Point
     * Variables: (double[]) coord - Array that stores each coordinate of the point.
     * Methods: public Point(double[]) - Constructor.
     */
    public class Point
    {
        public double[] coord;  // Each element of the array represents a coordinate of the point.
        public Point(double[] coord)
        {
            this.coord = coord;
        }
    }

    /**
     * Class Name: Node
     * Variables: (Point) pos - Stores the point.
     *            (int) cutDim - Stores the cut dimension.
     *            (Node) left - The left child of the Node.
     *            (Node) right - The right child of the Node.
     * Methods: public Node(Point, int) - Constructor.
     */
    public class Node
    {
        public Point point;
        public int cutDim;  // cut Dimension.
        public Node left;
        public Node right;

        // Constructor.
        public Node(Point point, int cutDim)
        {
            this.point = point;
            this.cutDim = cutDim;
            left = null;
            right = null;
        }
    }

    /**
     * Class Name: KDTree
     * Variables: (Node) root - Root of the tree.
     * Methods: public bool Insert(Point) - Calls private Insert() method to perform insertion.
     *          private void Insert(Node, Point) - Performs insertion.
     *          public bool Contains(Point) - Calls private Contains() method to check if the given point exists in the tree.
     *          public bool Delete(Point) - Calls private Remove() method to perform deletion.
     *          private Node Delete(Point, Node) - Performs deletion.
     *          private Point FindMin(Node, int) - Finds the point with the smallest value in the specified dimension of the KD Tree.
     *          private Point MinAlongDim(Point, Point, int) - Finds the minimum of the two given points along the specified dimension. 
     *          public void Print() - Calls private Print() method to print the tree.
     *          private void Print(Node, int) - Prints the tree.     
     */
    public class KDTree
    {
        private Node root;  // Root of the tree.

        /**
         * Method Name: Insert(Point)
         * Arguments: (Point) point: The point to be inserted.
         * Purpose: Inserts the given point into the KD Tree, if it doesn't already exist by calling the private Insert() method.
         * Returns: (bool) Returns true if the insertion is successful, false otherwise.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        public bool Insert(Point point)
        {
            // If the root is null, create a new node and make it the root.
            if (root == null)
            {
                root = new Node(point, 0);
                return true;
            }
            else if (Contains(point))   // Stops duplicates from being inserted.
            {
                return false;
            }
            // Otherwise, call the private Insert method to recursively insert the point.
            else
            {
                Insert(root, point);
                return true;
            }
        }

        /**
         * Method Name: Insert(Node, Point)
         * Arguments: (Node) node: The current node being processed.
         *            (Point) point: The point to be inserted.
         * Purpose: Recursively inserts the given point into the KD Tree.
         */
        private void Insert(Node node, Point point)
        {
            int dim = node.cutDim;  // Get the dimension to be used for comparison.

            // Compare the point's coordinate value with the current node's point's coordinate value.
            if (point.coord[dim] < node.point.coord[dim])
            {
                if (node.left == null)  // If the left child is null, create a new node and insert the point.
                {
                    node.left = new Node(point, (dim + 1) % point.coord.Length);    
                }
                // Otherwise, recursively insert the point into the left child.
                else
                {
                    Insert(node.left, point);   // Recursively insert the point into the left child.
                }
            }
            else
            {
                if (node.right == null)     // If the right child is null, create a new node and insert the point.
                {
                    node.right = new Node(point, (dim + 1) % point.coord.Length);
                }
                // Otherwise, recursively insert the point into the right child.
                else
                {
                    Insert(node.right, point);  // Recursively insert the point into the right child.
                }
            }
        }

        /**
         * Method Name: Contains(Point)
         * Arguments: (Point) point: The point to check for in the KD Tree.
         * Purpose: Checks if the given point exists in the KD Tree.
         * Returns: (bool) Returns true if the point exists in the tree, false otherwise.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        public bool Contains(Point point)
        {
            Node current = root;    // Starts at the root of the tree.
            while (current != null)
            {
                // If the current node's point is equal to the given point, return true.
                if (current.point.coord.SequenceEqual(point.coord))
                {
                    return true;
                }
                // Otherwise, check if the given point should be in the left or right subtree.
                else if (point.coord[current.cutDim] < current.point.coord[current.cutDim])
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }
            return false;   // If the point was not found in the tree, return false.
        }

        /**
         * Method Name: Delete(Point)
         * Arguments: (Point) point: The point to be deleted.
         * Purpose: Deletes the given point from the KD Tree, if it exists by calling the private Delete() method.
         * Returns: (bool) Returns true if the deletion is successful, false otherwise.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        public bool Delete(Point point)
        {
            // Checks if the point exists in the tree
            if (Contains(point))
            {
                Delete(point, root);    // Calls the private Remove() method to remove the point.
                return true;
            }
            return false;
        }

        /**
         * Method Name: Delete(Point, Node)
         * Arguments: (Point) point: The point to be deleted.
         *            (Node) node: The node being processed during the deletion operation.
         * Purpose: Recursively deletes the given point from the KD Tree and returns the updated root node.
         * Returns: (Node) The root node of the updated KD Tree after the deletion operation.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        private Node Delete(Point point, Node node)
        {
            if (node.point.coord.SequenceEqual(point.coord))
            {
                // If the node to be deleted has a right child, replace it with the smallest node in the right subtree.
                if (node.right != null)
                { 
                    node.point = FindMin(node.right, node.cutDim);
                    node.right = Delete(node.point, node.right);
                }
                // If the node to be deleted has a left child, replace it with the smallest node in the left subtree.
                else if (node.left != null)
                { 
                    node.point = FindMin(node.left, node.cutDim);
                    node.right = Delete(node.point, node.left); 
                    node.left = null; 
                }
                // If the node to be deleted has no children, simply set the node to null.
                else
                { 
                    node = null; 
                }
            }
            // If the point to be deleted is less than the current node, traverse the left subtree.
            else if (point.coord[node.cutDim] < node.point.coord[node.cutDim])
            {
                node.left = Delete(point, node.left); 
            }
            // If the point to be deleted is greater than or equal to the current node, traverse the right subtree.
            else
            { 
                node.right = Delete(point, node.right);
            }
            return node;
        }

        /**
         * Method Name: FindMin(Node, int)
         * Arguments: (Node) node: The current node being processed.
         *            (int) dimension: The dimension to find the minimum point along.
         * Purpose: Finds the point with the smallest value in the specified dimension of the KD Tree.
         * Returns: (Point) Returns the point with the smallest value in the specified dimension, or null if the tree is empty.
         * Time complexity: O(log n), where n is the number of points in the tree.
         */
        private Point FindMin(Node node, int dimension)
        {
            // If the node is null, return null.
            if (node == null)
            {
                return null;
            }
            // If the node's cut dimension is equal to the specified dimension, then the minimum point will be on the left side of the node.
            if (node.cutDim == dimension)
            {
                // If the node has no left child, then it is the minimum point in the dimension.
                if (node.left == null)
                { 
                    return node.point; 
                }
                // Otherwise, recursively find the minimum point on the left side of the node.
                else
                {
                    return FindMin(node.left, dimension); 
                }
            }
            // Otherwise, we need to check both the left and right subtrees to find the minimum point in the specified dimension.
            else
            { 
                Point leftMin = FindMin(node.left, dimension);  // Recursively find the minimum point on the left side of the node.
                Point rightMin = FindMin(node.right, dimension);    // Recursively find the minimum point on the right side of the node.
                Point minPoint = MinAlongDim(node.point, leftMin, dimension);   // Find the minimum point among the current node, the left subtree, and the right subtree in the specified dimension.
                return MinAlongDim(minPoint, rightMin, dimension);
            }
        }

        /**
         * Method Name: MinAlongDim
         * Arguments: (Point) p1: The first point to compare.
         *            (Point) p2: The second point to compare.
         *            (int) dimension: The dimension along which to compare the points.
         * Purpose: Finds the minimum of the two given points along the specified dimension.
         * Returns: (Point) The point with the minimum coordinate value along the specified dimension.
         * Time complexity: O(1)
         */
        private Point MinAlongDim(Point p1, Point p2, int dimension)
        {
            if (p2 == null || p1.coord[dimension] <= p2.coord[dimension])
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }

        /**
         * Method Name: Print()
         * Purpose: Prints the KD Tree in a preorder traversal order by calling the private Print() method.
         * Time complexity: O(n), where n is the number of points in the tree.
         */
        public void Print()
        {
            Print(root, 0);
        }

        /**
         * Method Name: Print(Node, int)
         * Arguments: (Node) node: The current node being printed.
         *            (int) indent: The current indentation level for formatting.
         * Purpose: Recursively prints the nodes of the KD Tree in a preorder traversal order.
         * Time complexity: O(n), where n is the number of points in the tree.
         */
        private void Print(Node node, int indent)
        {
            if (node != null)
            {
                // Indent based on the current level.
                for (int i = 0; i < indent; i++)
                {
                    Console.Write("  ");
                }
                // Print the coordinates of the current point.
                Console.WriteLine($"({string.Join(", ", node.point.coord)})");
                // Recursively print the left and right subtrees.
                Print(node.left, indent + 1);
                Print(node.right, indent + 1);
            }
        }
    }
}
