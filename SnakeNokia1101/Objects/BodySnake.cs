using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNokia1101.Objects
{
    public enum Direction
    {
        DOWN, RIGHT, UP, LEFT
    }
    public class BodySnake : Segment
    {
        private Direction _direction;
        public Direction Direction
        {
            get => _direction;
            set => _direction = ((int)value % 2 != (int)_direction % 2)? value : _direction;
        }

        public BodySnake(int radius, Color color, Direction direction) : base(radius, color) {
            _direction = direction;
        }
    }
}
