using PS9.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PS9
{
    public partial class BoggleClient : Form, IBoggleService
    {
        public event Action CancelGame;

        public event Action CancelRegister;

        public event Action EnterGame;

        public event Action RegisterUser;

        public event Action<string> SubmitWord;

        public event Action UpdateProperties;

        /// <summary>
        /// Constructs a new BoggleClient that to be run, and enables and disables all controls as necessary.
        /// </summary>
        public BoggleClient()
        {
            InitializeComponent();
            ServerName_Textbox.Text = "http://ice.eng.utah.edu";
            RegisterUser_Button.Enabled = true;
            Cancel_Game_Button.Enabled = false;
            CancelRegister_Button.Enabled = false;
            Enter_Game_Button.Enabled = false;
            ScoreBoard_Textbox.ReadOnly = true;
            UpdateProperty_Timer.Tick += HandleUpdate;
        }

        /// <summary>
        /// Adds the passed in string to the Scoreboard_TextBox.
        /// </summary>
        /// <param name="word"></param>
        public void AddPlayedWord(string word)
        {
            ScoreBoard_Textbox.Text += word + Environment.NewLine;
        }

        // TODO: Look at this method and all other enable control methods and ensure they are doing what we actually want.
        public void EnableControlsInGame(bool state)
        {
            EnableTextBoxAndRegister(state);
            EnableEnterGameButton(!state);
            Cancel_Game_Button.Enabled = state;
            RegisterUser_Button.Enabled = !state;
        }

        // TODO: Look at this method and all other enable control methods and ensure they are doing what we actually want.

        public void EnableControlsJoin(bool state)
        {
            EnableTextBoxAndRegister(state);
            EnableEnterGameButton(state);
            Cancel_Game_Button.Enabled = !state;
        }

        // TODO: Look at this method and all other enable control methods and ensure they are doing what we actually want.

        public void EnableControlsRegister(bool state)
        {
            EnableTextBoxAndRegister(state);
            CancelRegister_Button.Enabled = !state;
        }

        // TODO: Look at this method and all other enable control methods and ensure they are doing what we actually want.

        public void EnableEnterGameButton(bool state)
        {
            Enter_Game_Button.Enabled = state;
        }

        // TODO: Look at this method and all other enable control methods and ensure they are doing what we actually want.

        public void EnableTextBoxAndRegister(bool state)
        {
            Username_Textbox.Enabled = state;
            ServerName_Textbox.Enabled = state;
            TimeLimit_Textbox.Enabled = state;
            RegisterUser_Button.Enabled = state;
        }

        // TODO: Look at this method and all other enable control methods and ensure they are doing what we actually want.

        public void EnableTimer(bool state)
        {
            UpdateProperty_Timer.Enabled = state;
        }

        /// <summary>
        /// Obtains the user's desired game time limit from the TimeLimit_Textbox, checks to see if
        /// it's valid, and then returns it.
        ///
        /// A valid time limit is an integer value "timeLimit" that satisfies the following conditions:
        ///
        /// timeLimit is greater than or equal to 5 AND timeLimit is less than or equal to 120.
        /// </summary>
        /// <returns>
        /// If the user has entered an invalid time limit, returns a value less than 0. Otherwise,
        /// returns the user's desired game time limit.
        /// </returns>
        public int GetDesiredTime()
        {
            int time = -1;
            try
            {
                time = Convert.ToInt32(TimeLimit_Textbox.Text);
            }
            catch (FormatException)
            {
                ShowMessage("Please only give an integer value for your " +
                    "desired time limit.");
            }

            if (time < 5 || time > 120)
            {
                ShowMessage("Please only select a time limit that is less " +
                    "than or equal to 120 seconds" + " and greater than or equal to 5 seconds");
            }
            return time;
        }

        /// <summary>
        /// Obtains the name of the server that the user would like to use for their Boggle game.
        /// </summary>
        /// <returns>
        /// Returns the name of the server that the user would like to use for their Boggle game.
        /// </returns>
        public string ObtainDesiredServer()
        {
            return ServerName_Textbox.Text;
        }

        /// <summary>
        /// Obtains the user's desired username.
        /// </summary>
        /// <returns></returns>
        public string ObtainUsername()
        {
            return Username_Textbox.Text;
        }

        public void Reset()
        {
            // Reset the board.
            Field0.Text = "";
            Field1.Text = "";
            Field2.Text = "";
            Field3.Text = "";
            Field4.Text = "";
            Field5.Text = "";
            Field6.Text = "";
            Field7.Text = "";
            Field8.Text = "";
            Field9.Text = "";
            Field10.Text = "";
            Field11.Text = "";
            Field12.Text = "";
            Field13.Text = "";
            Field14.Text = "";
            Field15.Text = "";

            // Enable and disable the appropriate controls.
            ServerName_Textbox.Text = "http://ice.eng.utah.edu";
            RegisterUser_Button.Enabled = true;
            Cancel_Game_Button.Enabled = false;
            CancelRegister_Button.Enabled = false;
            Enter_Game_Button.Enabled = true;
            ScoreBoard_Textbox.ReadOnly = true;
            SetOpponentScore("0");
            SetPlayerScore("0");
            ScoreBoard_Textbox.ResetText();
            SetOpponentNickname("...");
            SetRemainingTime("0");
            SetTimeLimit("0");
        }

        public void SetOpponentNickname(string nickname)
        {
            Opponent_Nickname.Text = nickname;
        }

        public void SetOpponentScore(string oppScore)
        {
            Opponent_CurrentScore_Label.Text = oppScore;
        }

        public void SetPlayerScore(string score)
        {
            Player_CurrentScore_Label.Text = score;
        }

        public void SetRemainingTime(string remainingTime)
        {
            Remaining_Time_Label.Text = remainingTime;
        }

        public void SetTimeLimit(string timeLimit)
        {
            Time_Limit_Label.Text = timeLimit;
            Remaining_Time_Label.Text = timeLimit;
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
        }

        public void ShowMessage(string errorMsg)
        {
            MessageBox.Show(errorMsg);
        }

        private void Cancel_Game_Button_Click(object sender, EventArgs e)
        {
            CancelGame();
        }

        private void CancelRegister_Button_Click(object sender, EventArgs e)
        {
            CancelRegister();
        }

        private void Enter_Button_Click(object sender, EventArgs e)
        {
            SubmitWord(Word_Textbox.Text);
            Word_Textbox.ResetText();
        }

        private void Enter_Game_Button_Click(object sender, EventArgs e)
        {
            EnterGame();
        }

        private void HandleUpdate(object sender, EventArgs e)
        {
            UpdateProperties();
        }

        private void Help_Button_Clicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowMessage("The Rules of Boggle can be found here: http://en.wikipedia.org/wiki/Boggle. " +
                "With this client, all you need to do is make a username of your choice, connect it to a server of your choice, and enter in your desired time limit. " +
                "Once you have all the fields filled in, click the register button, and then join when it is possible. Once you are in a game, all you need to do is type in words you" +
                "found from the interface into the textbox on the bottom and submit the word by clicking enter.");
        }

        private void RegisterUser_Button_Click(object sender, EventArgs e)
        {
            RegisterUser();
        }

        private void Word_Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Enter_Button_Click(sender, e);
            }
        }

        public void DisplayFinalScore(string P1_Name, string P2_Name, string P1_Score, string P2_Score, WordAndScore[] P1_Words, WordAndScore[] P2_Words)
        {
            GameResults gr = new GameResults();
            gr.ChangeLabels(P1_Name, P2_Name, P1_Score, P2_Score, P1_Words, P2_Words);
            gr.Show();
        }
    }
}