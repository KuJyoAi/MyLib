namespace Physical_World
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Sun = new System.Windows.Forms.PictureBox();
            this.Earth = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Moon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Sun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Earth)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Moon)).BeginInit();
            this.SuspendLayout();
            // 
            // Sun
            // 
            this.Sun.BackColor = System.Drawing.SystemColors.WindowText;
            this.Sun.Location = new System.Drawing.Point(505, 270);
            this.Sun.Name = "Sun";
            this.Sun.Size = new System.Drawing.Size(50, 47);
            this.Sun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Sun.TabIndex = 0;
            this.Sun.TabStop = false;
            // 
            // Earth
            // 
            this.Earth.BackColor = System.Drawing.SystemColors.WindowText;
            this.Earth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Earth.Location = new System.Drawing.Point(206, 205);
            this.Earth.Name = "Earth";
            this.Earth.Size = new System.Drawing.Size(24, 26);
            this.Earth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Earth.TabIndex = 1;
            this.Earth.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Moon);
            this.groupBox1.Controls.Add(this.Earth);
            this.groupBox1.Controls.Add(this.Sun);
            this.groupBox1.Location = new System.Drawing.Point(61, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1120, 785);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Moon
            // 
            this.Moon.BackColor = System.Drawing.SystemColors.WindowText;
            this.Moon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Moon.Location = new System.Drawing.Point(164, 244);
            this.Moon.Name = "Moon";
            this.Moon.Size = new System.Drawing.Size(15, 16);
            this.Moon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Moon.TabIndex = 2;
            this.Moon.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 809);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Earth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Moon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Sun;
        private System.Windows.Forms.PictureBox Earth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox Moon;
    }
}

