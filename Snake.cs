using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CampoSnake
{
    class Snake
    {
        private int dx;
        private int dy;
        private int size;
        public List<SnakePart> body = new List<SnakePart>();

        public Snake(int size, int x, int y, int dx, int dy)
        {
            this.size = size;
            this.dx = dx;
            this.dy = dy;

            for (int i = 0; i < size; i++)
                body.Insert(0, new SnakePart(x + i, y));
        }

        public void MoveUp()
        {
            if (dy == 0)
            {
                this.dx = 0;
                this.dy = -1;
            }
        }

        public void MoveDown()
        {
            if (dy == 0)
            {
                this.dx = 0;
                this.dy = 1;
            }
        }

        public void MoveRight()
        {
            if (dx == 0)
            {
                this.dx = 1;
                this.dy = 0;
            }
        }

        public void MoveLeft()
        {
            if (dx == 0)
            {
                this.dx = -1;
                this.dy = 0;
            }
        }

        public void Move()
        {
            for (int i = body.Count - 1; i >= 0; i--)
                if (i != 0)
                {
                    body[i].x = body[i - 1].x;
                    body[i].y = body[i - 1].y;
                }
                else
                {
                    body[i].x += this.dx;
                    body[i].y += this.dy;
                }
        }

        public void Eat(string type)
        {
            // snake grows when eating an apple
            if (type == "apple")
                body.Insert(0, new SnakePart(body[0].x + dx, body[0].y + dy));
            // snake shrinks when eating an orange
            else if (type == "orange")
                body.RemoveAt(body.Count - 1);
        }

        public void Draw(Graphics dc, int scaleX, int scaleY)
        {
            for (int i = 0; i < body.Count; i++)
            {
                Rectangle rect = new Rectangle(body[i].x * scaleX, body[i].y * scaleY, scaleX, scaleY);
                dc.FillRectangle(new SolidBrush(Color.Black), rect);
            }
        }
    }

    class SnakePart
    {
        public int x;
        public int y;

        public SnakePart(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
