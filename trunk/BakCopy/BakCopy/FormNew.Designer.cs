namespace BakCopy
{
    partial class FormNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNew));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.srcDir = new System.Windows.Forms.TextBox();
            this.dstDir = new System.Windows.Forms.TextBox();
            this.srcDirBtn = new System.Windows.Forms.Button();
            this.dstDirBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.filetypeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.subBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source directory :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination directory :";
            // 
            // srcDir
            // 
            this.srcDir.BackColor = System.Drawing.Color.White;
            this.srcDir.Location = new System.Drawing.Point(184, 44);
            this.srcDir.Name = "srcDir";
            this.srcDir.ReadOnly = true;
            this.srcDir.Size = new System.Drawing.Size(270, 21);
            this.srcDir.TabIndex = 22;
            // 
            // dstDir
            // 
            this.dstDir.BackColor = System.Drawing.Color.White;
            this.dstDir.Location = new System.Drawing.Point(183, 104);
            this.dstDir.Name = "dstDir";
            this.dstDir.ReadOnly = true;
            this.dstDir.Size = new System.Drawing.Size(271, 21);
            this.dstDir.TabIndex = 33;
            // 
            // srcDirBtn
            // 
            this.srcDirBtn.Location = new System.Drawing.Point(471, 45);
            this.srcDirBtn.Name = "srcDirBtn";
            this.srcDirBtn.Size = new System.Drawing.Size(75, 23);
            this.srcDirBtn.TabIndex = 2;
            this.srcDirBtn.Text = "Select";
            this.srcDirBtn.UseVisualStyleBackColor = true;
            this.srcDirBtn.Click += new System.EventHandler(this.srcDirBtn_Click);
            // 
            // dstDirBtn
            // 
            this.dstDirBtn.Location = new System.Drawing.Point(471, 104);
            this.dstDirBtn.Name = "dstDirBtn";
            this.dstDirBtn.Size = new System.Drawing.Size(75, 23);
            this.dstDirBtn.TabIndex = 3;
            this.dstDirBtn.Text = "Select";
            this.dstDirBtn.UseVisualStyleBackColor = true;
            this.dstDirBtn.Click += new System.EventHandler(this.dstDirBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(184, 153);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(282, 152);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Copy file type :";
            // 
            // filetypeBox
            // 
            this.filetypeBox.Location = new System.Drawing.Point(183, 74);
            this.filetypeBox.Name = "filetypeBox";
            this.filetypeBox.Size = new System.Drawing.Size(99, 21);
            this.filetypeBox.TabIndex = 4;
            this.filetypeBox.Text = "*.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Task name :";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(184, 15);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(270, 21);
            this.nameBox.TabIndex = 1;
            // 
            // subBox
            // 
            this.subBox.AutoSize = true;
            this.subBox.Location = new System.Drawing.Point(304, 76);
            this.subBox.Name = "subBox";
            this.subBox.Size = new System.Drawing.Size(150, 16);
            this.subBox.TabIndex = 34;
            this.subBox.Text = "Include sub directory";
            this.subBox.UseVisualStyleBackColor = true;
            // 
            // FormNew
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(562, 190);
            this.Controls.Add(this.subBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.filetypeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.dstDirBtn);
            this.Controls.Add(this.srcDirBtn);
            this.Controls.Add(this.dstDir);
            this.Controls.Add(this.srcDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormNew";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Task";
            this.Load += new System.EventHandler(this.FormNew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox srcDir;
        private System.Windows.Forms.TextBox dstDir;
        private System.Windows.Forms.Button srcDirBtn;
        private System.Windows.Forms.Button dstDirBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox filetypeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.CheckBox subBox;
    }
}