using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    class NLPManager
    {
        private static Dictionary<string, string> intents = new()
        {
            // PASSWORD
            { "password", "password" },
            { "passwords", "password" },
            { "forgot password", "password" },
            { "strong password", "password" },
            { "password safety", "password" },

            // PHISHING
            { "phishing", "phishing" },
            { "phishing email", "phishing" },
            { "fake email", "phishing" },
            { "email scam", "phishing" },

            // PRIVACY
            { "privacy", "privacy" },
            { "personal information", "privacy" },
            { "private data", "privacy" },
            { "safe browsing", "privacy" },

            // SCAMS
            { "scam", "scam" },
            { "fraud", "scam" },
            { "online scam", "scam" },
            { "money scam", "scam" }
        };

        public static string DetectIntent(string input)
        {
            input = input.ToLower();

            foreach (var item in intents)
            {
                if (input.Contains(item.Key))
                    return item.Value;
            }

            return "";
        }
    }
}