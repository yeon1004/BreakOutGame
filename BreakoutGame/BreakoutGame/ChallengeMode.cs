using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakoutGame
{
    public partial class ChallengeMode : Form1
    {
        Random random;

        public ChallengeMode()
        {
            InitializeComponent();
            random = new Random();

            DrawMap();
            timer_addBlock.Start();
        }

        protected void DrawMap()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    PictureBox block = new PictureBox();
                    int blockCode = random.Next(0, 10) + 1;
                    switch (blockCode)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            block.BackgroundImage = new Bitmap("res/block_basic.png");
                            block.Tag = "1";
                            break;
                        case 6:
                            block.BackgroundImage = new Bitmap("res/block_stone.png");
                            block.Tag = "2";
                            break;
                        case 7:
                        case 8:
                            block.BackgroundImage = new Bitmap("res/block_glass.png");
                            block.Tag = "4";
                            break;
                        default:
                            continue;
                    }
                    block.BackgroundImageLayout = ImageLayout.Stretch;
                    block.Width = this.ClientRectangle.Width / 7;
                    block.Height = (this.ClientRectangle.Height * 2 / 3) / 16;
                    block.Left = j * block.Width;
                    block.Top = i * block.Height;
                    this.Controls.Add(block);
                }
            }
        }

        private void timer_addBlock_Tick(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if(ctrl is PictureBox)
                    ctrl.Top += ctrl.Height;
            }
            for(int i = 0; i < 7; i++)
            {
                PictureBox block = new PictureBox();
                int blockCode = random.Next(0, 10) + 1;
                switch (blockCode)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        block.BackgroundImage = new Bitmap("res/block_basic.png");
                        block.Tag = "1";
                        break;
                    case 6:
                        block.BackgroundImage = new Bitmap("res/block_stone.png");
                        block.Tag = "2";
                        break;
                    case 7:
                    case 8:
                        block.BackgroundImage = new Bitmap("res/block_glass.png");
                        block.Tag = "4";
                        break;
                    default:
                        continue;
                }
                block.BackgroundImageLayout = ImageLayout.Stretch;
                block.Width = this.ClientRectangle.Width / 7;
                block.Height = (this.ClientRectangle.Height * 2 / 3) / 16;
                block.Left = i * block.Width;
                block.Top = 0;
                this.Controls.Add(block);
            }
        }
    }
}
