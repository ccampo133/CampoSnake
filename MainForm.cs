using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CampoSnake
{
    public partial class MainForm : Form
    {
        #region member variables
        private SnakeGrid grid;
        private int nRow = 15;
        private int nCol = 20;
        private int width = 20;
        private int height = 20;
        private int snakeSize = 3;
        private int elapsed = 0;
        private int initFoodTimer = 3000;  // time in ms that the initial food timer starts at
        private int clearTime = 20000;  // time in ms that the food is cleared
        private int foodTimer;
        private DateTime start;
        private bool _gameOver;
        private bool _paused;

        private bool gameOver
        {
            get
            {
                return _gameOver;
            }
            set
            {
                lblGameOver.Visible = value;
                _gameOver = value;

                if (value)
                {
                    timerMain.Stop();
                    timerFood.Stop();
                }
                else
                    StartGame();
            }
        }

        private bool paused
        {
            get
            {
                return _paused;
            }
            set
            {
                _paused = value;
                lblPaused.Visible = value;

                if (value)
                {
                    timerMain.Stop();
                    timerFood.Stop();
                }
                else
                {
                    timerMain.Start();
                    timerFood.Start();
                }
            }
        }

        #endregion

        #region initialization
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            gameOver = false;
        }
        #endregion

        #region game control
        private void StartGame()
        {
            this.grid = new SnakeGrid(nRow, nCol, width, height, snakeSize);
            foodTimer = initFoodTimer;
            timerMain.Start();
            timerFood.Start();
            start = DateTime.Now;
            grid.GenerateFood();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameOver && !paused)
            {
                if (e.KeyCode == Keys.Up)
                    grid.Move(0, -1);
                if (e.KeyCode == Keys.Down)
                    grid.Move(0, 1);
                if (e.KeyCode == Keys.Right)
                    grid.Move(1, 0);
                if (e.KeyCode == Keys.Left)
                    grid.Move(-1, 0);
                if (e.KeyCode == Keys.P || e.KeyCode == Keys.Pause)
                    paused = true;
            }
            else if(gameOver)
            {
                if (e.KeyCode == Keys.Y)
                    gameOver = false;
                else if (e.KeyCode == Keys.N)
                    this.Close();
            }
            else if (paused)
            {
                if (e.KeyCode == Keys.Space)
                    paused = false;
            }
        }
        #endregion

        #region main loops - asynchronous timers
        private void timerMain_Tick(object sender, EventArgs e)
        {
            elapsed += timerMain.Interval;
            TimeSpan span = DateTime.Now.Subtract(start);
            lblElapsed.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
            if (elapsed % clearTime == 0)
                grid.ClearFood();
            if (grid.Move())
            {
                lblScore.Text = (100 * grid.score).ToString();
                foodTimer = initFoodTimer - grid.score * 5;
            }
            else
                gameOver = true;
            this.Invalidate();
        }

        private void timerFood_Tick(object sender, EventArgs e)
        {
            timerFood.Interval = initFoodTimer - grid.score * 5;
            grid.GenerateFood();
            this.Invalidate();
        }
        #endregion

        #region graphics
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            grid.Draw(dc);
            base.OnPaint(e);
        }
        #endregion
    }
}