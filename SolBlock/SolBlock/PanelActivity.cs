using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SolBlock
{
    public partial class PanelActivity : UserControl
    {
        public PanelActivity()
        {
            InitializeComponent();
        }
        public PanelActivity(string activ,string name,string text,string date,string time,Color color,string path)
        {
            InitializeComponent();
            label1.Text = activ;
            label2.Text = name;
            label3.Text = text;
            label4.Text = date;
            label5.Text = time;
            this.BackColor = color;
            if(path!=null)
                pictureBox1.Image = FileSystem.GetImage(path);
        }
        
    }
}
