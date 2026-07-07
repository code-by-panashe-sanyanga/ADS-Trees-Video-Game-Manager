using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSPortEx2
{
    //Binary Tree implementation for Assessed Exercise 2

    //Hints : 
    //Use lecture materials from Week 4B
    // and lab sheet 'Lab 5: BinTree and BSTree' to aid with implementation...

    class BinTree<T> where T : IComparable
    {
        // Protected field to store the root node
        protected Node<T> root;

        // Default constructor that creates an empty tree
        public BinTree()
        {
            root = null;
        }

        // Constructor with a node that creates a tree with the given node as root
        public BinTree(Node<T> node)
        {
            root = node;
        }


        //Functions for EX.2A
        // In-order traversal: Left, Root, Right
        public void InOrder(ref string buffer)
        {
            StringBuilder sb = new StringBuilder();
            InOrderRecursive(root, sb);
            buffer = sb.ToString();
        }
        // Recursive helper for in-order traversal
        public void InOrderRecursive(Node<T> node, StringBuilder sb)
        {
            if (node != null)
            {
                InOrderRecursive(node.Left, sb);
                sb.Append(node.Data.ToString());
                sb.Append(" ");
                InOrderRecursive(node.Right, sb);
            }
        }
        // Pre-order traversal: Root, Left, Right
        public void PostOrder(ref string buffer)
        {
            StringBuilder sb = new StringBuilder();
            PreOrderRecursive(root, sb);
            buffer = sb.ToString();
        }
        // Recursive helper for pre-order traversal
        private void PreOrderRecursive(Node<T> node, StringBuilder sb)
        {
            if (node != null)
            {
                sb.Append(node.Data.ToString());
                sb.Append(" ");
                PreOrderRecursive(node.Left, sb);
                PreOrderRecursive(node.Right, sb);
            }
        }
        // Post-order traversal: Left, Right, Root
        public void PostOrder(ref string buffer)
        {
            StringBuilder sb = new StringBuilder();
            PostOrderRecursive(root, sb);
            buffer = sb.ToString();
        }


        // Recursive helper for post-order traversal
        private void PostOrderRecursive(Node<T> node, StringBuilder sb)
        {
            if (node != null)
            {
                PostOrderRecursive(node.Left, sb);
                PostOrderRecursive(node.Right, sb);
                sb.Append(node.Data.ToString());
                sb.Append(" ");
            }
        }








        //Free space, use as necessary to address task requirements... 





    } // End of class
}
