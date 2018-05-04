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
    public partial class PanelProg : UserControl
    {
        public PanelProg()
        {
            InitializeComponent();
        }
        public class AppRemoveEventArgs : EventArgs
        {
            public IProcess process;
            public AppRemoveEventArgs(IProcess process)
            {
                this.process = process;
            }
        }
        public EventHandler<AppRemoveEventArgs> eventRemove;
        public IProcess process { get; set; }
        public string TypeBlock { get; set; }
        public PanelProg(string nameProg,string typeBlock,string dopInfo,IProcess process,Color color)
        {
            InitializeComponent();
            this.nameProg.Text = nameProg;
            if(typeBlock=="AllTime")
             this.typeBlock.Text = "Постоянная блокировка";
            if (typeBlock == "AnyTime")
                this.typeBlock.Text = "Временно разрешен";
            if (typeBlock == "DiapasonTime")
                this.typeBlock.Text = "Разрешен в диапазоне";
            if (typeBlock == "Title")
                this.typeBlock.Text = "По ключевому слову";
            this.panel.BackColor = color;
            TypeBlock = typeBlock;
            this.process = process;
        }
        public PanelProg(string nameProg, string typeBlock, IProcess process ,Color color)
        {
            InitializeComponent();
            this.nameProg.Text = nameProg;
            if (typeBlock == "AllTime")
                this.typeBlock.Text = "Постоянная блокировка";
            if (typeBlock == "AnyTime")
                this.typeBlock.Text = "Временно разрешен";
            if (typeBlock == "DiapasonTime")
                this.typeBlock.Text = "Разрешен в диапазоне";
            if (typeBlock == "Title")
                this.typeBlock.Text = "По ключевому слову";
            this.panel.BackColor = color;
            TypeBlock = typeBlock;
            this.typeBlock.Top = this.nameProg.Top;
            this.process = process;
        }

        private void pictureDelete_Click(object sender, EventArgs e)
        {
            eventRemove?.Invoke(this, new AppRemoveEventArgs(process));
        }
    }
}
