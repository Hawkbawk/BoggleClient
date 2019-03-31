using PS9.Models;
using System;

namespace PS9
{
    internal interface IBoggleService
    {
        event Action EnterGame;

        event Action RegisterUser;

        event Action CancelRegister;

        event Action CancelGame;

        event Action<string> SubmitWord;

        event Action UpdateProperties;

        string ObtainUsername();

        string ObtainDesiredServer();

        void SetTimeLimit(string timeLimit);

        int GetDesiredTime();

        void SetRemainingTime(string remainingTime);

        void SetPlayerScore(string score);

        void SetOpponentScore(string oppScore);

        void AddPlayedWord(string word);

        void EnableControlsRegister(bool state);

        void EnableEnterGameButton(bool state);

        void EnableControlsJoin(bool state);

        void EnableControlsInGame(bool state);

        void ShowMessage(string errorMsg);

        void SetUpBoard(string boardContents);

        void EnableTextBoxAndRegister(bool state);

        void SetOpponentNickname(string nickname);

        void EnableTimer(bool state);

        void Reset();

        void DisplayFinalScore(string P1_Name, string P2_Name, string P1_Score, string P2_Score, WordAndScore[] P1_Words, WordAndScore[] P2_Words);
    }
}