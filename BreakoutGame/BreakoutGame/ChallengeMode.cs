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

namespace BreakoutGame
{
    public partial class ChallengeMode : Form1
    {
        int levelCnt;

        public ChallengeMode()
        {
            InitializeComponent();
            random = new Random();

            DrawMap();
            gameTimer.Interval = 20;
            gameTimer.Tick += timer1_Tick2;
            levelCnt = 0;
            lbLevel.Text = "1";
            timer_addBlock.Interval = 10 * 1000; //블럭 내려오는 속도 : 10초
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
            if (!run) return;
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
            levelCnt++;
            if(levelCnt == 10) //난이도 증가(블럭 내려오는 속도 빨라짐)
            {
                if(timer_addBlock.Interval > 0) timer_addBlock.Interval -= 500;
                lbLevel.Text = (Convert.ToInt32(lbLevel.Text) + 1).ToString();
                levelCnt = 0;
            }
        }

        public void timer1_Tick2(object sender, EventArgs e)
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
                MessageBox.Show("게임 종료!\r최종 스코어 : " + lbScore.Text);

                checkRank();

                // hide main form
                this.Hide();

                // show other form
                Rank rank = new Rank();
                Rank.newRank = -1;
                rank.Location = this.Location;
                rank.ShowDialog();

                // close application
                this.Close();
            }
            else if ((ball.Left <= rc.Left || ball.Right >= rc.Right) || IsBumpedX(bar))
            {
                ball_x = -ball_x;
            }
            else if (ball.Top <= rc.Top)
            {
                ball_y = -ball_y;
            }
            else if (IsBumpedY(bar)) //공 바에 튕길 때 각도 조절
            {
                int ballc = ball.Right - (ball.Width / 2);
                switch ((ballc - bar.Left) / (bar.Width / 8) + 1)
                {
                    case 1:
                    case 2:
                        if (ball_y < 0) ball_y = 2;
                        else ball_y = -3;
                        if (ball_x < 0) ball_x = -5;
                        else if (ball_x > 0) ball_x = 5;
                        break;
                    case 7:
                    case 8:
                        if (ball_y > 0) ball_y = -2;
                        else ball_y = 3;
                        if (ball_x < 0) ball_x = -5;
                        else if (ball_x > 0) ball_x = 5;
                        break;
                    case 3:
                    case 6:
                        if (ball_y < 0) ball_y = 3;
                        else if (ball_y > 0) ball_y = -3;
                        if (ball_x < 0) ball_x = -5;
                        else if (ball_x > 0) ball_x = 5;
                        break;
                    case 4:
                    case 5:
                        if (ball_y < 0) ball_y = 4;
                        else if (ball_y > 0) ball_y = -4;
                        if (ball_x < 0) ball_x = -4;
                        else if (ball_x > 0) ball_x = 4;
                        break;
                    default:
                        ball_y = -ball_y;
                        break;
                }
                //ball_y = -ball_y;
                if (item1_on >= 0)
                {
                    ball_x = ball_x * 3 / 2;
                    ball_y = ball_y * 3 / 2;
                }
            }

