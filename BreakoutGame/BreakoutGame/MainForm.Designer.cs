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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStage = new System.Windows.Forms.Button();
            this.BtnChallenge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStage
            // 
            this.btnStage.BackColor = System.Drawing.Color.LightGray;
            this.btnStage.BackgroundImage = global::BreakoutGame.Properties.Resources.button11;
            this.btnStage.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStage.ForeColor = System.Drawing.Color.Black;
            this.btnStage.Location = new System.Drawing.Point(64, 544);
            this.btnStage.Name = "btnStage";
            this.btnStage.Size = new System.Drawing.Size(191, 157);
            this.btnStage.TabIndex = 1;
            this.btnStage.UseVisualStyleBackColor = false;
            this.btnStage.Click += new System.EventHandler(this.btnStage_Click);
            // 
            // BtnChallenge
            // 
            this.BtnChallenge.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnChallenge.BackgroundImage = global::BreakoutGame.Properties.Resources.challenge;
            this.BtnChallenge.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnChallenge.ForeColor = System.Drawing.Color.Black;
            this.BtnChallenge.Location = new System.Drawing.Point(292, 544);
            this.BtnChallenge.Name = "BtnChallenge";
            this.BtnChallenge.Size = new System.Drawing.Size(191, 157);
            this.BtnChallenge.TabIndex = 2;
            this.BtnChallenge.UseVisualStyleBackColor = false;
            this.BtnChallenge.Click += new System.EventHandler(this.BtnChallenge_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::BreakoutGame.Properties.Resources.bgImage;
            this.ClientSize = new System.Drawing.Size(539, 761);
            this.Controls.Add(this.BtnChallenge);
            this.Controls.Add(this.btnStage);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(555, 800);
            this.MinimumSize = new System.Drawing.Size(555, 800);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStage;
        private System.Windows.Forms.Button BtnChallenge;
    }
}