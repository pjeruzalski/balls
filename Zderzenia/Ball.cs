using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zderzenia
{
    class Ball : CircleShape
    {
        public string name { get; set; }
        //public int diameter { get; set; }
        public double speed { get; set; }
        public float centerX;
        public float centerY;
        private const int maxX = 900;
        private const int maxY = 400;
        public Vector2f coordinates;

        public int direction;
        //{
        //    get
        //    {
        //        return direction;
        //    }
        //    set
        //    {
        //        if (value >= 0 && value <= 360)
        //        {
        //            direction = value;
        //        }
        //    }
        //} 
        //public Ball(Ball copy)
        //    :base(copy)     
        //{
        //    //this.diameter = diameter;
        //    this.speed = copy.speed ;
        //    this.direction = copy.direction;
        //    this.coordinates = copy.coordinates;
        //    centerX = coordinates.X;
        //    centerY = coordinates.Y;
        //    this.name = copy.name;
        //    FillColor = copy.FillColor;
        //    Position = copy.coordinates;
        //    //this.diameter
        //    Radius = copy.Radius;
        //}
        public Ball(Color color, float diameter, double speed, int direction, Vector2f coordinates, string name)
        {

            //this.diameter = diameter;
            this.speed = speed;
            this.direction = -direction;
            this.coordinates = coordinates;
            centerX = coordinates.X;
            centerY = coordinates.Y;
            this.name = name;
            FillColor = new Color(color);
            Position = coordinates;
            //this.diameter
            Radius = diameter / 2.0f;

        }

        public void move()
        {
            float xMove = (float)speed * (float)Math.Cos((float)direction / 180.0f * (float)Math.PI);
            float yMove = (float)speed * (float)Math.Sin((float)direction / 180.0f * (float)Math.PI);
            centerX += xMove;
            centerY += yMove;

            coordinates.X = centerX - Radius;
            coordinates.Y = centerY - Radius;
            Position = coordinates;
            if (centerX <= Radius || centerX >= maxX - Radius)
            {
                if (direction <= 180) direction = 180 - direction;
                else if (direction > 180) direction = 540 - direction;
                if (centerX < Radius) centerX = Radius;
                if (centerX > maxX - Radius) centerX = maxX - Radius;

            }
            if (centerY <= Radius || centerY >= maxY - Radius)
            {
                direction = 360 - direction;
                if (centerY < Radius) centerY = Radius;
                if (centerY > maxY - Radius) centerY = maxY - Radius;
            }
        }

        internal static bool checkCollision(Ball ball1, Ball ball2)
        {
            float centersDistance = (float)Math.Sqrt(Math.Pow((ball2.centerX - ball1.centerX),2) + Math.Pow((ball2.centerY - ball1.centerY), 2));
            return centersDistance <= ball1.Radius + ball2.Radius;
        }


    }
}
