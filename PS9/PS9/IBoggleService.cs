﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS9
{
    interface IBoggleService
    {
        event Action EnterGame;
        event Action CancelGame;
        event Action<string> SubmitWord;

        string ObtainUsername();

        string ObtainDesiredServer();

        void SetTimeLimit(int timeLimit);

        int GetDesiredTime();
        void SetRemainingTime(int remainingTime);

        void SetPlayerScore(int score);

        void SetOpponentScore(int oppScore);

        void SetCurrentPlayedWords(List<string> words);

        void EnableControls(bool state);

        void ShowErrorMessage(string errorMsg);



    }
}
