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
    public partial class PanelSite : UserControl
    {
        public EventHandler<SiteEventArgs> eventRemove;
        public PanelSite()
        {
            InitializeComponent();
        }

        public PanelSite(string text,Color color)
        {
            InitializeComponent();
            bunifuCustomLabel1.Text = text;
            panel.BackColor = color;
        }

        private void pictureDelete_Click(object sender, EventArgs e)
        {
            eventRemove?.Invoke(this, new SiteEventArgs(bunifuCustomLabel1.Text));
        }

        private void PanelSite_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(pictureDelete, "Удалить папку");
        }
    }
}
