namespace BreakoutGame
{
    partial class Rank
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rank));
            this.dgvRank = new System.Windows.Forms.DataGridView();
            this.rankNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnToMain = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRank)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRank
            // 
            this.dgvRank.AllowUserToAddRows = false;
            this.dgvRank.AllowUserToDeleteRows = false;
            this.dgvRank.AllowUserToResizeColumns = false;
            this.dgvRank.AllowUserToResizeRows = false;
            this.dgvRank.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvRank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rankNum,
            this.userName,
            this.userScore});
            this.dgvRank.Location = new System.Drawing.Point(84, 185);
            this.dgvRank.Name = "dgvRank";
            this.dgvRank.ReadOnly = true;
            this.dgvRank.RowTemplate.Height = 23;
            this.dgvRank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRank.Size = new System.Drawing.Size(365, 421);
            this.dgvRank.TabIndex = 0;
            // 
            // rankNum
            // 
            this.rankNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.rankNum.HeaderText = "순위";
            this.rankNum.Name = "rankNum";
            this.rankNum.ReadOnly = true;
            this.rankNum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.rankNum.Width = 107;
            // 
            // userName
            // 
            this.userName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.userName.HeaderText = "이름";
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            this.userName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // userScore
            // 
            this.userScore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.userScore.HeaderText = "점수";
            this.userScore.Name = "userScore";
            this.userScore.ReadOnly = true;
            this.userScore.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btnToMain
            // 
            this.btnToMain.BackgroundImage = global::BreakoutGame.Properties.Resources.main2;
            this.btnToMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnToMain.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnToMain.ForeColor = System.Drawing.Color.Black;
            this.btnToMain.Location = new System.Drawing.Point(84, 665);
            this.btnToMain.Name = "btnToMain";
            this.btnToMain.Size = new System.Drawing.Size(365, 51);
            this.btnToMain.TabIndex = 2;
            this.btnToMain.UseVisualStyleBackColor = true;
            this.btnToMain.Click += new System.EventHandler(this.btnToMain_Click);
            // 
            // Rank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::BreakoutGame.Properties.Resources.rank;
            this.ClientSize = new System.Drawing.Size(539, 761);
            this.Controls.Add(this.btnToMain);
            this.Controls.Add(this.dgvRank);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(555, 800);
            this.MinimumSize = new System.Drawing.Size(555, 800);
            this.Name = "Rank";
            this.Text = "Rank";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Rank_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn userScore;
        private System.Windows.Forms.Button btnToMain;
    }
}