
// KRISTY RATH
// 0707345
// AS4
// BINARY SEARCH TREE with insert, delete, search, findsmallest, find sibling, find parent sibling methods

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace As5
{

    public class splayNode<T>
    {
        public T value;
        public splayNode<T> parent;
        public splayNode<T> left;
        public splayNode<T> right;
        public override String ToString()
        {
            string str = "Value: " + value + " Left: " + left + " Right: " + right;
            return str;
        }

        // compares 2 splayNodes 
        public int CompareTo(splayNode<T> obj)
        {
            if (obj == null)
            {
                return -2;
            }
            else if (this == obj)
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }



    }
    class SplayTree<T> where T : IComparable
    {

        public int count = 0;
        // CONSTRUCTOR
        public SplayTree()
        {
            splayNode<T> root;
            count = 0;
        }

        // INSERT METHO 
        public splayNode<T> insert(splayNode<T> root, T v) // int v
        {

            // insert at empty list

            if (count == 0 || root == null) 
            {
                root = new splayNode<T>();
                root.parent = null;
                root.value = v;
            }
            // insert to left when value is smaller than root
            else if (root.value.CompareTo(v) == 1)
            {
                root.left = insert(root.left, v);
                root.left.parent = root;

            }
            // insert to right when value is larger than root
            else
            {
                root.right = insert(root.right, v);
                root.right.parent = root;
            }

            return root;
           

        }




        // TRAVERSE METHOD
        public void traverse(splayNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            Console.WriteLine(root.value.ToString());
            traverse(root.left);
            traverse(root.right);


        }

        // INORDER TRAVERSAL
        public string inOrder(splayNode<T> root)
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

        // PREORDER TRAVERSAL
        public string preOrder(splayNode<T> root)
        {

            if (root == null)
            {
                return "";
            }
            Console.WriteLine(root.value.ToString());
            preOrder(root.left);
            preOrder(root.right);
            return "";
        }

        // POSTORDER TRAVERSAL
        public string postOrder(splayNode<T> root)
        {

            if (root == null)
            {
                return "";
            }
            postOrder(root.left);
            postOrder(root.right);
            Console.WriteLine(root.value.ToString());

            return "";
        }

        // SEARCH METHOD
        public splayNode<T> Search(splayNode<T> root, splayNode<T> splayNodeToFind)
        {

            return Splay(root, splayNodeToFind);
        }

        // SPLAY METHOD
        // recursively rotates the most recently accessed element to the root
        // OPTIONS:
        // can be used to delete a node >>> splays the parent node to the root
        // can be used to search for node >>> splays the found element to the root, otherwise splay the most recently accessed element
        public splayNode<T> Splay(splayNode<T> root, splayNode<T> splayNodeToFind, bool deleteNode = false)
        {
            // CASE 1: splayNodeToFind is root 
            if (splayNodeToFind.value.CompareTo(root.value) == 0)
            {
                // deletes node if option is set
                if (deleteNode == true)
                {
                    root = delete(root, splayNodeToFind);
                }
                return root;
            }
            // CASE: 3: ZIG ZIG / ZAG ZIG (rotate left twice or rotate left then right)
            // TRAVERSE LEFT SUBTREE
            else if (root.value.CompareTo(splayNodeToFind.value) == 1 && root.left != null)
            {
                // checks if value is found in the left child
                // 2 Configurations:
                // In search >>> if found, visit node
                // In delete >>> if found, don't visit node. Delete then splay parent.

                // FOR SEARCH, if node is found in left child, rotate right
                if (root.left.value.CompareTo(splayNodeToFind.value) != 0) {
                    // if node is found in left.left or left.right, execute one rotation
                    // the second rotation executes after
                    if (root.left.value.CompareTo(splayNodeToFind.value) == 1)
                    {
                        if (root.left.left != null)
                        {
                            root.left.left = Splay(root.left.left, splayNodeToFind, deleteNode);
                   
                            root.left = RotateRight(root.left);

                        }


                    }
                    else if (root.left.value.CompareTo(splayNodeToFind.value) == -1)
                    {
                        if (root.left.right != null)
                        {
                            root.left.right = Splay(root.left.right, splayNodeToFind, deleteNode);
                  
                            root.left = RotateLeft(root.left);
                            
                        }

                    }
                }
                else
                {
                    // FOR DELETE NODE, if node to delete is found in as child leaf node, delete 
                    if (deleteNode == true && root.left.left == null && root.left.right == null)
                    {
                        root = delete(root.left, splayNodeToFind);
                        // returns the parent node to be splay
                        return root;
                        
                    }
                }

                // second rotation
                root = RotateRight(root);
            }
            // CASE 4: ZAG ZAG / ZIG ZAG (rotate right twice or rotate right then left)
            // TRAVERSE RIGHT SUBTREE
            else if (root.value.CompareTo(splayNodeToFind.value) == -1 && root.right != null)
            {
                // checks if value is found in the right child
                // 2 Configurations:
                // In search >>> if found, visit node
                // In delete >>> if found, don't visit node. Delete then splay parent.
                if (root.right.value.CompareTo(splayNodeToFind.value) != 0)
                {
                    if (root.right.value.CompareTo(splayNodeToFind.value) == 1)
                    {
                        if (root.right.left != null)
                        {
                            root.right.left = Splay(root.right.left, splayNodeToFind, deleteNode);

                            // ZAG
                            root.right = RotateRight(root.right);
                        }


                    }
                    else if (root.right.value.CompareTo(splayNodeToFind.value) == -1)
                    {
                        if (root.right.right != null)
                        {
                            root.right.right = Splay(root.right.right, splayNodeToFind, deleteNode);

                            // ZIG
                            root.right = RotateLeft(root.right);
                        }

                    }
                }
                else
                {
                    // FOR DELETE NODE, if node to delete is found in as child leaf node, delete 
                    if (deleteNode == true && root.right.left == null && root.right.right == null)
                    {
                        root = delete(root.right, splayNodeToFind);
                        return root;

                    }
                }
                // second rotation (ZIG)
                root = RotateLeft(root);
            }
            return root;
        }



        public splayNode<T> Delete(splayNode<T> root, splayNode<T> toDelete)
        {
            return Splay(root, toDelete, true);
        }


        // DELETE METHOD

        private splayNode<T> delete(splayNode<T> root, splayNode<T> toDelete)
        {
            // check if item to delete exists
            //if (Search(root, toDelete) != null)
            if (root.value != null)
            {
                int children = GetNumChildren(root);

                // IF splayNode HAS 0 CHILDREN, IS LEAF splayNode, THEN NULLIFY
                if (children == 0)
                {
                    if (isRightChild(root))
                    {
                        root.parent.right = null;

                    }
                    else
                    {

                        root.parent.left = null;
                    }
                    return root.parent;
                    
                }
                // IF splayNode HAS 1 CHILDREN, REASSIGN CHILDREN TO PARENT
                else if (children == 1)
                {
                    // DELETE splayNodeS WITH 1 LEFT CHILDREN
                    if (HasLeftChild(root))
                    {
                        if (root.parent != null)
                        {
                            root.left.parent = root.parent;
                            // check if splayNode is a right or left splayNode, to assign parent
                            if (isRightChild(root))
                            {
                                root.parent.right = root.left;
                            }
                            else
                            {
                                root.parent.left = root.left;
                            }
                        }
                        else
                        {
                            root.left.parent = null;
                            root = root.left;

                        }

                    }
                    // DELETE splayNodeS WITH 1 RIGHT CHILDREN

                    else
                    {
                        if (root.parent != null)
                        {

                            root.right.parent = root.parent;
                            if (isRightChild(root))
                            {
                                root.parent.right = root.right;
                                return root.parent.right;
                            }
                            else
                            {
                                root.parent.left = root.right;
                                return root.parent.left;
                            }
                        }
                        else
                        {

                            root.right.parent = null;
                            root = root.right;
                            return root;
                        }


                    }
                    root = null;
                }
                // DELETING splayNodeS WITH TWO CHILDREN
                else if (children == 2)
                {
                    // finding smallest number of right subtree
                    splayNode<T> smallest = FindSmallest(root.right);
                    Console.WriteLine("Smallest on right side of tree: " + smallest.ToString());

                    // connect position of smallest splayNode to toDelete splayNode
                    smallest.left = root.left;
                    smallest.right = root.right;
                    root.right.parent = smallest;
                    root.left.parent = smallest;

                    // disconnect current parent of smallest value
                    if (smallest.parent != null)
                    {
                        if (isRightChild(smallest))
                        {
                            smallest.parent.right = null;

                        }
                        else
                        {
                            smallest.parent.left = null;
                        }
                    }

                    // assign parent to smallest splayNode
                    if (root.parent != null)
                    {
                        smallest.parent = root.parent;
                        if (isRightChild(root))
                        {
                            root.parent.right = smallest;
                            return root.parent.right;
                        }
                        else
                        {
                            root.parent.left = smallest;
                            return root.parent.left;
                        }
                    }
                    else
                    {
                        // if smallest replaces root, no parent is assigned
                        smallest.parent = null;
                        root = smallest;
                        return root;
                    }




                    root = null;
                }
                return root;
            }
            return root;
        }



        // GET SIBLINGS METHOD
        public List<splayNode<T>> GetSiblings(splayNode<T> root)
        {

            List<splayNode<T>> siblings = new List<splayNode<T>>();

            // if splayNode is root, return no siblings
            if (root.parent == null)
            {
                return siblings;
            }
            else
            {
                // check if parent is left or right child, then add its sibling
                if (root.parent.left != null)
                {
                    if (root.parent.left.CompareTo(root) == 0)
                    {
                        siblings.Add(root.parent.right);
                    }
                }
                else if (root.parent.right != null)
                {

                    if (root.parent.right.CompareTo(root) == 0)
                    {
                        siblings.Add(root.parent.left);

                    }
                }
            }

            return siblings;

        }

        // GETSIBLINGSOFPARENT
        public List<splayNode<T>> GetSiblingOfParent(splayNode<T> root)
        {
            List<splayNode<T>> siblingsOfParent;

            splayNode<T> parent = root.parent;
            siblingsOfParent = GetSiblings(parent);
            return siblingsOfParent;
        }


        public string breadthFirst(splayNode<T> root)
        {
            // first in front, out from front
            LinkedList<splayNode<T>> queue = new LinkedList<splayNode<T>>();
            LinkedList<splayNode<T>> visitedsplayNodes = new LinkedList<splayNode<T>>();


            // add root to queue
            queue.AddLast(root);
            int numLoops = 0;
            while (queue.Count != 0)
            {
                splayNode<T> n = queue.First();
                 Console.Write(n.value.ToString() + " || ");


                if (n.left != null)
                {
                    queue.AddLast(n.left);
                }
                if (n.right != null)
                {
                    queue.AddLast(n.right);
                }
                // remove from queue once iterated through
                queue.RemoveFirst();
                numLoops++;
            }

            return "";

        }

        // FINDSMALLEST METHOD
        public splayNode<T> FindSmallest(splayNode<T> root)
        {
            // go to the left most branch
            while (root.left != null)
            {
                root = root.left;
            }
            return root;

        }

        // GETNUMCHILDREN METHOD
        public int GetNumChildren(splayNode<T> nd)
        {
            int numChildren = 0;

            if (nd.left != null)
            {
                numChildren++;
            }
            if (nd.right != null)
            {
                numChildren++;
            }
            return numChildren;

        }
        // HASRIGHTCHILD METHOD
        public bool HasRightChild(splayNode<T> nd)
        {
            if (nd.right != null)
            {
                return true;
            }
            return false;
        }

        // HASLEFTCHILD METHOD
        public bool HasLeftChild(splayNode<T> nd)
        {
            if (nd.left != null)
            {
                return true;
            }
            return false;
        }
        // ISRIGHTCHILD METHOD
        public bool isRightChild(splayNode<T> nd)
        {
            if (nd.parent != null && nd.parent.right == nd)
            {
                return true;
            }
            return false;
        }
        public splayNode<T> RotateRight (splayNode<T> node)
        {
            
            splayNode<T> temp = node.left;
            splayNode<T> subtree = temp.right;

            // Perform rotation
            temp.right = node;
            node.left = subtree;
            return temp;
        }
        public splayNode<T> RotateLeft(splayNode<T> node)
        {
        

            splayNode<T> temp = node.right;
            splayNode<T> subtree = temp.left;

            // Perform rotation
            temp.left = node;
            node.right = subtree;

            return temp;
        }


     

    }
}

