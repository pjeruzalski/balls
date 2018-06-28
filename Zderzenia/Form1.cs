using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Zderzenia
{
    public partial class Form1 : Form
    {

        //Ball ball = new Ball(Color.Red,20,01.15,320,new Point(50,50));
        //Ball ball2 = new Ball(Color.Green, 30, 2.32, 20, new Point(300, 200));
        private Ball currentBall;
        private List<Ball> ballList = new List<Ball>();
        private Graphics g;
        private Random random = new Random();
        private int selectedItemNumber = 0;
        private int counter = 0;
        public Form1()
        {
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
            //ControlStyles.UserPaint |
            //ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            g = CreateGraphics();
            Paint += new PaintEventHandler(panel_Paint);
            initTimer();
            
            foreach (Color color in new ColorConverter().GetStandardValues())
            {
                cbxColor.Items.Add(color);
            }
            cbxColor.SelectedIndex = selectedItemNumber;
            selectedItemNumber++;
        }

        private void initTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1;
            timer.Elapsed += new ElapsedEventHandler(this.timerTick);
            timer.Start();
        }

        private void timerTick(object sender, ElapsedEventArgs e)
        {
            counter++;
            foreach (Ball i in ballList)
            {
                i.move();
            }

                this.Invalidate();
                Invoke((MethodInvoker)delegate
               {
                   Refresh();
               });

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {           
            foreach (Ball i in ballList)
            {
                //
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            Color selected = (Color)cbxColor.SelectedItem;
            selectedItemNumber++;
            cbxColor.SelectedItem = cbxColor.Items[selectedItemNumber];
            //currentBall = new Zderzenia.Ball(selected, (int)GetRandomNumber(5, 40), GetRandomNumber(0.1, 3.0), (int)GetRandomNumber(0, 360), new Point(20, 20), "Kula" + ballList.Count);
            ballList.Add(currentBall);
            cbxBall.Items.Add(currentBall.name);
            updateForm();
        }

        private void updateForm()
        {
            cbxBall.SelectedIndex = cbxBall.Items.Count - 1;
            txbName.Text = currentBall.name;
            //txbSpeed.Text = System.String.ToString(currentBall.speed);
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
