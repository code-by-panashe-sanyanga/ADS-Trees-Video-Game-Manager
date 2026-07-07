using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPortEx2
{
    //Binary Search Tree implementation for Assessed Exercise 2

    //Hints : 
    //Use lecture materials from Week 5
    // and lab sheet 'Lab 5: BinTree and BSTree' to aid with implementation.

    class BSTree<T> : BinTree<T> where T : IComparable
    {

        // Constructor to create an empty binary search tree
        // Initialises root to null to start with an empty tree

        public BSTree()
        {
            root = null;
        }

        //Functions for EX.2A
        // Insert an item into the BST
        public void InsertItem(T item)
        {
            InsertItemRecursive(ref root, item);
        }
        // Recursive helper for insertion
        private void InsertItemRecursive(ref Node<T> node, T item)
        {
            if (node == null)
            {
                node = new Node<T>(item);
            }
            else if (item.CompareTo(node.Data) < 0)
            {
                InsertItemRecursive(ref node.Left, item);
            }
            else if (item.CompareTo(node.Data) > 0)
            {
                InsertItemRecursive(ref node.Right, item);
            }
            // If item equals node.Data, do nothing (no duplicates allowed)
            // This maintains the unique title requirement for games
        }

        // Calculate the height of the tree
        public int Height()
        {
            return HeightRecursive(root);
        }

        // Recursive helper for height calculation
        protected int HeightRecursive(Node<T> node)
        {
            if (node == null)
                return -1;
            int leftHeight = HeightRecursive(node.Left);
            int rightHeight = HeightRecursive(node.Right);
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        // Find the earliest game (game with earliest release year)
        // note: typo in method name but works fine so leaving it
        // Must use recursion to search entire tree since tree is ordered by title, not year
        public T EarlieseGame()
        {
            if (root == null)
                throw new InvalidOperationException("Tree is empty");

            return EarlieseGameRecursive(root);
        }

        // Recursive helper to find game with earliest release year
        private T EarlieseGameRecursive(Node<T> node)
        {
            if (node == null)
                return default(T);

            T earliest = node.Data;
            T leftEarliest = default(T);
            T rightEarliest = default(T);

            // Check left subtree recursively to find earliest game there
            if (node.Left != null)
            {
                leftEarliest = EarlieseGameRecursive(node.Left);
                if (leftEarliest != null && leftEarliest is VideoGame leftGame && earliest is VideoGame currentGame)
                {
                    if (leftGame.Releaseyear < currentGame.Releaseyear)
                        earliest = leftEarliest;
                }
            }

            // Check right subtree recursively to find earliest game there
            if (node.Right != null)
            {
                rightEarliest = EarlieseGameRecursive(node.Right);
                if (rightEarliest != null && rightEarliest is VideoGame rightGame && earliest is VideoGame currentGame2)
                {
                    if (rightGame.Releaseyear < currentGame2.Releaseyear)
                        earliest = rightEarliest;
                }
            }

            return earliest;
        }

        //Functions for EX.2B

        // Count how many items are in the tree
        // Uses recursion to visit every node and count them
        public int Count()
        {
            return CountRecursive(root);
        }

        // Recursive helper that counts nodes in subtree
        // Similar structure to Height but counting nodes instead of levels
        private int CountRecursive(Node<T> node)
        {
            if (node == null)
                return 0;  // Empty subtree has zero nodes
            else
                // Count current node (1) plus all nodes in left and right subtrees
                return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }

        // Update an existing game by finding it and replacing its data
        // If the game doesn't exist, it gets inserted instead (based on spec requirements)
        public void Update(T item)
        {
            UpdateRecursive(ref root, item);
        }

        // Recursive helper that searches for item and updates it
        // Uses BST property to navigate: smaller items left, larger items right
        private void UpdateRecursive(ref Node<T> node, T item)
        {
            if (node == null)
            {
                // Didn't find the game, so insert it as a new node
                // This means Update can also add games if they don't exist
                node = new Node<T>(item);
            }
            else if (item.CompareTo(node.Data) < 0)
            {
                // Item is smaller, search in left subtree
                UpdateRecursive(ref node.Left, item);
            }
            else if (item.CompareTo(node.Data) > 0)
            {
                // Item is larger, search in right subtree
                UpdateRecursive(ref node.Right, item);
            }
            else
            {
                // Found it! The CompareTo returned 0, so titles match
                // Replace the node's data with the new item
                node.Data = item;
            }
        }

        // Remove an item from the tree
        // Needed for later AVL tree implementation and general tree operations
        public void RemoveItem(T item)
        {
            RemoveItemRecursive(ref root, item);
        }

        // Recursive helper for removal - this is the tricky one
        // Has to handle four different cases depending on how many children the node has
        private void RemoveItemRecursive(ref Node<T> node, T item)
        {
            if (node == null)
                return;  // Item not in tree, nothing to remove

            if (item.CompareTo(node.Data) < 0)
            {
                // Item is smaller, search left subtree
                RemoveItemRecursive(ref node.Left, item);
            }
            else if (item.CompareTo(node.Data) > 0)
            {
                // Item is larger, search right subtree
                RemoveItemRecursive(ref node.Right, item);
            }
            else
            {
                // Found the node to remove - now handle the different cases
                if (node.Left == null && node.Right == null)
                {
                    // Case 1: No children - just remove the node
                    node = null;
                }
                else if (node.Left == null)
                {
                    // Case 2: Only right child - replace node with its right child
                    node = node.Right;
                }
                else if (node.Right == null)
                {
                    // Case 3: Only left child - replace node with its left child
                    node = node.Left;
                }
                else
                {
                    // Case 4: Two children - find inorder successor (smallest in right subtree)
                    // Copy successor's data to current node, then remove the successor
                    Node<T> successor = FindMin(node.Right);
                    node.Data = successor.Data;
                    RemoveItemRecursive(ref node.Right, successor.Data);
                }
            }
        }

        // Helper to find the minimum node in a subtree
        // Used when removing a node with two children
        // Minimum is always the leftmost node
        private Node<T> FindMin(Node<T> node)
        {
            while (node.Left != null)
                node = node.Left;

            return node;
        }

        // Contains: checks if tree contains specified item
        // Uses BST property for efficient search, can skip subtrees
        public bool Contains(T item)
        {
            return ContainsRecursive(root, item);
        }

        // Recursive helper searches using BST navigation
        // Returns true if item found, false if search reaches null node
        private bool ContainsRecursive(Node<T> node, T item)
        {
            if (node == null)
                return false;

            int comparison = item.CompareTo(node.Data);
            if (comparison == 0)
                return true;
            else if (comparison < 0)
                // Item smaller, must be in left subtree if exists
                return ContainsRecursive(node.Left, item);
            else
                // Item larger, must be in right subtree if exists
                return ContainsRecursive(node.Right, item);
        }

        // ListGamesByYear: collects all games with matching release year
        // Returns formatted string in buffer parameter
        // Requires full traversal since tree ordered by title, not year
        public void ListGamesByYear(int year, ref string buffer)
        {
            StringBuilder sb = new StringBuilder();
            ListGamesByYearRecursive(root, year, sb);
            buffer = sb.ToString();
        }

        // Recursive helper uses in-order traversal to check all nodes
        // Adds matching games to StringBuilder with newline separators
        private void ListGamesByYearRecursive(Node<T> node, int year, StringBuilder sb)
        {
            if (node != null)
            {
                // Traverse left subtree
                ListGamesByYearRecursive(node.Left, year, sb);

                // Check current node if it is a VideoGame
                if (node.Data is VideoGame game && game.Releaseyear == year)
                {
                    sb.Append(game.ToString());
                    sb.Append("\n");
                }

                // Traverse right subtree
                ListGamesByYearRecursive(node.Right, year, sb);
            }
        }

        // GetAllItems: returns all items from tree in sorted order
        // Uses in-order traversal to produce ascending sorted list
        // Needed for migrating data between BST and AVL tree structures
        public List<T> GetAllItems()
        {
            List<T> items = new List<T>();
            GetAllItemsRecursive(root, items);
            return items;
        }

        // Recursive helper performs in-order traversal
        // Adds items to list in sorted order (left, node, right)
        private void GetAllItemsRecursive(Node<T> node, List<T> items)
        {
            if (node != null)
            {
                GetAllItemsRecursive(node.Left, items);
                items.Add(node.Data);
                GetAllItemsRecursive(node.Right, items);
            }
        }

        //Free space, use as necessary to address task requirements... 





    }// End of class
}
