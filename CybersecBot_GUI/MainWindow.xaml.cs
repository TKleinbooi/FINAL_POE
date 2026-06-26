using CybersecBot_GUI;
using CybersecBot_GUI.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CybersecBot_GUI
{
    public partial class MainWindow : Window
    {
        private QuizManager quiz;

        public MainWindow()
        {
            InitializeComponent();

            quiz = new QuizManager();
            quiz.Load();
            ActivityLogger.LogActivity(
    "Quiz Started");

            RefreshActivityLog();
            LoadQuestion();
        }

        // ---------------- TASKS (SEND) ----------------
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return;

            // Display the user's message
            rtbChat.AppendText($"You: {input}\n");

            // Log the user's message
            ActivityLogger.LogActivity($"User asked: {input}");

            // Get chatbot response
            string response = Chatbot.GetBotResponse(input);

            // Display the bot's response
            rtbChat.AppendText($"Bot: {response}\n\n");

            // Log the bot's response
            ActivityLogger.LogActivity($"Bot responded about: {input}");

            // Refresh the Activity Log tab
            RefreshActivityLog();

            // Clear input and scroll to the latest message
            txtUserInput.Clear();
            rtbChat.ScrollToEnd();
        }

        // ---------------- ADD TASK ----------------
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string reminder = dpReminder.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

                TaskManager.AddTask(
                    txtTaskTitle.Text,
                    txtTaskDescription.Text,
                    reminder);

                CyberTask task = new CyberTask(
                    txtTaskTitle.Text,
                    txtTaskDescription.Text,
                    reminder);

                DatabaseManager.AddTask(task);

                ActivityLogger.LogActivity($"Added task: {task.Title}");

                MessageBox.Show("Task added successfully!");

                txtTaskTitle.Clear();
                txtTaskDescription.Clear();
                dpReminder.SelectedDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ---------------- VIEW TASKS ----------------
        private void btnViewTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tasks = DatabaseManager.LoadTasks();
                MessageBox.Show("Tasks loaded: " + tasks.Count);

                lstActivityLog.Items.Clear();

                foreach (var task in tasks)
                {
                    lstActivityLog.Items.Add(
                        $"{task.Title} | {task.Description} | {task.Reminder} | Completed: {task.Completed}");
                }

                // Log AFTER displaying tasks
                ActivityLogger.LogActivity("Viewed all tasks.");

                // ❌ DO NOT CALL RefreshActivityLog() HERE
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ---------------- COMPLETE TASK ----------------
        private void btnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtTaskNumber.Text);

                DatabaseManager.UpdateTask(id, true);

                ActivityLogger.LogActivity($"Completed Task ID {id}");

                MessageBox.Show("Task completed!");
            }
            catch
            {
                MessageBox.Show("Please enter a valid Task ID.");
            }
        }

        private void RefreshActivityLog()
        {
            lstActivityLog.Items.Clear();

            foreach (string activity in ActivityLogger.GetActivityLog())
            {
                lstActivityLog.Items.Add(activity);
            }
        }

        // ---------------- QUIZ ----------------
        private void LoadQuestion()
        {
            Question q = quiz.Current();

            if (q == null)
            {
                txtQuestion.Text = "🎉 Quiz Complete!";
                cmbAnswers.Items.Clear();

                txtScore.Text =
                    $"Final Score: {quiz.Score()} / {quiz.Total()}";

                ActivityLogger.LogActivity(
                    $"Quiz finished. Score: {quiz.Score()}/{quiz.Total()}");

                RefreshActivityLog();

                return;
            }

            txtQuestion.Text = q.Text;

            cmbAnswers.Items.Clear();

            cmbAnswers.Items.Add("A: " + q.OptionA);
            cmbAnswers.Items.Add("B: " + q.OptionB);
            cmbAnswers.Items.Add("C: " + q.OptionC);

            cmbAnswers.SelectedIndex = -1;

            txtScore.Text =
                $"Question {quiz.CurrentIndex()} of {quiz.Total()}";
        }

        private void btnSubmitQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (cmbAnswers.SelectedItem == null)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            string selected =
                cmbAnswers.SelectedItem.ToString().Substring(0, 1);

            bool correct =
                quiz.Submit(selected);

            if (correct)
            {
                MessageBox.Show("✅ Correct!");

                ActivityLogger.LogActivity(
                    $"Correct answer for Question {quiz.CurrentIndex()}");
            }
            else
            {
                MessageBox.Show("❌ Incorrect.");

                ActivityLogger.LogActivity(
                    $"Incorrect answer for Question {quiz.CurrentIndex()}");
            }

            RefreshActivityLog();

            quiz.Next();

            LoadQuestion();
        }

        // ---------------- EXIT ----------------
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}