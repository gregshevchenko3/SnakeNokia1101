using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNokia1101.Objects
{
    abstract public class Segment : ISegmentBehavior
    {
        private int _x, _y, _radius;
        private Color _color;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Radius { get => _radius; set => _radius = value; }
        public Color Color { get => _color; }

        public Segment(int radius, Color color)
        {
            X = X = 0;
            Radius = radius;
            _color = color;
        }
        public virtual void Render(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Color), new Rectangle(X - Radius/2, Y - Radius/2, Radius, Radius));
        }
        public bool intersects_with(Segment other)
        {
            double distance = Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
            return distance < (Radius + other.Radius);
        }
    }
}
