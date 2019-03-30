namespace PS9
{
    partial class BoggleClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Word_Textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Enter_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Remaining_Time_Label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Player_CurrentScore_Label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Opponent_CurrentScore_Label = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Time_Limit_Label = new System.Windows.Forms.Label();
            this.ScoreBoard_Textbox = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ServerName_Textbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Username_Textbox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Enter_Game_Button = new System.Windows.Forms.Button();
            this.Cancel_Game_Button = new System.Windows.Forms.Button();
            this.Field0 = new System.Windows.Forms.Button();
            this.Field1 = new System.Windows.Forms.Button();
            this.Field2 = new System.Windows.Forms.Button();
            this.Field3 = new System.Windows.Forms.Button();
            this.Field7 = new System.Windows.Forms.Button();
            this.Field6 = new System.Windows.Forms.Button();
            this.Field5 = new System.Windows.Forms.Button();
            this.Field4 = new System.Windows.Forms.Button();
            this.Field11 = new System.Windows.Forms.Button();
            this.Field10 = new System.Windows.Forms.Button();
            this.Field9 = new System.Windows.Forms.Button();
            this.Field8 = new System.Windows.Forms.Button();
            this.Field15 = new System.Windows.Forms.Button();
            this.Field14 = new System.Windows.Forms.Button();
            this.Field13 = new System.Windows.Forms.Button();
            this.Field12 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.TimeLimit_Textbox = new System.Windows.Forms.TextBox();
            this.RegisterUser_Button = new System.Windows.Forms.Button();
            this.CancelRegister_Button = new System.Windows.Forms.Button();
            this.UpdateProperty_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Word_Textbox
            // 
            this.Word_Textbox.Location = new System.Drawing.Point(59, 397);
            this.Word_Textbox.Margin = new System.Windows.Forms.Padding(2);
            this.Word_Textbox.Name = "Word_Textbox";
            this.Word_Textbox.Size = new System.Drawing.Size(431, 22);
            this.Word_Textbox.TabIndex = 0;
            this.Word_Textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Word_Textbox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 397);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Word:";
            // 
            // Enter_Button
            // 
            this.Enter_Button.Location = new System.Drawing.Point(535, 397);
            this.Enter_Button.Margin = new System.Windows.Forms.Padding(2);
            this.Enter_Button.Name = "Enter_Button";
            this.Enter_Button.Size = new System.Drawing.Size(77, 25);
            this.Enter_Button.TabIndex = 2;
            this.Enter_Button.Text = "Enter";
            this.Enter_Button.UseVisualStyleBackColor = true;
            this.Enter_Button.Click += new System.EventHandler(this.Enter_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(461, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Remaining Time:";
            // 
            // Remaining_Time_Label
            // 
            this.Remaining_Time_Label.AutoSize = true;
            this.Remaining_Time_Label.Location = new System.Drawing.Point(623, 85);
            this.Remaining_Time_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Remaining_Time_Label.Name = "Remaining_Time_Label";
            this.Remaining_Time_Label.Size = new System.Drawing.Size(16, 17);
            this.Remaining_Time_Label.TabIndex = 20;
            this.Remaining_Time_Label.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(441, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Your Current Score:";
            // 
            // Player_CurrentScore_Label
            // 
            this.Player_CurrentScore_Label.AutoSize = true;
            this.Player_CurrentScore_Label.Location = new System.Drawing.Point(623, 102);
            this.Player_CurrentScore_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Player_CurrentScore_Label.Name = "Player_CurrentScore_Label";
            this.Player_CurrentScore_Label.Size = new System.Drawing.Size(16, 17);
            this.Player_CurrentScore_Label.TabIndex = 22;
            this.Player_CurrentScore_Label.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(415, 123);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Opponent\'s Current Score:";
            // 
            // Opponent_CurrentScore_Label
            // 
            this.Opponent_CurrentScore_Label.AutoSize = true;
            this.Opponent_CurrentScore_Label.Location = new System.Drawing.Point(623, 123);
            this.Opponent_CurrentScore_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Opponent_CurrentScore_Label.Name = "Opponent_CurrentScore_Label";
            this.Opponent_CurrentScore_Label.Size = new System.Drawing.Size(16, 17);
            this.Opponent_CurrentScore_Label.TabIndex = 24;
            this.Opponent_CurrentScore_Label.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(490, 70);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "Time Limit:";
            // 
            // Time_Limit_Label
            // 
            this.Time_Limit_Label.AutoSize = true;
            this.Time_Limit_Label.Location = new System.Drawing.Point(623, 70);
            this.Time_Limit_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Time_Limit_Label.Name = "Time_Limit_Label";
            this.Time_Limit_Label.Size = new System.Drawing.Size(16, 17);
            this.Time_Limit_Label.TabIndex = 26;
            this.Time_Limit_Label.Text = "0";
            // 
            // ScoreBoard_Textbox
            // 
            this.ScoreBoard_Textbox.Location = new System.Drawing.Point(418, 186);
            this.ScoreBoard_Textbox.Margin = new System.Windows.Forms.Padding(2);
            this.ScoreBoard_Textbox.Name = "ScoreBoard_Textbox";
            this.ScoreBoard_Textbox.ReadOnly = true;
            this.ScoreBoard_Textbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ScoreBoard_Textbox.Size = new System.Drawing.Size(215, 164);
            this.ScoreBoard_Textbox.TabIndex = 27;
            this.ScoreBoard_Textbox.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(482, 155);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "Current Words:";
            // 
            // ServerName_Textbox
            // 
            this.ServerName_Textbox.Location = new System.Drawing.Point(147, 39);
            this.ServerName_Textbox.Margin = new System.Windows.Forms.Padding(4);
            this.ServerName_Textbox.Name = "ServerName_Textbox";
            this.ServerName_Textbox.Size = new System.Drawing.Size(215, 22);
            this.ServerName_Textbox.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 42);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 17);
            this.label11.TabIndex = 30;
            this.label11.Text = "Client Server:";
            // 
            // Username_Textbox
            // 
            this.Username_Textbox.Location = new System.Drawing.Point(147, 10);
            this.Username_Textbox.Margin = new System.Windows.Forms.Padding(4);
            this.Username_Textbox.Name = "Username_Textbox";
            this.Username_Textbox.Size = new System.Drawing.Size(215, 22);
            this.Username_Textbox.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(59, 14);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 17);
            this.label12.TabIndex = 32;
            this.label12.Text = "Username:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(56, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(151, 17);
            this.label13.TabIndex = 35;
            this.label13.Text = "Currently Playing With:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(204, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 17);
            this.label14.TabIndex = 36;
            this.label14.Text = " ...";
            // 
            // Enter_Game_Button
            // 
            this.Enter_Game_Button.Location = new System.Drawing.Point(392, 32);
            this.Enter_Game_Button.Margin = new System.Windows.Forms.Padding(2);
            this.Enter_Game_Button.Name = "Enter_Game_Button";
            this.Enter_Game_Button.Size = new System.Drawing.Size(121, 25);
            this.Enter_Game_Button.TabIndex = 38;
            this.Enter_Game_Button.Text = "Enter Game";
            this.Enter_Game_Button.UseVisualStyleBackColor = true;
            this.Enter_Game_Button.Click += new System.EventHandler(this.Enter_Game_Button_Click);
            // 
            // Cancel_Game_Button
            // 
            this.Cancel_Game_Button.Location = new System.Drawing.Point(518, 31);
            this.Cancel_Game_Button.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel_Game_Button.Name = "Cancel_Game_Button";
            this.Cancel_Game_Button.Size = new System.Drawing.Size(121, 25);
            this.Cancel_Game_Button.TabIndex = 39;
            this.Cancel_Game_Button.Text = "Cancel Game";
            this.Cancel_Game_Button.UseVisualStyleBackColor = true;
            this.Cancel_Game_Button.Click += new System.EventHandler(this.Cancel_Game_Button_Click);
            // 
            // Field0
            // 
            this.Field0.BackColor = System.Drawing.SystemColors.Control;
            this.Field0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field0.Location = new System.Drawing.Point(59, 140);
            this.Field0.Margin = new System.Windows.Forms.Padding(2);
            this.Field0.Name = "Field0";
            this.Field0.Size = new System.Drawing.Size(51, 50);
            this.Field0.TabIndex = 40;
            this.Field0.Text = "A";
            this.Field0.UseVisualStyleBackColor = false;
            // 
            // Field1
            // 
            this.Field1.BackColor = System.Drawing.SystemColors.Control;
            this.Field1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field1.Location = new System.Drawing.Point(101, 140);
            this.Field1.Margin = new System.Windows.Forms.Padding(2);
            this.Field1.Name = "Field1";
            this.Field1.Size = new System.Drawing.Size(51, 50);
            this.Field1.TabIndex = 41;
            this.Field1.Text = "-";
            this.Field1.UseVisualStyleBackColor = false;
            // 
            // Field2
            // 
            this.Field2.BackColor = System.Drawing.SystemColors.Control;
            this.Field2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field2.Location = new System.Drawing.Point(143, 140);
            this.Field2.Margin = new System.Windows.Forms.Padding(2);
            this.Field2.Name = "Field2";
            this.Field2.Size = new System.Drawing.Size(51, 50);
            this.Field2.TabIndex = 42;
            this.Field2.Text = "-";
            this.Field2.UseVisualStyleBackColor = false;
            // 
            // Field3
            // 
            this.Field3.BackColor = System.Drawing.SystemColors.Control;
            this.Field3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field3.Location = new System.Drawing.Point(185, 140);
            this.Field3.Margin = new System.Windows.Forms.Padding(2);
            this.Field3.Name = "Field3";
            this.Field3.Size = new System.Drawing.Size(51, 50);
            this.Field3.TabIndex = 43;
            this.Field3.Text = "-";
            this.Field3.UseVisualStyleBackColor = false;
            // 
            // Field7
            // 
            this.Field7.BackColor = System.Drawing.SystemColors.Control;
            this.Field7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field7.Location = new System.Drawing.Point(185, 186);
            this.Field7.Margin = new System.Windows.Forms.Padding(2);
            this.Field7.Name = "Field7";
            this.Field7.Size = new System.Drawing.Size(51, 50);
            this.Field7.TabIndex = 47;
            this.Field7.Text = "-";
            this.Field7.UseVisualStyleBackColor = false;
            // 
            // Field6
            // 
            this.Field6.BackColor = System.Drawing.SystemColors.Control;
            this.Field6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field6.Location = new System.Drawing.Point(143, 186);
            this.Field6.Margin = new System.Windows.Forms.Padding(2);
            this.Field6.Name = "Field6";
            this.Field6.Size = new System.Drawing.Size(51, 50);
            this.Field6.TabIndex = 46;
            this.Field6.Text = "-";
            this.Field6.UseVisualStyleBackColor = false;
            // 
            // Field5
            // 
            this.Field5.BackColor = System.Drawing.SystemColors.Control;
            this.Field5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field5.Location = new System.Drawing.Point(101, 186);
            this.Field5.Margin = new System.Windows.Forms.Padding(2);
            this.Field5.Name = "Field5";
            this.Field5.Size = new System.Drawing.Size(51, 50);
            this.Field5.TabIndex = 45;
            this.Field5.Text = "-";
            this.Field5.UseVisualStyleBackColor = false;
            // 
            // Field4
            // 
            this.Field4.BackColor = System.Drawing.SystemColors.Control;
            this.Field4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field4.Location = new System.Drawing.Point(59, 186);
            this.Field4.Margin = new System.Windows.Forms.Padding(2);
            this.Field4.Name = "Field4";
            this.Field4.Size = new System.Drawing.Size(51, 50);
            this.Field4.TabIndex = 44;
            this.Field4.Text = "-";
            this.Field4.UseVisualStyleBackColor = false;
            // 
            // Field11
            // 
            this.Field11.BackColor = System.Drawing.SystemColors.Control;
            this.Field11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field11.Location = new System.Drawing.Point(185, 231);
            this.Field11.Margin = new System.Windows.Forms.Padding(2);
            this.Field11.Name = "Field11";
            this.Field11.Size = new System.Drawing.Size(51, 50);
            this.Field11.TabIndex = 51;
            this.Field11.Text = "-";
            this.Field11.UseVisualStyleBackColor = false;
            // 
            // Field10
            // 
            this.Field10.BackColor = System.Drawing.SystemColors.Control;
            this.Field10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field10.Location = new System.Drawing.Point(143, 231);
            this.Field10.Margin = new System.Windows.Forms.Padding(2);
            this.Field10.Name = "Field10";
            this.Field10.Size = new System.Drawing.Size(51, 50);
            this.Field10.TabIndex = 50;
            this.Field10.Text = "-";
            this.Field10.UseVisualStyleBackColor = false;
            // 
            // Field9
            // 
            this.Field9.BackColor = System.Drawing.SystemColors.Control;
            this.Field9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field9.Location = new System.Drawing.Point(101, 231);
            this.Field9.Margin = new System.Windows.Forms.Padding(2);
            this.Field9.Name = "Field9";
            this.Field9.Size = new System.Drawing.Size(51, 50);
            this.Field9.TabIndex = 49;
            this.Field9.Text = "-";
            this.Field9.UseVisualStyleBackColor = false;
            // 
            // Field8
            // 
            this.Field8.BackColor = System.Drawing.SystemColors.Control;
            this.Field8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field8.Location = new System.Drawing.Point(59, 231);
            this.Field8.Margin = new System.Windows.Forms.Padding(2);
            this.Field8.Name = "Field8";
            this.Field8.Size = new System.Drawing.Size(51, 50);
            this.Field8.TabIndex = 48;
            this.Field8.Text = "-";
            this.Field8.UseVisualStyleBackColor = false;
            // 
            // Field15
            // 
            this.Field15.BackColor = System.Drawing.SystemColors.Control;
            this.Field15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field15.Location = new System.Drawing.Point(185, 277);
            this.Field15.Margin = new System.Windows.Forms.Padding(2);
            this.Field15.Name = "Field15";
            this.Field15.Size = new System.Drawing.Size(51, 50);
            this.Field15.TabIndex = 55;
            this.Field15.Text = "-";
            this.Field15.UseVisualStyleBackColor = false;
            // 
            // Field14
            // 
            this.Field14.BackColor = System.Drawing.SystemColors.Control;
            this.Field14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field14.Location = new System.Drawing.Point(143, 277);
            this.Field14.Margin = new System.Windows.Forms.Padding(2);
            this.Field14.Name = "Field14";
            this.Field14.Size = new System.Drawing.Size(51, 50);
            this.Field14.TabIndex = 54;
            this.Field14.Text = "-";
            this.Field14.UseVisualStyleBackColor = false;
            // 
            // Field13
            // 
            this.Field13.BackColor = System.Drawing.SystemColors.Control;
            this.Field13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field13.Location = new System.Drawing.Point(101, 277);
            this.Field13.Margin = new System.Windows.Forms.Padding(2);
            this.Field13.Name = "Field13";
            this.Field13.Size = new System.Drawing.Size(51, 50);
            this.Field13.TabIndex = 53;
            this.Field13.Text = "-";
            this.Field13.UseVisualStyleBackColor = false;
            // 
            // Field12
            // 
            this.Field12.BackColor = System.Drawing.SystemColors.Control;
            this.Field12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Field12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Field12.Location = new System.Drawing.Point(59, 277);
            this.Field12.Margin = new System.Windows.Forms.Padding(2);
            this.Field12.Name = "Field12";
            this.Field12.Size = new System.Drawing.Size(51, 50);
            this.Field12.TabIndex = 52;
            this.Field12.Text = "-";
            this.Field12.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 75);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(129, 17);
            this.label15.TabIndex = 56;
            this.label15.Text = "Desired TIme Limit:";
            // 
            // TimeLimit_Textbox
            // 
            this.TimeLimit_Textbox.Location = new System.Drawing.Point(147, 71);
            this.TimeLimit_Textbox.Margin = new System.Windows.Forms.Padding(4);
            this.TimeLimit_Textbox.Name = "TimeLimit_Textbox";
            this.TimeLimit_Textbox.Size = new System.Drawing.Size(215, 22);
            this.TimeLimit_Textbox.TabIndex = 57;
            // 
            // RegisterUser_Button
            // 
            this.RegisterUser_Button.Location = new System.Drawing.Point(393, 7);
            this.RegisterUser_Button.Margin = new System.Windows.Forms.Padding(2);
            this.RegisterUser_Button.Name = "RegisterUser_Button";
            this.RegisterUser_Button.Size = new System.Drawing.Size(121, 25);
            this.RegisterUser_Button.TabIndex = 58;
            this.RegisterUser_Button.Text = "Register User";
            this.RegisterUser_Button.UseVisualStyleBackColor = true;
            this.RegisterUser_Button.Click += new System.EventHandler(this.RegisterUser_Button_Click);
            // 
            // CancelRegister_Button
            // 
            this.CancelRegister_Button.Location = new System.Drawing.Point(518, 9);
            this.CancelRegister_Button.Margin = new System.Windows.Forms.Padding(2);
            this.CancelRegister_Button.Name = "CancelRegister_Button";
            this.CancelRegister_Button.Size = new System.Drawing.Size(121, 25);
            this.CancelRegister_Button.TabIndex = 59;
            this.CancelRegister_Button.Text = "Cancel Register";
            this.CancelRegister_Button.UseVisualStyleBackColor = true;
            this.CancelRegister_Button.Click += new System.EventHandler(this.CancelRegister_Button_Click);
            // 
            // UpdateProperty_Timer
            // 
            this.UpdateProperty_Timer.Interval = 5000;
            // 
            // BoggleClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 445);
            this.Controls.Add(this.CancelRegister_Button);
            this.Controls.Add(this.RegisterUser_Button);
            this.Controls.Add(this.TimeLimit_Textbox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Field15);
            this.Controls.Add(this.Field14);
            this.Controls.Add(this.Field13);
            this.Controls.Add(this.Field12);
            this.Controls.Add(this.Field11);
            this.Controls.Add(this.Field10);
            this.Controls.Add(this.Field9);
            this.Controls.Add(this.Field8);
            this.Controls.Add(this.Field7);
            this.Controls.Add(this.Field6);
            this.Controls.Add(this.Field5);
            this.Controls.Add(this.Field4);
            this.Controls.Add(this.Field3);
            this.Controls.Add(this.Field2);
            this.Controls.Add(this.Field1);
            this.Controls.Add(this.Field0);
            this.Controls.Add(this.Cancel_Game_Button);
            this.Controls.Add(this.Enter_Game_Button);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Username_Textbox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ServerName_Textbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ScoreBoard_Textbox);
            this.Controls.Add(this.Time_Limit_Label);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Opponent_CurrentScore_Label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Player_CurrentScore_Label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Remaining_Time_Label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Enter_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Word_Textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoggleClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boggle Game Client (NOT A VIRUS)";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Help_Button_Clicked);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Word_Textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Enter_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Remaining_Time_Label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Player_CurrentScore_Label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Opponent_CurrentScore_Label;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Time_Limit_Label;
        private System.Windows.Forms.RichTextBox ScoreBoard_Textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ServerName_Textbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Username_Textbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button Enter_Game_Button;
        private System.Windows.Forms.Button Cancel_Game_Button;
        private System.Windows.Forms.Button Field0;
        private System.Windows.Forms.Button Field1;
        private System.Windows.Forms.Button Field2;
        private System.Windows.Forms.Button Field3;
        private System.Windows.Forms.Button Field7;
        private System.Windows.Forms.Button Field6;
        private System.Windows.Forms.Button Field5;
        private System.Windows.Forms.Button Field4;
        private System.Windows.Forms.Button Field11;
        private System.Windows.Forms.Button Field10;
        private System.Windows.Forms.Button Field9;
        private System.Windows.Forms.Button Field8;
        private System.Windows.Forms.Button Field15;
        private System.Windows.Forms.Button Field14;
        private System.Windows.Forms.Button Field13;
        private System.Windows.Forms.Button Field12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TimeLimit_Textbox;
        private System.Windows.Forms.Button RegisterUser_Button;
        private System.Windows.Forms.Button CancelRegister_Button;
        private System.Windows.Forms.Timer UpdateProperty_Timer;
    }
}

