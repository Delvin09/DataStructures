using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {

  class Program {
    static void Main(string[] args) {

      var arr = new int[] { 5, 2, 3, 1, 8, 10, 7 };
      var tree = new BinaryTree();
      foreach (var item in arr)
        tree.Add(item);

      var r = tree.Find(100);

      foreach (var item in tree.ToArray()) {
        Console.Write(item + ", ");
      }
      Console.WriteLine();
    }
  }
}