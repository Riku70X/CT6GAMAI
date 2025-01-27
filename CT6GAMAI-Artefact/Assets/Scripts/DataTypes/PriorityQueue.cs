// Most logic taken from https://visualstudiomagazine.com/Articles/2012/11/01/Priority-Queues-with-C.aspx

using System.Collections.Generic;
using Assets.Scripts.Desires;
using UnityEngine;

namespace Assets.Scripts.DataTypes
{
    public class PriorityQueue <T> where T : Desire
    {
        private List<T> data;

        public PriorityQueue()
        {
            data = new List<T>();
        }

        public void Enqueue(T item)
        {
            data.Add(item);
            int childIndex = data.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (data[childIndex].CompareTo(data[parentIndex]) >= 0)
                    break;
                (data[parentIndex], data[childIndex]) = (data[childIndex], data[parentIndex]);
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            if (data.Count <= 0)
            {
                Debug.LogError("ERROR: Can't Dequeue an empty Queue");
                return null;
            }

            // Assumes pq isn't empty
            int lastIndex = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[lastIndex];
            data.RemoveAt(lastIndex);

            --lastIndex;
            int parentIndex = 0;
            while (true)
            {
                int childIndex = parentIndex * 2 + 1;
                if (childIndex > lastIndex) break;
                int rightChild = childIndex + 1;
                if (rightChild <= lastIndex && data[rightChild].CompareTo(data[childIndex]) < 0)
                    childIndex = rightChild;
                if (data[parentIndex].CompareTo(data[childIndex]) <= 0) break;
                (data[childIndex], data[parentIndex]) = (data[parentIndex], data[childIndex]);
                parentIndex = childIndex;
            }
            return frontItem;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < data.Count; ++i)
                s += data[i].ToString() + " ";
            s += "count = " + data.Count;
            return s;
        }

        public int Count()
        {
            return data.Count;
        }

        public bool IsEmpty()
        {
            return data.Count == 0;
        }

        public T Peek()
        {
            T frontItem = data[0];
            return frontItem;
        }

        public bool IsConsistent()
        {
            if (data.Count == 0) return true;
            int lastIndex = data.Count - 1; // last index
            for (int parentIndex = 0; parentIndex < data.Count; ++parentIndex) // each parent index
            {
                int leftChildIndex = 2 * parentIndex + 1; // left child index
                int rightChildIndex = 2 * parentIndex + 2; // right child index
                if (leftChildIndex <= lastIndex && data[parentIndex].CompareTo(data[leftChildIndex]) > 0) return false;
                if (rightChildIndex <= lastIndex && data[parentIndex].CompareTo(data[rightChildIndex]) > 0) return false;
            }
            return true; // Passed all checks
        }

        public void Clear()
        {
            data.Clear();
        }
    }
}
