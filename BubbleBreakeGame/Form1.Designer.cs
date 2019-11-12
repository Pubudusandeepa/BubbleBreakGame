namespace BubbleBreakeGame
{
    partial class frmBubbleBreaker
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
            this.label2 = new System.Windows.Forms.Label();
            this.lblInf = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 0;
            // 
            // lblInf
            // 
            this.lblInf.AutoSize = true;
            this.lblInf.Location = new System.Drawing.Point(184, 155);
            this.lblInf.Name = "lblInf";
            this.lblInf.Size = new System.Drawing.Size(0, 13);
            this.lblInf.TabIndex = 1;
            // 
            // frmBubbleBreaker
            // 
            this.ClientSize = new System.Drawing.Size(773, 409);
            this.Controls.Add(this.lblInf);
            this.Controls.Add(this.label2);
            this.Name = "frmBubbleBreaker";
            this.Load += new System.EventHandler(this.frmBubbleBreaker_load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint1);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInf;
    }
}

