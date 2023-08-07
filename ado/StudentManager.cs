using System;
using MySql.Data.MySqlClient;

namespace ado
{ 
    public class StudentManager
    {
        static string ConnectionString = "Server=localhost;user=root;Database=studentdb;Password=christian2000";

        public void CreateTable()
        {

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE Student (
                    Id INT PRIMARY KEY,
                    FirstName NVARCHAR(50),
                    LastName NVARCHAR(50),
                    Age INT
                )";

                MySqlCommand command = new MySqlCommand(createTableQuery, connection);


                command.ExecuteNonQuery();

                Console.WriteLine("Table created successfully.");
            }
        }

        public void AddStudent(Student student)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = $"INSERT INTO student (Id, FirstName, LastName, Age) VALUES (@Id, @FirstName, @LastName, @Age)";

                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Age", student.Age);

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"successfully. - {rowsAffected}");
            }
        }


        public void UpdateStudent(Student student)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string updateQuery = $"UPDATE student SET FirstName = @FirstName,LastName = @LastName, Age = @Age WHERE ID = @Id";

                MySqlCommand command = new MySqlCommand(updateQuery, connection);

                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Age", student.Age);

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"successfully updated.");
            }
        }

        public void DeleteStudent(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string updateQuery = "DELETE FROM student WHERE ID = @ID";

                MySqlCommand command = new MySqlCommand(updateQuery, connection);

                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"successfully deleted.");
                }
                else
                {
                    Console.WriteLine("error!");
                }
            }
        }

        public Student GetById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM student WHERE ID = @ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID", id);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Student
                    {
                        Id = (int)reader["id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"],
                    };
                }
                else
                {

                    return null;
                }
                reader.Close();
            }

        }

        public List<Student> GetAll()
        {
            var Students = new List<Student>();
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM student";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student
                    {
                        Id = (int)reader["id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"],
                    };
                    Students.Add(student);

                }

                reader.Close();
            }

            return Students;
        }
    }
}