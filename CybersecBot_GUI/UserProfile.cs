using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
    class UserProfile
    {
        public string Name { get; set; }

        public string FavouriteTopic { get; set; }

        public int QuizScore { get; set; }

        public UserProfile()
        {
            Name = "";
            FavouriteTopic = "";
            QuizScore = 0;
        }
    }
}