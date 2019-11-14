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
            lblInf.BackColor = Color.Black;

        }

        private void frmBubbleBreaker_load(object sender, EventArgs e)
        {
            SetClientSizeCore(NUM_BUBBLES * BUBBLE_SIZE, NUM_BUBBLES * BUBBLE_SIZE);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.Black;
            DoubleBuffered = true;
            Start();
        }




        private void Start()
        {
            for (int row = 0; row < NUM_BUBBLES; row++)
            {
                for (int col = 0; col < NUM_BUBBLES; col++)
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
                    var xPos = col;
                    var yPos = row;
                    var isBubble = true;

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
                        case Colors.Blue:
                            bubbleColor = Color.Blue;
                            break;
                        default:
                            e.Graphics.FillRectangle(Brushes.Black,
                                xPos * BUBBLE_SIZE,
                                yPos * BUBBLE_SIZE,
                                BUBBLE_SIZE, BUBBLE_SIZE);
                            isBubble = false;
                            break;

                    }
                    if (isBubble)
                    {
                        e.Graphics.FillEllipse(new LinearGradientBrush(
                     new Point(row * BUBBLE_SIZE + 5, col * BUBBLE_SIZE),
                     new Point(row * BUBBLE_SIZE + BUBBLE_SIZE + 15, col * BUBBLE_SIZE + BUBBLE_SIZE + 15),
                     Color.White, bubbleColor),
                     yPos * BUBBLE_SIZE,
                     xPos * BUBBLE_SIZE,
                     BUBBLE_SIZE, BUBBLE_SIZE);

                        if (isSelected[row, col])
                        {
                            //Left outLine
                            if (col > 0 && colors[row, col] != colors[row, col - 1])
                                e.Graphics.DrawLine(Pens.White, xPos * BUBBLE_SIZE, yPos * BUBBLE_SIZE,
                                    xPos * BUBBLE_SIZE, yPos * BUBBLE_SIZE + BUBBLE_SIZE);
                            //right outline
                            if (col < NUM_BUBBLES-1  && colors[row, col] != colors[row, col + 1])
                                e.Graphics.DrawLine(Pens.White, xPos * BUBBLE_SIZE + BUBBLE_SIZE, yPos * BUBBLE_SIZE,
                                    xPos * BUBBLE_SIZE + BUBBLE_SIZE, yPos * BUBBLE_SIZE + BUBBLE_SIZE);
                            //top outline
                            if(row > 0 && colors[row,col] != colors[row - 1, col])
                            
                                e.Graphics.DrawLine(Pens.White, xPos * BUBBLE_SIZE , yPos * BUBBLE_SIZE,
                                    xPos * BUBBLE_SIZE + BUBBLE_SIZE, yPos * BUBBLE_SIZE);


                            

                            //Bottom outline
                            if (row < NUM_BUBBLES-1 && colors[row, col] != colors[row + 1, col])
                            
                                e.Graphics.DrawLine(Pens.White, xPos * BUBBLE_SIZE, yPos * BUBBLE_SIZE + BUBBLE_SIZE,
                                    xPos * BUBBLE_SIZE + BUBBLE_SIZE, yPos * BUBBLE_SIZE + BUBBLE_SIZE);


                            


                        }
                    }

                 

                }
            }
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            var x = Convert.ToInt32(e.X / BUBBLE_SIZE);
            var y = Convert.ToInt32(e.Y / BUBBLE_SIZE);
            var row = y;
            var col = x;

            if (isSelected[row, col] && numOfSelectedBubbles > 1)
            {
                score += Convert.ToInt32(lblInf.Text);
                this.Text = score + "point";
                RemoveBubbles();
                ClearSelected();
                MoveBubblesDown();
                 MoveBubblesRight();

                if (!HasMoreMoves())
                {
                    MessageBox.Show("Game Over");
                }


            }
            else
            {
                ClearSelected();

                if (colors[row, col] > Colors.None)
                {
                    HighlightNeighbors(row, col);

                    if (numOfSelectedBubbles > 1)
                    {
                        SetLable(numOfSelectedBubbles, x, y);
                    }
                }
            }

        }

        private void RemoveBubbles()
        {
            for (int row = 0; row < NUM_BUBBLES; row++)
            {
                for (int col = 0; col < NUM_BUBBLES; col++)
                {
                    if (isSelected[row, col])
                        colors[row, col] = Colors.None;
                }
            }
            this.Invalidate();
            Application.DoEvents();
        }

        private void ClearSelected()
        {
            for (int row = 0; row < NUM_BUBBLES; row++)
            {
                for (int col = 0; col < NUM_BUBBLES; col++)
                {
                    isSelected[row, col] = false;

                }
            }
            numOfSelectedBubbles = 0;
            lblInf.Visible = false;
        }

        private bool HasMoreMoves()
        {
            for (int row = 0; row < NUM_BUBBLES; row++)
            {
                for (int col = 0; col < NUM_BUBBLES; col++)
                {
                    if (colors[row, col] > Colors.None)
                    {
                        if (col < NUM_BUBBLES - 1 && colors[row, col] == colors[row, col + 1])
                            return true;
                        if (row < NUM_BUBBLES - 1 && colors[row, col] == colors[row + 1, col])
                            return true;
                    }

                }


            }
            return false;
        }

        private void SetLable(int numOfBubles, int x, int y)
        {
            var value = numOfBubles * (numOfBubles - 1);
            lblInf.Text = value.ToString();

            lblInf.Left = x * BUBBLE_SIZE + BUBBLE_SIZE;
            lblInf.Top = y * BUBBLE_SIZE + BUBBLE_SIZE;

            if (lblInf.Left > this.ClientSize.Width / 2)
                lblInf.Left -= BUBBLE_SIZE;

            if (lblInf.Top > this.ClientSize.Height / 2)
                lblInf.Top = BUBBLE_SIZE;
                lblInf.Visible = true;


        }

        private void MoveBubblesDown()
        {
            for (int col = 0; col < NUM_BUBBLES; col++)
            {
                var noneColorBubblePosition = NUM_BUBBLES - 1;
                var foundNoneColor = false;

                for (int row = NUM_BUBBLES - 1; row >= 0; row--)
                {
                    if (colors[row, col] == Colors.None)
                        foundNoneColor = true;

                    if (colors[row, col] != Colors.None && !foundNoneColor)
                        noneColorBubblePosition--;

                    if (colors[row, col] != Colors.None && foundNoneColor)
                    {
                        colors[noneColorBubblePosition, col] = colors[row, col];
                        noneColorBubblePosition--;
                    }

                }

                for (int r = noneColorBubblePosition; r >= 0; r--)
                {
                    colors[r, col] = Colors.None;
                }


            }
            this.Invalidate();
            Application.DoEvents();
        }


        private void MoveBubblesRight()
        {
            for (int row = 0; row < NUM_BUBBLES; row++)
            {
                var noneColorBubblePosition = NUM_BUBBLES - 1;
                var foundNoneColor = false;

                for (int col = NUM_BUBBLES - 1; col >= 0; col--)
                {
                    if (colors[row, col] == Colors.None)
                        foundNoneColor = true;

                    if (colors[row, col] != Colors.None && !foundNoneColor)
                        noneColorBubblePosition--;

                    if (colors[row, col] != Colors.None && foundNoneColor)
                    {
                        colors[row, noneColorBubblePosition] = colors[row, col];
                        noneColorBubblePosition--;
                    }

                }

                for (int c = noneColorBubblePosition; c >= 0; c--)
                {
                    colors[row, c] = Colors.None;
                }


            }
            this.Invalidate();
            Application.DoEvents();
            GenerateBubbles();
        }

        private void GenerateBubbles()
        {
            if (colors[NUM_BUBBLES - 1, 0] == Colors.None)
            {
                for(int row = NUM_BUBBLES-1; row >=0; row--)
                {
                    colors[row, 0] = (Colors)rand.Next(1, 6);
                }
                
            }

            this.Invalidate();
            Application.DoEvents();
            MoveBubblesRight();
        }


        private void HighlightNeighbors(int row, int col)
        {
            isSelected[row, col] = true;
            numOfSelectedBubbles++;

            //move up
            if (row > 0 && colors[row, col] == colors[row - 1, col] && !isSelected[row - 1, col])
            {
                HighlightNeighbors(row - 1, col);
            }
            //move down
            if (row < NUM_BUBBLES - 1 && colors[row, col] == colors[row + 1, col] && !isSelected[row + 1, col])
            {
                HighlightNeighbors(row + 1, col);
            }
            // Move left
            if (col > 0 && colors[row, col] == colors[row, col - 1] && !isSelected[row, col - 1])
            {
                HighlightNeighbors(row, col - 1);
            }
            //move right
            if (col < NUM_BUBBLES - 1 && colors[row, col] == colors[row, col + 1] && !isSelected[row, col + 1])
            {
                HighlightNeighbors(row, col + 1);
            }




        }

    }

}





