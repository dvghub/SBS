using System;
using System.Collections.Generic;
using System.Linq;
using BornToMove.Business;
using BornToMove.DAL;

namespace BornToMove {
    public static class Program {
        private static readonly Display Display = new Display();
        private static readonly BuMove Business = new BuMove();

        private static void Main(string[] args) {
            Display.Print("Hello!");
            ShowMenu();
        }
        
        private static void ShowMenu() {
            while (true) {
                Display.Print("It's time to MOVE!\n" + 
                              "Would you like to get a suggestion or see the list [s/L]?");
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
                            Display.Print("Please answer with [s]uggestion or [l]ist.");
                            break;
                    }
                } else {
                    Display.Print("Please provide some input.");
                    continue;
                }
                break;
            }
        }

        private static void ShowList() {
            while (true) {
                var moves = Business.GimmeAll();
                Display.Print("   Id Name          Rating\n" + 
                              "----------------------------------");
                var ids = new List<int>();
                moves.ForEach((move) => {
                    Display.Print($"[{move.Id,3}] {move.Name,-14}{move.Rating,-1}");
                    ids.Add(move.Id);
                });
                Display.Print("[  0] Add new exercise\n\n" + 
                              "Which exercise would you like to do today?");

                var input = Console.ReadLine();
                if (input != null) {
                    if (int.TryParse(input, out var id)) {
                        if (!ids.Contains(id)) {
                            Display.Print("Please provide an existing id.");
                            continue;
                        }
                        if (id == 0) AddExercise();
                        else ShowExercise(id);
                    } else {
                        Display.Print($"{input} is not a valid input.");
                        ShowList();
                    }
                } else {
                    Display.Print("Please provide some input.");
                    continue;
                }
                break;
            }
        }

        private static void ShowExercise(int id = 0) {
            var move = id == 0 ? Business.GimmeRandom() : Business.GimmeThis(id);
            Display.Print("   Id Name          Rating\n" +
                          "----------------------------------\n" +
                          $"[{move.Id, 3}] {move.Name, -14}{move.Rating, -1}\n" +
                          "----------------------------------");
            Display.PrintWrapped(move.Description);
            GetVotes(move.Id);
        }

        private static void AddExercise() {
            Display.Print("Please enter a name for the new exercise:");
            var names = Business.GimmeAll().Select((m) => m.Name).ToList();
            var name = Console.ReadLine();
            if (names.Contains(name)) {
                Display.Print("Exercise already exists.");
                AddExercise();
            }

            Display.Print("Please enter a description for the new exercise:");
                        
            var description = Console.ReadLine();
            var move = new UsableMove() {Name = name, Description = description};
            var id = Business.MakeThis(move);
                        
            Display.Print($"Exercise created with id: {id}");
            ShowMenu();
        }

        private static void GetVotes(int id) {
            while (true) {
                Display.Print("Please give this exercise a vote [1-5]:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var vote)) {
                    if (vote >= 1 && vote <= 5) {
                        var ratings = new int[3];
                        ratings[0] = id;
                        ratings[1] = vote;
                        GetRating(ratings);
                    } else {
                        Display.Print($"{input} is not a valid input. Please try again.");
                        continue;
                    }
                } else {
                    Display.Print($"{input} is not a valid input. Please try again.");
                    continue;
                }
                break;
            }
        }

        private static void GetRating(int[] ratings) {
            while (true) {
                Display.Print("Please give this exercise an intensity rating [1-5]:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var rating)) {
                    if (rating >= 1 && rating <= 5) {
                        ratings[2] = rating;
                        Business.ChangeThis(ratings);
                        Display.Print("Thanks! See you tomorrow!");
                    } else {
                        Display.Print($"{input} is not a valid input. Please try again.");
                        continue;
                    }
                } else {
                    Display.Print($"{input} is not a valid input. Please try again.");
                    continue;
                }
                break;
            }
        }
    }
}
