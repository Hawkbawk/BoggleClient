using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS9
{
    class BoggleClientController : BoggleServiceInterface
    {
        public void CancelJoinRequest(string UserToken)
        {
            throw new NotImplementedException();
        }

        public void GameStatus(string GameID, bool Brief)
        {
            throw new NotImplementedException();
        }

        public void JoinGame(string UserToken, int TimeLimit)
        {
            throw new NotImplementedException();
        }

        public void PlayWord(string GameID, string UserToken, string Word)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(string UserToken)
        {
            throw new NotImplementedException();
        }
    }
}
