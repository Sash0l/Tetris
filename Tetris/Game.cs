using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Controllers;

namespace Tetris
{
    public partial class Game : Form
    {
        
        
        string playerName;
        
        public Game()
        {
            InitializeComponent();
            //if (!File.Exists(RecordsController.recordPath))
                //File.Create(RecordsController.recordPath);
            playerName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя игрока","Настройка игрока");
            if(playerName == "")
            {
                playerName = "Новый игрок";
            }
            this.KeyUp += new KeyEventHandler(keyFunc);
            Init();
        }

        public void Init()
        {
            //RecordsController.ShowRecords(label3);
            DrawControler.size = 30;
            DrawControler.score = 0;
            DrawControler.linesRemoved = 0;
            DrawControler.currentShape = new Shape(3, 0);
            DrawControler.Interval = 300;
            label1.Text = "Счет: " + DrawControler.score;
            label2.Text = "Линии: " + DrawControler.linesRemoved;
            timer1.Interval = DrawControler.Interval;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
            Invalidate();
        }
        private void keyFunc(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:

                    if (!DrawControler.IsIntersects())
                    {
                        DrawControler.ResetArea();
                        DrawControler.currentShape.RotateShape();
                        DrawControler.Merge();
                        Invalidate();
                    }
                    break;
                case Keys.C:
                    timer1.Interval = 10;
                    break;
                case Keys.Right:
                    if (!DrawControler.CollideHor(1))
                    {
                        DrawControler.ResetArea();
                        DrawControler.currentShape.MoveRight();
                        DrawControler.Merge();
                        Invalidate();
                    }
                    break;
                case Keys.Left:
                    if (!DrawControler.CollideHor(-1))
                    {
                        DrawControler.ResetArea();
                        DrawControler.currentShape.MoveLeft();
                        DrawControler.Merge();
                        Invalidate();
                    }
                    break;
            }
        }

        
        private void update(object sender, EventArgs e)
        {
            DrawControler.ResetArea();
            if (!DrawControler.Collide())
            {
                DrawControler.currentShape.MoveDown();
            }
            else
            {
                DrawControler.Merge();
                DrawControler.SliceMap(label1,label2);
                timer1.Interval = DrawControler.Interval;
                DrawControler.currentShape.ResetShape(3,0);
                if (DrawControler.Collide())
                {
                    Record.SaveRecords(playerName);
                    DrawControler.ClearMap();
                    timer1.Tick -= new EventHandler(update);
                    timer1.Stop();
                    MessageBox.Show("Ваш результат: " + DrawControler.score);
                    Init();
                }
            }
            DrawControler.Merge();
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawControler.DrawGrid(e.Graphics);
            DrawControler.DrawMap(e.Graphics);
            DrawControler.ShowNextShape(e.Graphics);
        }       

    }
}
