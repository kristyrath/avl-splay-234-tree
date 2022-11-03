using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    Taken from https://simpledevcode.wordpress.com/2014/09/16/avl-tree-in-c/

    with modifications for C#, 

  */
namespace As5
{
    class AVLNode<T> where T : IComparable
    {
        //public int key;  //  we combine our key and value as the same thing but they could be distinct
        public T value;
        public AVLNode<T> left;
        public AVLNode<T> right;
        public int balance = 0; // Students need to worry about this
                                // public AVLNode<T>Parent;  //might be useful

        public int CompareTo(AVLNode<T> anotherNode)
        {
            if (anotherNode != null)
            {
                if (this == anotherNode)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
                
            }

            return -2;
        }

    }

    class AVLTree<T> where T : IComparable
    {
        //public AVLTree() {
        //    AVLNode<T> root;
        //}
            
        public AVLNode<T> insert(AVLNode<T> root, T v)
        {
            if (root == null)
            {
                root = new AVLNode<T>();
                root.value = v;
                return root;
            }
            //else if (v < root.value)
            else if (v.CompareTo(root.value) == -1)
            {

                root.left = insert(root.left, v);
                root = balance_tree(root);
            }
            //else if (v > root.value)
            else if (v.CompareTo(root.value) == 1)
            {
                root.right = insert(root.right, v);
                root = balance_tree(root);
            }

            return root;
        }

        // This is the code which actually balances the tree, it calls the different cases
        // but RotateLeftLeft, RotateLeftRight, and RotateRightLeft are to be filled in by the student.  
        private AVLNode<T>balance_tree(AVLNode<T>root)
        {
            int b_factor = balance_factor(root);// balance factor is left_height - right_height
            if (b_factor > 1)
            {
                //we have to rotate right
                if (balance_factor(root.left) == 1)
                {
                    //simple right rotation
                    root = RotateLeftLeft(root);
                }
                else
                {
                    //straighten the bend first, and rotate right.
                    root = RotateLeftRight(root);
                }
            }
            else if (b_factor < -1)
            {
                //we have to rotate left
                if (balance_factor(root.right) == 1)
                {
                    //straighten the bend and then rotate left.
                    root = RotateRightLeft(root);
                }
                else
                {
                    //use this as a template
                    //This is a simple left rotation
                    root = RotateRightRight(root);
                }
            }
            return root;
        }//end balance_tree

        // SEARCH METHOD (SOURCED FROM BST SEARCH)
        public AVLNode<T> Find(AVLNode<T> root, AVLNode<T> AVLNodeToFind)
        {
            // IF AVLNode TO SEARCH FOR IS NOT DEFINED, RETURN NULL AVLNode
            AVLNode<T> newAVLNode = null;
            if (AVLNodeToFind == null)
            {
                return newAVLNode;
            }
            // ITERATE TO EITHER LEFT OR RIGHT BRANCH 
            AVLNode<T> currentAVLNode = root;
            while (currentAVLNode != null)
            {
                // if found, return AVLNode
                if (AVLNodeToFind.value.CompareTo(currentAVLNode.value) == 0)
                {
                    return currentAVLNode;
                }
                // if larger than current AVLNode, go to right subtree
                else if (AVLNodeToFind.value.CompareTo(currentAVLNode.value) == 1)
                {
                    currentAVLNode = currentAVLNode.right;
                }
                // if smaller than current AVLNode, go to left subtree
                else
                {
                    currentAVLNode = currentAVLNode.left;
                }
            }

            return newAVLNode;


        }

// RECURSIVE DELETE FUNCTION FOR AVL TREES
//- Traverse tree 
//    - Left 
//        - Recursive call
//    - Right
//        - Recursive call
//    - Found
//        - Delete then reconnect
//        - AVLNode<T> with only one child
//            - Save the one child in the root
//        - AVLNode<T> with no child
//        - AVLNode<T> with two children
//            - Get in order successor(smallest in right subtree)
//            - AA
//- Check balance 
//    - If tree only has 1 node, no need
//    - Update height
//    - Balance factor 
//- Rebalance cases
//    - Left Left Case
//        - Balance > 1 for current and child left height > right
//    - Left Right Case
//        - Balance > 1 for current and child right height > left
//    - Right Right case
//        - Balance< 0 for current and child right height> left 
//    - Right left case
//        - Balance< 0 for current and child left height> right


