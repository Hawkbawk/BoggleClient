using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS9
{
    public class GetGameResponse
    {
        public string GameState;
        public string Board;
        public string TimeLimit;
        public string TimeLeft;
        public Player Player1;
        public Player Player2;
    }
    public class Player
    {
        public string Nickname;
        public int Score;
        public Word[] WordsPlayed;
    }

    public class Word
    {
        public string word;
        public int score;
    }
}
