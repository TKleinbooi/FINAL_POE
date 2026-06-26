using CybersecBot_GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    class TaskManager
    {
        // Stores all cybersecurity tasks
        private static List<CyberTask> tasks = new List<CyberTask>();

        // Add a new task
        public static void AddTask(string title, string description, string reminder)
        {
            CyberTask newTask = new CyberTask(title, description, reminder);

            tasks.Add(newTask);

            // Record the activity
            ActivityLogger.LogActivity($"Added task: {title}");
        }

        // Return all tasks
        public static List<CyberTask> GetTasks()
        {
            return tasks;
        }

        // Mark a task as completed
        public static bool CompleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Completed = true;

                ActivityLogger.LogActivity($"Completed task: {tasks[index].Title}");

                return true;
            }

            return false;
        }

        // Delete a task
        public static bool DeleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                ActivityLogger.LogActivity($"Deleted task: {tasks[index].Title}");

                tasks.RemoveAt(index);

                return true;
            }

            return false;
        }
    }
}