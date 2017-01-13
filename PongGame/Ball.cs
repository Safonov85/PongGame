using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame
{
    public class Ball
    {
        SolidBrush ballBrush = new SolidBrush(Color.WhiteSmoke);

        public float x = 0.0F;
        public float y = 0.0F;
        float width = 10.0F;
        float height = 10.0F;
        

        public void CreateBall(Graphics graphics)
        {
            graphics.FillEllipse(ballBrush, x, y, width, height);
        }
    }
}
