using System;
using System.Collections.Generic;

namespace Day23
{
    class Program
    {
        static List<int> values = new List<int>();
        static Node first = null;
        static Dictionary<int, Node> nodeLookupTable = new Dictionary<int, Node>();
        static void Main(string[] args)
        {
            string input = "364289715";

            for (int i = 0; i < input.Length; i++)
            {
                values.Add(input[i] - 48);
            }
            CreateNodeList(first, values);
            Console.WriteLine("Puzzle 1: " + Puzzle1());
            for (int i = 10; i < 1000001; i++)
            {
                values.Add(i);
            }
            first = null;
            nodeLookupTable.Clear();
            CreateNodeList(first, values);
            Console.WriteLine("Puzzle 2: " + Puzzle2());
        }

        static int Puzzle1()
        {
            Node current = first;
            for (int move = 0; move < 100; move++)
            {
                Move(current);
                current = current.Next;
            }

            //Generate output
            Node valueNode = nodeLookupTable[1].Next;
            List<int> finalCupOrder = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                finalCupOrder.Add(valueNode.Value);
                valueNode = valueNode.Next;
            }

            return Convert.ToInt32(String.Join("", finalCupOrder));
        }

        static ulong Puzzle2()
        {
            Node current = first;
            for (int i = 0; i < 10000001; i++)
            {
                Move(current);
                current = current.Next;
            }

            Node firstValueNode = nodeLookupTable[1].Next;
            return (ulong)firstValueNode.Value * (ulong)firstValueNode.Next.Value;
        }

        static void Move(Node current)
        {
            List<Node> selectedCups = new List<Node>();
            List<int> selectedValues = new List<int>();

            int currentCupLabel = current.Value;
            //select Cups
            for (int i = 0; i < 3; i++)
            {
                Node selectedNode = current.Next;
                selectedCups.Add(selectedNode);
                selectedValues.Add(selectedNode.Value);
                current.Next = selectedNode.Next;
            }

            //Find destination cup label
            int destinationCupLabel = currentCupLabel;
            do
            {
                destinationCupLabel--;
                if (destinationCupLabel == 0) destinationCupLabel = values.Count;
            } while (selectedValues.Contains(destinationCupLabel));

            //Get destination cup
            Node insertionNode = nodeLookupTable[destinationCupLabel];

            //Reinsert cups
            Node next = insertionNode.Next;
            insertionNode.Next = selectedCups[0];
            selectedCups[2].Next = next;
        }

        static void CreateNodeList(Node firstNode, List<int> values)
        {
            Node previous = null;
            foreach (int value in values)
            {
                Node node = new Node(value);
                nodeLookupTable.Add(value, node);
                if (first == null) first = node;

                if (previous != null) previous.Next = node;
                previous = node;
            }
            previous.Next = first;
        }
    }
}
