using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNokia1101.Objects
{
    class HeadSnake : BodySnake 
    {
        public HeadSnake(int radius, Color color, Direction direction) : base (radius, color, direction) { }
        public override void Render(Graphics graphics)
        {
            Bitmap bitmap = new Bitmap(Resource1.snake);
            Color bg = bitmap.GetPixel(1, 1);
            bitmap.MakeTransparent(bg);
            switch (Direction)
            {
                case Direction.UP:
                    bitmap = RotateImage(bitmap, 180);
                    graphics.FillRectangle(new SolidBrush(Color), new Rectangle(X - Radius / 2, Y + Radius, Radius, Radius));
                    break;
                case Direction.DOWN:
                    bitmap = RotateImage(bitmap, 0);
                    graphics.FillRectangle(new SolidBrush(Color), new Rectangle(X - Radius / 2 , Y - 2*Radius, Radius, Radius));
                    break;
                case Direction.LEFT:
                    bitmap = RotateImage(bitmap, 90);
                    graphics.FillRectangle(new SolidBrush(Color), new Rectangle(X + Radius, Y - Radius / 2, Radius, Radius));
                    break;
                case Direction.RIGHT:
                    bitmap = RotateImage(bitmap, 270);
                    graphics.FillRectangle(new SolidBrush(Color), new Rectangle(X - 2 * Radius, Y - Radius / 2, Radius, Radius));
                    break;
            }
            graphics.DrawImage(bitmap, new Rectangle(X - Radius, Y - Radius, 2 * Radius, 2 * Radius));
        }
        private Bitmap RotateImage(Bitmap bitmap, int v)
        {
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            bitmap1.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            using (Graphics g = Graphics.FromImage(bitmap1))
            {
                g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                g.RotateTransform(v);
                g.ScaleTransform(1.5f, 1.5f);
                g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                g.DrawImage(bitmap, new Point(0, 0));
            }
            return bitmap1;
        }
    }
}
