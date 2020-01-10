using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BornToMove {
    public class Display {
        public static void Print(string toPrint) {
            Console.WriteLine(toPrint);
        }

        public static void PrintHeader() {
            Console.WriteLine("   Id Name          Rating\n" + 
                              "----------------------------------");
        }

        public static void PrintItem(string index, string name, int rating) {
            Console.WriteLine($" {index,3}  {name,-14}{rating,-1}");
        }
        
        public static void PrintWrapped(string paragraph) { //Copy pasted but I know what it does
            paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
            var left = Console.CursorLeft; var top = Console.CursorTop; var lines = new List<string>();
            for (var i = 0; paragraph.Length > 0; i++) {
                lines.Add(paragraph.Substring(0, Math.Min(35, paragraph.Length)));
                var length = lines[i].LastIndexOf(" ", StringComparison.Ordinal);
                if (length > 0) lines[i] = lines[i].Remove(length);
                paragraph = paragraph.Substring(Math.Min(lines[i].Length + 1, paragraph.Length));
                Console.SetCursorPosition(left, top + i); Console.WriteLine(lines[i]);
            }
        }
    }
}