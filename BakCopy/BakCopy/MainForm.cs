using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Threading;
using System.Collections;

namespace BakCopy
{
    public partial class MainForm : Form
    {
        private static IniFiles ini = new IniFiles(Application.StartupPath + @"\config.ini");
        private Hashtable threadNameTable = new Hashtable();//全局线程Name列表

        public MainForm()
        {
            InitializeComponent();

            start_default_task();
            load_listview();
        }

        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string new_taskname = "";

            FormNew fn = new FormNew();
            DialogResult dr = fn.ShowDialog();
            new_taskname = fn.new_taskname;
            fn.Dispose();

            //log
            if (dr != DialogResult.Cancel)//(new_taskname != null && new_taskname.Length > 0)
            {
                writeLog("New task:" + new_taskname);
            }            

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
                DialogResult dr = fn.ShowDialog();
                //MessageBox.Show(dr.ToString());
                fn.Dispose();

                //log
                if (dr != DialogResult.Cancel)
                {
                    writeLog("Modify task : " + taskname);
                }                

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
            string srcDir, dstDir, filetype;
            bool subdir = false;

            for (int i = 0; i < sectionList.Count; i++)
            {
                if (sectionList[i] != null)
                {
                    autostart = ini.ReadInteger(sectionList[i], "autostart", 0);
                    if (autostart < 1)
                    {
                        //read setting
                        srcDir = ini.ReadString(sectionList[i], "srcDir", "");
                        dstDir = ini.ReadString(sectionList[i], "dstDir", "");
                        filetype = ini.ReadString(sectionList[i], "filetype", "");
                        subdir = ini.ReadInteger(sectionList[i], "subdir", 0) > 0;

                        // start thread
                        Thread myThread = new Thread(new ThreadStart(new FileCopyThread(srcDir,dstDir,filetype,subdir).run));
                        myThread.IsBackground = true;
                        myThread.Name = sectionList[i];
                        myThread.Start();
                        this.threadNameTable.Add(sectionList[i], myThread);

                        //change status
                        ini.WriteInteger(sectionList[i], "autostart", 1);

                        //log
                        logText.Append(sectionList[i]).Append(",");
                        //MessageBox.Show("Start task " + sectionList[i]);
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

                        //stop it
                        if (this.threadNameTable.ContainsKey(sectionList[i]))
                        {
                            Thread th = (Thread)this.threadNameTable[sectionList[i]];
                            th.Join();
                            th.Abort();
                            this.threadNameTable.Remove(sectionList[i]);
                        }
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

                //read setting
                string srcDir = ini.ReadString(taskname, "srcDir", "");
                string dstDir = ini.ReadString(taskname, "dstDir", "");
                string filetype = ini.ReadString(taskname, "filetype", "");
                bool subdir = ini.ReadInteger(taskname, "subdir", 0) > 0;

                // start thread
                Thread myThread = new Thread(new ThreadStart(new FileCopyThread(srcDir, dstDir, filetype, subdir).run));
                myThread.IsBackground = true;
                myThread.Name = taskname;
                myThread.Start();
                this.threadNameTable.Add(taskname, myThread);

                //change status
                ini.WriteInteger(taskname, "autostart", 1);

                load_listview();
                //MessageBox.Show("Start it here...");

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

                //stop it
                foreach (DictionaryEntry de in this.threadNameTable)
                {
                    if (de.Key.ToString() == taskname)
                    {
                        //Thread th = (Thread)this.threadNameTable[taskname];
                        Thread th = (Thread)de.Value;
                        th.Join();
                        th.Abort();
                        this.threadNameTable.Remove(taskname);
                        break;
                    }
                }

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

        /// <summary>
        /// 启动默认状态是start的任务
        /// </summary>
        public void start_default_task()
        {
            StringBuilder logText = new StringBuilder();
            logText.Append("Batch start default task:");

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
                        //read setting
                        string srcDir = ini.ReadString(sectionList[i], "srcDir", "");
                        string dstDir = ini.ReadString(sectionList[i], "dstDir", "");
                        string filetype = ini.ReadString(sectionList[i], "filetype", "");
                        bool subdir = ini.ReadInteger(sectionList[i], "subdir", 0) > 0;

                        // start thread
                        Thread myThread = new Thread(new ThreadStart(new FileCopyThread(srcDir, dstDir, filetype, subdir).run));
                        myThread.IsBackground = true;
                        myThread.Name = sectionList[i];
                        myThread.Start();
                        this.threadNameTable.Add(sectionList[i], myThread);

                        //log
                        logText.Append(sectionList[i]).Append(",");
                    }
                }
            }

            sectionList.Clear();

            //log
            writeLog(logText.ToString());
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
