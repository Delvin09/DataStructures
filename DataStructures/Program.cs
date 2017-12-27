using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {

  class Program {
    static void Main(string[] args) {

      var hash = new object().GetHashCode();
      var r1 = hash % 5;
      var r2 = hash % 10;

      var arr = new int[] { 5, 2, 3, 1, 8, 10 };
      var tree = new BinaryTree();
      foreach (var item in arr)
        tree.Add(item);

      var arrSorted = new int[arr.Length];
      int i = 0;
      foreach (var item in tree.ToArray()) {
        Console.Write(item + ", ");
        arrSorted[i] = (int)item;
        i++;
      }
      Console.WriteLine();
      Console.WriteLine(BinarySearch(arrSorted, 7));
    }

    private static int BinarySearch(int[] arr, int value) {

      int begin = 0;
      int end = arr.Length - 1;
      while (begin <= end && begin >= 0 && end < arr.Length) {
        int middle = ((end - begin) / 2) + begin;
        if (arr[middle] > value) // берем меньшую часть
          end = middle - 1;
        else if (arr[middle] < value) // берем большую часть
          begin = middle + 1;
        else
          return middle;
      }
      return -1;
    }
  }
}