using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Zderzenia
{
    class MyWindow
    {
        RenderWindow window;
        private Ball currentBall;
        Color color;
        RectangleShape line = new RectangleShape();
        Vertex[] lines = new Vertex[2];
        Color[] colors =
        {
            Color.Magenta,
            Color.Blue,
            Color.Cyan,
            Color.Green,
            Color.Red,
            Color.White,
            Color.Yellow
        };
        private List<Ball> ballList = new List<Ball>();
        private Random random = new Random();
        private const int maxX = 900;
        private const int maxY = 400;
        public MyWindow()
        {
            initWindow();
            initButtonsWindow();
            initTimer();


            // Start the game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Clear screen
                window.Clear();

                foreach (Ball i in ballList)
                {
                    window.Draw(i);
                }

                // Update the window
                window.Draw(line);
                window.Draw(lines, PrimitiveType.Lines);
                window.Display();
            }
        }

        private void initButtonsWindow()
        {
            Form buttonsForm = new Form();
            buttonsForm.Visible = true;
            buttonsForm.AutoSize = true;
            buttonsForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button addButton = new Button();
            addButton.Click += new EventHandler(addButtonClick);
            addButton.Text = "Dodaj";
            buttonsForm.Controls.Add(addButton);
        }

        private void initWindow()
        {
            window = new RenderWindow(new VideoMode(maxX, maxY), "SFML window");
            window.Closed += new EventHandler(OnClose);
            window.Display();
            window.SetActive();
            window.SetVisible(true);
        }

        protected void addButtonClick(object sender, EventArgs e)
        {
            //Button button = sender as Button;
            color = colors[random.Next(0, 6)];
           
            
            bool collision = false;
            do
                {
                currentBall = new Ball(color, 
                    (float)GetRandomNumber(5, 40),
                    GetRandomNumber(0.1, 3.0),
                    (int)GetRandomNumber(0, 360),
                    new Vector2f((float)GetRandomNumber(20.0, maxX - 20.0),
                        (float)GetRandomNumber(20.0, maxY - 20.0)), 
                    "Kula" + ballList.Count);
                foreach (Ball b in ballList)
                {
                    collision = Ball.checkCollision(currentBall, b);
                    if (collision) break;
                }
            } while (collision);
            
            ballList.Add(currentBall);
        }
        private void initTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1;
            timer.Elapsed += new ElapsedEventHandler(timerTick);
            timer.Start();
        }

        private void timerTick(object sender, ElapsedEventArgs e)
        {
            foreach (Ball i in ballList)
            {
                i.move();
            }
            Ball[] balls = ballList.ToArray();
            for(int i = 0; i < balls.Length-1; i++)
            {
                for(int j = i+1;j<balls.Length; j++)
                {
                    if (Ball.checkCollision(balls[i], balls[j]))
                    {
                        ballList[i].FillColor = Color.Green;
                        ballList[j].FillColor = Color.Green;
                        bounce(balls[i], balls[j]);
                    }
                }
            }
        }

        private void bounce(Ball ball1, Ball ball2)
        {

            int tmpDir = ball1.direction;
            double tmpSpeed = ball1.speed;
            ball1.direction = ball2.direction;
            ball1.speed = ball2.speed;

            ball2.direction = tmpDir;
            ball2.speed = tmpSpeed;

            lines[0].Position = new Vector2f(ball1.centerX, ball1.centerY);
            lines[1].Position = new Vector2f(ball2.centerX, ball2.centerY);
            lines[0].Color = Color.Red;
            lines[1].Color = Color.Red;
            window.Draw(lines,PrimitiveType.Lines);
        }

        private void OnClose(object sender, EventArgs e)
        {
            window.Close();
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
