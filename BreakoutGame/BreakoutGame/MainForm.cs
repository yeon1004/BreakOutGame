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
            // hide main form
            this.Hide();

            // show other form
            SelectStage selectStage = new SelectStage();
            selectStage.Location = this.Location;
            selectStage.ShowDialog();

            // close application
            this.Close();
        }

        private void BtnChallenge_Click(object sender, EventArgs e)
        {
            // hide main form
            this.Hide();

            // show other form
            ChallengeMode challengeMode = new ChallengeMode();
            ChallengeMode.formName = "ChallengeMode";
            challengeMode.Location = this.Location;
            challengeMode.ShowDialog();

            // close application
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
