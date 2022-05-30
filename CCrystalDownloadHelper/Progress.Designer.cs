
namespace CCrystalDownloadHelper {
    partial class Progress {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lCaption = new System.Windows.Forms.Label();
            this.lBorder = new System.Windows.Forms.Label();
            this.lProgress = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lTooltip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 40);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(440, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            // 
            // lCaption
            // 
            this.lCaption.AutoSize = true;
            this.lCaption.Location = new System.Drawing.Point(12, 17);
            this.lCaption.Name = "lCaption";
            this.lCaption.Size = new System.Drawing.Size(81, 13);
            this.lCaption.TabIndex = 2;
            this.lCaption.Text = "Downloading ...";
            // 
            // lBorder
            // 
            this.lBorder.BackColor = System.Drawing.Color.Transparent;
            this.lBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lBorder.Location = new System.Drawing.Point(0, 0);
            this.lBorder.Name = "lBorder";
            this.lBorder.Size = new System.Drawing.Size(464, 84);
            this.lBorder.TabIndex = 3;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(12, 62);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(0, 13);
            this.lProgress.TabIndex = 4;
            // 
            // lTooltip
            // 
            this.lTooltip.AutoSize = true;
            this.lTooltip.Location = new System.Drawing.Point(442, 17);
            this.lTooltip.Name = "lTooltip";
            this.lTooltip.Size = new System.Drawing.Size(10, 13);
            this.lTooltip.TabIndex = 5;
            this.lTooltip.Text = " ";
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 84);
            this.Controls.Add(this.lTooltip);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.lCaption);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lCaption;
        private System.Windows.Forms.Label lBorder;
        private System.Windows.Forms.Label lProgress;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lTooltip;
    }
}