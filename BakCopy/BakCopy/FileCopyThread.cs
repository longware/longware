using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

namespace BakCopy
{
    class FileCopyThread
    {
        private FileSystemWatcher fsw;
        private string srcDir = "";
        private string dstDir = "";
        private string fileType = "*.*";
        private bool includeSubdir = false;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public FileCopyThread(string srcDir, string dstDir, string fileType, bool includeSubdir)
        {
            this.srcDir = srcDir;
            this.dstDir = dstDir;
            this.fileType = fileType;
            this.includeSubdir = includeSubdir;

            this.fsw = new FileSystemWatcher();
            this.fsw.Path = this.srcDir;
            this.fsw.Filter = this.fileType;
            this.fsw.EnableRaisingEvents = false;
            this.fsw.IncludeSubdirectories = this.includeSubdir;            
        }

        public void run()
        {
            this.fsw.Changed += new FileSystemEventHandler(this.fsw_Changed);
            this.fsw.Created += new FileSystemEventHandler(this.fsw_Created);
            this.fsw.Deleted += new FileSystemEventHandler(this.fsw_Deleted);
            this.fsw.Renamed += new RenamedEventHandler(this.fsw_Renamed);
            this.fsw.EnableRaisingEvents = true;
            Console.WriteLine("FileCopyThread.run={0} HashCode={1}", System.Threading.Thread.CurrentThread.Name, this.GetHashCode());
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Change File: " + e.FullPath + " " + e.ChangeType);
            //TODO copy file here
        }

        private void fsw_Created(object sender, FileSystemEventArgs e)
        {
            this.fsw_Changed(sender, e);
            //TODO copy file here
        }

        private void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Delete File: " + e.FullPath + " " + e.ChangeType);
            //TODO delete file here
        }

        private void fsw_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
            //TODO rename file here

        }

        ~FileCopyThread() 
        {
            this.fsw.EnableRaisingEvents = false;
            Console.WriteLine("~FileCopyThread={0} HashCode={1}", System.Threading.Thread.CurrentThread.Name, this.GetHashCode());
        }
    }
}
