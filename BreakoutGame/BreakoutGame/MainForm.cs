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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStage_Click(object sender, EventArgs e)
        {
            this.Close();
            new System.Threading.Thread(() =>
            {
                Application.Run(new SelectStage());
            }).Start();
        }

        private void BtnChallenge_Click(object sender, EventArgs e)
        {
            this.Close();
            new System.Threading.Thread(() =>
            {
                Application.Run(new ChallengeMode());
            }).Start();
        }
}
}
