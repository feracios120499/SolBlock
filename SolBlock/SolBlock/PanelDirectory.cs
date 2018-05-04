using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolBlockLibrary;

namespace SolBlock
{
    public partial class PanelDirectory : UserControl
    {

        public EventHandler<DirectoryEventArgs> eventRemove;
        public PanelDirectory()
        {
            InitializeComponent();
        }
        public PanelDirectory(string text,Color color)
        {
            InitializeComponent();
            bunifuCustomLabel1.Text = text;
            panel.BackColor = color;
        }


        private void PanelDirectory_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(pictureDelete, "Удалить папку");
        }

        private void pictureDelete_Click(object sender, EventArgs e)
        {
            eventRemove?.Invoke(this, new DirectoryEventArgs(new ModelBlockDirectory(null,bunifuCustomLabel1.Text)));
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            Color color;
            Form1 f1 = (Form1)Application.OpenForms[0];
            f1.ClearPanelFile();
            foreach(var item in FileSystem.GetFiles(bunifuCustomLabel1.Text, bunifuCustomLabel1.Text))
            {
                color = flag ? Color.FromArgb(20, 19, 31) : Color.FromArgb(36, 35, 50);
                flag = !flag;
                f1.AddPanelFile(bunifuCustomLabel1.Text,item, color);
            }
        }
    }
}
