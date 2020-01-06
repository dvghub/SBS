using System;
using System.Collections.Generic;

namespace Organiser {
    public class Sorter {
        public static List<int> ShiftHigher(List<int> list) {
            var length = list.Count;
            for (var j = 0; j < length-1; j++) {
                for (var i = 0; i < length-1; i++) {
                    if (list[i] < list[i + 1]) continue;
                    var temp = list[i];
                    list[i] = list[i+1];
                    list[i + 1] = temp;
                }
            }
            return list;
        }
        
        public static void Rotate(List<int> list, int low, int high) {
            if (low >= high) return;
            var split = Partition(list, low, high);

            Rotate(list, low, split - 1);
            Rotate(list, split + 1, high);
        }
        
        private static int Partition(List<int> list, int low, int high) {
            var pivot = list[high];
            var index = (low - 1);
            int temp;

            for (var j = low; j < high; j++) {
                if (list[j] > pivot) continue;
                index++;

                temp = list[index];
                list[index] = list[j];
                list[j] = temp;
            }

            temp = list[index + 1];
            list[index + 1] = list[high];
            list[high] = temp;

            return index + 1;
        }
    }
}