using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPortEx2
{
    //Node (tree) implementation for Assessed Exercise 2

    //Hints : 
    //Use lecture materials from Week 4B
    // and lab sheet 'Lab 5: BinTree and BSTree' to aid with implementation.

    class Node<T> where T : IComparable
    {
        // Private field: data item stored in this node
        private T data;

        // Public fields: references to left and right child nodes
        // Must be public fields (not properties) to allow ref parameter passing
        // In BST, left subtree contains items smaller than current node
        // In BST, right subtree contains items larger than current node
        public Node<T> Left;
        public Node<T> Right;
        // Constructor to create a new node with the given item
        // Initialises the node with data and sets both children to null
        public Node(T item)
        {
            data = item;
            Left = null;
            Right = null;
        }
        // Data stored in this node
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

    } // End of class
}
