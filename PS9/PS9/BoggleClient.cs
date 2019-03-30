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
            UpdateProperty_Timer.Tick += HandleUpdate;
        }

        private void HandleUpdate(object sender, EventArgs e)
        {
            UpdateProperties();
        }

        public event Action EnterGame;
        public event Action CancelGame;
        public event Action<string> SubmitWord;
        public event Action RegisterUser;
        public event Action CancelRegister;
        public event Action GetHelp;
        public event Action UpdateProperties;

        public string ObtainUsername()
        {
            return Username_Textbox.Text;
        }

        public string ObtainDesiredServer()
        {
            return ServerName_Textbox.Text;
        }

        public void SetTimeLimit(string timeLimit)
        {
            TimeLimit_Textbox.Text = timeLimit;
            Time_Limit_Label.Text = timeLimit;
            Remaining_Time_Label.Text = timeLimit;           //Needs to be fixed to be with the server rather than with the local
        }

        public async void SetRemainingTime(string remainingTime)
        {
            Remaining_Time_Label.Text = remainingTime;
        }

        public void SetPlayerScore(string score)
        {
            Player_CurrentScore_Label.Text = score;
        }

        public void SetOpponentScore(string oppScore)
        {
            Opponent_CurrentScore_Label.Text = oppScore;
        }

        public void SetCurrentPlayedWords(List<string> words)
        {
            throw new NotImplementedException();
        }

        public void EnableTextBoxAndRegister(bool state)
        {
            Username_Textbox.Enabled = state;
            ServerName_Textbox.Enabled = state;
            TimeLimit_Textbox.Enabled = state;
            RegisterUser_Button.Enabled = state;
        }

        public void EnableControlsRegister(bool state)
        {
            EnableTextBoxAndRegister(state);
            CancelRegister_Button.Enabled = !state;
        }

        public void EnableEnterGameButton(bool state)
        {
            Enter_Game_Button.Enabled = state;
        }

        public void EnableControlsJoin(bool state)
        {
            EnableTextBoxAndRegister(state);
            EnableEnterGameButton(state);
            Cancel_Game_Button.Enabled = !state;
        }

        public void EnableControlsInGame(bool state)
        {
            EnableTextBoxAndRegister(state);
            EnableEnterGameButton(state);
            Cancel_Game_Button.Enabled = !state;
        }


        public void ShowErrorMessage(string errorMsg)
        {
            MessageBox.Show(errorMsg);
        }

        public int GetDesiredTime()
        {
            int time = -1;

            time = Convert.ToInt32(TimeLimit_Textbox.Text);


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

        public void SetUpBoard(string boardContents)
        {
            List<string> board = new List<string>();
            foreach (char c in boardContents)
            {
                if (c == 'Q')
                {
                    board.Add(c + "U");
                }
                else
                {
                    board.Add(c.ToString());
                }
            }
            Field0.Text = board[0];
            Field1.Text = board[1];
            Field2.Text = board[2];
            Field3.Text = board[3];
            Field4.Text = board[4];
            Field5.Text = board[5];
            Field6.Text = board[6];
            Field7.Text = board[7];
            Field8.Text = board[8];
            Field9.Text = board[9];
            Field10.Text = board[10];
            Field11.Text = board[11];
            Field12.Text = board[12];
            Field13.Text = board[13];
            Field14.Text = board[14];
            Field15.Text = board[15];
            UpdateProperty_Timer.Enabled = true;
        }

        private void Help_Button_Clicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetHelp();
        }
    }
}
