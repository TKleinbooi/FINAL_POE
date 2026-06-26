using CybersecBot_GUI;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    internal class Chatbot
    {
        public static string GetBotResponse(string input)
        {
            input = input.ToLower();

            // Emotion detection
            string emotion =
                SentimentAnalyzer.DetectEmotion(input);

            if (!string.IsNullOrEmpty(emotion))
                return emotion;

            // NLP detection
            string keyword =
                NLPManager.DetectIntent(input);

            if (!string.IsNullOrEmpty(keyword))
            {
                MemoryManager.SaveInterest(keyword);

                return ResponseHandler.GetResponse(keyword);
            }

            // Memory follow-up
            if (input.Contains("more")
                || input.Contains("another")
                || input.Contains("help"))
            {
                string topic =
                    MemoryManager.RecallInterest();

                if (!string.IsNullOrEmpty(topic))
                {
                    return "Here's another tip:\n\n"
                        + ResponseHandler.GetResponse(topic);
                }
            }

            return "I'm not sure I understand. Could you rephrase?";
        }
    }
}