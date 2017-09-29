namespace Algorithms
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
            this.btnHannuo = new System.Windows.Forms.Button();
            this.btnCalPrimeNumber = new System.Windows.Forms.Button();
            this.btnPyramid = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHannuo
            // 
            this.btnHannuo.Location = new System.Drawing.Point(13, 13);
            this.btnHannuo.Name = "btnHannuo";
            this.btnHannuo.Size = new System.Drawing.Size(75, 23);
            this.btnHannuo.TabIndex = 0;
            this.btnHannuo.Text = "汉诺";
            this.btnHannuo.UseVisualStyleBackColor = true;
            this.btnHannuo.Click += new System.EventHandler(this.btnHannuo_Click);
            // 
            // btnCalPrimeNumber
            // 
            this.btnCalPrimeNumber.Location = new System.Drawing.Point(94, 13);
            this.btnCalPrimeNumber.Name = "btnCalPrimeNumber";
            this.btnCalPrimeNumber.Size = new System.Drawing.Size(75, 23);
            this.btnCalPrimeNumber.TabIndex = 1;
            this.btnCalPrimeNumber.Text = "求质数";
            this.btnCalPrimeNumber.UseVisualStyleBackColor = true;
            this.btnCalPrimeNumber.Click += new System.EventHandler(this.btnCalPrimeNumber_Click);
            // 
            // btnPyramid
            // 
            this.btnPyramid.Location = new System.Drawing.Point(175, 13);
            this.btnPyramid.Name = "btnPyramid";
            this.btnPyramid.Size = new System.Drawing.Size(75, 23);
            this.btnPyramid.TabIndex = 2;
            this.btnPyramid.Text = "金字塔";
            this.btnPyramid.UseVisualStyleBackColor = true;
            this.btnPyramid.Click += new System.EventHandler(this.btnPyramid_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 461);
            this.Controls.Add(this.btnPyramid);
            this.Controls.Add(this.btnCalPrimeNumber);
            this.Controls.Add(this.btnHannuo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHannuo;
        private System.Windows.Forms.Button btnCalPrimeNumber;
        private System.Windows.Forms.Button btnPyramid;
    }
}

