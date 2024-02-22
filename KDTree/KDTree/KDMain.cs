/*****************************************************************************************************
 * Main for KDTree
 * Written by: Athul Subash Marottikkal - April 2023
 ****************************************************************************************************/
namespace KDTree
{
    internal class KDMain
    {
        static void Main(string[] args)
        {
            KDTree tree = new KDTree();

            double[,] coords = new double[,] {
                {2, 3}, { 2, 3}, {5, 4}, {1, 8}, {9, 3}, {6, 1}, {7, 5}, {3, 2},
                {4, 7}, {2, 9}, {8, 6}, {3, 5}, {1, 2}, {6, 8}, {7, 2}, {9, 6}
            };

            for (int i = 0; i < coords.GetLength(0); i++)
            {
                Point point = new Point(new double[] { coords[i, 0], coords[i, 1] });
                tree.Insert(point);
            }

            tree.Print();

            Point del = new Point(new double[] { 5, 4 });
            if (tree.Contains(del))
            {
                Console.WriteLine("\nContains {5, 4}");
            }
            else
            {
                Console.WriteLine("\nNode doesnt exist");
            }

            
            Console.ReadLine();
        }
    }
}