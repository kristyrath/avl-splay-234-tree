using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Original Code by Michael Audet - Used with Permission by Sri/Trent
namespace As5
{
    class Program
    {
        static void Main(string[] args)
        {

            TTFTree<int> my234Tree = new TTFTree<int>();
            AVLTree<int> myAVLTree = new AVLTree<int>();
            AVLNode<int> AVLroot = null;
            splayNode<int> splayRoot = new splayNode<int>();
            SplayTree<int> mySplayTree = new SplayTree<int>();
            // NOTE: test values have been shorted to reduce crashing the device
            //int[] testValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8};
            int[] testValuesShuffled = { 37, 46, 17, 39, 43, 18, 23, 47};

            //int[] testValues = { 0, 1, 2, 3, 4, 5, 6, 7 };

            int[] testValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            //int[] testValuesShuffled = {37, 46, 18, 39, 43, 18, 23, 41, 25, 14, 24, 48, 8, 20, 27, 7, 5, 1, 11, 24};
            int[] rightLeftCase = { 40, 22, 55, 50 };
            int[] righRightCase = { 40, 22, 55, 56};
            int[] leftRightCase = { 40, 11, 55, 22 }; 


            int choice = 0;

        

  

            Console.WriteLine(Environment.NewLine + "SELECT AN OPERATION");
            Console.WriteLine("\n2-3-4 TREE OPERATIONS: ");
            Console.WriteLine("1) Insert one key.");
            Console.WriteLine("2) Remove one key.");
            Console.WriteLine("3) In-order print");
            Console.WriteLine("4) Breadth-first print");
            Console.WriteLine("5) Insert array of integers");
            Console.WriteLine("6) Check if value is in tree.");
            Console.WriteLine("7) Delete an element.");

            Console.WriteLine("\nAVL TREE OPERATIONS: ");
            Console.WriteLine("8) Insert array of integers");
            Console.WriteLine("9) Delete an element.");
            Console.WriteLine("10) Print with breadth-first");
            Console.WriteLine("100) Print with tree format");

            Console.WriteLine("\nSPLAY TREE OPERATIONS: ");
            Console.WriteLine("11) Insert test values.");
            Console.WriteLine("12) Search test values. ");
            Console.WriteLine("13) Breadth-first print. ");
            Console.WriteLine("14) Delete test values. ");



            Console.WriteLine("-1) Exit.");
            while (choice != -1)
            {
            
                choice = ReadInt("\nI require an integer selection");
                switch (choice)
                {
                    case 1:
                        {

                            int key = ReadInt("Please enter an integer key to add to the tree.");
                            if (my234Tree.Insert(key))
                            {
                                Console.WriteLine("Key successfully added to the tree.");
                            }
                            else
                            {
                                Console.WriteLine("The insert failed.  It may have already been in the tree.");
                            }
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Remove a given key to be implemented by student, this didn't do anything");
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("The tree contains the following values in order:");
                            my234Tree.inOrder();
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("The tree contains the following values breadth first:");
                            my234Tree.breadthFirst();
                        }
                        break;
                    case 5:
                        {

                            foreach (int key in testValues)
                            {
                                my234Tree.Insert(key);
                            }
                            Console.WriteLine("Added a bunch of stuff");

                        }
                        break;

                    case 6:
                        {
                            int key = ReadInt("Please enter an integer key to check.");
                            if (my234Tree.Find(key) != null)
                            {
                                Console.WriteLine("The tree contains {0}.", key);
                            }
                            else
                            {
                                Console.WriteLine("The tree does not contain {0}.", key);
                            }
                        }
                        break;
                    case 7:
                        {
                            int key = ReadInt("2-3-4 TREE >>> Please enter an integer key to delete.");
                            my234Tree.FindAndDelete(key);
                        }
                        break;
                    case 8:
                        {
                            foreach (int key in testValues)
                          {
                                AVLroot = myAVLTree.insert(AVLroot, key);
                            }
                            Console.WriteLine("Added a bunch of stuff");
                            
                        }
                        break;
                    case 9:
                        {
                            Console.WriteLine("AVL TREE >>> DELETE");
                            int key = ReadInt("AVL TREE >>> Please enter an integer key to delete.");
                            AVLNode<int> nodeToFind = new AVLNode<int>();
                            nodeToFind.value = key;
                            AVLroot = myAVLTree.Delete(AVLroot, nodeToFind); ;
                        }
                        break;
                    case 10: {
                            if (AVLroot != null)
                            {
                                Console.WriteLine("AVL BREADTH FIRST PRINT");
                                myAVLTree.breadthFirst(AVLroot);

                            }
                            else
                            {
                                Console.WriteLine("Root is empty.");

                            }
                            break;

                        }

                    case 100:
                        {
                            if (AVLroot != null)
                            {
                                Console.WriteLine("AVL PRINT LEVEL ORDER");
                                myAVLTree.printLevelOrder(AVLroot);

                            }
                            else
                            {
                                Console.WriteLine("Root is empty.");

                            }

                        }
                        break;
                    case 11:
                        {
                            Console.WriteLine("SPLAY TREE >>> INSERT ARRAY OF INT");
                            foreach (int key in testValuesShuffled)
                            {
                                Random random = new Random();
                                splayRoot = mySplayTree.insert(splayRoot, key);
                                mySplayTree.count++;
                            }
                            Console.WriteLine("Added a bunch of stuff");
                        }
                        break;
                    case 12:
                        {
                            Console.WriteLine("SPLAY TREE >>> SEARCH");
                            int key = ReadInt("SPLAY TREE >>> enter int");
                            splayNode<int> nodeToFind = new splayNode<int>();
                            splayNode<int> resultNode;

                            nodeToFind.value = key;
                            resultNode = mySplayTree.Search(splayRoot, nodeToFind);

                            if (resultNode.value.CompareTo(nodeToFind.value)==0)
                            {
                                Console.WriteLine("Node is found! VALUE IS: " + resultNode.value.ToString());
                
                            }
                            else
                            {
                                Console.WriteLine("Node is not found!");

                            }
                            splayRoot = resultNode;
                            splayRoot.parent = default;

                        }
                        break;
                    case 13:
                        {
                            if (splayRoot != null)
                            {
                                Console.WriteLine("SPLAY TREE >>> BREADTH-FIRST PRINT");
                                mySplayTree.breadthFirst(splayRoot);
                            }
                            else
                            {
                                Console.WriteLine("Root is empty.");
                            }
               

                        }
                        break;
                    case 14:
                        {
                            Console.WriteLine("SPLAY TREE >>> DELETE");
                            int key = ReadInt("SPLAY TREE >>> enter int");
                            splayNode<int> nodeToDelete = new splayNode<int>();
                            nodeToDelete.value = key;
                            splayRoot = mySplayTree.Delete(splayRoot, nodeToDelete);
                        }
                        break;
              
                }//end switch
            }//end big while

        }
        //Ask the user for a integer value
        //Handle bad input, only exits oince a numeric value is received
        static int ReadInt(string prompt)
        {
            int choice = -1;
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            while (!int.TryParse(input, out choice))
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
            }
            return choice;
        }

    }//end program
}
