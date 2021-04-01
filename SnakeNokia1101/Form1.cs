using SnakeNokia1101.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeNokia1101
{
    public partial class Form1 : Form
    {
        private GameField _gameField;
        private int score = 0;
        private int timeTick = 0;
        private int gameover_ticks = 10;
        private bool lockGOTimerEvent = false;
        public Form1()
        {
            InitializeComponent();
            _gameField = new GameField();
            Controls.Add(_gameField.GameFieldControl);
            _gameField.GameFieldControl.Location = new Point(0, toolStrip1.Size.Height);
            _gameField.GameFieldControl.Size = new Size(Size.Width, Size.Height - toolStrip1.Size.Height);
            _gameField.OnConsume += ScoreIncrement;
            _gameField.OnSelfConsume += GameOver;
            timer1.Tick += Timer1_Tick;
            KeyUp += _gameField.Key;

        }
        private void GameOver()
        {
            timer1.Stop();
            timer1.Tick -= Timer1_Tick;
            timer1.Interval = 250;
            timer1.Tick += GameOverBlink;
            timer1.Start();
        }
        private void GameOverBlink(object sender, EventArgs e)
        {
            if (lockGOTimerEvent) return;
            if (gameover_ticks > 0)
            {
                _gameField.BlinkSnake(gameover_ticks % 2 > 0);
                _gameField.GameFieldControl.Refresh();
                gameover_ticks--;
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
                timer1.Dispose();
                if (MessageBox.Show($"Your score: {score}\n Youre time: {timeTick}\n Want to play again?!!", "GAME OVER!!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Form1_Load(this, null);
                else
                    Close();
                lockGOTimerEvent = true;
            }
        }
        private void ScoreIncrement()
        {
            ScoreLabel.Text = $"Score: {score += 5}";
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timeTick++;
            TimeLabel.Text = $"Time: {timeTick }";
            _gameField.Move();
            _gameField.GameFieldControl.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1 = new Timer();
            timeTick = 0;
            score = 0;
            TimeLabel.Text = $"Time: {timeTick }";
            ScoreLabel.Text = $"Score: {score }";
            _gameField.Start();
            timer1.Interval = 150;
            timer1.Tick -= GameOverBlink;
            timer1.Tick += Timer1_Tick;
            lockGOTimerEvent = false;
            timer1.Start();
        }


    }
}
