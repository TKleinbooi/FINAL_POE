using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    internal class SentimentAnalyzer
    {
        public static string DetectEmotion(string input)
        {
            input = input.ToLower();

            // Worried emotion
            if (input.Contains("worried"))
            {
                return "I understand cybersecurity can feel overwhelming. You're doing great by learning.";
            }

            // Frustrated emotion
            if (input.Contains("frustrated"))
            {
                return "Cybersecurity can be frustrating sometimes. I am here to help.";
            }

            // Curious emotion
            if (input.Contains("curious"))
            {
                return "Curiosity is awesome! Learning cybersecurity helps keep you safe online.";
            }

            // No emotion detected
            return "";
        }
    }
}