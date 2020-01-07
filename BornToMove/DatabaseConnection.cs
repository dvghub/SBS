using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BornToMove {
    public class DatabaseConnection {
        private DatabaseConnection() { }
        public MySqlConnection Connection { get; private set; } = null;
        public string DatabaseName { get; set; } = string.Empty;

        private static DatabaseConnection _instance = null;
        public static DatabaseConnection Instance() {
            if (_instance == null) _instance = new DatabaseConnection();
            return _instance;
        }

        public bool IsConnected() {
            if (Connection == null) {
                if (string.IsNullOrEmpty(DatabaseName)) return false;
                var config = string.Format("Server=localhost; database={0}; UID=sql_manager; password=lookatmeimastrongpassword", DatabaseName);
                Connection = new MySqlConnection(config);
                Connection.Open();
            }
            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}