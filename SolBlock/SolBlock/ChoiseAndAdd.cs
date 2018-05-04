using System;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Management;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Drawing;
using MaterialSkin.Controls;
using System.Data.SQLite;

namespace SolBlock
{
    public partial class ChoiseAndAdd : Form
    {
        static SQLiteConnection sqlConnection = new SQLiteConnection("Data Source=SolBlockDB.db; Version=3");
        public ChoiseAndAdd()
        {
            InitializeComponent();
            
        }
        class ProcessAllTime
        {
            public string Name { get; set; }
            public ProcessAllTime(string Name)
            {
                this.Name = Name;
            }
        }
        public ChoiseAndAdd(int IdUser)
        {
            InitializeComponent();
            this.IdUser = IdUser;
        }
        Color color;
        bool flag = false;
        int IdUser;
        List<Process> listProcess = new List<Process>();
        List<PanelsCurrentApp> listPanels = new List<PanelsCurrentApp>();
        List<ProcessAllTime> checkProcess = new List<ProcessAllTime>();
        private void ChoiseAndAdd_Load(object sender, EventArgs e)
        {
            loadApp();
        }
        private void loadApp()
        {
            foreach(PanelsCurrentApp pr in listPanels)
            {
                panel1.Controls.Remove(pr);
            }
            listProcess.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.Length > 0)
                {
                    try
                    {
                        addPanels(p.ProcessName,p.MainWindowTitle, FileSystem.GetImage(p.MainModule.FileName));
                    }
                    catch { }
                    listProcess.Add(p);
                }
            }
        }
        private void addPanels(string name,string title,Image image)
        {
            if (flag)
            {
                color = Color.FromArgb(20, 19, 31);
                flag = false;
            }
            else
            {
                color = Color.FromArgb(36, 35, 50);
                flag = true;
            }
            var panel = new PanelsCurrentApp(image,name,title, color);
            panel1.Controls.Add(panel);
            panel.Top = 0;
            panel.Left = 0;
            listPanels.Add(panel);
            panel.Dock = DockStyle.Top;
        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            loadApp();
        }
        public void checkPlus(string text)
        {
            checkProcess.Add(new ProcessAllTime(text));
        }
        public void checkMinus(string text)
        {
            checkProcess.Remove(checkProcess.Find(p => p.Name == text));
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Minimum = numericUpDown2.Value + 1;
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {
                panelAnyTime.Visible = false;
                panelDiapasonTime.Visible = false;
            }
            else if (materialRadioButton2.Checked)
            {
                panelAnyTime.Visible = true;
                panelDiapasonTime.Visible = false;
            }
            else
            {
                panelAnyTime.Visible = false;
                panelDiapasonTime.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {
                foreach(var str in checkProcess)
                {
                    
                        addProccesAllTime(str.Name, "AllTime");

                }
            }
            else if (materialRadioButton2.Checked)
            {
                foreach (var str in checkProcess)
                {
                    addProccessAnyTime(str.Name, "AnyTime", numericUpDown1.Value.ToString());
                }
            }
            else
            {
                foreach(var str in checkProcess)
                {
                    
                    addProccesDiapasonTime(str.Name, "DiapasonTime", (numericUpDown2.Value.ToString() + "-" + numericUpDown3.Value.ToString()).ToString());
                }
            }
            this.Close();
        }
        private void addProccessAnyTime(string proccess, string type, string time)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,TimeBlock,NameProgBlock,IdUser) values(@IdBlock,@TypeBlock,@TimeBlock,@NameBlock,@IdUser)", sqlConnection);
            command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
            command.Parameters.AddWithValue("TypeBlock", type);
            command.Parameters.AddWithValue("TimeBlock", time);
            command.Parameters.AddWithValue("NameBlock", proccess);
            command.Parameters.AddWithValue("IdUser", IdUser);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private void addProccesAllTime(string proccess, string type)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,NameProgBlock,IdUser) VALUES (@IdBlock,@TypeBlock,@NameBlock,@IdUser)", sqlConnection);
            command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
            command.Parameters.AddWithValue("TypeBlock", type);
            command.Parameters.AddWithValue("NameBlock", proccess);
            command.Parameters.AddWithValue("IdUser", IdUser);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private void addProccesDiapasonTime(string proccess, string type, string diapason)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,DiapasonBlock,NameProgBlock,IdUser) VALUES (@IdBlock,@TypeBlock,@DiapasonBlock,@NameBlock,@IdUser)", sqlConnection);
            command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
            command.Parameters.AddWithValue("TypeBlock", type);
            command.Parameters.AddWithValue("DiapasonBlock", diapason);
            command.Parameters.AddWithValue("NameBlock", proccess);
            command.Parameters.AddWithValue("IdUser", IdUser);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
