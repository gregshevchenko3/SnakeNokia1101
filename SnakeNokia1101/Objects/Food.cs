using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeNokia1101.Objects
{
    class Food : Segment
    {
        public Food(int radius, Color color) : base(radius, color) { }
        public override void Render(Graphics graphics)
        {
            Bitmap bitmap = new Bitmap(Resource1.apple);
            graphics.DrawImage(bitmap, new Rectangle(X - Radius, Y - Radius, 2 * Radius, 2 * Radius));
        }
        
        public void Respawn(PictureBox gameFieldControl, Snake snake)
        {
            
            Random rand = new Random(DateTime.Now.Millisecond);
            bool success = true;
            int x, y;
            do
            {
                x = rand.Next(0 + Radius, gameFieldControl.Width - Radius);
                y = rand.Next(0 + Radius, gameFieldControl.Height - Radius);
                foreach (var item in snake.SnakeSegs)
                {
                    if ((x >= item.X - Radius) && (x <= item.X + Radius))
                    {
                        success = false;
                    }
                    else
                        success = true;
                    if ((y >= item.Y - Radius) && (y <= item.Y + Radius))
                    {
                        success = false;
                    }
                    else
                        success = true;
                }
            }
            while (!success);
            X = x;
            Y = y;
        }
    }
}
