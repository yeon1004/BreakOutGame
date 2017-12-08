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
    public partial class StageMode : Form1
    {
        public static string mapId;
        public StageMode()
        {
            InitializeComponent();
            DrawMap(mapId);

            gameTimer.Tick += timer1_Tick;

            gameTimer.Start();
            itemTimer.Start();
            itemConTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (run)
            {
                Rectangle rc = this.ClientRectangle;

                //새로고침 범위 최소화하기 위한 ocircle
                Rectangle ocircle = new Rectangle(ball.Location, ball.Size);

                //볼 이동
                ball.Offset(ball_x, ball_y);

                //공 튕기기
                if (ball.Bottom > rc.Bottom)
                {
                    gameTimer.Stop();
                    MessageBox.Show("패배!");
                    DialogResult result = MessageBox.Show("다시하시겠습니까?", "GameOver", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        this.Close();
                        new System.Threading.Thread(() =>
                        {
                            Application.Run(new StageMode());
                        }).Start();
                    }
                    else if (result == DialogResult.No)
                    {
                        this.Close();
                        new System.Threading.Thread(() =>
                        {
                            Application.Run(new MainForm());
                        }).Start();
                    }
                    //ball_y = -ball_y; //게임 안끝남. 테스트용
                }
                else if ((ball.Left <= rc.Left || ball.Right >= rc.Right) || IsBumpedX(bar))
                {
                    ball_x = -ball_x;
                }
                else if (ball.Top <= rc.Top || IsBumpedY(bar))
                {
                    ball_y = -ball_y;
                }

                if (item4_on != 0)
                {
                    //벽돌 맞을 경우
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is PictureBox)
                        {
                            if (IsBumpedX((PictureBox)ctrl))
                            {
                                this.Controls.Remove(ctrl);
                                ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                            }
                            else if (IsBumpedY((PictureBox)ctrl))
                            {
                                this.Controls.Remove(ctrl);
                                ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                            }
                        }

                        if (ctrl is Button)
                        {
                            if (IsBumpedX((Button)ctrl) || IsBumpedY((Button)ctrl))
                            {
                                int itemId = Convert.ToInt32(ctrl.Tag.ToString());
                                switch (itemId)
                                {
                                    case 1:     //공 빨라짐
                                        this.Controls.Remove(ctrl);
                                        if (item1_on == 0) Item1();
                                        break;
                                    case 2:     //바 짧아짐
                                        this.Controls.Remove(ctrl);
                                        if (item2_on == 0) Item2();
                                        break;
                                    case 3:     //바 길어짐
                                        this.Controls.Remove(ctrl);
                                        if (item3_on == 0) Item3();
                                        break;
                                    case 4:     //공 많아짐
                                        this.Controls.Remove(ctrl);
                                        if (item4_on == 0) Item4();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //벽돌 맞을 경우
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is PictureBox)
                        {
                            if (IsBumpedX((PictureBox)ctrl))
                            {
                                int iblockTag = Convert.ToInt32(ctrl.Tag.ToString());
                                switch (iblockTag)
                                {
                                    case 1:      //일반 벽돌
                                        ball_x = -ball_x;
                                        this.Controls.Remove(ctrl);
                                        ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                        break;
                                    case 2:      //돌벽돌
                                        ball_x = -ball_x;
                                        ctrl.BackgroundImage = new Bitmap("res/block_stone2.png");
                                        ctrl.Tag = "21";
                                        break;
                                    case 21:    //한번 맞았던 돌벽돌
                                        ball_x = -ball_x;
                                        this.Controls.Remove(ctrl);
                                        ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                        break;
                                    case 3:     //철벽돌
                                        ball_x = -ball_x;
                                        break;
                                    case 4:      //유리 벽돌
                                        this.Controls.Remove(ctrl);
                                        ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if (IsBumpedY((PictureBox)ctrl))
                            {
                                int iblockTag = Convert.ToInt32(ctrl.Tag.ToString());
                                switch (iblockTag)
                                {
                                    case 1:      //일반 벽돌
                                        ball_y = -ball_y;
                                        this.Controls.Remove(ctrl);
                                        ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                        break;
                                    case 2:      //돌벽돌
                                        ball_y = -ball_y;
                                        ctrl.BackgroundImage = new Bitmap("res/block_stone2.png");
                                        ctrl.Tag = "21";
                                        break;
                                    case 21:    //한번 맞았던 돌벽돌
                                        ball_y = -ball_y;
                                        this.Controls.Remove(ctrl);
                                        ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                        break;
                                    case 3:     //철벽돌
                                        ball_y = -ball_y;
                                        break;
                                    case 4:      //유리 벽돌
                                        this.Controls.Remove(ctrl);
                                        ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        if (ctrl is Button)
                        {
                            if (IsBumpedX((Button)ctrl) || IsBumpedY((Button)ctrl))
                            {
                                int itemId = Convert.ToInt32(ctrl.Tag.ToString());
                                switch (itemId)
                                {
                                    case 1:     //공 빨라짐
                                        this.Controls.Remove(ctrl);
                                        if (item1_on == 0) Item1();
                                        break;
                                    case 2:     //바 짧아짐
                                        this.Controls.Remove(ctrl);
                                        if (item2_on == 0) Item2();
                                        break;
                                    case 3:     //바 길어짐
                                        this.Controls.Remove(ctrl);
                                        if (item3_on == 0) Item3();
                                        break;
                                    case 4:     //공 많아짐
                                        this.Controls.Remove(ctrl);
                                        if (item4_on == 0) Item4();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                //최소 범위만 새로고침
                if (ocircle != ball)
                {
                    int left = Math.Min(ocircle.Left, ball.Left);
                    int top = Math.Min(ocircle.Top, ball.Top);
                    int right = Math.Max(ocircle.Right, ball.Right);
                    int bottom = Math.Max(ocircle.Bottom, ball.Bottom);
                    this.Invalidate(new Rectangle(left, top, right - left + 1, bottom - top + 1));
                }
            }
        }
    }
}
