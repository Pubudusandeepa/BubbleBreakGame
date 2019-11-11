using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbleBreakeGame
{
    public partial class frmBubbleBreaker : Form
    {
        enum Colors
        {
            None,
            Red,
            Green,
            Yellow,
            Blue,
            Purple
        };

        const int NUM_BUBBLES = 10;
        const int BUBBLE_SIZE = 50;
        Colors[,] colors;
        Random rand;
        int score;
        bool[,] isSelected;
        int numOfSelectedBubbles;

        public frmBubbleBreaker()
        {
            InitializeComponent();
            rand = new Random();
            numOfSelectedBubbles = 0;
            score = 0;
            colors = new Colors[NUM_BUBBLES, NUM_BUBBLES];
            isSelected = new bool[NUM_BUBBLES, NUM_BUBBLES];
            lblInf.BackColor = Color.White;

        }

        private void frmBubbleBreaker_load(object sender, EventArgs e)
        {
            SetClientSizeCore(NUM_BUBBLES * BUBBLE_SIZE, NUM_BUBBLES * BUBBLE_SIZE);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.Black;
            Start();
        }




        private void Start()
        {
            for(int row =0; row < NUM_BUBBLES; row++)
            {
                for(int col=0; col < NUM_BUBBLES; col++)
                {
                    colors[row, col] = (Colors)rand.Next(1, 6);
                }
            }

            this.Text = score + "points";

        }

        private void Form_Paint1(object sender, PaintEventArgs e)
        {
            for (int row = 0; row < NUM_BUBBLES; row++)
            {
                for (int col = 0; col < NUM_BUBBLES; col++)
                {
                    Color bubbleColor = Color.Empty;

                    switch (colors[row, col])
                    {
                        case Colors.Red:
                            bubbleColor = Color.Red;
                            break;
                        case Colors.Yellow:
                            bubbleColor = Color.Yellow;
                            break;
                        case Colors.Green:
                            bubbleColor = Color.Green;
                            break;
                        case Colors.Purple:
                            bubbleColor = Color.Purple;
                            break;

                    }
                    e.Graphics.FillEllipse(new LinearGradientBrush(
                        new Point(row * BUBBLE_SIZE, col * BUBBLE_SIZE),
                        new Point(row * BUBBLE_SIZE + BUBBLE_SIZE, col * BUBBLE_SIZE + BUBBLE_SIZE),
                        Color.White, bubbleColor), row * BUBBLE_SIZE, col * BUBBLE_SIZE, BUBBLE_SIZE, BUBBLE_SIZE);

                }
            }
        }
    }
}
