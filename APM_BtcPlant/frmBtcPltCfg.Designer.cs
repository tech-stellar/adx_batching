namespace APM_BtcPlant
{
    partial class frmBtcPltCfg
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
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIncoming = new System.Windows.Forms.TextBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.btnIncoming = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEpicUser = new System.Windows.Forms.TextBox();
            this.txtEpicPass = new System.Windows.Forms.TextBox();
            this.txtEpicURL = new System.Windows.Forms.TextBox();
            this.txtConfig = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDBString = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(348, 427);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(116, 23);
            this.btnSaveConfig.TabIndex = 1;
            this.btnSaveConfig.Text = "Save Configuration";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(470, 427);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(525, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Batching Plant";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnError);
            this.groupBox1.Controls.Add(this.btnProcess);
            this.groupBox1.Controls.Add(this.btnIncoming);
            this.groupBox1.Controls.Add(this.txtError);
            this.groupBox1.Controls.Add(this.txtProcess);
            this.groupBox1.Controls.Add(this.txtIncoming);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Incoming files folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process files folder:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Error files folder:";
            // 
            // txtIncoming
            // 
            this.txtIncoming.Location = new System.Drawing.Point(134, 32);
            this.txtIncoming.Name = "txtIncoming";
            this.txtIncoming.Size = new System.Drawing.Size(284, 20);
            this.txtIncoming.TabIndex = 3;
            this.txtIncoming.TextChanged += new System.EventHandler(this.txtIncoming_TextChanged);
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(134, 58);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(284, 20);
            this.txtProcess.TabIndex = 4;
            this.txtProcess.TextChanged += new System.EventHandler(this.txtProcess_TextChanged);
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(134, 84);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(284, 20);
            this.txtError.TabIndex = 5;
            this.txtError.TextChanged += new System.EventHandler(this.txtError_TextChanged);
            // 
            // btnIncoming
            // 
            this.btnIncoming.Location = new System.Drawing.Point(424, 30);
            this.btnIncoming.Name = "btnIncoming";
            this.btnIncoming.Size = new System.Drawing.Size(24, 23);
            this.btnIncoming.TabIndex = 6;
            this.btnIncoming.Text = "...";
            this.btnIncoming.UseVisualStyleBackColor = true;
            this.btnIncoming.Click += new System.EventHandler(this.btnIncoming_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(424, 56);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(24, 23);
            this.btnProcess.TabIndex = 7;
            this.btnProcess.Text = "...";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(424, 82);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(24, 23);
            this.btnError.TabIndex = 8;
            this.btnError.Text = "...";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDBString);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtConfig);
            this.groupBox2.Controls.Add(this.txtEpicURL);
            this.groupBox2.Controls.Add(this.txtEpicPass);
            this.groupBox2.Controls.Add(this.txtEpicUser);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 180);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Epicor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Epicor User ID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Password :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(128, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "URL :";
            // 
            // txtEpicUser
            // 
            this.txtEpicUser.Location = new System.Drawing.Point(172, 25);
            this.txtEpicUser.Name = "txtEpicUser";
            this.txtEpicUser.Size = new System.Drawing.Size(284, 20);
            this.txtEpicUser.TabIndex = 3;
            this.txtEpicUser.TextChanged += new System.EventHandler(this.txtEpicUser_TextChanged);
            // 
            // txtEpicPass
            // 
            this.txtEpicPass.Location = new System.Drawing.Point(172, 54);
            this.txtEpicPass.Name = "txtEpicPass";
            this.txtEpicPass.Size = new System.Drawing.Size(284, 20);
            this.txtEpicPass.TabIndex = 4;
            this.txtEpicPass.TextChanged += new System.EventHandler(this.txtEpicPass_TextChanged);
            // 
            // txtEpicURL
            // 
            this.txtEpicURL.Location = new System.Drawing.Point(172, 80);
            this.txtEpicURL.Name = "txtEpicURL";
            this.txtEpicURL.Size = new System.Drawing.Size(284, 20);
            this.txtEpicURL.TabIndex = 5;
            this.txtEpicURL.TextChanged += new System.EventHandler(this.txtEpicURL_TextChanged);
            // 
            // txtConfig
            // 
            this.txtConfig.Location = new System.Drawing.Point(172, 106);
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.Size = new System.Drawing.Size(284, 20);
            this.txtConfig.TabIndex = 6;
            this.txtConfig.TextChanged += new System.EventHandler(this.txtConfig_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(84, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "SysConfig File :";
            // 
            // txtDBString
            // 
            this.txtDBString.Location = new System.Drawing.Point(172, 132);
            this.txtDBString.Name = "txtDBString";
            this.txtDBString.Size = new System.Drawing.Size(284, 20);
            this.txtDBString.TabIndex = 8;
            this.txtDBString.TextChanged += new System.EventHandler(this.txtDBString_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Epicor DB Connection String :";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(533, 409);
            this.tabControl1.TabIndex = 3;
            // 
            // frmBtcPltCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBtcPltCfg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDBString;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConfig;
        private System.Windows.Forms.TextBox txtEpicURL;
        private System.Windows.Forms.TextBox txtEpicPass;
        private System.Windows.Forms.TextBox txtEpicUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnError;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnIncoming;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.TextBox txtIncoming;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;

    }
}