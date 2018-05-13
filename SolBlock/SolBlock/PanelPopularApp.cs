using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;

namespace SolBlock
{
    public partial class PanelPopularApp : UserControl
    {


        private int ThisHeight;
        private int PanelHeight;
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                if (value)
                {
                    panel1.Height = PanelHeight;
                    this.Height = ThisHeight;
                    bunifuImageButton1.Image = Properties.Resources.Chevron_Down_50px;
                }
                else
                {
                    panel1.Height = 0;
                    this.Height = 56;
                    bunifuImageButton1.Image = Properties.Resources.Chevron_Right_64px;
                }
            }
        }

        public PanelPopularApp()
        {
            InitializeComponent();
        }

        public PanelPopularApp(Image imageApp, string nameApp, Color color)
        {
            InitializeComponent();
            ImageApp.Image = imageApp;
            NameApp.Text = nameApp;
            BackColor = color;
            ThisHeight = this.Height;
            PanelHeight = panel1.Height;
            IsOpen = false;
        }

        private void PanelPopularApp_Click(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }
    }
}
