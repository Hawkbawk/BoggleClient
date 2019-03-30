using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PS9
{
    class BoggleClientController
    {
        private IBoggleService view;
        private CancellationTokenSource tokenSource;
        private string UserToken { get; set; }

        private string Nickname { get; set; }
        private int ActualTime { get; set; }
        private string DesiredServer { get; set; }
        private string GameID { get; set; }
        private string Board { get; set; }
        private bool GameCompleted { get; set; }

        private GameResults gr;

        public BoggleClientController(IBoggleService _view)
        {
            view = _view;

            view.EnterGame += HandleEnterGame;
            view.CancelGame += HandleCancelGame;
            view.SubmitWord += HandleSubmitWord;
            view.CancelRegister += HandleCancelRegister;
            view.RegisterUser += HandleRegisterUser;
            view.GetHelp += HandleGetHelp;
        }

        private void HandleGetHelp()
        {
            view.ShowErrorMessage("The Rules of Boggle can be found here: http://en.wikipedia.org/wiki/Boggle. " +
                "With this client, all you need to do is make a username of your choice, connect it to a server of your choice, and enter in your desired time limit. " +
                "Once you have all the fields filled in, click the register button, and then join when it is possible. Once you are in a game, all you need to do is type in words you" +
                "found from the interface into the textbox on the bottom and submit the word by clicking enter.");
        }

        private void HandleRegisterUser()
        {
            Nickname = view.ObtainUsername();
            RegisterUser(view.ObtainUsername());
        }

        private void HandleCancelRegister()
        {
            tokenSource.Cancel();
        }

        private async void HandleEnterGame()
        {
            view.SetOpponentScore("0");
            view.SetPlayerScore("0");
            await JoinGame(view.GetDesiredTime());
            await StartGame();
        }

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
                        dynamic responseAsObject = JsonConvert.DeserializeObject(responseAsString);

                        if (responseAsObject["GameState"].ToString().Equals("active"))
                        {
                            isPending = false;
                        }

                    }


                }
            }
            catch (Exception)
            {
                view.ShowErrorMessage("A server side error has occurred. Please try again.");
            }
        }

        private async Task StartGame()
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource = new CancellationTokenSource();
                    //view.EnableControls(false);
                    dynamic body = new ExpandoObject();
                    body.GameID = GameID;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games/" + GameID + "/false";

                    // Parse the server's response.
                    HttpResponseMessage response = await client.GetAsync(gamesURI, tokenSource.Token);
                    string responseAsString = await response.Content.ReadAsStringAsync();
                    dynamic responseAsObject = JsonConvert.DeserializeObject(responseAsString);
                    if (responseAsObject["GameState"].ToString().Equals("pending"))
                    {
                        await CheckGameStatus();
                    }
                    Board = responseAsObject["Board"].ToString();
                    view.SetTimeLimit(responseAsObject["TimeLimit"].ToString());
                    view.SetUpBoard(Board.ToString());
                    dynamic player1 = JsonConvert.DeserializeObject(responseAsObject["Player1"].ToString());
                    dynamic player2 = JsonConvert.DeserializeObject(responseAsObject["Player2"].ToString());

                    bool ArePlayerOne = player1["Nickname"].ToString().Equals(Nickname);

                    Task updateBoard = Task.Run(() => UpdateBoard(ArePlayerOne));


                }
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Failed to start game.");
            }
        }

        private async void UpdateBoard(bool ArePlayerOne)
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
                    string gamesURI = DesiredServer + "/BoggleService/games/" + GameID + "/true";
                    // Check the server every second until the game is completed.
                    while (!GameCompleted)
                    {
                        // Query the server
                        HttpResponseMessage response = await client.GetAsync(gamesURI, tokenSource.Token);
                        string responseAsString = await response.Content.ReadAsStringAsync();
                        dynamic responseAsObject = JsonConvert.DeserializeObject(responseAsString);

                        // Parse the input and update the GameState
                        if (responseAsObject["GameState"].ToString().Equals("completed"))
                        {
                            GameCompleted = true;
                        }
                        // Update the TimeLeft
                        view.SetRemainingTime(responseAsObject["TimeLeft".ToString()]);

                        // Figure out which player we are and grab their info.
                        dynamic currentPlayer = new ExpandoObject();
                        if(ArePlayerOne)
                        {
                            currentPlayer = responseAsObject["Player1"];
                        } else
                        {
                            currentPlayer = responseAsObject["Player2"];
                        }

                        // Convert the JSON to a list of strings.
                        List<string> words = ConvertJSONToList(JsonConvert.SerializeObject(currentPlayer["WordsPlayed"]));

                        view.SetCurrentPlayedWords(words);


                        Thread.Sleep(1000);
                    }

                }
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Threading issues my dude.");
            }
        }

        private async Task JoinGame(int TimeLimit)
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource = new CancellationTokenSource();
                    view.EnableControlsJoin(false);  //Stuff from Joe's Controller3
                    // Create a dynamic object that will be serialized.
                    dynamic body = new ExpandoObject();
                    body.UserToken = UserToken;
                    body.TimeLimit = TimeLimit;

                    // Serialize the body and create the URI
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string usersURI = DesiredServer + "/BoggleService/games";

                    // Post to the server.
                    HttpResponseMessage response = await client.PostAsync(usersURI, content, tokenSource.Token);

                    // Tell the user if there are any problems with their input.
                    if (response.StatusCode.Equals(403))
                    {
                        throw new Exception("You've given an invalid name/time limit!!");
                    }
                    else if (response.StatusCode.Equals(409))
                    {
                        throw new Exception("A player with the same username as you is already in the pending game!");
                    }
                    // Otherwise, parse the input from the server.
                    string responseBodyAsString = await response.Content.ReadAsStringAsync();
                    dynamic responseBodyAsObject = JsonConvert.DeserializeObject(responseBodyAsString);
                    GameID = responseBodyAsObject["GameID"];
                }

            }
            catch (Exception)
            {
                view.ShowErrorMessage("Failed to join game.");
            }
            finally
            {
                view.EnableControlsJoin(true);
            }
        }

        private List<string> ConvertJSONToList(string json)
        {
            JObject listAsJObject = JObject.Parse(json);

            JArray listAsArray = (JArray)listAsJObject["Words"];

            return listAsArray.ToObject<List<string>>();
        }


        private void HandleSubmitWord(string obj)
        {
            PlayWord(obj);
        }


        private async void PlayWord(string word)
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource = new CancellationTokenSource();
                    //view.EnableControlsJoin(false);  //Stuff from Joe's Controller3
                    // Create a dynamic object that will be serialized.
                    dynamic body = new ExpandoObject();
                    body.UserToken = UserToken;
                    body.Word = word;

                    // Serialize the body and create the URI
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games/"+GameID;
                    HttpResponseMessage response = await client.PutAsync(gamesURI, content, tokenSource.Token);

                    // Tell the user if there are any problems with their input.
                    if (response.StatusCode.Equals(403))
                    {
                        throw new Exception("The Word is null, empty, or longer than 30 characters OR gameID or UserToken is invalid OR if UserToken is not a player in the game identified by gameID.");
                    }
                    else if (response.StatusCode.Equals(409))
                    {
                        throw new Exception("Game is not Active!");
                    }

                }
            }catch(Exception e)
            {
                view.ShowErrorMessage(e.ToString());
            }
            finally
            {
                //view.EnableControls(false);  //Stuff from Joe's Controller3
            }


        }

        private void HandleCancelGame()
        {
            CancelGame();
        }


        private async void CancelGame()
        {
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource.Cancel();
                    view.EnableControlsJoin(false);  //Stuff from Joe's Controller3
                    StringContent content = new StringContent(JsonConvert.SerializeObject(UserToken), Encoding.UTF8, "application/json");
                    string gamesURI = DesiredServer + "/BoggleService/games";
                    HttpResponseMessage response = await client.PutAsync(gamesURI, content, tokenSource.Token);
                    if (response.StatusCode.Equals(403))
                    {
                        throw new Exception("UserToken is invalid or is not a player in the pending game.");
                    }
                }

            }
            catch (Exception)
            {
                view.ShowErrorMessage("A server side error has occurred. Please try again.");
            }
            finally
            {
                view.EnableControlsJoin(true);
            }

        }


        private async void RegisterUser(string username)
        {

            DesiredServer = view.ObtainDesiredServer();
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource = new CancellationTokenSource();
                    view.EnableControlsRegister(false);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(username), Encoding.UTF8, "application/json");
                    string usersURI = DesiredServer + "/BoggleService/users";
                    HttpResponseMessage response = await client.PostAsync(usersURI, content, tokenSource.Token);
                    if (username.Equals("") || username.Equals("@"))
                    {
                        throw new ArgumentNullException("You've given an invalid name!");
                    }
                    if (response.StatusCode.Equals(403))
                    {
                        throw new Exception("You've given an invalid name!");
                    }
                    username = await response.Content.ReadAsStringAsync();
                    UserToken = (string)JsonConvert.DeserializeObject(username);
                    view.EnableEnterGameButton(true);
                }

            }
            catch (Exception e)
            {
                if (e.Equals(typeof (TaskCanceledException)))
                {
                    view.ShowErrorMessage("A server side error has occurred. Please try again.");
                    view.ShowErrorMessage(e.ToString());
                    view.EnableEnterGameButton(false);
                }
            }
            finally
            {
                view.EnableControlsRegister(true);
            }

        }

        private static HttpClient CreateClient(string address)
        {
            //Create a client whose base address is whatever the user inputs
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(address);

            //Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }





    }
}
