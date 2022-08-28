using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using RequestForm.Models;
using RequestForm.Interfaces;

namespace RequestForm.Controllers
{
    public class sqliteController : DBInterface
    {
        private string ConnectionString;

        public sqliteController(string connectionString = "Data Source=data.db")
        {
            ConnectionString = connectionString;
        }

        public void CreateTables()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS drives(id INTEGER PRIMARY KEY,
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
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS software(id INTEGER PRIMARY KEY,
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

        #region Drives

        public List<Drive> GetAllDrives()
        {
            List<Drive> drives = new List<Drive>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM 'drives'";


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            drives.Add(
                                new Drive
                                {
                                    Id = reader.GetInt32(0),
                                    DriveLetter = reader.GetString(1),
                                    DriveName = reader.GetString(2)
                                });
                        }
                    }
                }
            }
            return drives;
        }

        public void AddDrive(Drive newDrive)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO drives(DriveLetter, DriveName) VALUES('{newDrive.DriveLetter}','{newDrive.DriveName}')";

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

        public void EditDrive(Drive editedDrive)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"UPDATE drives SET DriveLetter = {editedDrive.DriveLetter}, DriveName = {editedDrive.DriveName}
                                          WHERE Id = {editedDrive.Id}";

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

        public void DeleteDrive(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM drives WHERE Id = {id}";

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

        #endregion

        #region Software

        public void AddSoftware(Software newSoftware)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO software(SoftwareName) VALUES('{newSoftware.SoftwareName}')";

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

        public void EditSoftware(Software editedSoftware)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"UPDATE software SET SoftwareName = {editedSoftware.SoftwareName}
                                          WHERE Id = {editedSoftware.Id}";

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

        public void DeleteSoftware(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"DELETE FROM software WHERE Id = {id}";

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

        public List<Software> GetAllSoftware()
        {
            List<Software> software = new List<Software>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM 'software'";


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            software.Add(
                                new Software
                                {
                                    Id = reader.GetInt32(0),
                                    SoftwareName = reader.GetString(1),
                                });
                        }
                    }
                }
            }
            return software;
        }

        #endregion
    }
}