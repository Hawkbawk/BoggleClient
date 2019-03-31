﻿using Newtonsoft.Json;
using PS9.Models;
using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PS9
{
    internal class BoggleClientController
    {

        #region Private Fields

        /// <summary>
        /// A token that can be used to cancel an HTTP request if it's taking too long.
        /// </summary>
        private CancellationTokenSource tokenSource;

        /// <summary>
        /// Some object that implements the methods defined in the IBoggleService interface.
        /// Typically a GUI that a user interacts with, but can also just be a stub method for testing.
        /// </summary>
        private IBoggleService view;

        #endregion Private Fields

        #region Private Properties

        /// <summary>
        /// A private instance variable for determining which player the user is in their game.
        /// </summary>
        private bool ArePlayerOne { get; set; }

        /// <summary>
        /// A private instance variable that represents the board that the user is currently playing with.
        /// </summary>
        private string Board { get; set; }

        /// <summary>
        /// A private instance variable that represents the base address of the server the user would like to connect to.
        /// </summary>
        private string DesiredServer { get; set; }

        /// <summary>
        /// A private instance variable that represents the ID of the game that the player is currently registered under.
        /// </summary>
        private string GameID { get; set; }

        /// <summary>
        /// A private instance variable that states whether or not the player is in a game.
        /// </summary>
        private bool InAGame { get; set; }

        /// <summary>
        /// A private instance variable that represents the user's desired nickname.
        /// </summary>
        private string UserNickname { get; set; }

        /// <summary>
        /// A private instance variable that represents the user's unique, identifying UserToken, as
        /// generated by their desired server.
        /// </summary>
        private string UserToken { get; set; }

        #endregion Private Properties

        #region Public Constructors

        /// <summary>
        /// Constructs a new BoggleClientController.
        /// </summary>
        /// <param name="_view"></param>
        public BoggleClientController(IBoggleService _view)
        {
            view = _view;

            view.EnterGame += HandleEnterGame;
            view.CancelGame += HandleCancelGame;
            view.SubmitWord += HandleSubmitWord;
            view.CancelRegister += HandleCancelRegister;
            view.RegisterUser += HandleRegisterUser;
            view.UpdateProperties += UpdateBoard;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Creates a new HttpClient with the passed in address, to be used for querying a server
        /// that implements the Boggle API.
        /// </summary>
        /// <param name="address">The base address of the server to be queryed</param>
        /// <returns>An HttpClient for querying the specified server</returns>
        private static HttpClient CreateClient(string address)
        {
            //Create a client whose base address is whatever the user inputs
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(address)
            };

            //Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        /// <summary>
        /// Repeatedly checks the game's status until the user either cancel's their request or the
        /// game is no longer pending.
        /// </summary>
        /// <returns></returns>
        private async Task CheckGameStatus()
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource = new CancellationTokenSource();
                    bool isPending = true;
                    //view.EnableControls(false);
                    dynamic body = new ExpandoObject();
                    body.GameID = GameID;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games/" + GameID + "/true";
                    while (isPending)
                    {
                        HttpResponseMessage response = await client.GetAsync(gamesURI, tokenSource.Token);
                        string responseAsString = await response.Content.ReadAsStringAsync();
                        GetGameStatus responseAsObject = JsonConvert.DeserializeObject<GetGameStatus>(responseAsString);

                        if (responseAsObject.GameState.Equals("active"))
                        {
                            isPending = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is TaskCanceledException)
                {
                    view.ShowMessage("Game canceled.");
                }
            }
        }

        /// <summary>
        /// Finishes the game by disabling the timer and displaying each player's list of fina words
        /// played as well as each word's associated score.
        /// </summary>
        /// <param name="responseBody"></param>
        private void FinishGame(GetGameStatus responseBody)
        {
            Player player1 = responseBody.Player1;
            Player player2 = responseBody.Player2;
            view.Reset();
            InAGame = false;
            if (player1.WordsPlayed == null)
            {
                player1.WordsPlayed = new WordAndScore[0];
            }
            if (player2.WordsPlayed == null)
            {
                player2.WordsPlayed = new WordAndScore[0];
            }

            view.EnableTimer(false);
            view.DisplayFinalScore(player1.Nickname, player2.Nickname,
                player1.Score.ToString(), player2.Score.ToString(),
                player1.WordsPlayed, player2.WordsPlayed);
        }

        /// <summary>
        /// Cancels the game that the user is currently in or trying to join.
        /// </summary>
        private async void HandleCancelGame()
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    // TODO: Ensure that the controls are properly updating before querying the server to in the BoggleClientController.CancelGame method.

                    // Setup the stuff for querying the server
                    StringContent content = new StringContent(JsonConvert.SerializeObject(UserToken), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games";
                    // Do a PUT request to tell the server we want to cancel our game.
                    HttpResponseMessage response = await client.PutAsync(gamesURI, content);
                    // Catch any errors from the server's response.
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        //view.ShowMessage("You're not currently in a pending " +
                        //    "game. You probably shouldn't even be able to hit that button. Oops :)");
                        //return;
                    }

                    // Change our URI so we can get the status of our game.
                    gamesURI += "/" + GameID + "/false";

                    // Query the server about our game.
                    response = await client.GetAsync(gamesURI);

                    // Deserialize the server's response.
                    string responseAsString = await response.Content.ReadAsStringAsync();
                    GetGameStatus responseBody = JsonConvert.DeserializeObject<GetGameStatus>(responseAsString);

                    // Reset the board for a canceled pending game
                    if (responseBody.GameState.Equals("pending"))
                    {
                        // TODO: Reset the board for a pending game being canceled.
                        view.SetUpControlsAfterRegister();
                        view.EnableTimer(false);
                        InAGame = false;
                        tokenSource.Cancel();
                    }
                    // Or reset the board for a canceled active/completed game, although you shouldn't technically .
                    else
                    {
                        tokenSource.Cancel();
                        FinishGame(responseBody);
                    }
                }
            }
            catch (Exception e)
            {
                if (!(e is TaskCanceledException))
                {
                    view.ShowMessage("A server side error has occurred. Please try again.");
                }
            }
        }

        /// <summary>
        /// Cancels the user's pending registration with the server.
        /// </summary>
        private void HandleCancelRegister()
        {
            tokenSource.Cancel();
            view.Reset();
        }

        /// <summary>
        /// Starts the process for entering the user into a game of Boggle.
        /// </summary>
        private async void HandleEnterGame()
        {
            // If you're already in a game, you definitely shouldn't be able to enter a game again.
            if (InAGame)
            {
                return;
            }

            // Obtain the desired time limit.
            int desiredTime = view.GetDesiredTime();

            // If the time limit is invalid (IE less than 0), then simply stop trying to enter the game.
            if (desiredTime < 0)
            {
                return;
            }

            // Otherwise, wait for JoinGame to complete, and then start the game.
            await JoinGame(desiredTime);
            await StartGame();
        }

        /// <summary>
        /// Registers the user with their desired server by using their desired nickname. Shows an
        /// appropriate error message to the user if something goes wrong.
        /// </summary>
        private async void HandleRegisterUser()
        {
            // Obtains the user's desired username and gives them a message if it's invalid.
            UserNickname = view.GetUsername();
            if (UserNickname == null || UserNickname.Length > 50)
            {
                view.ShowMessage("You've given an invalid username!");
                return;
            }

            // Obtains the user's desired server and gives them a message if it's an invalid server name.
            DesiredServer = view.GetDesiredServer();
            if (DesiredServer == null)
            {
                view.ShowMessage("You can't have an empty server!");
                return;
            }

            // Tries to register the user with their desired server.
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    view.SetUpControlsWhileRegister();
                    // Setup everything needed to query the server.
                    tokenSource = new CancellationTokenSource();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(UserNickname), Encoding.UTF8, "application/json");
                    string usersURI = DesiredServer + "/BoggleService/users";
                    // Query the server and tell it to register this user.
                    HttpResponseMessage response = await client.PostAsync(usersURI, content, tokenSource.Token);
                    // Catch any errors that the server might give us.
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        view.ShowMessage("You've given an invalid name!");
                        view.Reset();
                        return;
                    }
                    // Otherwise, read in the UserToken from the server and store it.
                    string userTokenAsJson = await response.Content.ReadAsStringAsync();
                    UserToken = (string)JsonConvert.DeserializeObject(userTokenAsJson);
                    view.SetUpControlsAfterRegister();
                }
            }
            catch (Exception e)
            {
                if (e is TaskCanceledException)
                {
                    view.ShowMessage("Registration canceled.");
                    view.Reset();
                    return;
                }
            }
        }

        /// <summary>
        /// Submits the passed in word to the server handling all of the scoring and then updates the
        /// GUI to show the passed in word as played, as well as what score is associated with that word.
        /// </summary>
        /// <param name="word">The word to be played</param>
        private async void HandleSubmitWord(string word)
        {
            // Make it so the user can't submit a word if they're not in a game.
            if (!InAGame)
            {
                return;
            }
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    // Create a dynamic object that will be serialized.
                    dynamic body = new ExpandoObject();
                    body.UserToken = UserToken;
                    body.Word = word;

                    // Serialize the body and create the URI
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games/" + GameID;
                    // Query the server
                    HttpResponseMessage response = await client.PutAsync(gamesURI, content);

                    // Receive the submitted words score.
                    string score = await response.Content.ReadAsStringAsync();
                    // Tell the user if there are any problems with their input.
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        view.ShowMessage("The word you've given is either null or too long!");
                        return;
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        view.ShowMessage("Your current game isn't active!");
                        return;
                    }
                    // Otherwise, shows the player the word they just played and that word's score.
                    view.AddPlayedWord(word + "\t" + score);
                }
            }
            catch (Exception)
            {
                view.ShowMessage("A server side error has occurred. Please try again.");
            }
        }

        /// <summary>
        /// Put's the user in the queue with the server, saying they'd like to start a game. Stores
        /// the GameID that the server returns, so the player can actually join the game once it starts.
        /// </summary>
        /// <param name="TimeLimit">The user's desired time limit for the game</param>
        /// <returns></returns>
        private async Task JoinGame(int TimeLimit)
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    view.SetUpControlsWhileWaitingForGame();
                    tokenSource = new CancellationTokenSource();
                    // Create a dynamic object that will be serialized.
                    dynamic body = new ExpandoObject();
                    body.UserToken = UserToken;
                    body.TimeLimit = TimeLimit;

                    // Serialize the body and create the URI
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string usersURI = DesiredServer + "/BoggleService/games";

                    // Post to the server.
                    HttpResponseMessage response = await client.PostAsync(usersURI, content, tokenSource.Token);

                    // Tell the user if they're already in a game, so they can't start a new game!
                    if (!response.IsSuccessStatusCode)
                    {
                        view.ShowMessage("You're already in a game!");
                        return;
                    }
                    // Otherwise, parse the input from the server.
                    string responseBodyAsString = await response.Content.ReadAsStringAsync();
                    JoinGameResponse responseBody = JsonConvert.DeserializeObject<JoinGameResponse>(responseBodyAsString);
                    GameID = responseBody.GameID;
                    ArePlayerOne = responseBody.IsPending;
                }
            }
            catch (Exception e)
            {
                if (e is TaskCanceledException)
                {
                    HandleCancelGame();
                }
            }
        }

        /// <summary>
        /// Using the previously obtained GameID, waits for that game to start up. Once the game is
        /// no longer pending, sets up the view accordingly.
        /// </summary>
        /// <returns>Returns a Task with no return type so this method can be awaited</returns>
        private async Task StartGame()
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    view.SetUpControlsWhileWaitingForGame();
                    // Setup all the stuff necessary to query the server.
                    tokenSource = new CancellationTokenSource();
                    dynamic body = new ExpandoObject();
                    body.GameID = GameID;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games/" + GameID + "/false";

                    // Query the server
                    HttpResponseMessage response = await client.GetAsync(gamesURI, tokenSource.Token);

                    // Parse the server's response.
                    string responseAsString = await response.Content.ReadAsStringAsync();
                    GetGameStatus responseBody = JsonConvert.DeserializeObject<GetGameStatus>(responseAsString);
                    if (responseBody.GameState.Equals("pending"))
                    {
                        await CheckGameStatus();
                    }
                    // Requery the server for the new information.
                    response = await client.GetAsync(gamesURI, tokenSource.Token);
                    responseAsString = await response.Content.ReadAsStringAsync();
                    responseBody = JsonConvert.DeserializeObject<GetGameStatus>(responseAsString);

                    // Parse all the returned things.
                    Board = responseBody.Board;
                    view.SetTimeLimit(responseBody.TimeLimit);
                    view.SetUpBoard(Board.ToString());
                    if (ArePlayerOne)
                    {
                        view.SetOpponentNickname(responseBody.Player2.Nickname);
                    }
                    else
                    {
                        view.SetOpponentNickname(responseBody.Player1.Nickname);
                    }
                    view.EnableTimer(true);
                    InAGame = true;
                    view.SetUpControlsInGame();
                }
            }
            catch (Exception e)
            {
                if (e is TaskCanceledException)
                {
                    view.SetUpControlsAfterRegister();
                    return;
                }
            }
        }

        /// <summary>
        /// Method called by the timer every second that updates the view to contain the most current
        /// information about the current state of the game.
        /// </summary>
        private async void UpdateBoard()
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    // Setup the stuff necessary to query the server.
                    tokenSource = new CancellationTokenSource();
                    dynamic body = new ExpandoObject();
                    body.GameID = GameID;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games/" + GameID + "/false";
                    // Check the server every second until the game is completed.
                    // Query the server
                    HttpResponseMessage response = await client.GetAsync(gamesURI, tokenSource.Token);
                    string responseAsString = await response.Content.ReadAsStringAsync();
                    GetGameStatus responseBody = JsonConvert.DeserializeObject<GetGameStatus>(responseAsString);

                    // If the game is completed, finish up the game
                    if (responseBody.GameState.Equals("completed"))
                    {
                        FinishGame(responseBody);
                        return;
                    }

                    // Otherwise, update the view's elements to match the newly received information.
                    view.SetRemainingTime(responseBody.TimeLeft);

                    // Depending on if you are or aren't player one, set's the scores accordingly.
                    if (ArePlayerOne)
                    {
                        view.SetPlayerScore(responseBody.Player1.Score.ToString());
                        view.SetOpponentScore(responseBody.Player2.Score.ToString());
                    }
                    else
                    {
                        view.SetPlayerScore(responseBody.Player2.Score.ToString());
                        view.SetOpponentScore(responseBody.Player1.Score.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                if (!(e is TaskCanceledException))
                {
                    view.ShowMessage("A server side error has occurred. Please try again.");
                }
            }
        }

        #endregion Private Methods

    }
}