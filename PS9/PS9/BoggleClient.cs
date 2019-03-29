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
            ServerName_Textbox.Text = "http://ice.eng.utah.edu";
            RegisterUser_Button.Enabled = true;
            Cancel_Game_Button.Enabled = false;
            CancelRegister_Button.Enabled = false;
            Enter_Game_Button.Enabled = false;
        }

        public event Action EnterGame;
        public event Action CancelGame;
        public event Action<string> SubmitWord;
        public event Action RegisterUser;
        public event Action CancelRegister;

        public string ObtainUsername()
        {
            return Username_Textbox.Text;
        }

        public string ObtainDesiredServer()
        {
            return ServerName_Textbox.Text;
        }

        public void SetTimeLimit(int timeLimit)
        {
            TimeLimit_Textbox.Text = timeLimit.ToString();
            Time_Limit_Label.Text = timeLimit.ToString();
            Remaining_Time_Label.Text = timeLimit.ToString();
        }

        public void SetRemainingTime(int remainingTime)
        {
            Remaining_Time_Label.Text = remainingTime.ToString();
        }

        public void SetPlayerScore(int score)
        {
            Player_CurrentScore_Label.Text = score.ToString();
        }

        public void SetOpponentScore(int oppScore)
        {
            Opponent_CurrentScore_Label.Text = oppScore.ToString();
        }

        public void SetCurrentPlayedWords(List<string> words)
        {
            throw new NotImplementedException();
        }

        public void EnableTextBox(bool state)
        {
            Username_Textbox.Enabled = state;
            ServerName_Textbox.Enabled = state;
            TimeLimit_Textbox.Enabled = state;
        }

        public void EnableControlsRegister(bool state)
        {
            EnableTextBox(state);
            Enter_Game_Button.Enabled = state;
            RegisterUser_Button.Enabled = state;
            CancelRegister_Button.Enabled = !state;
        }

        public void EnableControlsJoin(bool state)
        {
            EnableTextBox(state);
            Enter_Game_Button.Enabled = state;
            Cancel_Game_Button.Enabled = !state;
        }

        public void EnableControlsInGame(bool state)
        {
            EnableTextBox(state);
            Enter_Game_Button.Enabled = state;
            Cancel_Game_Button.Enabled = !state;
            RegisterUser_Button.Enabled = state;
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

        private void RegisterUser_Button_Click(object sender, EventArgs e)
        {
            RegisterUser();
        }

        private void CancelRegister_Button_Click(object sender, EventArgs e)
        {
            CancelRegister();
        }
    }
}
