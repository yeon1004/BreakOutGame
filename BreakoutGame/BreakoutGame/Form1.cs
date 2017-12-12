using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Media;
/*
* 창 사이즈 : 555*800
* 블럭 패널 : 539*520
* 블럭 : 77*32
* 바 : 100*15
* 아이템 : 20*20
* 볼 : 10*10
*/
namespace BreakoutGame
{
    public partial class Form1 : Form
    {
        protected bool run;
        protected Rectangle ball;
        protected int ballSize;
        protected int ball_x, ball_y;
        protected TextBox bar;
        protected int barWidth, barHeight;
        protected Random random;
        protected int item1_on;
        protected int item2_on;
        protected int item3_on;
        protected int item4_on;
        
        protected System.Windows.Forms.Timer gameTimer;
        protected System.Windows.Forms.Timer itemTimer;
        protected System.Windows.Forms.Timer itemConTimer;

        static public string formName = "Form1";
        public static string mapId = "0";

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            run = false;
            startMsg.Left = this.ClientRectangle.Width / 2 - startMsg.Width / 2;

            ballSize = 13;
            ball = new Rectangle(0, 0, ballSize, ballSize);         //공 생성
            ball.Offset(this.ClientRectangle.Width / 2, this.ClientRectangle.Height * 4 / 5);

            ball_x = ball_y = 4;                                    //공 속도
            bar = txtBar;
            barWidth = 150;                                         //바 가로 길이
            barHeight = 20;                                         //바 세로 길이
            bar.Size = new Size(barWidth, barHeight);               //바 사이즈 설정
            bar.Left = this.ClientRectangle.Width / 2 - bar.Width;  //바 위치 가운데로
            
            //DrawMap();

            random = new Random();

            item1_on = -1;
            item2_on = -1;
            item3_on = -1;
            item4_on = -1;

            gameTimer = new System.Windows.Forms.Timer();
            itemTimer = new System.Windows.Forms.Timer();
            itemConTimer = new System.Windows.Forms.Timer();

            gameTimer.Interval = 10;
            itemTimer.Interval = 100;
            itemConTimer.Interval = 1000;

            gameTimer.Tick += timer1_Tick;
            itemTimer.Tick += timer2_Tick;
            itemConTimer.Tick += timer3_Tick;

        }

