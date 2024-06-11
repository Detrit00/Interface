using System.Data.SQLite;
namespace Course_Work4
{
    public class Query
    {
        public static List<User>? ConnectionToSQLAndShowUsers()
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
                using var reader = cmd.ExecuteReader();//получение строк из источника данных
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
        public static List<User>? AddAndShowUsers(string name,
                                                 string weight,
                                                 string speed,
                                                 string material,
                                                 string servicelife,
                                                 int id)
        {
            try
            {
                string sqlexp1 = $"UPDATE sqlite_sequence SET seq={id} WHERE name = 'AstronomicalObjects'";
                string sqlexp = @"insert into AstronomicalObjects(
                                Name,
                                Weight,
                                Speed,
                                Material,
                                ServiceLife)
                                values(@name, @weight, @speed, @material, @servicelife)";
                using var connection = new SQLiteConnection(@"Data Source = AstronomicalObject.db");
                connection.Open();

                var cm1 = new SQLiteCommand(sqlexp1, connection);
                var cmd = new SQLiteCommand(sqlexp, connection);

                SQLiteParameter Name = new("@name", name);
                cmd.Parameters.Add(Name);
                SQLiteParameter Weight = new("@weight", weight);
                cmd.Parameters.Add(Weight);
                SQLiteParameter Speed = new("@speed", speed);
                cmd.Parameters.Add(Speed);
                SQLiteParameter Material = new("@material", material);
                cmd.Parameters.Add(Material);
                SQLiteParameter ServiceLife = new("@servicelife", servicelife);
                cmd.Parameters.Add(ServiceLife);
                cm1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select * from AstronomicalObjects";
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
        public static List<User>? UpdateAndShowUsers(string name,
                                                    string weight,
                                                    string speed,
                                                    string material,
                                                    string servicelife,
                                                    int id)
        {
            try
            {
                string sqlexp = $@"UPDATE AstronomicalObjects
                                set Name = @name,
	                            Weight = @weight,
	                            Speed = @speed,
	                            Material = @material,
	                            ServiceLife = @servicelife
                                where ID = @id";
                using var connection = new SQLiteConnection(@"Data Source = AstronomicalObject.db");
                connection.Open();

                var cmd = new SQLiteCommand(sqlexp, connection);

                SQLiteParameter Name = new("@name", name);
                cmd.Parameters.Add(Name);
                SQLiteParameter Weight = new("@weight", weight);
                cmd.Parameters.Add(Weight);
                SQLiteParameter Speed = new("@speed", speed);
                cmd.Parameters.Add(Speed);
                SQLiteParameter Material = new("@material", material);
                cmd.Parameters.Add(Material);
                SQLiteParameter ServiceLife = new("@servicelife", servicelife);
                cmd.Parameters.Add(ServiceLife);
                SQLiteParameter ID = new("@id", id);
                cmd.Parameters.Add(ID);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select * from AstronomicalObjects";
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
        public static List<User>? DeleteAndShowUsers(int id)
        {
            try
            {
                string sqlexp = $@"delete from AstronomicalObjects where ID = {id}";
                using var connection = new SQLiteConnection(@"Data Source = AstronomicalObject.db");
                connection.Open();

                var cmd = new SQLiteCommand(sqlexp, connection);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "select * from AstronomicalObjects";

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
        public static List<User>? TruncateAllUsers()
        {
            try
            {
                string sqlexp = $@"delete from AstronomicalObjects";
                using var connection = new SQLiteConnection(@"Data Source = AstronomicalObject.db");
                connection.Open();

                var cmd = new SQLiteCommand(sqlexp, connection);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select * from AstronomicalObjects";
                using var reader = cmd.ExecuteReader();
                List<User> users = new();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static List<User>? TakeFromFileAndShowUsers(string name,
                                                           string weight,
                                                           string speed,
                                                           string material,
                                                           string servicelife)
        {
            try
            {
                string sqlexp1 = "UPDATE sqlite_sequence SET seq=0 WHERE name = 'AstronomicalObjects'";
                
                string sqlexp = @"insert into AstronomicalObjects(
                                Name,
                                Weight,
                                Speed,
                                Material,
                                ServiceLife)
                                values(@name, @weight, @speed, @material, @servicelife)";
                using var connection = new SQLiteConnection(@"Data Source = AstronomicalObject.db");
                connection.Open();
                var cm1 = new SQLiteCommand(sqlexp1, connection);
                var cmd = new SQLiteCommand(sqlexp, connection);

                SQLiteParameter Name = new("@name", name);
                cmd.Parameters.Add(Name);
                SQLiteParameter Weight = new("@weight", weight);
                cmd.Parameters.Add(Weight);
                SQLiteParameter Speed = new("@speed", speed);
                cmd.Parameters.Add(Speed);
                SQLiteParameter Material = new("@material", material);
                cmd.Parameters.Add(Material);
                SQLiteParameter ServiceLife = new("@servicelife", servicelife);
                cmd.Parameters.Add(ServiceLife);
                cm1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select * from AstronomicalObjects";
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
