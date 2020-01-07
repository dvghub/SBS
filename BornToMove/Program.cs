using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BornToMove {
    class Program {
        static void Main(string[] args) {
            
            Console.WriteLine("Hello!");
            ShowMenu();
        }

        private static void ShowMenu() {
            while (true) {
                Console.WriteLine("It's time to MOVE!");
                Console.WriteLine("Would you like to get a suggestion or see the list [s/L]?");
                var input = Console.ReadLine();

                if (input != null) {
                    switch (input[0]) {
                        case 's':
                        case 'S':
                            ShowExercise();
                            break;
                        case 'l':
                        case 'L':
                            ShowList();
                            break;
                        default:
                            Console.WriteLine("Please enter a valid answer.");
                            continue;
                    }
                }

                break;
            }
        }

        private static void ShowList() {
            var moves = MoveCrud.ReadAll();
            Console.WriteLine("   Id Name          Rating");
            Console.WriteLine("----------------------------------");
            foreach (var move in moves) {
                Console.WriteLine($"[{move.Id, 3}] {move.Name, -14}{move.Rating, -1}");
            }
            Console.WriteLine("[  0] Add new exercise");
            Console.WriteLine("\nWhich exercise would you like to do today?");
            
            var input = Console.ReadLine();
            try {
                var id = int.Parse(input);
                if (id < 0 || id > moves.Count) throw new FormatException();
                if (id == 0) AddExercise();
                else ShowExercise(id);
            } catch (FormatException e) {
                Console.WriteLine($"{input} is not a valid input. Please try again.");
                ShowList();
            }
        }

        private static void ShowExercise(int id = 0) {
            var move = id == 0 ? MoveCrud.Read() : MoveCrud.Read(id);
            Console.WriteLine("   Id Name          Rating");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"[{move.Id, 3}] {move.Name, -14}{move.Rating, -1}");
            Console.WriteLine("----------------------------------");
            WordWrap(move.Description);
            GetVotes();
        }

        private static void AddExercise() {
            Console.WriteLine("Please enter a name for the new exercise:");
            var name = Console.ReadLine();
            var names = new MoveCrud().ReadAllNames();
            if (names.Contains(name)) {
                Console.WriteLine("Exercise already exists.");
                AddExercise();
            }
            Console.WriteLine("Please enter a rating for the new exercise:");
            var input = Console.ReadLine();
            try {
                var rating = int.Parse(input);
                Console.WriteLine("Please enter a description for the new exercise:");
                var description = Console.ReadLine();
                var move = new Move {
                    Name = name,
                    Description = description,
                    Rating = rating
                };
                var id = new MoveCrud().Create(move);
                Console.WriteLine($"Exercise created with id: {id}");
                ShowMenu();
            } catch (FormatException e) {
                Console.WriteLine($"{input} is not a valid input. Please try again.");
                AddExercise();
            }
        }

        private static void GetVotes() {
            Console.WriteLine("Please give this exercise a vote [1-5]:");
            var input = Console.ReadLine();
            try {
                var vote = int.Parse(input);
                if (vote < 1 || vote > 5) throw new FormatException();
                GetRating();
            } catch (FormatException e) {
                Console.WriteLine($"{input} is not a valid input. Please try again.");
                GetVotes();
            }
        }

        private static void GetRating() {
            Console.WriteLine("Please give this exercise an intensity rating:");
            var input = Console.ReadLine();
            try {
                var rating = int.Parse(input);
                if (rating < 1 || rating > 5) throw new FormatException();
                Console.WriteLine("Thanks! See you tomorrow!");
            } catch (FormatException e) {
                Console.WriteLine($"{input} is not a valid input. Please try again.");
                GetRating();
            }
        }
        
        private static void WordWrap(string paragraph) { //Copy pasted but I know what it does
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
