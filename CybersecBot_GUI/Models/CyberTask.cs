using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI.Models
{
    class CyberTask
    {
        // Title of the task
        public string Title { get; set; }

        // Description of the task
        public string Description { get; set; }

        // Reminder date or text
        public string Reminder { get; set; }

        // Indicates whether the task has been completed
        public bool Completed { get; set; }

        // Constructor
        public CyberTask(string title, string description, string reminder)
        {
            Title = title;
            Description = description;
            Reminder = reminder;
            Completed = false;
        }
    }
}