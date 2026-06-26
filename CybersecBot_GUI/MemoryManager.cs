using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    internal class MemoryManager
    {
        private static Dictionary<string,
            string> memory =
            new Dictionary<string,
            string>();

        public static void SaveInterest(
            string topic)
        {
            memory["interest"] =
                topic;
        }

        public static string RecallInterest()
        {
            if (memory.ContainsKey(
                "interest"))
            {
                return memory[
                    "interest"];
            }

            return "";
        }
    }
}