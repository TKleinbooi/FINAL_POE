using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    internal class ResponseHandler
    {
        private static Random random =
            new Random();

        private static Dictionary<string,
            List<string>> responses =
            new Dictionary<string,
            List<string>>()
        {
            {
                "password",
                new List<string>()
                {
                    "Use strong passwords with symbols and numbers.",
                    "Avoid using personal information in passwords.",
                    "Never reuse passwords across accounts."
                }
            },

            {
                "phishing",
                new List<string>()
                {
                    "Be careful of suspicious emails.",
                    "Never click suspicious links.",
                    "Scammers often pretend to be trusted organisations."
                }
            },

            {
                "privacy",
                new List<string>()
                {
                    "Enable privacy settings on your accounts.",
                    "Avoid oversharing online.",
                    "Always enable two-factor authentication."
                }
            },

            {
                "scam",
                new List<string>()
                {
                    "Be cautious of urgent money requests.",
                    "Scammers create fake emergencies.",
                    "Always verify suspicious messages."
                }
            }
        };

        public static string GetResponse(
            string input)
        {
            foreach (var keyword
                in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    var list =
                        responses[keyword];

                    return list[
                        random.Next(list.Count)];
                }
            }

            return
                "I'm not sure I understand. Can you rephrase?";
        }
    }
}