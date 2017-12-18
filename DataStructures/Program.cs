using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {

  class Program {
    static void Main(string[] args) {

      var list = new DuoLinkedList();
      for (int i = 0; i < 15; i++) {
        list.AddLast(i * 10);
      }

      //var node = list.Find(140);
      //list.Insert(node, 777);

      foreach (var item in list)
        Console.WriteLine(item);

    }
  }
}
