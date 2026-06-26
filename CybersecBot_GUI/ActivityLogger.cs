using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    class ActivityLogger
    {
        // Stores all activity logs
        private static List<string> activityLog = new List<string>();

        // Add a new activity
        public static void LogActivity(string activity)
        {
            string logEntry = $"{DateTime.Now:G} - {activity}";
            activityLog.Add(logEntry);
        }

        // Return all activities
        public static List<string> GetActivityLog()
        {
            return activityLog;
        }

        // Clear the activity log (optional)
        public static void ClearLog()
        {
            activityLog.Clear();
        }
    }
}