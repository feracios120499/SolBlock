using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolBlock
{
    public partial class FormMessage : Form
    {
        public FormMessage()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        public FormMessage(string textMessage,string path)
        {
            InitializeComponent();
            labelText.Text = textMessage;
            TopMost = true;
            pictureBox1.Image = FileSystem.GetImage(path);
            var control = new Control(this, null);
            SetForegroundWindow(control.Handle);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
