using MySql.Data.MySqlClient;

namespace YGOSharp
{
    public class Database
    {
        private const string ConnectionString = "SERVER=;DATABASE=;UID=;PASSWORD=;";

        private MySqlConnection m_connection;

        public Database()
        {
            m_connection = new MySqlConnection(ConnectionString);
            m_connection.Open();
        }

        public MySqlCommand Query(string query)
        {
            return new MySqlCommand(query, m_connection);
        }
    }
}
