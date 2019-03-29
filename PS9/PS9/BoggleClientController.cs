using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace PS9
{
    class BoggleClientController
    {
        private IBoggleService view;
        private CancellationTokenSource tokenSource;
        private string UserToken { get; set; }
        private int ActualTime { get; set; }
        private string DesiredServer { get; set; }
        private string GameID { get; set; }

        public BoggleClientController(IBoggleService _view)
        {
            view = _view;

            view.EnterGame += HandleEnterGame;
            view.CancelGame += HandleCancelGame;
            view.SubmitWord += HandleSubmitWord;
            view.CancelRegister += HandleCancelRegister;
            view.RegisterUser += HandleRegisterUser;
        }

        private void HandleRegisterUser()
        {
            RegisterUser(view.ObtainUsername());
        }

        private void HandleCancelRegister()
        {
            tokenSource.Cancel();
        }

        private void HandleEnterGame()
        {
            view.SetOpponentScore(0);
            view.SetPlayerScore(0);
            JoinGame(view.GetDesiredTime());
            StartGame();
        }

        private void CheckGameStatus()
        {
            view.ShowErrorMessage("It's checking for the game status.");
            try
            {
                using (HttpClient client = CreateClient(DesiredServer))
                {
                    tokenSource = new CancellationTokenSource();
                    //view.EnableControls(false);

                }
            }
            finally
            {

            }
        }

        private void StartGame()
        {
            view.EnableControlsInGame(false);




            view.EnableControlsInGame(true);
        }

        private async void JoinGame(int TimeLimit)
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
                    if ((bool)responseBodyAsObject["IsPending"])
                    {
                        CheckGameStatus();
                    }
                    return;

                }

            }
            catch (Exception e)
            {
                view.ShowErrorMessage(e.ToString());
            }
            finally
            {
                view.EnableControlsJoin(true);
            }
        }



        private void HandleSubmitWord(string obj)
        {
            throw new NotImplementedException();
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
            catch (Exception e)
            {
                view.ShowErrorMessage(e.ToString());
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
                    if (response.StatusCode.Equals(403))
                    {
                        throw new Exception("You've given an invalid name!");
                    }
                    username = await response.Content.ReadAsStringAsync();
                    UserToken = (string)JsonConvert.DeserializeObject(username);
                }

            }
            catch (Exception e)
            {
                view.ShowErrorMessage(e.ToString());
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
