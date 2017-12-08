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
        public StageMode()
        {
            InitializeComponent();
            DrawMap(mapId);
        }

        private void StageMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
