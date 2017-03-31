using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PongGame
{
    public class Paddle
    {
        public Rectangle paddle;
        public SolidBrush paddleColor;
        public int xPaddle = 10;
        public int yPaddle = 60;

        // Constructor for a new Paddle object
        public Paddle(int xPosition, int yPosition)
        {
            paddle = new Rectangle(xPosition, yPosition, xPaddle, yPaddle);
            paddleColor = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(240, 240, 240));
        }
    }
}
