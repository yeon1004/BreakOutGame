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
    public partial class SelectStage : Form
    {
        Button[] stageBtns;

        public SelectStage()
        {
            InitializeComponent();

            stageBtns = new Button[15];

            for(int i = 0; i < 15; i++)
            { 
                stageBtns[i] = new Button();
                stageBtns[i].Text = (i+1).ToString();
                stageBtns[i].BackColor = Color.FromArgb(20, 20, 20);
                stageBtns[i].ForeColor = Color.White;
                stageBtns[i].FlatStyle = FlatStyle.Flat;
                stageBtns[i].FlatAppearance.BorderSize = 0; 
                stageBtns[i].Font = new Font(new FontFamily("돋움"), 20, FontStyle.Bold);
                stageBtns[i].Width = this.ClientRectangle.Width / 3;
                stageBtns[i].Height = (this.ClientRectangle.Height * 3 / 4) / 5;
                stageBtns[i].Margin = new Padding(0);
                stageBtns[i].Click += BtnStage_Click;
            }

            int stg = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    stageBtns[stg].Left = stageBtns[i].Width * j;
                    stageBtns[stg].Top = stageBtns[i].Height * i + (this.ClientRectangle.Height / 4);
                    this.Controls.Add(stageBtns[stg++]);
                }
            }
        }

        private void BtnStage_Click(object sender, EventArgs e)
        {
            this.Close();
            for (int i = 0; i < 15; i++)
            {
                if ((Button)sender == stageBtns[i])
                {
                    StageMode.mapId = (i + 1).ToString();
                    break;
                }
            }
            // hide main form
            this.Hide();

            // show other form
            StageMode stageMode = new StageMode();
            StageMode.formName = "StageMode";
            stageMode.ShowDialog();
            stageMode.Location = this.Location;

            // close application
            this.Close();
        }

        private void SelectStage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // hide main form
            this.Hide();

            // show other form
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
            mainForm.Location = this.Location;

            // close application
            this.Close();
        }
    }
}
