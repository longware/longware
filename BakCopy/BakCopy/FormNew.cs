using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;

namespace BakCopy
{
    public partial class FormNew : Form
    {
        private static IniFiles ini = new IniFiles(Application.StartupPath + @"\config.ini");
        public bool isModify = false;
        public string modify_taskname = "";
        public string new_taskname = "";

        public FormNew()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            string section = nameBox.Text;
            string src = srcDir.Text;
            string dst = dstDir.Text;
            string filetype = filetypeBox.Text;

            if (section == "" || src == "" || dst == "" || filetype == "")
            {
                MessageBox.Show("All field above is not allowed empty", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (src == dst)
            {
                MessageBox.Show("Destination directory must not same as Source directory", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dst.IndexOf(src) > -1)
            {
                MessageBox.Show("Destination directory can not be a sub directory of Source directory", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //check if exists
            bool isExists = false;
            StringCollection sectionList = new StringCollection();
            ini.ReadSections(sectionList);
            for (int i = 0; i < sectionList.Count; i++)
            {
                bool express = this.isModify ? (sectionList[i] != null && sectionList[i] != this.modify_taskname && sectionList[i] == section) : (sectionList[i] != null && sectionList[i] == section);
                if (express)
                {
                    isExists = true;
                    break;
                }
            }
            sectionList.Clear();
            if (isExists)
            {
                MessageBox.Show("The same task name is exists, please change a task name", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.isModify && section != this.modify_taskname)
            {
                ini.EraseSection(this.modify_taskname);
            }

            ini.WriteString(section, "srcDir", src);
            ini.WriteString(section, "dstDir", dst);
            ini.WriteString(section, "filetype", filetype);
            ini.WriteInteger(section, "subdir", Convert.ToInt16(subBox.Checked));
            ini.WriteInteger(section, "autostart", 0);

            this.new_taskname = section;//for MainForm callback
            this.Close();
        }

        private void modify_task(string taskname)
        {
            if (this.isModify && this.modify_taskname != "")
            {

            }
        }

        private void srcDirBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            if (srcDir.Text != "")
            {
                dialog.SelectedPath = srcDir.Text;//设置此次默认目录为上一次选中目录
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //重置当前路径
                Directory.SetCurrentDirectory(Application.StartupPath);
                srcDir.Text = dialog.SelectedPath;
            }
            else
            {
                return;
            }
        }

        private void dstDirBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            if (dstDir.Text != "")
            {
                dialog.SelectedPath = dstDir.Text;//设置此次默认目录为上一次选中目录
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //重置当前路径
                Directory.SetCurrentDirectory(Application.StartupPath);
                dstDir.Text = dialog.SelectedPath;
            }
            else
            {
                return;
            }
        }

        private void FormNew_Load(object sender, EventArgs e)
        {
            if (this.isModify)
            {
                nameBox.Text = this.modify_taskname;
                srcDir.Text = ini.ReadString(this.modify_taskname, "srcDir", "");
                dstDir.Text = ini.ReadString(this.modify_taskname, "dstDir", "");
                filetypeBox.Text = ini.ReadString(this.modify_taskname, "filetype", "");
                subBox.Checked = ini.ReadInteger(this.modify_taskname, "subdir", 0) > 0;
                //ini.ReadInteger(this.modify_taskname, "autostart", 0);
            }
        }

        private void FormNew_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show(e.KeyCode + "\n" + e.KeyData + "\n" + e.KeyValue );
        }

        private void FormNew_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode + "\n" + e.KeyData + "\n" + e.KeyValue);
        }

        private void FormNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(e.KeyChar.ToString());
        }

        private void FormNew_KeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode + "\n" + e.KeyData + "\n" + e.KeyValue);
        }

        /// <summary>
        /// esc退出窗体
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        this.Close();//csc关闭窗体
                        break;
                }
            }
            return false;
        }

    }
}
