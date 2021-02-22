namespace BetterSudoku
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.manualEntry = new System.Windows.Forms.Button();
            this.sloveBtn = new System.Windows.Forms.Button();
            this.loadGame = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveGame = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnHistory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(29, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 445);
            this.panel1.TabIndex = 0;
            // 
            // manualEntry
            // 
            this.manualEntry.Location = new System.Drawing.Point(592, 48);
            this.manualEntry.Name = "manualEntry";
            this.manualEntry.Size = new System.Drawing.Size(170, 39);
            this.manualEntry.TabIndex = 1;
            this.manualEntry.Text = "Ruční zadání";
            this.manualEntry.UseVisualStyleBackColor = true;
            this.manualEntry.Click += new System.EventHandler(this.ManualEntry_Click);
            // 
            // sloveBtn
            // 
            this.sloveBtn.Location = new System.Drawing.Point(592, 422);
            this.sloveBtn.Name = "sloveBtn";
            this.sloveBtn.Size = new System.Drawing.Size(170, 44);
            this.sloveBtn.TabIndex = 2;
            this.sloveBtn.Text = "Vyřešit";
            this.sloveBtn.UseVisualStyleBackColor = true;
            this.sloveBtn.Click += new System.EventHandler(this.SolveBtn_Click);
            // 
            // loadGame
            // 
            this.loadGame.Location = new System.Drawing.Point(592, 114);
            this.loadGame.Name = "loadGame";
            this.loadGame.Size = new System.Drawing.Size(169, 39);
            this.loadGame.TabIndex = 3;
            this.loadGame.Text = "Načíst hru";
            this.loadGame.UseVisualStyleBackColor = true;
            this.loadGame.Click += new System.EventHandler(this.LoadGame_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveGame
            // 
            this.saveGame.Location = new System.Drawing.Point(592, 181);
            this.saveGame.Name = "saveGame";
            this.saveGame.Size = new System.Drawing.Size(169, 40);
            this.saveGame.TabIndex = 4;
            this.saveGame.Text = "Uložit";
            this.saveGame.UseVisualStyleBackColor = true;
            this.saveGame.Click += new System.EventHandler(this.SaveGame_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(592, 248);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(170, 40);
            this.btnHistory.TabIndex = 5;
            this.btnHistory.Text = "Historie her";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 544);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.saveGame);
            this.Controls.Add(this.loadGame);
            this.Controls.Add(this.sloveBtn);
            this.Controls.Add(this.manualEntry);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button manualEntry;
        private System.Windows.Forms.Button sloveBtn;
        private System.Windows.Forms.Button loadGame;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button saveGame;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnHistory;
    }
}

