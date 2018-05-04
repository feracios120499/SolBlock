using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolBlock
{
    class FileSystem
    {
        public static string ToHashCode(string path)
        {
            string hashCode = null;
            foreach(char c in path)
            {
                hashCode += Convert.ToInt32(c);
            }
            return hashCode;
        }
        static void WalkDirectoryTree(string root,List<string> allFiles)
        {
            string[] files = null;
            string[] subDirs = null;
            try
            {
                files = Directory.GetFiles(root, "*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                
            }
            if (files != null)
            {
                subDirs = Directory.GetDirectories(root);
                foreach (var fi in files)
                {
                    allFiles.Add(fi);
                }
                foreach (var dirInfo in subDirs)
                {
                    allFiles.Add(dirInfo);
                    WalkDirectoryTree(dirInfo, allFiles);
                }
            }

        }
        public static void SaveStructureFile(string path)
        {
            string nameFile = ToHashCode(path) + ".txt";
            List<string> files=new List<string>();
            WalkDirectoryTree(path, files);
            using(StreamWriter sw=new StreamWriter(Application.StartupPath+"\\"+nameFile, false, Encoding.Unicode))
            {
                foreach(var item in files)
                {
                    sw.WriteLine(item);
                }
            }
        }
        private static int CountSlash(string str)
        {
            return str.ToCharArray().Where(c => c == '\\').Count();
        }
        public static string ParentDirectory(string path)
        {
            return path.Substring(0,  path.LastIndexOf('\\'));
        }
        public static string CurrentFileFromPath(string path)
        {
            return path.Substring(path.LastIndexOf('\\')+1);
        }
        public static List<string> GetFiles(string startPath,string currentPath)
        {
            List<string> files = new List<string>();
            string str;
            using (StreamReader sr = new StreamReader(Application.StartupPath+"\\"+ToHashCode(startPath) + ".txt", Encoding.Unicode))
            {
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine();
                    if ((CountSlash(str) - 1) == CountSlash(currentPath)&&str.Contains(currentPath))
                    {
                        files.Add(str);
                    }
                }
            }
            return files;
        }
        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;
        private struct SHFILEINFO
        {

            public IntPtr hIcon;

            public int iIcon;

            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        [DllImport("Shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

        public static Image GetImage(string path)
        {
            try
            {
                IntPtr hImgLarge;
                SHFILEINFO shinfo = new SHFILEINFO();

                string FileName = path;

                System.Drawing.Icon myIcon;

                hImgLarge = SHGetFileInfo(FileName, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);

                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);

                return myIcon.ToBitmap();
            }
            catch
            {
                return null;
            }

        }
        public static bool IsDirectory(string file)
        {
            if (System.IO.File.Exists(file))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