        protected void DrawMap(string mapId)
        {
            //맵 파일 읽어오기
            if (Convert.ToInt32(mapId) < 1) mapId = "1"; 
            FileStream file = new FileStream("res/map" + mapId + ".txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string map_data;
            string[] blocks;
            for (int i = 0; i < 16; i++)
            {
                map_data = sr.ReadLine();
                blocks = map_data.Split('\t');      //문자열 자르기

                for (int j = 0; j < 7; j++)
                {
                    PictureBox block = new PictureBox();
                    switch (Convert.ToInt32(blocks[j]))
                    {
                        case 1:
                            int blockid = random.Next(0, 4) + 1;
                            switch(blockid)
                            {
                                case 1:
                                    block.BackgroundImage = new Bitmap("res/block_basic.png");
                                    break;
                                case 2:
                                    block.BackgroundImage = new Bitmap("res/block_color1.png");
                                    break;
                                case 3:
                                    block.BackgroundImage = new Bitmap("res/block_color2.png");
                                    break;
                                case 4:
                                    block.BackgroundImage = new Bitmap("res/block_color3.png");
                                    break;
                            }
                            //block.BackgroundImage = new Bitmap("res/block_basic.png");
                            block.Tag = "1";
                            break;
                        case 2:
                            block.BackgroundImage = new Bitmap("res/block_stone.png");
                            block.Tag = "2";
                            break;
                        case 3:
                            block.BackgroundImage = new Bitmap("res/block_iron.png");
                            block.Tag = "3";
                            break;
                        case 4:
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

            sr.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillEllipse(new SolidBrush(Color.Yellow), ball);                  //공 색깔 Yellow
        }

        public void timer1_Tick(object sender, EventArgs e)
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

                if(Form1.formName == "StageMode")
                {
                    // hide main form
                    this.Hide();

                    // show other form
                    SelectStage selectStage = new SelectStage();
                    selectStage.ShowDialog();
                    selectStage.Location = this.Location;

                    // close application
                    this.Close();
                }
                else
                {
                    // hide main form
                    this.Hide();

                    // show other form
                    ChallengeMode challengeMode = new ChallengeMode();
                    challengeMode.ShowDialog();
                    challengeMode.Location = this.Location;

                    // close application
                    this.Close();
                }
            }
            else if ((ball.Left <= rc.Left || ball.Right >= rc.Right) || IsBumpedX(bar))
            {
                ball_x = -ball_x;
            }
            else if (ball.Top < rc.Top)
            {
                ball_y = -ball_y;
            }
            else if(IsBumpedY(bar)) //공 바에 튕길 때 각도 조절
            {
                int ballc = ball.Right - (ball.Width / 2);
                switch((ballc - bar.Left) / (bar.Width / 8) + 1)
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
                if(item1_on >= 0)
                {
                    ball_x = ball_x * 3 / 2;
                    ball_y = ball_y * 3 / 2;
                }
            }

            int blockNum = 0;
            if (item4_on >= 0)
            {
                //벽돌 맞을 경우
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is PictureBox)
                    {
                        blockNum++;
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
                                case 4:     //공 무적
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
                        blockNum++;
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
                                case 4:     //공 무적
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
            if (blockNum <= 0) GameClear();

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

        protected void GameClear()
        {
            if (!run) return;
            gameTimer.Stop();
            itemTimer.Stop();
            itemConTimer.Stop();
            MessageBox.Show("스테이지 클리어!");
            if(Convert.ToInt32(mapId) >= 15)
            {
                MessageBox.Show("마지막 스테이지입니다!");
                // hide main form
                this.Hide();

                // show other form
                SelectStage selectStage = new SelectStage();
                selectStage.Location = this.Location;
                selectStage.ShowDialog();

                // close application
                this.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("다음 스테이지로 넘어가시겠습니까?", "GameClear", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // hide main form
                    this.Hide();

                    // show other form
                    StageMode stageMode = new StageMode();
                    StageMode.formName = "StageMode";
                    StageMode.mapId = (Convert.ToInt32(mapId) + 1).ToString();
                    stageMode.Location = this.Location;
                    stageMode.ShowDialog();

                    // close application
                    //this.Close();
                }
                else
                {
                    // hide main form
                    this.Hide();

                    // show other form
                    SelectStage selectStage = new SelectStage();
                    selectStage.Location = this.Location;
                    selectStage.ShowDialog();

                    // close application
                    this.Close();
                }
            }
        }

        protected void Item1()
        {
            //ball_x = ball_x * 3 / 2;
            //ball_y = ball_y * 3 / 2;
            item1_on = 10;
        }

        protected void Item1_Remove()
        {
            //ball_x = ball_x * 2 / 3;
            //ball_y = ball_y * 2 / 3;
            item1_on = -1;
        }

        protected void Item2()
        {
            bar.Size = new Size(barWidth / 2, barHeight);
            item2_on = 10;
        }

        protected void Item2_Remove()
        {
            bar.Size = new Size(barWidth, barHeight);
            item2_on = -1;
        }

        protected void Item3()
        {
            bar.Size = new Size(barWidth * 2, barHeight);
            item3_on = 10;
        }

        protected void Item3_Remove()
        {
            bar.Size = new Size(barWidth, barHeight);
            item3_on = -1;
        }
        protected void Item4()
        {
            item4_on = 5;
        }

        protected void Item4_Remove()
        {
            item4_on = -1;
        }

        protected void Form1_KeyDown(object sender, KeyEventArgs e)
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
                        // hide main form
                        this.Hide();

                        // show other form
                        MainForm stageMode = new MainForm();
                        stageMode.Location = this.Location;
                        stageMode.ShowDialog();

                        // close application
                        this.Close();
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

        protected bool IsBumpedX(PictureBox pb)
        {
            if((ball.Right >= pb.Left && ball.Left < pb.Right) 
                || (ball.Left <= pb.Right && ball.Right > pb.Left))
            {
                if(ball.Top > pb.Top - ball.Height/2 && ball.Bottom < pb.Bottom + ball.Height/2)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsBumpedY(PictureBox pb)
        {
            if((ball.Top <= pb.Bottom && ball.Bottom > pb.Top)
                || (ball.Bottom >= pb.Top && ball.Top < pb.Bottom))
            {
                if(ball.Left > pb.Left - ball.Width/2 && ball.Right < pb.Right + ball.Width/2)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsBumpedX(TextBox pb)
        {
            if ((ball.Right >= pb.Left && ball.Left < pb.Right)
                || (ball.Left <= pb.Right && ball.Right > pb.Left))
            {
                if (ball.Top > pb.Top - ball.Height / 2 && ball.Bottom < pb.Bottom + ball.Height / 2)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsBumpedY(TextBox pb)
        {
            if ((ball.Top <= pb.Bottom && ball.Bottom > pb.Top)
                || (ball.Bottom >= pb.Top && ball.Top < pb.Bottom))
            {
                if (ball.Left > pb.Left - ball.Width / 2 && ball.Right < pb.Right + ball.Width / 2)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsBumpedX(Button pb)
        {
            if ((ball.Right >= pb.Left && ball.Left < pb.Right)
                || (ball.Left <= pb.Right && ball.Right > pb.Left))
            {
                if (ball.Top > pb.Top - ball.Height / 2 && ball.Bottom < pb.Bottom + ball.Height / 2)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsBumpedY(Button pb)
        {
            if ((ball.Top <= pb.Bottom && ball.Bottom > pb.Top)
                || (ball.Bottom >= pb.Top && ball.Top < pb.Bottom))
            {
                if (ball.Left > pb.Left - ball.Width / 2 && ball.Right < pb.Right + ball.Width / 2)
                {
                    return true;
                }
            }
            return false;
        }

        protected void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        protected void timer2_Tick(object sender, EventArgs e)
        {
            if(run)
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button)
                    {
                        ctrl.Top += 3; //아이템 떨어지는 속도
                        if (ctrl.Bottom >= this.ClientRectangle.Bottom) this.Controls.Remove(ctrl);
                    }
                }
            }
        }

        protected void timer3_Tick(object sender, EventArgs e)
        {
            if(run)
            {
                if (item1_on == 0)
                {
                    itemState.Text = "";
                    Item1_Remove();
                }
                if (item2_on == 0)
                {
                    Item2_Remove();
                }
                if (item3_on == 0)
                {
                    Item3_Remove();
                }
                if (item4_on == 0)
                {
                    Item4_Remove();
                }
                //--------------------------
                if (item1_on > 0)
                {
                    itemState.Text = "[ 공 빨라짐 (" + item1_on + ")]";
                    item1_on--;
                }
                else
                {
                    itemState.Text = "";
                }
                if (item2_on > 0)
                {
                    itemState.Text = itemState.Text + "[ 바 짧아짐 (" + item2_on + ")]";
                    item2_on--;
                }
                if (item3_on > 0)
                {
                    itemState.Text = itemState.Text + "[ 바 길어짐 (" + item3_on + ")]";
                    item3_on--;
                }
                if (item4_on > 0)
                {
                    itemState.Text = itemState.Text + "[ 공 무적효과 (" + item4_on + ")]";
                    item4_on--;
                }
            }
        }

        protected void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(run)
            {
                int point = e.X;
                bar.Left = point - (bar.Width / 2);
            }
        }

        protected override void OnNotifyMessage(Message m)
        {
            if(m.Msg != 0x14)
                base.OnNotifyMessage(m);
        }

        protected void ItemDrop(int left, int right, int bottom, int top)
        {
            int itemSize = 20;
            int itemLeft = left + ((right - left) / 2) - (itemSize / 2);
            int itemTop = top + ((bottom - top) / 2) - (itemSize / 2);

            Button itemBtn = new Button();
            itemBtn.Enabled = false;
            itemBtn.FlatStyle = FlatStyle.Flat;
            int itemNum = random.Next(0, 30) + 1; //1~N중 1~5일 때만 아이템 드롭
            //int itemNum = 1; //아이템 매번 발생(테스트용)
            switch(itemNum)
            {
                case 1: //공 빨라짐
                    itemBtn.BackgroundImage = new Bitmap("res/item1_ballfast.png");
                    itemBtn.Tag = "1";
                    break;
                case 2: //바 짧아짐
                    itemBtn.BackgroundImage = new Bitmap("res/item2_barshort.png");
                    itemBtn.Tag = "2";
                    break;
                case 3: //바 길어짐
                    itemBtn.BackgroundImage = new Bitmap("res/item3_barlong.png");
                    itemBtn.Tag = "3";
                    break;
                case 4: //공 많아짐
                    itemBtn.BackgroundImage = new Bitmap("res/item4_superball.png");
                    itemBtn.Tag = "4";
                    break;
                case 5: //랜덤
                    itemBtn.BackgroundImage = new Bitmap("res/item5_random.png");
                    itemBtn.Tag = (random.Next(0, 4) + 1).ToString();
                    break;
                default:
                    return;
            }
            itemBtn.BackgroundImageLayout = ImageLayout.Stretch;
            itemBtn.Width = itemSize;
            itemBtn.Height = itemSize;
            itemBtn.Left = itemLeft;
            itemBtn.Top = itemTop;
            this.Controls.Add(itemBtn);
        }
    }
}
