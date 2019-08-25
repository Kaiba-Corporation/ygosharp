using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace YGOSharp
{
    public class DatabasePlayer
    {
        public string Username { get; private set; }
        public string Rank { get; private set; }
        public int Id { get; private set; }
        public int Ban { get; private set; }
        public int Rating { get; private set; }
        public int Gold { get; private set; }
        public int Qualification { get; private set; }
        public string IpAddress { get; private set; }
        public bool IsValid { get; private set; }
        public string Team { get; private set; }
        public string ImageUrl { get; private set; }
        public string CardBackUrl { get; private set; }
        public string Border { get; private set; }
        public string CardSkins { get; private set; }

        public DatabasePlayer()
        {
            IsValid = false;
        }

        public DatabasePlayer(string username)
        {
            IsValid = false;
            Username = username;

            lock (Program.Database)
                using (MySqlCommand cmd = Program.Database.Query("SELECT ID, Ban, Rank, Rating, MRating, TRating, Qualified, IP, Border, ImageURL, CardBackURL, CardSkins FROM account WHERE Username = ?username"))
                {
                    cmd.Parameters.Add(new MySqlParameter("?username", username));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Id = reader.GetInt32("ID");
                            Ban = reader.GetInt32("Ban");
                            Rank = reader.GetString("Rank");
                            Qualification = reader.GetInt32("Qualified");
                            Rating = Math.Max(Math.Max(reader.GetInt32("Rating"), reader.GetInt32("MRating")), reader.GetInt32("TRating"));
                            IpAddress = reader.GetString("IP");
                            Border = reader.GetString("Border");
                            ImageUrl = reader.GetString("ImageURL");
                            CardBackUrl = reader.GetString("CardBackURL");
                            CardSkins = reader.GetString("CardSkins");
                            IsValid = true;
                        }
                    }
                }
        }

        public void AddRewards()
        {
            lock (Program.Database)
                using (MySqlCommand cmd = Program.Database.Query("UPDATE account SET Gold = Gold + 50, XP = XP + 50 WHERE ID = ?id"))
                {
                    cmd.Parameters.Add(new MySqlParameter("?id", Id));
                    cmd.ExecuteNonQuery();
                }
        }
    }
} 