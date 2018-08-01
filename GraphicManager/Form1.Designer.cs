namespace GraphicManager
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通常描画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ネガ反転ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.モザイクToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.グレースケールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.エッジ抽出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.青写真ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.セピア調ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ノイズ除去ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.エッジ抽出黒線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.細線化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.表示VToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くToolStripMenuItem,
            this.toolStripMenuItem1,
            this.保存ToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.開くToolStripMenuItem.Text = "開く";
            this.開くToolStripMenuItem.Click += new System.EventHandler(this.開くToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(91, 6);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 表示VToolStripMenuItem
            // 
            this.表示VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通常描画ToolStripMenuItem,
            this.ネガ反転ToolStripMenuItem,
            this.モザイクToolStripMenuItem,
            this.グレースケールToolStripMenuItem,
            this.エッジ抽出ToolStripMenuItem,
            this.青写真ToolStripMenuItem,
            this.セピア調ToolStripMenuItem,
            this.ノイズ除去ToolStripMenuItem,
            this.エッジ抽出黒線ToolStripMenuItem,
            this.細線化ToolStripMenuItem});
            this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
            this.表示VToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.表示VToolStripMenuItem.Text = "表示(&V)";
            // 
            // 通常描画ToolStripMenuItem
            // 
            this.通常描画ToolStripMenuItem.Name = "通常描画ToolStripMenuItem";
            this.通常描画ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.通常描画ToolStripMenuItem.Text = "通常描画";
            this.通常描画ToolStripMenuItem.Click += new System.EventHandler(this.通常描画ToolStripMenuItem_Click);
            // 
            // ネガ反転ToolStripMenuItem
            // 
            this.ネガ反転ToolStripMenuItem.Name = "ネガ反転ToolStripMenuItem";
            this.ネガ反転ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ネガ反転ToolStripMenuItem.Text = "ネガ反転";
            this.ネガ反転ToolStripMenuItem.Click += new System.EventHandler(this.ネガ反転ToolStripMenuItem_Click);
            // 
            // モザイクToolStripMenuItem
            // 
            this.モザイクToolStripMenuItem.Name = "モザイクToolStripMenuItem";
            this.モザイクToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.モザイクToolStripMenuItem.Text = "モザイク";
            this.モザイクToolStripMenuItem.Click += new System.EventHandler(this.モザイクToolStripMenuItem_Click);
            // 
            // グレースケールToolStripMenuItem
            // 
            this.グレースケールToolStripMenuItem.Name = "グレースケールToolStripMenuItem";
            this.グレースケールToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.グレースケールToolStripMenuItem.Text = "グレースケール";
            this.グレースケールToolStripMenuItem.Click += new System.EventHandler(this.グレースケールToolStripMenuItem_Click);
            // 
            // エッジ抽出ToolStripMenuItem
            // 
            this.エッジ抽出ToolStripMenuItem.Name = "エッジ抽出ToolStripMenuItem";
            this.エッジ抽出ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.エッジ抽出ToolStripMenuItem.Text = "エッジ抽出";
            this.エッジ抽出ToolStripMenuItem.Click += new System.EventHandler(this.エッジ抽出ToolStripMenuItem_Click);
            // 
            // 青写真ToolStripMenuItem
            // 
            this.青写真ToolStripMenuItem.Name = "青写真ToolStripMenuItem";
            this.青写真ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.青写真ToolStripMenuItem.Text = "青写真";
            this.青写真ToolStripMenuItem.Click += new System.EventHandler(this.青写真ToolStripMenuItem_Click);
            // 
            // セピア調ToolStripMenuItem
            // 
            this.セピア調ToolStripMenuItem.Name = "セピア調ToolStripMenuItem";
            this.セピア調ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.セピア調ToolStripMenuItem.Text = "セピア調";
            this.セピア調ToolStripMenuItem.Click += new System.EventHandler(this.セピア調ToolStripMenuItem_Click);
            // 
            // ノイズ除去ToolStripMenuItem
            // 
            this.ノイズ除去ToolStripMenuItem.Name = "ノイズ除去ToolStripMenuItem";
            this.ノイズ除去ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ノイズ除去ToolStripMenuItem.Text = "ノイズ除去";
            this.ノイズ除去ToolStripMenuItem.Click += new System.EventHandler(this.ノイズ除去ToolStripMenuItem_Click);
            // 
            // エッジ抽出黒線ToolStripMenuItem
            // 
            this.エッジ抽出黒線ToolStripMenuItem.Name = "エッジ抽出黒線ToolStripMenuItem";
            this.エッジ抽出黒線ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.エッジ抽出黒線ToolStripMenuItem.Text = "エッジ抽出（黒線）";
            this.エッジ抽出黒線ToolStripMenuItem.Click += new System.EventHandler(this.エッジ抽出黒線ToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "bmp";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // 細線化ToolStripMenuItem
            // 
            this.細線化ToolStripMenuItem.Name = "細線化ToolStripMenuItem";
            this.細線化ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.細線化ToolStripMenuItem.Text = "細線化";
            this.細線化ToolStripMenuItem.Click += new System.EventHandler(this.細線化ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 表示VToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通常描画ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ネガ反転ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem モザイクToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem グレースケールToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem エッジ抽出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 青写真ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem セピア調ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ノイズ除去ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem エッジ抽出黒線ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 細線化ToolStripMenuItem;
    }
}

