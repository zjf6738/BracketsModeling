﻿namespace WindowsFormsApplication2
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_numOfPoint = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.格网 = new System.Windows.Forms.ToolStripMenuItem();
            this.网格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.托槽编辑工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.增加结点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拉伸工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_filename = new System.Windows.Forms.TextBox();
            this.bt_subdivision = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.state = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.格网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderWindowControl1
            // 
            this.renderWindowControl1.AddTestActors = false;
            this.renderWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.renderWindowControl1.Location = new System.Drawing.Point(3, 91);
            this.renderWindowControl1.Name = "renderWindowControl1";
            this.renderWindowControl1.Size = new System.Drawing.Size(588, 418);
            this.renderWindowControl1.TabIndex = 0;
            this.renderWindowControl1.TestText = null;
            this.renderWindowControl1.Load += new System.EventHandler(this.renderWindowControl1_Load);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(617, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "点数：";
            // 
            // tb_numOfPoint
            // 
            this.tb_numOfPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_numOfPoint.Enabled = false;
            this.tb_numOfPoint.Location = new System.Drawing.Point(675, 121);
            this.tb_numOfPoint.Name = "tb_numOfPoint";
            this.tb_numOfPoint.Size = new System.Drawing.Size(100, 21);
            this.tb_numOfPoint.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.格网,
            this.托槽编辑工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(787, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 格网
            // 
            this.格网.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.网格ToolStripMenuItem});
            this.格网.Name = "格网";
            this.格网.Size = new System.Drawing.Size(41, 20);
            this.格网.Text = "显示";
            // 
            // 网格ToolStripMenuItem
            // 
            this.网格ToolStripMenuItem.Name = "网格ToolStripMenuItem";
            this.网格ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.网格ToolStripMenuItem.Text = "网格";
            this.网格ToolStripMenuItem.Click += new System.EventHandler(this.网格ToolStripMenuItem_Click);
            // 
            // 托槽编辑工具ToolStripMenuItem
            // 
            this.托槽编辑工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.增加结点ToolStripMenuItem,
            this.拉伸工具ToolStripMenuItem});
            this.托槽编辑工具ToolStripMenuItem.Name = "托槽编辑工具ToolStripMenuItem";
            this.托槽编辑工具ToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.托槽编辑工具ToolStripMenuItem.Text = "托槽编辑工具";
            // 
            // 增加结点ToolStripMenuItem
            // 
            this.增加结点ToolStripMenuItem.Name = "增加结点ToolStripMenuItem";
            this.增加结点ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.增加结点ToolStripMenuItem.Text = "增加结点";
            this.增加结点ToolStripMenuItem.Click += new System.EventHandler(this.增加结点ToolStripMenuItem_Click);
            // 
            // 拉伸工具ToolStripMenuItem
            // 
            this.拉伸工具ToolStripMenuItem.Name = "拉伸工具ToolStripMenuItem";
            this.拉伸工具ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.拉伸工具ToolStripMenuItem.Text = "拉伸工具";
            this.拉伸工具ToolStripMenuItem.Click += new System.EventHandler(this.拉伸工具ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "stl文件（*.stl）|*.stl|所有文件（*.*）|*.*";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(599, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "打开文件名：";
            // 
            // tb_filename
            // 
            this.tb_filename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_filename.Enabled = false;
            this.tb_filename.Location = new System.Drawing.Point(675, 91);
            this.tb_filename.Name = "tb_filename";
            this.tb_filename.Size = new System.Drawing.Size(100, 21);
            this.tb_filename.TabIndex = 5;
            // 
            // bt_subdivision
            // 
            this.bt_subdivision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_subdivision.Location = new System.Drawing.Point(675, 157);
            this.bt_subdivision.Name = "bt_subdivision";
            this.bt_subdivision.Size = new System.Drawing.Size(87, 23);
            this.bt_subdivision.TabIndex = 6;
            this.bt_subdivision.Text = "subdivision";
            this.bt_subdivision.UseVisualStyleBackColor = true;
            this.bt_subdivision.Click += new System.EventHandler(this.bt_subdivision_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.state});
            this.statusStrip1.Location = new System.Drawing.Point(0, 512);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(787, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // state
            // 
            this.state.AutoSize = false;
            this.state.Name = "state";
            this.state.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.state.Size = new System.Drawing.Size(703, 17);
            this.state.Spring = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(660, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 57);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(787, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton1.Text = "打开";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton2.Text = "格网";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.格网ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // 格网ToolStripMenuItem
            // 
            this.格网ToolStripMenuItem.Name = "格网ToolStripMenuItem";
            this.格网ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.格网ToolStripMenuItem.Text = "格网";
            this.格网ToolStripMenuItem.Click += new System.EventHandler(this.格网ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 534);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_filename);
            this.Controls.Add(this.bt_subdivision);
            this.Controls.Add(this.renderWindowControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_numOfPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Kitware.VTK.RenderWindowControl renderWindowControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_numOfPoint;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 格网;
        private System.Windows.Forms.ToolStripMenuItem 网格ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_filename;
        private System.Windows.Forms.Button bt_subdivision;
        private System.Windows.Forms.ToolStripMenuItem 托槽编辑工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 增加结点ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel state;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 拉伸工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 格网ToolStripMenuItem;
    }
}

