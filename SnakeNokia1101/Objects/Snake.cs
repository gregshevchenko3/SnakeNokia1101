using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNokia1101.Objects
{
    class Snake
    {
        private List<BodySnake> _snake;
        public List<BodySnake> SnakeSegs { get => _snake; }
        public bool Show { get; set; } = true;
        public Snake()
        {
            _snake = new List<BodySnake>();
            _snake.Add(new HeadSnake(25, Color.Green, Direction.RIGHT));
            _snake.Add(new BodySnake(25, Color.Green, Direction.RIGHT));
            _snake.Add(new BodySnake(25, Color.Green, Direction.RIGHT));

            _snake[0].X = 300; 
            _snake[0].Y = 150;

            _snake[1].X = _snake[0].X - 2*_snake[0].Radius;
            _snake[1].Y = _snake[0].Y;

            _snake[2].X = _snake[1].X - _snake[1].Radius;
            _snake[2].Y = _snake[1].Y;
        }
        public void Render(Graphics graphics)
        {
            if (Show)
            {
                foreach (var item in _snake)
                {
                    item.Render(graphics);
                }
            }
        }
        public bool intersect_with_food(Food _food)
        {
            foreach (Segment seg in _snake)
                if (seg.intersects_with(_food)) return true;
            return false;
        }
        public bool consume(Food _food)
        {
            return _snake[0].intersects_with(_food);
        }
        public bool self_intersect()
        {
            for (int i = 4; i < _snake.Count; i++)
                if (_snake[0].intersects_with(_snake[i])) return true;
            return false;
        }
        public void grow()
        {
            bool minus = ((int)_snake.Last().Direction / 2) != 0;
            bool X = ((int)_snake.Last().Direction % 2) != 0;
            BodySnake prev = _snake.Last();

            _snake.Add(new BodySnake(25, Color.Green, prev.Direction));
            _snake.Last().X = X ? (minus ? prev.X + prev.Radius : prev.X - prev.Radius): prev.X;
            _snake.Last().Y = X ? prev.Y : (minus ? prev.Y + prev.Radius : prev.Y - prev.Radius);
        }
    }
}
