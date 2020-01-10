using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organiser {
    class Program {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            try {
                var amount = int.Parse(input);
                var list = CreateList(amount);
                var list2 = new List<int>(list);
                
                foreach (var item in list) {
                    Console.Write(item);
                    Console.Write(" ");
                }
                Console.WriteLine("");
                Console.WriteLine("");
                
                var sorter = new Sorter();
                
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                sorter.ShiftHigher(list);
                stopwatch.Stop();
                var shiftTime = stopwatch.Elapsed;
                foreach (var item in list) {
                    Console.Write(item + " ");
                }
                Console.WriteLine("");
                Console.WriteLine(IsSorted(list) ? "List was sorted correctly." : "List was not sorted.");
                Console.WriteLine("");
                
                var comparer = new Comparer();
                
                stopwatch.Start();
                sorter.Rotate(list2, 0, list2.Count-1, comparer);
                stopwatch.Stop();
                var rotateTime = stopwatch.Elapsed;
                foreach (var item in list2) {
                    Console.Write(item + " ");
                }
                Console.WriteLine("");
                Console.WriteLine(IsSorted(list2) ? "List was sorted correctly." : "List was not sorted.");
                Console.WriteLine("");
                
                Console.WriteLine("Shift time: " + shiftTime.Ticks);
                Console.WriteLine("Rotate time: " + rotateTime.Ticks);
            } catch (FormatException) {
                Console.WriteLine("Input was not an int.");
            }
        }

        private static List<int> CreateList(int amount) {
            var list = new List<int>();
            var rnd = new Random();
            for (int i = 0; i < amount; i++) {
                list.Add(rnd.Next(-99, 100));
            }
            return list;
        }

        private static bool IsSorted(List<int> list) {
            for (int i = 0; i < list.Count-1; i++) {
                if (list[i] > list[i+1]) {
                    return false;
                }
            }
            return true;
        }
    }
}