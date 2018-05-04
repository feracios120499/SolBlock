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
    public partial class PanelPopularApp : UserControl
    {
        
        
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value;
                if (value)
                {
                    this.Height = 286;
                }
                else
                {
                    this.Height = 0;
                }
            }
        }

        public PanelPopularApp()
        {
            InitializeComponent();
        }

        public PanelPopularApp(Image imageApp,string nameApp,Color color)
        {
            InitializeComponent();
            ImageApp.Image = imageApp;
            NameApp.Text = nameApp;
            BackColor = color;
        }
    }
}
