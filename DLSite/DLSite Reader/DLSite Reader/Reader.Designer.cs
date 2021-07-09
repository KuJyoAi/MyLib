namespace DLSite_Reader
{
    partial class Reader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reader));
            this.rightMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移动到VoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动到桌面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightMain
            // 
            this.rightMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移动到VoiceToolStripMenuItem,
            this.移动到桌面ToolStripMenuItem});
            this.rightMain.Name = "rightMain";
            this.rightMain.Size = new System.Drawing.Size(161, 70);
            this.rightMain.Text = "qwwq";
            this.rightMain.Opening += new System.ComponentModel.CancelEventHandler(this.rightMain_Opening);
            // 
            // 移动到VoiceToolStripMenuItem
            // 
            this.移动到VoiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.voiceToolStripMenuItem,
            this.testToolStripMenuItem,
            this.deCollectionToolStripMenuItem});
            this.移动到VoiceToolStripMenuItem.Name = "移动到VoiceToolStripMenuItem";
            this.移动到VoiceToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.移动到VoiceToolStripMenuItem.Text = "移入";
            // 
            // voiceToolStripMenuItem
            // 
            this.voiceToolStripMenuItem.Name = "voiceToolStripMenuItem";
            this.voiceToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.voiceToolStripMenuItem.Text = "Voice";
            this.voiceToolStripMenuItem.Click += new System.EventHandler(this.voiceToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // 移动到桌面ToolStripMenuItem
            // 
            this.移动到桌面ToolStripMenuItem.Name = "移动到桌面ToolStripMenuItem";
            this.移动到桌面ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.移动到桌面ToolStripMenuItem.Text = "复制到桌面存库";
            this.移动到桌面ToolStripMenuItem.Click += new System.EventHandler(this.移动到桌面ToolStripMenuItem_Click);
            // 
            // deCollectionToolStripMenuItem
            // 
            this.deCollectionToolStripMenuItem.Name = "deCollectionToolStripMenuItem";
            this.deCollectionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deCollectionToolStripMenuItem.Text = "DeCollection";
            this.deCollectionToolStripMenuItem.Click += new System.EventHandler(this.deCollectionToolStripMenuItem_Click);
            // 
            // Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1664, 801);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reader";
            this.Text = "Voice Cover Reader";
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Reader_KeyPress);
            this.rightMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip rightMain;
        private System.Windows.Forms.ToolStripMenuItem 移动到VoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动到桌面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deCollectionToolStripMenuItem;
    }
}

