namespace BreakoutGame
{
    partial class MainForm
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
            this.title = new System.Windows.Forms.Label();
            this.btnStage = new System.Windows.Forms.Button();
            this.BtnChallenge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.title.Location = new System.Drawing.Point(82, 245);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(378, 48);
            this.title.TabIndex = 0;
            this.title.Text = "Breakout Game";
            // 
            // btnStage
            // 
            this.btnStage.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStage.ForeColor = System.Drawing.Color.Black;
            this.btnStage.Location = new System.Drawing.Point(44, 532);
            this.btnStage.Name = "btnStage";
            this.btnStage.Size = new System.Drawing.Size(222, 205);
            this.btnStage.TabIndex = 1;
            this.btnStage.Text = "스테이지 모드";
            this.btnStage.UseVisualStyleBackColor = true;
            this.btnStage.Click += new System.EventHandler(this.btnStage_Click);
            // 
            // BtnChallenge
            // 
            this.BtnChallenge.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnChallenge.ForeColor = System.Drawing.Color.Black;
            this.BtnChallenge.Location = new System.Drawing.Point(272, 532);
            this.BtnChallenge.Name = "BtnChallenge";
            this.BtnChallenge.Size = new System.Drawing.Size(222, 205);
            this.BtnChallenge.TabIndex = 2;
            this.BtnChallenge.Text = "도전 모드";
            this.BtnChallenge.UseVisualStyleBackColor = true;
            this.BtnChallenge.Click += new System.EventHandler(this.BtnChallenge_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(539, 761);
            this.Controls.Add(this.BtnChallenge);
            this.Controls.Add(this.btnStage);
            this.Controls.Add(this.title);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximumSize = new System.Drawing.Size(555, 800);
            this.MinimumSize = new System.Drawing.Size(555, 800);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button btnStage;
        private System.Windows.Forms.Button BtnChallenge;
    }
}