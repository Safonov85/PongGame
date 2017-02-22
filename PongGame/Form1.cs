using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongGame
{
    public partial class Form1 : Form
    {
        enum Direction
        {
            RightDown,
            RightUp,
            LeftDown,
            LeftUp
        };

        Bitmap bitmap;
        SolidBrush brush;
        SolidBrush mapBrush;
        Color backgroundColor = Color.FromArgb(170, 170, 170);
        int xPosition, yPosition, xPositionP2, yPositionP2;
        Graphics graphics;
        Graphics graphicsOverride;
        float speedBall;
        int speedP2;
        bool up, down;
        int scorePlayer, scoreComputer;

        Paddle paddle;
        Paddle paddle2;
        Ball ball;
        Direction direction = Direction.LeftDown;

        private void timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(backgroundColor);
            //MoveP2();
            ComputerMove();
            paddle = new Paddle(xPosition, yPosition);
            paddle2 = new Paddle(xPositionP2, yPositionP2);
            if(ball.x < 30)
            {
                if (PaddleP1Collision())
                {
                    if(direction == Direction.LeftDown)
                    {
                        direction = Direction.RightDown;
                    }
                    else
                    {
                        direction = Direction.RightUp;
                    }
                }
                else
                {
                    scoreComputer += 1;
                    PrintScore();
                    BallMiddlePosition();
                }
            }
            if(ball.x > 560)
            {
                if (PaddleP2Collission())
                {
                    if(direction == Direction.RightDown)
                    {
                        direction = Direction.LeftDown;
                    }
                    else
                    {
                        direction = Direction.LeftUp;
                    }
                }
                else
                {
                    scorePlayer += 1;
                    PrintScore();
                    BallMiddlePosition();
                }
            }

            BounceWall();
            ball.CreateBall(graphics);

            graphics.FillRectangle(paddle.paddleColor, paddle.paddle);
            graphics.FillRectangle(paddle2.paddleColor, paddle2.paddle);

            graphicsOverride.DrawImage(bitmap, 0, 0, this.Width, this.Height);
        }

        void PrintScore()
        {
            this.Text = "Player: " + scorePlayer + "  Computer: " + scoreComputer;
        }

        bool PaddleP1Collision()
        {
            if(yPosition < ball.y && ball.y < yPosition + paddle.yPaddle)
            {
                return true;
            }

            return false;
        }

        bool PaddleP2Collission()
        {
            if(yPositionP2 < ball.y && ball.y < yPositionP2 + paddle2.yPaddle)
            {
                return true;
            }

            return false;
        }

        void DetectPaddleCollision()
        {

        }

        void BounceWall()
        {
            if(direction == Direction.RightDown)
            {
                ball.x += speedBall;
                ball.y += speedBall;
                if(ball.y > 380)
                {
                    direction = Direction.RightUp;
                }
            }
            else if(direction == Direction.RightUp)
            {
                ball.x += speedBall;
                ball.y -= speedBall;
                if(ball.y < 0)
                {
                    direction = Direction.RightDown;
                }
            }
            else if(direction == Direction.LeftUp)
            {
                ball.x -= speedBall;
                ball.y -= speedBall;
                if(ball.y < 0)
                {
                    direction = Direction.LeftDown;
                }
            }
            else if(direction == Direction.LeftDown)
            {
                ball.x -= speedBall;
                ball.y += speedBall;
                if (ball.y > 380)
                {
                    direction = Direction.LeftUp;
                }
            }
        }

        void ComputerMove()
        {
            if(direction == Direction.RightDown || direction == Direction.RightUp)
            {
                float currentPlace = (ball.y - 30f) * 0.9f;
                yPositionP2 = (int)currentPlace;
            }
        }

        void MoveP2()
        {
            if(down == true)
            {
                if (yPositionP2 > 330)
                {
                    return;
                }
                yPositionP2 += speedP2;
            }
            if(up == true)
            {
                yPositionP2 -= speedP2;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                down = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                up = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                down = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                up = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Hide();
            yPosition = e.Y;
        }

        public Form1()
        {
            InitializeComponent();
            LoadInstances();
        }

        void LoadInstances()
        {
            brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(250, 250, 250));
            mapBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 150, 150));
            bitmap = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(bitmap);
            graphicsOverride = this.CreateGraphics();
            xPosition = 20;
            ball = new Ball();
            BallMiddlePosition();
            speedBall = 3.0F;
            xPositionP2 = 570;
            yPositionP2 = 200;
            speedP2 = 5;
            scorePlayer = 0;
            scoreComputer = 0;
            PrintScore();
            timer.Start();
        }

        void BallMiddlePosition()
        {
            ball.x = this.Width / 2;
            ball.y = this.Height / 2;
        }
    }
}
