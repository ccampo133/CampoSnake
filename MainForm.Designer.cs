namespace CampoSnake
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.lblHeaderScore = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.timerFood = new System.Windows.Forms.Timer(this.components);
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblPaused = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // lblHeaderScore
            // 
            this.lblHeaderScore.AutoSize = true;
            this.lblHeaderScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderScore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeaderScore.Location = new System.Drawing.Point(12, 308);
            this.lblHeaderScore.Name = "lblHeaderScore";
            this.lblHeaderScore.Size = new System.Drawing.Size(44, 13);
            this.lblHeaderScore.TabIndex = 0;
            this.lblHeaderScore.Text = "Score:";
            this.lblHeaderScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblScore.Location = new System.Drawing.Point(62, 305);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(121, 19);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerFood
            // 
            this.timerFood.Interval = 3000;
            this.timerFood.Tick += new System.EventHandler(this.timerFood_Tick);
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.BackColor = System.Drawing.Color.Transparent;
            this.lblGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.Location = new System.Drawing.Point(118, 109);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(159, 87);
            this.lblGameOver.TabIndex = 2;
            this.lblGameOver.Text = "Game Over! \r\nAgain?\r\n(Y/N)";
            this.lblGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGameOver.Visible = false;
            // 
            // lblElapsed
            // 
            this.lblElapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblElapsed.Location = new System.Drawing.Point(290, 311);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(100, 13);
            this.lblElapsed.TabIndex = 3;
            this.lblElapsed.Text = "lblElapsed";
            this.lblElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaused
            // 
            this.lblPaused.AutoSize = true;
            this.lblPaused.BackColor = System.Drawing.Color.Transparent;
            this.lblPaused.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaused.Location = new System.Drawing.Point(40, 109);
            this.lblPaused.Name = "lblPaused";
            this.lblPaused.Size = new System.Drawing.Size(328, 58);
            this.lblPaused.TabIndex = 4;
            this.lblPaused.Text = "Game PAUSED.\r\nPress SPACE to continue...";
            this.lblPaused.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPaused.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(402, 330);
            this.Controls.Add(this.lblPaused);
            this.Controls.Add(this.lblElapsed);
            this.Controls.Add(this.lblGameOver);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblHeaderScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CampoSnake";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Label lblHeaderScore;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Timer timerFood;
        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label lblPaused;

    }
}

