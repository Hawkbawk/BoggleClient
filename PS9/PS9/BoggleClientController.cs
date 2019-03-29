using Newtonsoft.Json;
using System;
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

        public BoggleClientController(IBoggleService _view)
        {
            view = _view;

            view.EnterGame += HandleEnterGame;
            view.CancelGame += HandleCancelGame;
            view.SubmitWord += HandleSubmitWord;

        }

        private void HandleEnterGame()
        {
            RegisterUser(view.ObtainUsername(), view.ObtainDesiredServer());
        }

        private void HandleSubmitWord(string obj)
        {
            throw new NotImplementedException();
        }

        private void HandleCancelGame()
        {
            throw new NotImplementedException();
        }

        private async void RegisterUser(string username, string address)
        {

            try
            {
                using (HttpClient client = CreateClient(address))
                {
                    tokenSource = new CancellationTokenSource();
                    view.EnableControls(false);  //Stuff from Joe's Controller3
                    StringContent content = new StringContent(JsonConvert.SerializeObject(username), Encoding.UTF8, "application/json");
                    string usersURI = "http://ice.eng.utah.edu/BoggleService/users";
                    HttpResponseMessage response = await client.PostAsync(usersURI, content, tokenSource.Token);
                    if (response.StatusCode.Equals(403))
                    {
                        throw new Exception("You've given an invalid name!");
                    }
                    username = await response.Content.ReadAsStringAsync();

                }

            }
            catch (Exception e)
            {
                view.ShowErrorMessage(e.ToString());
            }
            finally
            {
                view.EnableControls(true);
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
