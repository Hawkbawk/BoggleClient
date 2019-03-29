using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PS9
{
    class BoggleClientController
    {
        IBoggleService view;

        public BoggleClientController(IBoggleService _view)
        {
            view = _view;

            view.EnterGame += HandleEnterGame;
            view.CancelGame += HandleCancelGame;
            view.SubmitWord += HandleSubmitWord;

        }

        private void HandleSubmitWord(string obj)
        {
            throw new NotImplementedException();
        }

        private void HandleCancelGame()
        {
            throw new NotImplementedException();
        }

        public async void RegisterUser(string UserToken, string address)
        {

            try
            {
                using (HttpClient client = CreateClient(address))
                {
                    //tokenSource = new CancellationTokenSource();
                    //view.EnableControls(false);  //Stuff from Joe's Controller3
                    StringContent content = new StringContent(JsonConvert.SerializeObject(UserToken), Encoding.UTF8, "POST BoggleService/users");

                    //HttpResponseMessage response = await client.PostAsync("RegisterUser", content, tokenSource.Token);
                }

            }
            catch
            {

            }
            finally
            {
                //view.EnableControls(true); //Stuff from Joe's Controller3
            }
            
        }

        private static HttpClient CreateClient(string address)
        {
            //Create a client whose base address is whatever the user inputs
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(address);

            //Tell the server that the client will accept this particular type of respose data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", address);

            return client;
        }







    }
}
