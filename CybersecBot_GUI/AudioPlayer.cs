using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CybersecBot_GUI
{
    internal class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                // Play greeting sound
                SoundPlayer player =
                    new SoundPlayer("Audio\\greeting.wav");

                player.PlaySync();
            }
            catch
            {
                MessageBox.Show(
                    "Could not play greeting audio.",
                    "Audio Error"
                );
            }
        }
    }
}