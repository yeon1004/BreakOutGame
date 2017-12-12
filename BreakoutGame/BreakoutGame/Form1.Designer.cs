namespace BreakoutGame
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtBar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startMsg = new System.Windows.Forms.Label();
            this.itemState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBar
            // 
            this.txtBar.BackColor = System.Drawing.Color.White;
            this.txtBar.Enabled = false;
            this.txtBar.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBar.Location = new System.Drawing.Point(3, 711);
            this.txtBar.Name = "txtBar";
            this.txtBar.ReadOnly = true;
            this.txtBar.Size = new System.Drawing.Size(100, 20);
            this.txtBar.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(3, 747);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Space bar : 시작/멈춤, Esc : 나가기";
            // 
            // startMsg
            // 
            this.startMsg.AutoSize = true;
            this.startMsg.BackColor = System.Drawing.Color.Transparent;
            this.startMsg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startMsg.Location = new System.Drawing.Point(3, 628);
            this.startMsg.Name = "startMsg";
            this.startMsg.Size = new System.Drawing.Size(171, 12);
            this.startMsg.TabIndex = 6;
            this.startMsg.Text = "Spase bar를 눌러 시작하세요.";
            // 
            // itemState
            // 
            this.itemState.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.itemState.BackColor = System.Drawing.Color.Transparent;
            this.itemState.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.itemState.Location = new System.Drawing.Point(207, 747);
            this.itemState.Name = "itemState";
            this.itemState.Size = new System.Drawing.Size(329, 12);
            this.itemState.TabIndex = 7;
            this.itemState.Text = "\r\n";
            this.itemState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(539, 761);
            this.Controls.Add(this.itemState);
            this.Controls.Add(this.startMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(555, 800);
            this.MinimumSize = new System.Drawing.Size(555, 800);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 311);
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        //private System.Windows.Forms.TextBox bar;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label startMsg;
        private System.Windows.Forms.TextBox txtBar;
        private System.Windows.Forms.Label itemState;
    }
}