        public AVLNode<T> Delete(AVLNode<T> root, AVLNode<T> NodeToDelete)
        {

            if (root != null)
            {
                NodeToDelete = Find(root, NodeToDelete);
                
                if (NodeToDelete != null)
                {
                    AVLNode<T> result;
                    // RECURSIVELY TRAVERSE NODE

                    // traverse rigth child

                    if (root.right != null && root.value.CompareTo(NodeToDelete.value) == -1)
                    {
                        result = Delete(root.right, NodeToDelete);
                        if (result == null)
                        {
                            root.right = null;
                        }
                    }
                    // traverse left child
                    else if (root.left != null && root.value.CompareTo(NodeToDelete.value) == 1)
                    {
                        result = Delete(root.left, NodeToDelete);
                        if (result == null)
                        {
                            root.left = null;
                        }
                    }
                    // if value to delete is found
                    else
                    {
                        if (root.right != null & root.left != null)
                        {
                            // replace root with successor
                            AVLNode<T> successor = getSuccesor(root);
                            root.value = successor.value;

                            // delete the successor's previous position
                            // parameter specified as root.right instead of success so that each recursive return can rebalance
                            result = Delete(root.right, successor);

                        }
                        else if (root.right != null)
                        {
                            root = root.right;
                        }
                        else if (root.left != null)
                        {
                            root = root.left;
                        }
                        else
                        {

                            root = null;
                            return root;
                        }
                        
                    }

                    // REBALANCE EACH AVLNode<T> SUBTREE AT EVERY RECURSIVE VISIT
                    if (root != null)
                    {
                        switch (Balance(root))
                        {
                            case ("Right Right Case"):
                                root = RotateRightRight(root);
                                break;
                            case ("Right Left Case"):
                                root = RotateRightLeft(root);
                                break;
                            case ("Left Right Case"):
                                root = RotateLeftRight(root);
                                break;
                            case ("Left Left Case"):
                                root = RotateLeftLeft(root);
                                break;
                            case ("Balanced"):
                                return root;
                        }
                    }
       


                    
                }
            }
            return root;

        }

        // BALANCE METHOD
        // purpose:
        // used to determine whether the imbalance is a right right, right left, left left, or left right case
        // called in delete method to perform the right rotation
        public string Balance(AVLNode<T> root)
        {


            // RIGHT RIGHT CASE
            if (root != null)
            {
                int rootBalance = balance_factor(root);

                // right heavy 
                if (rootBalance < -1)
                {
                    //right right heavy
                    if (balance_factor(root.right) <= 0)
                    {
                        return "Right Right Case";
                    }
                    // right left heavy
                    else
                    {
                        return "Right Left Case";
                    }
                }
                // left heavy
                else if (rootBalance > 1)
                {
                    if (balance_factor(root.right) <= 0)
                    {
                        return "Left Right Case";
                    }
                    else
                    {
                        return "Left Left Case";
                    }

                }
                return "Balanced";
            }
            else
            {
                return "Balanced";
            }
        }

        // get successor of a note to replace after deletion
        public AVLNode<T> getSuccesor(AVLNode<T> current )
        {
            AVLNode<T> successor = null;
            current = current.right;
            // recursively retrieve the left most element of a subtree
            while (current.left != null)
            {
                current = current.left;
            }
            if (current.left == null)
            {
                successor = current;
            }
            return successor;
        }


        public int getHeight(AVLNode<T>current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = Math.Max(l, r);
                height = m + 1;
            }
            return height;
        }
        public int balance_factor(AVLNode<T>current)
        {
            int l = 0;
            int r = 0;
            if (current != null) {

                if (current.left != null)
                {
                    l = getHeight(current.left);
                }
                if (current.right != null)
                {
                    r = getHeight(current.right);

                }
            }

            int b_factor = l - r;
            return b_factor;
        }
        //Level order traversal repurposed from http://www.geeksforgeeks.org/level-order-tree-traversal/

        // NOTE: this function properly spaces for 1 to 2 signle digit values, 3 digits and above may alter the format slightly

