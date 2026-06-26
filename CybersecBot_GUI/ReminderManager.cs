using System;
using System.Collections.Generic;
using CybersecBot_GUI.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    class ReminderManager
    {
        public static List<CyberTask> GetUpcomingReminders()
        {
            List<CyberTask> reminders = new List<CyberTask>();

            foreach (CyberTask task in TaskManager.GetTasks())
            {
                if (!task.Completed)
                {
                    reminders.Add(task);
                }
            }

            return reminders;
        }
    }
}