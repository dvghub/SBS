using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace BornToMove {
    class Program {
        static void Main(string[] args) {
            
            Console.WriteLine("Hello!");
            ShowMenu();
            
            
            
            var conn = DatabaseConnection.Instance();
            conn.DatabaseName = "borntomovedb";
            if (conn.IsConnected()) {
                var query = "SELECT * FROM moves";
                var command = new MySqlCommand(query, conn.Connection);
                var reader = command.ExecuteReader();
                var moves = new List<Move>();

                while (reader.Read()) {
                    var move = new Move {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Rating = reader.GetInt32(3)
                    };
                    moves.Add(move);
                }
            }
        }

        private static void ShowMenu() {
            while (true) {
                Console.WriteLine("It's time to MOVE!");
                Console.WriteLine("Would you like to get a suggestion or see the list [s/L]?");
                var input = Console.ReadLine();

                switch (input) {
                    case "s":
                    case "S":
                        ShowSuggestion();
                        break;
                    case "l":
                    case "L":
                        ShowList();
                        break;
                    default:
                        continue;
                }

                break;
            }
        }

        private static void ShowList() {
            
        }

        private static void ShowSuggestion() {
            
        }
    }
}