        public void printLevelOrder(AVLNode<T>root)
        {
            int treeHeight = getHeight(root);
            int i;
            string temp;//formatting 
            for (i = 1; i <= treeHeight; i++)
            {
                Console.WriteLine();
     
                // prints space before
                string spaceBefore = "";
                // calculates the width of the space before pritning elements
                int spaceWidth = (int)Math.Pow(2, treeHeight - i + 1) - 1;
                // form string
                for (int j = 0; j < (spaceWidth / 2); j++)
                {
                    spaceBefore += "  ";
                }
                Console.Write(spaceBefore);//the formatting being printed


                // prints elements with spaces between
                printGivenLevel(root, i, treeHeight, i);
            }
        }
        public void printGivenLevel(AVLNode<T> root, int level, int treeHeight, int currentLevel)
        {
           
           
                if (level > 1)
                {
                    printGivenLevel(root.left, level - 1, treeHeight, currentLevel);


                    printGivenLevel(root.right, level - 1, treeHeight, currentLevel);
                }
                else
                {
                    string space = "";

                
                    // calculate width of a single space between elements


                    // FORMULA ensures that the total of numbers of slots(columns) in the last row, always fits
                    // all the preceding parent elements with spacing
                    int spaceWidth = (int)Math.Pow(2, treeHeight - currentLevel + 1) - 1;

                    // concatenate space string with appropriate amount
                    for (int i = 0; i < spaceWidth; i++)
                    {
                        space += "  ";
                    }
     
                    // prints elements with space after
                    if (root == null)
                    {
                        Console.Write("ni" + space);
                        return;
                    }
                    else if (level == 1)
                    {
                        string str = "";
                        // pad single values with space
                        if (root.value.CompareTo(10) < 0)
                        {
                            str = " ";
                        }
                        Console.Write(str + root.value + space);
                    }
                }


            
            

        }
        public string PreOrder(AVLNode<T>root)
        {
            if (root == null)
            {
                return "nil";
            }
            return root.value.ToString() + " " + PreOrder(root.left) + " " + PreOrder(root.right);


        }
        //AVL Lab

        //
        // right sub tree of a right subtree case

        // Note that you should use the diagram from the notes (slides 18 as of this writing) to try and implement the following
        //This handles the case where the tree is in the right-right case.
        //We rotate left.
        public AVLNode<T>RotateRightRight(AVLNode<T>parent)
        {
            AVLNode<T> pivot = parent.right;
            if (parent != null)
            { 
                parent.right = pivot.left;
                pivot.left = parent;
                return pivot;
            }
            return pivot;

        }

        // This handles the case where the tree is out of balance with AVLNodes in the left-left case.  

        public AVLNode<T>RotateLeftLeft(AVLNode<T>parent)
        {
            AVLNode<T>pivot = parent.left;
            if (parent != null)
            {
                parent.left = pivot.right;
                pivot.right = parent;
            }
            return pivot;
        }
        //This handles the case where the out of balance AVLNode<T>is in the left-right case.

        public AVLNode<T>RotateLeftRight(AVLNode<T>parent)
        {
            AVLNode<T> pivot = parent.left;
            if (parent != null)
            {
                parent.left = RotateRightRight(pivot);
                return RotateLeftLeft(parent);
            }
            return pivot; 
        }
        //This handles the case where the out of balance AVLNode<T>is in the right-left case.

        public AVLNode<T>RotateRightLeft(AVLNode<T>parent)
        {
            AVLNode<T>pivot = parent.right;
            if (parent != null)
            {
                parent.right = RotateLeftLeft(pivot);
                return RotateRightRight(parent);
            }
            return pivot; 
        }
        
        public string inOrder(AVLNode<T> root)
        {
            
            if (root == null)
            {
                return "";
            }
            inOrder(root.left);
            Console.WriteLine(root.value.ToString());
            inOrder(root.right);
            return "";
        }
        public string breadthFirst(AVLNode<T> root)
        {
            // first in front, out from front
            LinkedList<AVLNode<T>> queue = new LinkedList<AVLNode<T>>();



            queue.AddLast(root);
            int numLoops = 0;
            while (queue.Count != 0)
            {
                //Console.WriteLine("\nLoop iteration" + numLoops);
                AVLNode<T> n = queue.First();
                Console.Write(n.value.ToString() );

                //Console.WriteLine("Queue:");
                //foreach (AVLNode<T> nd in queue)
                //{
                //    Console.WriteLine(nd.value.ToString());
                //}

                if (n.left != null)
                {
                    queue.AddLast(n.left);
                }
                if (n.right != null)
                {
                    queue.AddLast(n.right);
                }
                Console.Write(" || ");


                queue.RemoveFirst();
                numLoops++;
            }

            return "";

        }





    }//end class AVL Tree
}//end namespace

