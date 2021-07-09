namespace Wallpaper
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.Sequence_RB = new System.Windows.Forms.RadioButton();
            this.Random_RB = new System.Windows.Forms.RadioButton();
            this.Dont_RB = new System.Windows.Forms.RadioButton();
            this.Timer_Random = new System.Windows.Forms.Timer(this.components);
            this.ICO = new System.Windows.Forms.NotifyIcon(this.components);
            this.RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sequence_RB
            // 
            this.Sequence_RB.AutoSize = true;
            this.Sequence_RB.Location = new System.Drawing.Point(265, 167);
            this.Sequence_RB.Name = "Sequence_RB";
            this.Sequence_RB.Size = new System.Drawing.Size(71, 16);
            this.Sequence_RB.TabIndex = 0;
            this.Sequence_RB.TabStop = true;
            this.Sequence_RB.Text = "顺序播放";
            this.Sequence_RB.UseVisualStyleBackColor = true;
            this.Sequence_RB.CheckedChanged += new System.EventHandler(this.Sequence_RB_CheckedChanged);
            // 
            // Random_RB
            // 
            this.Random_RB.AutoSize = true;
            this.Random_RB.Location = new System.Drawing.Point(265, 189);
            this.Random_RB.Name = "Random_RB";
            this.Random_RB.Size = new System.Drawing.Size(71, 16);
            this.Random_RB.TabIndex = 1;
            this.Random_RB.TabStop = true;
            this.Random_RB.Text = "随机播放";
            this.Random_RB.UseVisualStyleBackColor = true;
            this.Random_RB.CheckedChanged += new System.EventHandler(this.Random_RB_CheckedChanged);
            // 
            // Dont_RB
            // 
            this.Dont_RB.AutoSize = true;
            this.Dont_RB.Location = new System.Drawing.Point(265, 211);
            this.Dont_RB.Name = "Dont_RB";
            this.Dont_RB.Size = new System.Drawing.Size(59, 16);
            this.Dont_RB.TabIndex = 2;
            this.Dont_RB.TabStop = true;
            this.Dont_RB.Text = "不切换";
            this.Dont_RB.UseVisualStyleBackColor = true;
            // 
            // Timer_Random
            // 
            this.Timer_Random.Enabled = true;
            this.Timer_Random.Interval = 2000;
            this.Timer_Random.Tick += new System.EventHandler(this.Timer_Random_Tick);
            // 
            // ICO
            // 
            this.ICO.ContextMenuStrip = this.RightMenu;
            this.ICO.Icon = ((System.Drawing.Icon)(resources.GetObject("ICO.Icon")));
            this.ICO.Visible = true;
            this.ICO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ICO_MouseDoubleClick);
            // 
            // RightMenu
            // 
            this.RightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.RightMenu.Name = "RightMenu";
            this.RightMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 398);
            this.Controls.Add(this.Dont_RB);
            this.Controls.Add(this.Random_RB);
            this.Controls.Add(this.Sequence_RB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "桌面壁纸切换器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
            this.RightMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Sequence_RB;
        private System.Windows.Forms.RadioButton Random_RB;
        private System.Windows.Forms.RadioButton Dont_RB;
        private System.Windows.Forms.Timer Timer_Random;
        private System.Windows.Forms.NotifyIcon ICO;
        private System.Windows.Forms.ContextMenuStrip RightMenu;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}

