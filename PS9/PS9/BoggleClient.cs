using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PS9
{
    public partial class BoggleClient : Form, IBoggleService
    {
        public BoggleClient()
        {
            InitializeComponent();
            ServerName_Textbox.Text = "http://ice.eng.utah.edu/BoggleService";
            Cancel_Game_Button.Enabled = false;
        }

        public event Action EnterGame;
        public event Action CancelGame;
        public event Action<string> SubmitWord;



        public string ObtainUsername()
        {
            return Username_Textbox.Text;
        }

        public string ObtainDesiredServer()
        {
            return ServerName_Textbox.Text;
        }

        public void SetTimeLimit()
        {

        }

        public void SetRemainingTime()
        {
            throw new NotImplementedException();
        }

        public void SetPlayerScore(int score)
        {
            throw new NotImplementedException();
        }

        public void SetOpponentScore(int oppScore)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentPlayedWords(List<string> words)
        {
            throw new NotImplementedException();
        }

        public void EnableControls(bool state)
        {
            Username_Textbox.Enabled = state;
            ServerName_Textbox.Enabled = state;
            TimeLimit_Textbox.Enabled = state;
            Enter_Game_Button.Enabled = state;
            Cancel_Game_Button.Enabled = !state;
        }

        public void ShowErrorMessage(string errorMsg)
        {
            MessageBox.Show(errorMsg);
        }

        public int GetDesiredTime()
        {
            int time = -1;
            try
            {
                time = Convert.ToInt32(TimeLimit_Textbox.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please only enter in integer values for your time limit.");
                return -1;
            }
            if (time < 5 || time > 120)
            {
                throw new ArgumentOutOfRangeException();
            }
            return time;
        }

        private void Enter_Game_Button_Click(object sender, EventArgs e)
        {
            EnterGame();
        }

        private void Cancel_Game_Button_Click(object sender, EventArgs e)
        {
            CancelGame();
        }
    }
}
