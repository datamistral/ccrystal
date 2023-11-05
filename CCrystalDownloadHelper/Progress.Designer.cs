
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
            this.lUrl = new System.Windows.Forms.Label();
            this.lBorder = new System.Windows.Forms.Label();
            this.lProgress = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lTooltip = new System.Windows.Forms.Label();
            this.tUrl = new System.Windows.Forms.TextBox();
            this.bDownload = new System.Windows.Forms.Button();
            this.lCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 86);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(545, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // lUrl
            // 
            this.lUrl.AutoSize = true;
            this.lUrl.Location = new System.Drawing.Point(12, 53);
            this.lUrl.Name = "lUrl";
            this.lUrl.Size = new System.Drawing.Size(20, 13);
            this.lUrl.TabIndex = 2;
            this.lUrl.Text = "Url";
            // 
            // lBorder
            // 
            this.lBorder.BackColor = System.Drawing.Color.Transparent;
            this.lBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lBorder.Location = new System.Drawing.Point(0, 0);
            this.lBorder.Name = "lBorder";
            this.lBorder.Size = new System.Drawing.Size(569, 130);
            this.lBorder.TabIndex = 3;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(12, 108);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(48, 13);
            this.lProgress.TabIndex = 4;
            this.lProgress.Text = "Progress";
            // 
            // lTooltip
            // 
            this.lTooltip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lTooltip.AutoSize = true;
            this.lTooltip.Location = new System.Drawing.Point(442, 63);
            this.lTooltip.Name = "lTooltip";
            this.lTooltip.Size = new System.Drawing.Size(10, 13);
            this.lTooltip.TabIndex = 5;
            this.lTooltip.Text = " ";
            // 
            // tUrl
            // 
            this.tUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUrl.Location = new System.Drawing.Point(38, 49);
            this.tUrl.Name = "tUrl";
            this.tUrl.Size = new System.Drawing.Size(438, 20);
            this.tUrl.TabIndex = 6;
            // 
            // bDownload
            // 
            this.bDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDownload.Location = new System.Drawing.Point(482, 49);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(75, 23);
            this.bDownload.TabIndex = 7;
            this.bDownload.Text = "Download";
            this.bDownload.UseVisualStyleBackColor = true;
            this.bDownload.Click += new System.EventHandler(this.bDownload_Click);
            // 
            // lCaption
            // 
            this.lCaption.AutoSize = true;
            this.lCaption.Location = new System.Drawing.Point(12, 21);
            this.lCaption.Name = "lCaption";
            this.lCaption.Size = new System.Drawing.Size(161, 13);
            this.lCaption.TabIndex = 8;
            this.lCaption.Text = "Download Crystal reports runtime";
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 130);
            this.Controls.Add(this.lCaption);
            this.Controls.Add(this.bDownload);
            this.Controls.Add(this.tUrl);
            this.Controls.Add(this.lTooltip);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.lUrl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Progress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lUrl;
        private System.Windows.Forms.Label lBorder;
        private System.Windows.Forms.Label lProgress;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lTooltip;
        private System.Windows.Forms.TextBox tUrl;
        private System.Windows.Forms.Button bDownload;
        private System.Windows.Forms.Label lCaption;
    }
}