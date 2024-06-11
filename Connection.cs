using System.Data.SQLite;
namespace Course_Work4
{
    public class Connection
    {
        internal static List<User>? ConnectionToSQLAndShowUsers()
        {
            try
            {
                using var connection = new SQLiteConnection(@"Data Source = AstronomicalObject.db");
                connection.Open();
                using var cmd = new SQLiteCommand(@"select ID,
	                                                Name,
	                                                Weight,
	                                                Speed,
	                                                Material,
	                                                ServiceLife
                                                    FROM AstronomicalObjects;", connection);
                using var reader = cmd.ExecuteReader();
                List<User> users = new();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Weight = reader.GetString(2),
                        Speed = reader.GetString(3),
                        Material = reader.GetString(4),
                        ServiceLife = reader.GetString(5)
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
