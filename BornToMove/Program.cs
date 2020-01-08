using System;

namespace BornToMove {
    public static class Program {
        private static readonly Display Display = new Display();
        
        static void Main(string[] args) {
            Display.Print("Hello!");
            ShowMenu();
        }
        
        private static void ShowMenu() {
            while (true) {
                Display.Print("It's time to MOVE!\n" + "Would you like to get a suggestion or see the list [s/L]?");
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
                var moves = MoveCrud.ReadAll();
                Display.Print("   Id Name          Rating\n" + "----------------------------------");
                foreach (var move in moves) { Display.Print($"[{move.Id,3}] {move.Name,-14}{move.Rating,-1}"); }
                Display.Print("[  0] Add new exercise\n\n" + "Which exercise would you like to do today?");

                var input = Console.ReadLine();
                if (input != null) {
                    if (int.TryParse(input, out var id)) {
                        if (id < 0 || id > moves.Count) throw new FormatException();
                        if (id == 0) AddExercise();
                        else ShowExercise(id);
                    } else {
                        Display.Print($"{input} is not a valid input.");
                        ShowList();
                    }
                    Display.Print("Please provide some input.");
                    continue;
                }
                break;
            }
        }

        private static void ShowExercise(int id = 0) {
            var move = id == 0 ? MoveCrud.Read() : MoveCrud.Read(id);
            Display.Print("   Id Name          Rating" +
                          "----------------------------------" +
                          $"[{move.Id, 3}] {move.Name, -14}{move.Rating, -1}" +
                          "----------------------------------");
            Display.PrintWrapped(move.Description);
            GetVotes();
        }

        private static void AddExercise() {
            while (true) {
                Display.Print("Please enter a name for the new exercise:");
                var names = new MoveCrud().ReadAllNames();
                var name = Console.ReadLine();
                if (names.Contains(name)) {
                    Display.Print("Exercise already exists.");
                    AddExercise();
                }

                Display.Print("Please enter a rating for the new exercise:");
                var input = Console.ReadLine();
                if (input != null) {
                    if (int.TryParse(input, out var rating)) {
                        Display.Print("Please enter a description for the new exercise:");
                        var description = Console.ReadLine();

                        var move = new Move {Name = name, Description = description, Rating = rating};

                        var id = new MoveCrud().Create(move);
                        Display.Print($"Exercise created with id: {id}");
                        ShowMenu();
                    } else {
                        Display.Print($"{input} is not a valid input.");
                        continue;
                    }
                }
                break;
            }
        }

        private static void GetVotes() {
            while (true) {
                Display.Print("Please give this exercise a vote [1-5]:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var vote)) {
                    if (vote >= 1 && vote <= 5) {
                        GetRating();
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

        private static void GetRating() {
            while (true) {
                Display.Print("Please give this exercise an intensity rating [1-5]:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var rating)) {
                    if (rating >= 1 || rating <= 5) {
                        Console.WriteLine("Thanks! See you tomorrow!");
                    } else {
                        Console.WriteLine($"{input} is not a valid input. Please try again.");
                        continue;
                    }
                } else {
                    Console.WriteLine($"{input} is not a valid input. Please try again.");
                    continue;
                }
                break;
            }
        }
    }
}
