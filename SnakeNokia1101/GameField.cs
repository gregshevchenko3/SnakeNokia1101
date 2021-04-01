using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeNokia1101
{
    public delegate void ConsumeNotify();
    public delegate void SelfConsumeNotify();
    class GameField
    {
        private System.Windows.Forms.PictureBox _gameFieldControl;
        private Objects.Food _food;
        private Objects.Snake _snake;
        private System.Windows.Forms.Timer _keyTimer;
        private bool kbdLock = false;
        public event ConsumeNotify OnConsume;
        public event SelfConsumeNotify OnSelfConsume;

        public System.Windows.Forms.PictureBox GameFieldControl { get => _gameFieldControl; }
        public GameField()
        {
            _gameFieldControl = new System.Windows.Forms.PictureBox();
            _gameFieldControl.BackColor = Color.Gray;
            
            _gameFieldControl.Paint += _gameFieldControl_Paint;

            _food = new Objects.Food(25, Color.Red);
            _snake = new Objects.Snake();
            _keyTimer = new System.Windows.Forms.Timer();
            _keyTimer.Interval = 100;
            _keyTimer.Tick += (object sender, EventArgs e) => kbdLock = false;
        }
        public void Key(object sender, KeyEventArgs e)
        {
            if (!kbdLock)
            {
                switch (e.KeyCode)
                {
                    case System.Windows.Forms.Keys.Left:
                        _snake.SnakeSegs[0].Direction = Objects.Direction.LEFT;
                        break;
                    case System.Windows.Forms.Keys.Right:
                        _snake.SnakeSegs[0].Direction = Objects.Direction.RIGHT;
                        break;
                    case System.Windows.Forms.Keys.Up:
                        _snake.SnakeSegs[0].Direction = Objects.Direction.UP;
                        break;
                    case System.Windows.Forms.Keys.Down:
                        _snake.SnakeSegs[0].Direction = Objects.Direction.DOWN;
                        break;
                }
            }
        }
        internal void BlinkSnake(bool v)
        {
            _snake.Show = v;
        }
        public void Start()
        {
            _snake = new Objects.Snake();
            _food.Respawn(_gameFieldControl, _snake);
        }
        private void _gameFieldControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!_snake.Show)
            {
                e.Graphics.Clear(_gameFieldControl.BackColor);
            }
            _food.Render(e.Graphics);
            _snake.Render(e.Graphics);

        }
        public void Move()
        {
            for (int i = 0; i < _snake.SnakeSegs.Count; i++)
            {
                bool minus = ((int)_snake.SnakeSegs[i].Direction / 2) != 0;
                bool X = ((int)_snake.SnakeSegs[i].Direction % 2) != 0;
                if (X)
                    _snake.SnakeSegs[i].X += minus ? -_snake.SnakeSegs[i].Radius : _snake.SnakeSegs[i].Radius;
                else
                    _snake.SnakeSegs[i].Y += minus ? -_snake.SnakeSegs[i].Radius : _snake.SnakeSegs[i].Radius;
                int realWidth = GameFieldControl.Width - 2*_snake.SnakeSegs[i].Radius;
                int realHeight = GameFieldControl.Height - 2*_snake.SnakeSegs[i].Radius;
                _snake.SnakeSegs[i].X %= realWidth;
                _snake.SnakeSegs[i].Y %= realHeight;
                if (_snake.SnakeSegs[i].X < _snake.SnakeSegs[i].Radius/2) _snake.SnakeSegs[i].X = realWidth;
                if (_snake.SnakeSegs[i].Y < _snake.SnakeSegs[i].Radius/2) _snake.SnakeSegs[i].Y = realHeight;
            }
            for (int i = _snake.SnakeSegs.Count - 1; i >= 0 ; i--)
            {
                if (ChangeDirectionForSegment(i))
                    _snake.SnakeSegs[i].Direction = _snake.SnakeSegs[i - 1].Direction;
            }
            if (_snake.consume(_food))
            {
                do
                {
                    _food.Respawn(GameFieldControl, _snake);
                }
                while (_snake.intersect_with_food(_food));
                _snake.grow();
                OnConsume?.Invoke();
            }
            if (_snake.self_intersect())
                OnSelfConsume?.Invoke();
            kbdLock = _snake.SnakeSegs[0].Direction != _snake.SnakeSegs[1].Direction;
        }
        private bool ChangeDirectionForSegment(int index)
        {
            // Голову повертаю не тут
            if (index == 0) return false;
            // Напрмки цього та попереднього сегментів співпадають 
            if (_snake.SnakeSegs[index - 1].Direction == _snake.SnakeSegs[index].Direction)
                return false;
            if (((int)_snake.SnakeSegs[index - 1].Direction % 2) != 0 && _snake.SnakeSegs[index].Y != _snake.SnakeSegs[index - 1].Y) return false;
            if (((int)_snake.SnakeSegs[index - 1].Direction % 2) == 0 && _snake.SnakeSegs[index].X != _snake.SnakeSegs[index - 1].X) return false;
            // Напрмки цього та попереднього сегментів НЕспівпадають 
            return true;
        }

    }
}
