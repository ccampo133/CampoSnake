using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CampoSnake
{
    class GameGrid
    {
        private int width;
        private int height;
        private int scaleX;
        private int scaleY;
        private string[,] grid;
        private Snake snake;

        // probability of foods; the sum of these should equal 100
        private int orangeChance = 10;
        private int appleChance = 90;

        public GameGrid(int width, int height, int scaleX, int scaleY)
        {
            this.width = width;
            this.height = height;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            grid = new string[width, height];
        }

        public Snake AddSnake(int size)
        {
            if (size < this.width)
                this.snake = new Snake(size, size, 0, 1, 0);
            else throw new Exception("Snake size must be less than the grid width.");
            return this.snake;
        }

        public void GenerateFood()
        {
            Random rand = new Random();
            int[] weights = new int[] { appleChance, orangeChance };
            string[] foods = new string[] { "apple", "orange" };

            // randomly select either apple or orange
            int weightSum = 0;
            for (int i = 0; i < weights.Length; i++)
                weightSum += weights[i];
            int rnd = rand.Next(weightSum);
            string type = "";
            for (int i = 0; i < weights.Length; i++)
            {
                if (rnd < weights[i])
                {
                    type = foods[i];
                    break;
                }
                rnd -= weights[i];
            }

            // place it randomly on the board, but not on the snake
            bool done = false;
            int x = 0;
            int y = 0;
            while (!done)
            {
                x = rand.Next(0, width);
                y = rand.Next(0, height);
                for (int i = 0; i < snake.body.Count; i++)
                    if (snake.body[i].x != x && snake.body[i].y != y)
                    {
                        done = true;
                        break;
                    }
            }
            grid[y, x] = type;
        }

        public bool IsCollision()
        {
            // handle edges
            for (int i = 0; i < snake.body.Count; i++)
            {
                if (snake.body[i].x >= this.width)
                    snake.body[i].x = 0;
                else if (snake.body[i].x < 0)
                    snake.body[i].x = this.width - 1;

                if (snake.body[i].y >= this.height)
                    snake.body[i].y = 0;
                else if (snake.body[i].y < 0)
                    snake.body[i].y = this.height - 1;
            }

            // check if the snake hits its tail
            int x0 = snake.body[0].x;
            int y0 = snake.body[0].y;
            for (int i = 1; i < snake.body.Count; i++)
                if (x0 == snake.body[i].x && y0 == snake.body[i].y)
                    return true;

            // check for food
            do
            {
                snake.Eat(grid[y0, x0]);
                grid[y0, x0] = null;
                x0 = snake.body[0].x;
                y0 = snake.body[0].y;
            } while (grid[y0, x0] != null) ;
            return false;
        }

        #region graphics
        public void Draw(Graphics dc)
        {
            snake.Draw(dc, scaleX, scaleY);

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    Rectangle rect = new Rectangle(i * scaleX, j * scaleY, scaleX, scaleY);
                    dc.DrawRectangle(new Pen(Color.Black), rect);
                    if (grid[j, i] == "apple")
                        dc.FillRectangle(new SolidBrush(Color.Red), rect);
                    else if(grid[j, i] == "orange")
                        dc.FillRectangle(new SolidBrush(Color.Orange), rect);
                }
        }
        #endregion
    }
}
