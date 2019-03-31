using PS9.Models;
using System;

namespace PS9
{
    internal interface IBoggleService
    {
        #region Public Events

        event Action CancelGame;

        event Action CancelRegister;

        event Action EnterGame;

        event Action RegisterUser;
        event Action<string> SubmitWord;

        event Action UpdateProperties;

        #endregion Public Events

        #region Public Methods

        void AddPlayedWord(string word);

        void DisplayFinalScore(string P1_Name, string P2_Name, string P1_Score, string P2_Score, WordAndScore[] P1_Words, WordAndScore[] P2_Words);

        void EnableTimer(bool state);

        string GetDesiredServer();

        int GetDesiredTime();

        string GetUsername();
        void Reset();

        void SetOpponentNickname(string nickname);

        void SetOpponentScore(string oppScore);

        void SetPlayerScore(string score);

        void SetRemainingTime(string remainingTime);

        void SetTimeLimit(string timeLimit);
        void SetUpBoard(string boardContents);

        void SetUpControlsAfterRegister();

        void SetUpControlsInGame();

        void SetUpControlsWhileRegister();

        void SetUpControlsWhileWaitingForGame();

        void ShowMessage(string errorMsg);

        #endregion Public Methods
    }
}