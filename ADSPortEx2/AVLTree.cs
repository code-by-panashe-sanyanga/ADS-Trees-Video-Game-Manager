using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPortEx2
{
    //AVL Tree implementation for Assessed Exercise 2

    //Hints : 
    //Use lecture materials from Week 6A
    // and lab sheet 'Lab 6: AVL Trees' to aid with implementation...

    //You may need to adjust your other tree classes to allow your AVL tree
    // access to certain attributes and functions

    class AVLTree<T> : BSTree<T> where T : IComparable
    {
        //Functions for EX.2C
        // Using 'new' keyword to hide base class method
        public new void InsertItem(T item)
        {
            InsertItemRecursive(ref root, item);
        }

        // Recursive helper for AVL insertion with balancing
        private void InsertItemRecursive(ref Node<T> node, T item)
        {
            if (node == null)
            {
                node = new Node<T>(item);
            }
            else if (item.CompareTo(node.Data) < 0)
            {
                InsertItemRecursive(ref node.Left, item);
                node = Balance(node);
            }
            else if (item.CompareTo(node.Data) > 0)
            {
                InsertItemRecursive(ref node.Right, item);
                node = Balance(node);
            }
            // If item equals node.Data, do nothing (no duplicates)
        }

        public new void RemoveItem(T item)
        {
            RemoveItemRecursive(ref root, item);
        }

        // Recursive helper for AVL removal with balancing
        private void RemoveItemRecursive(ref Node<T> node, T item)
        {
            if (node == null)
                return;

            if (item.CompareTo(node.Data) < 0)
            {
                RemoveItemRecursive(ref node.Left, item);
                node = Balance(node);
            }
            else if (item.CompareTo(node.Data) > 0)
            {
                RemoveItemRecursive(ref node.Right, item);
                node = Balance(node);
            }
            else
            {
                // Node to remove found, handle different cases based on children
                if (node.Left == null && node.Right == null)
                {
                    // Case 1: No children, simply remove the node
                    node = null;
                }
                else if (node.Left == null)
                {
                    // Case 2: Only right child, replace node with its right child
                    node = node.Right;
                }
                else if (node.Right == null)
                {
                    // Case 3: Only left child, replace node with its left child
                    node = node.Left;
                }
                else
                {
                    // Case 4: Two children, find inorder successor (smallest in right subtree)
                    // Replace node's data with successor's data, then remove successor
                    Node<T> successor = FindMin(node.Right);
                    node.Data = successor.Data;
                    RemoveItemRecursive(ref node.Right, successor.Data);
                    node = Balance(node);
                }
            }
        }

        // Calculate balance factor of a node
        // Balance factor = left height - right height
        private int BalanceFactor(Node<T> node)
        {
            if (node == null)
                return 0;
            return HeightRecursive(node.Left) - HeightRecursive(node.Right);
        }

        // Balance the tree by performing rotations if needed
        private Node<T> Balance(Node<T> node)
        {
            if (node == null)
                return null;

            int balanceFactor = BalanceFactor(node);

            // Left heavy, needs right rotation
            if (balanceFactor > 1)
            {
                // Check if left subtree is right heavy (left-right case)
                // If so, need to do double rotation: left rotation first, then right
                if (BalanceFactor(node.Left) < 0)
                {
                    // Left-Right rotation, rotate left first, then right
                    node.Left = RotateLeft(node.Left);
                }
                // Perform right rotation
                return RotateRight(node);
            }
            // Right heavy, needs left rotation
            else if (balanceFactor < -1)
            {
                // Check if right subtree is left heavy (right-left case)
                // If so, need to do double rotation: right rotation first, then left
                if (BalanceFactor(node.Right) > 0)
                {
                    // Right-Left rotation, rotate right first, then left
                    node.Right = RotateRight(node.Right);
                }
                // Perform left rotation
                return RotateLeft(node);
            }

            // Tree is balanced
            return node;
        }

        // Perform right rotation around given node
        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> leftChild = node.Left;
            node.Left = leftChild.Right;
            leftChild.Right = node;
            return leftChild;
        }

        // Perform left rotation around given node
        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> rightChild = node.Right;
            node.Right = rightChild.Left;
            rightChild.Left = node;
            return rightChild;
        }

        // Find minimum node in a subtree
        // Reusing from BSTree - works the same way
        private Node<T> FindMin(Node<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        //Free space, use as required






    }// End of class
}
