using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {

  class Program {
    static void Main(string[] args) {

      var list = new Queue();
      for (int i = 0; i < 15; i++) {
        list.Enqueue(i * 10);
      }

      Console.Clear();
      while (list.Count > 0) {
        Console.WriteLine(list.Dequeue());
      }

      //foreach (var item in list)
      //  Console.WriteLine(item);
    }
  }
}
