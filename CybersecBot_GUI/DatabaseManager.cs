using CybersecBot_GUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CybersecBot_GUI
{
    class DatabaseManager
    {
        private static string connectionString =
            "server=localhost;" +
            "database=CyberSecurityDB;" +
            "uid=root;" +
            "pwd=@Cyber123!;";
          //  "SslMode=Disabled;";

        // TEST CONNECTION (FIXED)
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // OPTIONAL UI CALL (separate so it actually runs)
        public static void ShowConnectionStatus()
        {
            if (TestConnection())
                MessageBox.Show("Connected to MySQL!");
            else
                MessageBox.Show("Could not connect to MySQL.");
        }

        // ---------------- TASKS ----------------

      
        public static void AddTask(CyberTask task)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    @"INSERT INTO Tasks
                    (Title, Description, Reminder, Completed)
                    VALUES
                    (@Title,@Description,@Reminder,@Completed)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@Reminder", task.Reminder);
                cmd.Parameters.AddWithValue("@Completed", task.Completed);

                cmd.ExecuteNonQuery();
            }
        }

        public static List<CyberTask> LoadTasks()
        {
            List<CyberTask> tasks = new List<CyberTask>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Tasks";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CyberTask task = new CyberTask(
                            reader["Title"].ToString(),
                            reader["Description"].ToString(),
                            reader["Reminder"].ToString());

                        task.Completed = Convert.ToBoolean(reader["Completed"]);

                        tasks.Add(task);
                    }
                }
            }

            return tasks;
        }

        public static void UpdateTask(int id, bool completed)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    "UPDATE Tasks SET Completed=@Completed WHERE TaskID=@TaskID";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Completed", completed);
                cmd.Parameters.AddWithValue("@TaskID", id);

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    "DELETE FROM Tasks WHERE TaskID=@TaskID";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TaskID", id);

                cmd.ExecuteNonQuery();
            }
        }

        // ---------------- QUIZ SUPPORT (ADDED) ----------------

        public static List<Question> LoadQuestions()
        {
            List<Question> questions = new();

            using (MySqlConnection conn = new(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Questions";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Text = reader["Question"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        Answer = reader["Answer"].ToString()
                    });
                }

                reader.Close();
            }

            return questions;
        }

        public static bool CheckQuizAnswer(int questionId, string selectedAnswer)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT Answer FROM Questions WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", questionId);

                string correct = cmd.ExecuteScalar()?.ToString();

                return correct == selectedAnswer;
            }
        }
    }
}