using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    class ChatMessage
    {
        public string Sender { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public ChatMessage(string sender, string message)
        {
            Sender = sender;
            Message = message;
            Time = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Time:HH:mm}] {Sender}: {Message}";
        }
    }
}