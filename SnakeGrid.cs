using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CampoSnake
{
    class SnakeGrid
    {
        #region member variables
        private int nCol;
        private int nRow;
        private int width;
        private int height;
        private int snakeSize;
        private int x;
        private int y;
        private int[,] grid;
        private int dx = 1;
        private int dy = 0;
        private int shrinkSize = 3;
        private int minSize = 3;
        private int growSize = 1;
        private string orientation = "right";
        private bool isChomp = false;
        private Dictionary<string, int> foodWeights = new Dictionary<string, int>();
        private Dictionary<string, int> foodNums = new Dictionary<string, int>();
        private Dictionary<int, string> foodNames = new Dictionary<int, string>();
        private Bitmap body = Properties.Resources.snake;
        private Bitmap head = Properties.Resources.snakeHead_right;
        private Bitmap apple = Properties.Resources.apple;
        private Bitmap orange = Properties.Resources.orange;

        public int score = 0;
        #endregion

        #region initialization
        public SnakeGrid(int nRow, int nCol, int width, int height, int snakeSize)
        {
            this.nRow = nRow;
            this.nCol = nCol;
            this.width = width;
            this.height = height;
            this.snakeSize = snakeSize;
            this.grid = new int[nRow, nCol];
            CreateSnake(this.snakeSize);
            DefineFoods();
        }

        // place a snake of size `size` on the grid, with the tail starting at 0, 0
        public void CreateSnake(int size)
        {
            for (int i = size; i > 0; i--)
                this.grid[0, i - 1] = i;

            // keep track of the snake's head
            this.x = size - 1;
            this.y = 0;
        }

        // define food types and their chance to spawn here
        public void DefineFoods()
        {   
            //format: name, weight (weights should add to 100)
            foodWeights.Add("apple", 96);
            foodWeights.Add("orange", 4);

            // map foods to some specific negative number so we can add them to the grid
            foodNums.Add("apple", -1);
            foodNums.Add("orange", -2);

            // create inverse dictionary to foodNums
            foreach (string key in foodNums.Keys)
                foodNames.Add(foodNums[key], key);
        }
        #endregion

        #region movement
        // should only be called on key presses
        public void Move(int dx, int dy)
        {
            if (dx < 0 && orientation == "right")
                return;
            else if (dx > 0 && orientation == "left")
                return;
            else if (dy > 0 && orientation == "up")
                return;
            else if (dy < 0 && orientation == "down")
                return;

            this.dx = dx;
            this.dy = dy;
            RotateHead();
        }

        // should only be called in main program loop (timer)
        // returns true if the move was successful (no collision), false otherwise
        public bool Move()
        {
            // reset head if it is in the chomp state
            if (isChomp)
            {
                RotateHead();
                isChomp = false;
            }

            this.x += dx;
            this.y += dy;

            // wrap around if an edge is hit
            if (this.x >= this.nCol)
                this.x = 0;
            else if (this.x < 0)
                this.x = this.nCol - 1;
            if (this.y >= this.nRow)
                this.y = 0;
            else if (this.y < 0)
                this.y = this.nRow - 1;

            if (!IsCollision())
                UpdateGrid();
            else 
                return false;
            return true;
        }

        // check if the head hits the tail, or a piece of food
        private bool IsCollision()
        {
            if (grid[this.y, this.x] > 0)
                return true;
            else if (grid[this.y, this.x] < 0)
                while(grid[this.y, this.x] < 0)
                    Eat();
            return false;
        }

        private void UpdateGrid()
        {
            // subtract 1 from the entire grid to move the snake
            for (int i = 0; i < nRow; i++)
                for (int j = 0; j < nCol; j++)
                    if (grid[i, j] > 0)
                        grid[i, j] -= 1;

            // move the head
            grid[y, x] = snakeSize;
        }
        #endregion

        #region food mechanics and logic
        private void Eat()
        {
            // logic for different food types
            int dSize = 0;
            string type = foodNames[grid[y, x]];
            if (type == "apple") // increase length
            {
                grid[y, x] = snakeSize;
                snakeSize += growSize;
                this.x += growSize * dx;
                this.y += growSize * dy;

                // check edges
                if (this.x >= this.nCol)
                    this.x = 0;
                else if (this.x < 0)
                    this.x = this.nCol - 1;

                if (this.y >= this.nRow)
                    this.y = 0;
                else if (this.y < 0)
                    this.y = this.nRow - 1;

                grid[y, x] = snakeSize;
                dSize = growSize;
            }
            else if (type == "orange")
            {
                int amtToShrink;
                if (snakeSize > shrinkSize + minSize)
                    amtToShrink = shrinkSize;
                else amtToShrink = snakeSize - minSize;
                
                // shrink snake
                snakeSize -= amtToShrink;
                for (int i = 0; i < nRow; i++)
                    for (int j = 0; j < nCol; j++)
                        if (grid[i, j] > amtToShrink)
                            grid[i, j] -= amtToShrink;
                        else if (grid[i, j] > 0)
                            grid[i, j] = 0;
                grid[y, x] = snakeSize;
                dSize = shrinkSize;
            }

            // update head
            if (orientation == "up")
                head = Properties.Resources.snakeHead_chomp_up;
            else if (orientation == "down")
                head = Properties.Resources.snakeHead_chomp_down;
            else if (orientation == "left")
                head = Properties.Resources.snakeHead_chomp_left;
            else if (orientation == "right")
                head = Properties.Resources.snakeHead_chomp_right;
            isChomp = true;

            // update the score
            score += dSize;
        }

        public void GenerateFood()
        {
            Random rand = new Random();

            // randomly select either apple or orange
            int weights = 0;
            foreach (string food in foodWeights.Keys)
                weights += foodWeights[food];

            int rnd = rand.Next(weights);
            string type = "";
            foreach(string food in foodWeights.Keys)
            {
                if (rnd < foodWeights[food])
                {
                    type = food;
                    break;
                }
                rnd -= foodWeights[food];
            }

            // add to grid, but not on snake or another piece of food
            int randX = rand.Next(nCol);
            int randY = rand.Next(nRow);
            while (grid[randY, randX] != 0)
            {
                randX = rand.Next(nCol);
                randY = rand.Next(nRow);
            }
            grid[randY, randX] = foodNums[type];
        }

        public void ClearFood()
        {
            for (int i = 0; i < nRow; i++)
                for (int j = 0; j < nCol; j++)
                    if (grid[i, j] < 0)
                        grid[i, j] = 0;
        }
        #endregion

        #region graphics
        public void Draw(Graphics dc)
        {
            for (int i = 0; i < nRow; i++)
                for (int j = 0; j < nCol; j++)
                {
                    Rectangle rect = new Rectangle(j * width, i * height, width, height);
                    if (grid[i, j] > 0 && grid[i, j] == snakeSize)
                        dc.DrawImage(head, rect);
                    else if (grid[i, j] > 0)
                        dc.DrawImage(body, rect);
                    else if (grid[i, j] < 0 && foodNames[grid[i, j]] == "apple")
                        dc.DrawImage(apple, rect);
                    else if (grid[i, j] < 0 && foodNames[grid[i, j]] == "orange")
                        dc.DrawImage(orange, rect);
                }
            dc.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, nCol*width, nRow*height));
        }

        public void RotateHead()
        {
            if (dy < 0)
            {
                orientation = "up";
                head = Properties.Resources.snakeHead_up;
            }
            else if (dy > 0)
            {
                orientation = "down";
                head = Properties.Resources.snakeHead_down;
            }
            else if (dx < 0)
            {
                orientation = "left";
                head = Properties.Resources.snakeHead_left;
            }
            else if (dx > 0)
            {
                orientation = "right";
                head = Properties.Resources.snakeHead_right;
            }
        }
        #endregion
    }
}
