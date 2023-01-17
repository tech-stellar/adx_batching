namespace APM_BtcPlant
{
    partial class frmBtcPltMain
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
            this.btnConfig = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProgress = new System.Windows.Forms.RichTextBox();
            this.btnImports = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(267, 339);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(98, 23);
            this.btnConfig.TabIndex = 0;
            this.btnConfig.Text = "Configuration";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtProgress);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 327);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtProgress
            // 
            this.txtProgress.Location = new System.Drawing.Point(17, 19);
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.ReadOnly = true;
            this.txtProgress.Size = new System.Drawing.Size(410, 291);
            this.txtProgress.TabIndex = 0;
            this.txtProgress.Text = "";
            // 
            // btnImports
            // 
            this.btnImports.Location = new System.Drawing.Point(108, 339);
            this.btnImports.Name = "btnImports";
            this.btnImports.Size = new System.Drawing.Size(153, 23);
            this.btnImports.TabIndex = 2;
            this.btnImports.Text = "Import incoming files";
            this.btnImports.UseVisualStyleBackColor = true;
            this.btnImports.Click += new System.EventHandler(this.btnImports_Click);
            // 
            // frmBtcPltMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 374);
            this.Controls.Add(this.btnImports);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmBtcPltMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "APM Batching Plant File Processing";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImports;
        private System.Windows.Forms.RichTextBox txtProgress;


    }
}

