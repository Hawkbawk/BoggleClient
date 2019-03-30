using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS9
{
    public partial class GameResults : Form
    {
        public GameResults()
        {
            InitializeComponent();
            
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void ChangeLabels(string P1_Name, string P2_Name, string P1_Score, string P2_Score, List<string> P1_Words, List<string> P2_Words)
        {
            label1.Text = P1_Name + "'s Results: ";
            label2.Text = P2_Name + "'s Results: ";
            label3.Text = "Final Score: " + P1_Score + "-" + P2_Score;

            foreach(string s in P1_Words)
            {
                richTextBox1.Text = s + "\r\n";
            }
            foreach (string s in P2_Words)
            {
                richTextBox2.Text = s + "\r\n";
            }
        }


    }
}
