namespace Decimal_Conversion
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
            this.BinBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OctalBox = new System.Windows.Forms.TextBox();
            this.DecimalBox = new System.Windows.Forms.TextBox();
            this.HexBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BinBox
            // 
            this.BinBox.Location = new System.Drawing.Point(55, 6);
            this.BinBox.Name = "BinBox";
            this.BinBox.Size = new System.Drawing.Size(277, 21);
            this.BinBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bin";
            // 
            // OctalBox
            // 
            this.OctalBox.Location = new System.Drawing.Point(55, 33);
            this.OctalBox.Name = "OctalBox";
            this.OctalBox.Size = new System.Drawing.Size(277, 21);
            this.OctalBox.TabIndex = 2;
            // 
            // DecimalBox
            // 
            this.DecimalBox.Location = new System.Drawing.Point(55, 60);
            this.DecimalBox.Name = "DecimalBox";
            this.DecimalBox.Size = new System.Drawing.Size(277, 21);
            this.DecimalBox.TabIndex = 3;
            // 
            // HexBox
            // 
            this.HexBox.Location = new System.Drawing.Point(55, 87);
            this.HexBox.Name = "HexBox";
            this.HexBox.Size = new System.Drawing.Size(277, 21);
            this.HexBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Octal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Decimal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Hex";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(131, 132);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 43);
            this.Start.TabIndex = 8;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 211);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HexBox);
            this.Controls.Add(this.DecimalBox);
            this.Controls.Add(this.OctalBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BinBox);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BinBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OctalBox;
        private System.Windows.Forms.TextBox DecimalBox;
        private System.Windows.Forms.TextBox HexBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Start;
    }
}

