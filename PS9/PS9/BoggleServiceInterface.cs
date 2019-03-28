using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS9
{
    interface BoggleServiceInterface
    {

        /// <summary>
        /// Creates a new user
        /// 
        /// If Nickname is null, or when trimmed is empty or longer than 50 characters, responds with status 403 (Forbidden).
        /// Otherwise, creates a new user with a unique user token and the trimmed Nickname.
        /// The returned user token should be used to identify the user in subsequent requests. Responds with status 200 (Ok).
        /// 
        /// </summary>
        /// <param name="UserToken">Nickname of the User joining a game</param>
        void RegisterUser(string UserToken);

        /// <summary>
        /// Joins the game
        /// 
        /// If UserToken is invalid, TimeLimit < 5, or TimeLimit > 120, responds with status 403 (Forbidden).
        /// 
        /// Otherwise, if UserToken is already a player in the pending game, responds with status 409 (Conflict).
        /// 
        /// Otherwise, if there are no players in the pending game, adds UserToken as the first player of the pending game, 
        /// and the TimeLimit as the pending game's requested time limit. Returns an object as illustrated below containing 
        /// the pending game's game ID. Responds with status 200 (Ok).
        /// 
        /// Otherwise, adds UserToken as the second player. The pending game becomes active and a new pending game with no players is created. 
        /// The active game's time limit is the integer average of the time limits requested by the two players. Returns an object as illustrated 
        /// below containing the new active game's game ID (which should be the same as the old pending game's game ID). Responds with status 200 (Ok).
        /// 
        /// </summary>
        /// <param name="UserToken"></param>
        /// <param name="TimeLimit"></param>
        void JoinGame(string UserToken, int TimeLimit);

        /// <summary>
        /// Cancel a pending request to join a game.
        /// 
        /// If UserToken is invalid or is not a player in the pending game, responds with status 403 (Forbidden).
        /// 
        /// Otherwise, removes UserToken from the pending game and responds with status 204 (NoContent).
        /// </summary>
        /// <param name="UserToken"></param>
        void CancelJoinRequest(string UserToken);

        /// <summary>
        /// Play a word in a game.
        /// 
        /// If Word is null or empty or longer than 30 characters when trimmed, or if gameID or UserToken is invalid, 
        /// or if UserToken is not a player in the game identified by gameID, responds with response code 403 (Forbidden).
        /// 
        /// Otherwise, if the game state is anything other than "active", responds with response code 409 (Conflict).
        /// 
        /// Otherwise, records the trimmed Word as being played by UserToken in the game identified by gameID. 
        /// Returns the score for Word in the context of the game (e.g. if Word has been played before the score is zero). 
        /// Responds with status 200 (OK). Note: The word is not case sensitive.
        /// 
        /// </summary>
        /// <param name="GameID"></param>
        /// <param name="UserToken"></param>
        /// <param name="Word"></param>
        void PlayWord(string GameID, string UserToken, string Word);


        /// <summary>
        /// Get game status information.
        /// 
        /// If gameID is invalid, responds with status 403 (Forbidden).
        /// 
        /// Otherwise, returns information about the game named by gameID as illustrated below. 
        /// Note that the information returned depends on whether brief is true or false as well as on the state of the game. 
        /// Responds with status code 200 (OK). Note: The Board and Words are not case sensitive.
        /// 
        /// </summary>
        /// <param name="GameID"></param>
        /// <param name="Brief"></param>
        void GameStatus(string GameID, bool Brief);

    }
}
