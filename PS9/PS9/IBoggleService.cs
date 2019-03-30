using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS9
{
    interface IBoggleService
    {
        event Action EnterGame;
        event Action RegisterUser;
        event Action CancelRegister;
        event Action CancelGame;
        event Action<string> SubmitWord;
        event Action GetHelp;
        event Action UpdateProperties;

        string ObtainUsername();

        string ObtainDesiredServer();

        void SetTimeLimit(string timeLimit);

        int GetDesiredTime();
        void SetRemainingTime(string remainingTime);

        void SetPlayerScore(string score);

        void SetOpponentScore(string oppScore);

        void SetCurrentPlayedWords(Word[] words);

        void EnableControlsRegister(bool state);

        void EnableEnterGameButton(bool state);

        void EnableControlsJoin(bool state);

        void EnableControlsInGame(bool state);

        void ShowErrorMessage(string errorMsg);

        void SetUpBoard(string boardContents);

        void EnableTextBoxAndRegister(bool state);

        void SetOpponentNickname(string nickname);
    }
}
