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
    public partial class PanelStatistics : UserControl
    {
        public PanelStatistics()
        {
            InitializeComponent();
        }
        public Label time { get; set; }
        public PanelStatistics(string name,string time,string last,Color color,string path)
        {
            InitializeComponent();
            labelName.Text = name;
            labelTime.Text = time;
            labelLast.Text = last;
            this.BackColor = color;
            pictureApp.Image = FileSystem.GetImage(path);
            this.time = labelTime;
        }  
    }
}
