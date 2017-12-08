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
    public partial class Rank : Form
    {
        public static int newRank;
        public Rank()
        {
            InitializeComponent();

            FileStream file = new FileStream("rank.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string rank_data;
            string[][] rankList = new string[10][];

            for (int i = 0; i < 10; i++)
            {
                rank_data = sr.ReadLine();
                rankList[i] = rank_data.Split('\t');
                sr.ReadLine();

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvRank, rankList[i][0], rankList[i][1], rankList[i][2]);
                row.DefaultCellStyle.BackColor = Color.Black;
                row.DefaultCellStyle.ForeColor = Color.White;
                row.DefaultCellStyle.SelectionBackColor = Color.Black;
                row.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvRank.Rows.Add(row);
            }
            sr.Close();
        }

        private void Rank_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnToMain_Click(object sender, EventArgs e)
        {
            // hide main form
            this.Hide();

            // show other form
            MainForm mainForm = new MainForm();
            mainForm.Location = this.Location;
            mainForm.ShowDialog();

            // close application
            this.Close();
        }
    }
}
