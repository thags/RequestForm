using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RequestForm.Models;

namespace RequestForm.Controllers
{
    public class DBController
    {

        public static void CreateTablesIfNonExistent()
        {
            using (var connection = new SqliteConnection($"Data Source=Data.db"))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"CREATE TABLE drives(id INTEGER PRIMARY KEY,
                                                                DriveLetter TEXT, 
                                                                DriveName TEXT
                                                                    );";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Dispose();
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"CREATE TABLE software(id INTEGER PRIMARY KEY,
                                                                SoftwareName TEXT
                                                                    );";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Dispose();
                    }
                }
            }
        }

        public static List<Drive> GetAllDrives()
        {
            List<Drive> drives = new List<Drive>();
            using (var connection = new SqliteConnection($"Data Source=Data.db"))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    //var tableData = new List<List<object>> { new List<object> { "Date", "Hours" } };
                    command.CommandText = "SELECT * FROM 'drives'";


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            drives.Add(
                                new Drive
                                {
                                    id = reader.GetInt32(0),
                                    DriveLetter = reader.GetString(1),
                                    DriveName = reader.GetString(2)
                                });
                        }
                    }
                }
            }
            return drives;
        }

        public static void NewDrive(string letter, string name)
        {
            using (var connection = new SqliteConnection($"Data Source=Data.db"))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO drives(DriveLetter, DriveName) VALUES('{letter}','{name}')";

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            }
        }
    }
}