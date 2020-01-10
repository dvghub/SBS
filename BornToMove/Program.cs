using System;
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
                              "Would you like to get a [s]uggestion or see the [l]ist?\n" +
                              "Or press [x] to exit.");
                var input = Console.ReadLine();
                if (input != null) {
                    switch (input.ToLower()[0]) {
                        case 's':
                            ShowExercise();
                            break;
                        case 'l':
                            ShowList();
                            break;
                        case 'x':
                            Environment.Exit(0);
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
                Display.PrintHeader();
                var index = 1;
                moves.ForEach((move) => {
                    Display.PrintItem(index.ToString(), move.Name, move.Ratings().Rating);
                    index++;
                });
                Display.Print("   0  Add new exercise\n\n" + 
                              "Which exercise would you like to do today?");

                var input = Console.ReadLine();
                if (input != null) {
                    if (int.TryParse(input, out var id)) {
                        if (id < 0 && id > index) {
                            Display.Print("Please provide an existing id.");
                            continue;
                        }
                        if (id == 0) AddExercise();
                        else ShowExercise(moves[id-1].Id);
                    } else {
                        Display.Print($"{input} is not a valid input.");
                        continue;
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
            Display.PrintHeader();
            Display.PrintItem("*", move.Name, move.Ratings().Rating);
            Display.Print("----------------------------------");
            Display.PrintWrapped(move.Description);
            var vote = GetVote();
            var rating = GetRating();
            Business.ChangeThis(new [] {move.Id, vote, rating});
            Display.Print("Thanks! See you tomorrow!");
        }

        private static void AddExercise() {
            Display.Print("Please enter a name for the new exercise:");
            var names = Business.GimmeAll().Select((m) => m.Name).ToList();
            var name = Console.ReadLine();
            if (names.Contains(name, StringComparer.OrdinalIgnoreCase)) {
                Display.Print("Exercise already exists.");
                AddExercise();
            }

            Display.Print("Please enter a description for the new exercise:");
                        
            var description = Console.ReadLine();
            var move = new Move() {Name = name, Description = description};
            var id = Business.MakeThis(move);
                        
            Display.Print($"Exercise created with id: {id}");
            ShowMenu();
        }

        private static int GetVote() {
            while (true) {
                Display.Print("Please give this exercise a vote [1-5]:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var vote)) {
                    if (vote >= 1 && vote <= 5) {
                        return vote;
                    }
                    Display.Print($"{input} is not a valid input. Please try again.");
                } else {
                    Display.Print($"{input} is not a valid input. Please try again.");
                }
            }
        }

        private static int GetRating() {
            while (true) {
                Display.Print("Please give this exercise an intensity rating [1-5]:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var rating)) {
                    if (rating >= 1 && rating <= 5) {
                        return rating;
                    }
                    Display.Print($"{input} is not a valid input. Please try again.");
                    continue;
                }
                Display.Print($"{input} is not a valid input. Please try again.");
            }
        }
    }
}
