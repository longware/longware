using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace BakCopy
{
    public partial class MainForm : Form
    {
        private static IniFiles ini = new IniFiles(Application.StartupPath + @"\config.ini");

        public MainForm()
        {
            InitializeComponent();
            load_listview();
        }

        private void fsw_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void fsw_Created(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void fsw_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void fsw_Renamed(object sender, System.IO.RenamedEventArgs e)
        {

        }

        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string new_taskname = "";

            FormNew fn = new FormNew();
            fn.ShowDialog();
            new_taskname = fn.new_taskname;
            fn.Dispose();

            //log
            writeLog("New task:" + new_taskname);

            load_listview();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It is so easy that doesn't need a help document", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By longware@qq.com \nVersion 0.1", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            logBox.Clear();
        }

        /// <summary>
        /// 修改task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            if (indexes.Count < 1)
            {
                return;
            }
            else if (indexes.Count > 1)
            {
                MessageBox.Show("Only one item can be operate at the same time", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string taskname = "";
                bool isRunning = false;
                foreach (int index in indexes)
                {
                    taskname = listView1.Items[index].Text;
                    isRunning = listView1.Items[index].ImageIndex > 0;
                    break;
                }

                if (isRunning)
                {
                    MessageBox.Show("This task is running, please stop it first", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
  
                FormNew fn = new FormNew();
                fn.isModify = true;
                fn.modify_taskname = taskname;
                fn.ShowDialog();
                fn.Dispose();

                //log
                writeLog("Modify task : " + taskname);

                load_listview();
            }
        }

        /// <summary>
        /// 删除task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            if (indexes.Count < 1)
            {
                return;
            }
            else if (indexes.Count > 1)
            {
                MessageBox.Show("Only one item can be operate at the same time", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string taskname = "";
                bool isRunning = false;
                foreach (int index in indexes)
                {
                    taskname = listView1.Items[index].Text;
                    isRunning = listView1.Items[index].ImageIndex > 0;
                    break;
                }

                if (isRunning)
                {
                    MessageBox.Show("This task is running, please stop it first", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure to delete '" + taskname + "' ?", "Warn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ini.EraseSection(taskname);
                    
                    //log
                    writeLog("Delete task : " + taskname);

                    load_listview();
                }
            }
        }

        private void startAllTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder logText = new StringBuilder();
            logText.Append("Batch start task:");

            StringCollection sectionList = new StringCollection();
            ini.ReadSections(sectionList);
            int autostart = 0;
            for (int i = 0; i < sectionList.Count; i++)
            {
                if (sectionList[i] != null)
                {
                    autostart = ini.ReadInteger(sectionList[i], "autostart", 0);
                    if (autostart < 1)
                    {
                        //start task here
                        ini.WriteInteger(sectionList[i], "autostart", 1);
                        logText.Append(sectionList[i]).Append(",");
                        MessageBox.Show("Start task " + sectionList[i]);
                    }
                }
            }

            sectionList.Clear();
            load_listview();

            //log
            writeLog(logText.ToString());
        }

        private void stopAllTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder logText = new StringBuilder();
            logText.Append("Batch stop task:");

            StringCollection sectionList = new StringCollection();
            ini.ReadSections(sectionList);
            int autostart = 0;
            for (int i = 0; i < sectionList.Count; i++)
            {
                if (sectionList[i] != null)
                {
                    autostart = ini.ReadInteger(sectionList[i], "autostart", 0);
                    if (autostart > 0)
                    {
                        //stop task here
                        ini.WriteInteger(sectionList[i], "autostart", 0);
                        logText.Append(sectionList[i]).Append(",");
                        MessageBox.Show("Stop task " + sectionList[i]);
                    }
                }
            }

            sectionList.Clear();
            load_listview();

            //log
            writeLog(logText.ToString());
        }

        private void startTaskMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            if (indexes.Count < 1)
            {
                return;
            }
            else if (indexes.Count > 1)
            {
                MessageBox.Show("Only one item can be operate at the same time", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string taskname = "";
                bool isRunning = false;
                foreach (int index in indexes)
                {
                    taskname = listView1.Items[index].Text;
                    isRunning = listView1.Items[index].ImageIndex > 0;
                    break;
                }

                if (isRunning)
                {
                    MessageBox.Show("This task is already in running", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //start it here
                ini.WriteInteger(taskname, "autostart", 1);
                load_listview();
                MessageBox.Show("Start it here...");

                //log
                writeLog("Start task : " + taskname);
            }
        }

        private void stopTaskMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            if (indexes.Count < 1)
            {
                return;
            }
            else if (indexes.Count > 1)
            {
                MessageBox.Show("Only one item can be operate at the same time", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string taskname = "";
                bool isRunning = false;
                foreach (int index in indexes)
                {
                    taskname = listView1.Items[index].Text;
                    isRunning = listView1.Items[index].ImageIndex > 0;
                    break;
                }

                if (!isRunning)
                {
                    MessageBox.Show("This task is already stoped", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //stop it here
                ini.WriteInteger(taskname, "autostart", 0);
                load_listview();
                MessageBox.Show("Stop it here...");

                //log
                writeLog("Stop task : " + taskname);
            }
        }

        /// <summary>
        /// 从ini文件读取数据加载到listview
        /// </summary>
        public void load_listview()
        {
            listView1.Clear();

            StringCollection sectionList = new StringCollection();
            ini.ReadSections(sectionList);
            int autostart = 0;
            for (int i = 0; i < sectionList.Count; i++)
            {
                if (sectionList[i] != null)
                {
                    autostart = ini.ReadInteger(sectionList[i], "autostart", 0);
                    listView1.Items.Add(sectionList[i], autostart);
                }
            }
            sectionList.Clear();
        }

        private void wordwrapBtn_Click(object sender, EventArgs e)
        {
            logBox.WordWrap = !logBox.WordWrap;
        }

        public void writeLog(String logText)
        {
            string msg = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss") + " " + logText + "\r\n";
            logBox.AppendText(msg);
            msg = null;
        }
    }
}
