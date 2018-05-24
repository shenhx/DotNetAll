namespace IBatisNetWinformDemo
{
    partial class FormProblemSolutions
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
            this.dgvDataShow = new System.Windows.Forms.DataGridView();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnQuery2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataShow)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDataShow
            // 
            this.dgvDataShow.AllowUserToAddRows = false;
            this.dgvDataShow.AllowUserToDeleteRows = false;
            this.dgvDataShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataShow.Location = new System.Drawing.Point(33, 41);
            this.dgvDataShow.Name = "dgvDataShow";
            this.dgvDataShow.ReadOnly = true;
            this.dgvDataShow.RowTemplate.Height = 23;
            this.dgvDataShow.Size = new System.Drawing.Size(525, 242);
            this.dgvDataShow.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(33, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(128, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询(TypeHandler)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnQuery2
            // 
            this.btnQuery2.Location = new System.Drawing.Point(167, 12);
            this.btnQuery2.Name = "btnQuery2";
            this.btnQuery2.Size = new System.Drawing.Size(178, 23);
            this.btnQuery2.TabIndex = 5;
            this.btnQuery2.Text = "查询(N+1 Select Lists)";
            this.btnQuery2.UseVisualStyleBackColor = true;
            this.btnQuery2.Click += new System.EventHandler(this.btnQuery2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(351, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询(composite key)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormProblemSolutions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 294);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnQuery2);
            this.Controls.Add(this.dgvDataShow);
            this.Controls.Add(this.btnQuery);
            this.Name = "FormProblemSolutions";
            this.Text = "FormProblemSolutions";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDataShow;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnQuery2;
        private System.Windows.Forms.Button button1;
    }
}