            if (item4_on >= 0)
            {
                //벽돌 맞을 경우
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is PictureBox)
                    {
                        if (IsBumpedX((PictureBox)ctrl))
                        {
                            this.Controls.Remove(ctrl);
                            lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
                            ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                        }
                        else if (IsBumpedY((PictureBox)ctrl))
                        {
                            this.Controls.Remove(ctrl);
                            lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
                            ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                        }
                        if (ctrl.Bottom > rc.Bottom)
                        {
                            gameTimer.Stop();
                            MessageBox.Show("게임 종료!\r최종 스코어 : " + lbScore.Text);

                            checkRank();

                            // hide main form
                            this.Hide();

                            // show other form
                            Rank rank = new Rank();
                            Rank.newRank = -1;
                            rank.Location = this.Location;
                            rank.ShowDialog();

                            // close application
                            this.Close();
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
                                    if (item1_on < 0) Item1();
                                    break;
                                case 2:     //바 짧아짐
                                    this.Controls.Remove(ctrl);
                                    if (item2_on < 0) Item2();
                                    break;
                                case 3:     //바 길어짐
                                    this.Controls.Remove(ctrl);
                                    if (item3_on < 0) Item3();
                                    break;
                                case 4:     //공 많아짐
                                    this.Controls.Remove(ctrl);
                                    if (item4_on < 0) Item4();
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
                                    lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
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
                                    lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
                                    ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                    break;
                                case 3:     //철벽돌
                                    ball_x = -ball_x;
                                    break;
                                case 4:      //유리 벽돌
                                    this.Controls.Remove(ctrl);
                                    lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
                                    ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                    break;
                                default:
                                    break;
                            }
                            if (ctrl.Bottom > rc.Bottom)
                            {
                                gameTimer.Stop();
                                MessageBox.Show("게임 종료!\r최종 스코어 : " + lbScore.Text);

                                checkRank();

                                // hide main form
                                this.Hide();

                                // show other form
                                Rank rank = new Rank();
                                Rank.newRank = -1;
                                rank.Location = this.Location;
                                rank.ShowDialog();

                                // close application
                                this.Close();
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
                                    lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
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
                                    lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
                                    ItemDrop(ctrl.Left, ctrl.Right, ctrl.Bottom, ctrl.Top);
                                    break;
                                case 3:     //철벽돌
                                    ball_y = -ball_y;
                                    break;
                                case 4:      //유리 벽돌
                                    this.Controls.Remove(ctrl);
                                    lbScore.Text = (Convert.ToInt32(lbScore.Text) + 10).ToString();
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
                                    if (item1_on < 0) Item1();
                                    break;
                                case 2:     //바 짧아짐
                                    this.Controls.Remove(ctrl);
                                    if (item2_on < 0) Item2();
                                    break;
                                case 3:     //바 길어짐
                                    this.Controls.Remove(ctrl);
                                    if (item3_on < 0) Item3();
                                    break;
                                case 4:     //공 많아짐
                                    this.Controls.Remove(ctrl);
                                    if (item4_on < 0) Item4();
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

        protected new void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Rectangle rc = this.ClientRectangle;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    run = !run;
                    if (run)
                    {
                        startMsg.Visible = false;

                        gameTimer.Start();
                        itemTimer.Start();
                        itemConTimer.Start();

                        this.MouseMove += Form1_MouseMove;
                    }
                    else
                    {
                        gameTimer.Stop();
                        itemTimer.Stop();
                        itemConTimer.Stop();

                        this.MouseMove -= Form1_MouseMove;
                    }
                    break;
                case Keys.Escape:
                    run = !run;
                    DialogResult result = MessageBox.Show("나가시겠습니까?", "게임 종료", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        this.Close();
                        new System.Threading.Thread(() =>
                        {
                            Application.Run(new MainForm());
                        }).Start();
                    }
                    else if (result == DialogResult.No)
                        run = !run;
                    if (run)
                    {
                        startMsg.Visible = false;

                        gameTimer.Start();
                        itemTimer.Start();
                        itemConTimer.Start();

                        this.MouseMove += Form1_MouseMove;
                    }
                    else
                    {
                        gameTimer.Stop();
                        itemTimer.Stop();
                        itemConTimer.Stop();

                        this.MouseMove -= Form1_MouseMove;
                    }
                    break;
                default: break;
            }
        }

        private void checkRank()
        {
            FileStream file = new FileStream("rank.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string rank_data;
            string[][] rankList = new string[10][];

            for (int i = 0; i < 10; i++)
            {
                rank_data = sr.ReadLine();
                //MessageBox.Show(rank_data);
                rankList[i] = rank_data.Split('\t');      //문자열 자르기
                sr.ReadLine();
            }
            sr.Close();

            int newRankId = -1;
            for (int i = 0; i < 10; i++)
            {
                if (Convert.ToInt32(lbScore.Text) > Convert.ToInt32(rankList[i][2]))
                {
                    newRankId = i;
                    break;
                }
            }
            if (newRankId > 0)
            {
                for (int j = 10 - 1; j > newRankId + 1; j--)
                {
                    rankList[j] = rankList[j - 1];
                }

                MessageBox.Show((newRankId+1) + "위 달성!");
                string input = Microsoft.VisualBasic.Interaction.InputBox("이름을 입력해주세요.");

                rankList[newRankId][0] = (newRankId + 1).ToString();
                rankList[newRankId][1] = input;
                rankList[newRankId][2] = lbScore.Text;
            }

            FileStream file2 = new FileStream("rank.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(file2, Encoding.Unicode);
            for (int k = 0; k < 10; k++)
            {
                int num = k + 1;
                sw.WriteLine(num + "\t" + rankList[k][1] + "\t" + rankList[k][2] + "\r\n");
            }
            sw.Close();
            //MessageBox.Show("파일 쓰기 완료");

            // hide main form
            this.Hide();

            // show other form
            Rank rank = new Rank();
            Rank.newRank = newRankId;
            rank.Location = this.Location;
            rank.ShowDialog();

            // close application
            this.Close();
        }
    }
}
