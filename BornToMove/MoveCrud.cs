using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BornToMove {
    public class MoveCrud {
        private static DatabaseConnection Connect() {
            var conn = DatabaseConnection.Instance();
            conn.DatabaseName = "borntomovedb";
            return conn.IsConnected() ? conn : null;
        }
        
        public int Create(Move move) {
            var conn = DatabaseConnection.Instance();
            const string query = "INSERT INTO moves (name, description, rating) VALUES (@name, @description, @rating)";
            var command = new MySqlCommand(query, conn.Connection);
            command.Parameters.AddWithValue("@name", move.Name);
            command.Parameters.AddWithValue("@description", move.Description);
            command.Parameters.AddWithValue("@rating", move.Rating);
            return command.ExecuteNonQuery();
        }

        public static Move Read(int id) {
            var conn = Connect();
            var move = new Move();
            const string query = "SELECT * FROM moves WHERE id = @id";
            var command = new MySqlCommand(query, conn.Connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();

            while (reader.Read()) {
                move = new Move {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Rating = reader.GetInt32(3)
                };
            }
            reader.Close();
            return move;
        }
        
        public static Move Read() {
            var conn = Connect();
            var move = new Move();
            
            var query = "SELECT id FROM moves";
            var command = new MySqlCommand(query, conn.Connection);
            var reader = command.ExecuteReader();
            
            var ids = new List<int>();
            while (reader.Read()) { ids.Add(reader.GetInt32(0)); }
            var id = new Random().Next(1, ids.Count);
            
            reader.Close();
            
            query = "SELECT * FROM moves WHERE id = @id";
            command = new MySqlCommand(query, conn.Connection);
            command.Parameters.AddWithValue("@id", id);
            reader = command.ExecuteReader();

            while (reader.Read()) {
                move = new Move {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Rating = reader.GetInt32(3)
                };
            }
            reader.Close();
            return move;
        }

        public static List<Move> ReadAll() {
            var conn = Connect();
            var moves = new List<Move>();
            const string query = "SELECT * FROM moves";
            var command = new MySqlCommand(query, conn.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read()) {
                var move = new Move {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Rating = reader.GetInt32(3)
                };
                moves.Add(move);
            }
            reader.Close();
            return moves;
        }
        
        public List<string> ReadAllNames() {
            var conn = Connect();
            var names = new List<string>();
            const string query = "SELECT name FROM moves";
            var command = new MySqlCommand(query, conn.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read()) {
                names.Add(reader.GetString(0));
            }
            reader.Close();
            return names;
        }
    }
}