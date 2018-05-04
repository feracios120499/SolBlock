using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolBlock
{
    public partial class PanelsCurrentApp : UserControl
    {
        public string Name { get; set; }
        public PanelsCurrentApp()
        {
            InitializeComponent();
        }
        public PanelsCurrentApp(Image image,string name,string Title,Color color)
        {
            InitializeComponent();
            this.pictureBox1.Image = image;
            this.nameProg.Text = name;
            Name = name;
            this.panel1.BackColor = color;
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChoiseAndAdd f1 = (ChoiseAndAdd)(Application.OpenForms[1]);
            if (materialCheckBox1.Checked)
            {
                f1.checkPlus(Name);
            }
            else
                f1.checkMinus(Name);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
