using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SolBlock
{
    public partial class PanelFile : UserControl
    {
        public PanelFile()
        {
            InitializeComponent();
        }
        public PanelFile(string startPath,string path,Color color)
        {
            this.Tag = startPath;
            InitializeComponent();
            label1.Text = FileSystem.CurrentFileFromPath(path);
            pictureBox1.Image = FileSystem.GetImage(path);
            this.BackColor = color;
        }

        private void PanelFile_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            bool flag = false;
            Color color;
            Form1 f1 = (Form1)Application.OpenForms[0];
            if (FileSystem.IsDirectory(f1.GetDirectoryFile() + "\\" + label1.Text))
            {
                f1.ClearPanelFile();
                foreach (var item in FileSystem.GetFiles(Tag.ToString(), f1.GetDirectoryFile() + "\\" + label1.Text))
                {
                    color = flag ? Color.FromArgb(20, 19, 31) : Color.FromArgb(36, 35, 50);
                    flag = !flag;
                    f1.AddPanelFile(Tag.ToString(), item, color);
                }
            }
            else
            {
                Process.Start(f1.GetDirectoryFile() + "\\" + label1.Text);
            }
        }
    }
}
