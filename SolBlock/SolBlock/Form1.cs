using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.IO;
using System.Security.Principal;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using AltoControls;
using System.Management;
using Microsoft.Win32;
using System.Data.SQLite;
using SolBlockLibrary;
namespace SolBlock
{
    public partial class Form1 : Form
    {
        BlockDirectory blockDirectory;
        BlockSite blockSite;
        BlockApp blockApp;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(int IdUser1)
        {
            InitializeComponent();
            IdUser = IdUser1;
        }
        private bool flagDirectory = false;
        private bool flagSite = false;
        private bool flagApp = false;
        private void AddBlockDirectory(object sender, DirectoryEventArgs e)
        {
            if (flagDirectory)
            {
                color = Color.FromArgb(20, 19, 31);
                flagDirectory = false;
            }
            else
            {
                color = Color.FromArgb(36, 35, 50);
                flagDirectory = true;
            }
            var panel = new PanelDirectory(e.BlockDirectory.PathBlock, color);
            panelDirectory.Controls.Add(panel);
            panel.Left = 0;
            panel.eventRemove += RemovePanelDirectory;
            panel.Dock = DockStyle.Top;
            panel.Tag = e.BlockDirectory.PathBlock;
        }
        private void AddBlockApp(object sender,AppEventArgs e)
        {
            if (flagApp)
            {
                color = Color.FromArgb(20, 19, 31);
                flagApp = false;
            }
            else
            {
                color = Color.FromArgb(36, 35, 50);
                flagApp = true;
            }
            PanelProg panel;
            IProcess process = sender as IProcess;
            if (e.Info == null)
                panel = new PanelProg(e.Name, e.Type,process, color);
            else
                panel = new PanelProg(e.Name, e.Type, e.Info,process, color);
            panelBlockApp.Controls.Add(panel);
            panel.eventRemove += RemovePanelApp;
            panel.Left = 0;
            panel.Dock = DockStyle.Top;
            panel.Tag = name;
        }
        private void AddBlockSite(object sender, SiteEventArgs e)
        {
            if (flagSite)
            {
                color = Color.FromArgb(20, 19, 31);
                flagSite = false;
            }
            else
            {
                color = Color.FromArgb(36, 35, 50);
                flagSite = true;
            }
            var panel = new PanelSite(e.PathBlock, color);
            panelBlockSites.Controls.Add(panel);
            panel.eventRemove += RemovePanelSite;
            panel.Dock = DockStyle.Top;
            panel.Tag = e.PathBlock;
        }
        private void Message(object sender,AppEventArgs e)
        {
            FormMessage form = new FormMessage($"Приложение {e.Name} заблокировано!",e.Info);
            form.ShowDialog();
        }
        const string name = "SolBlock.exe";
        string UUID;
        public bool SetAutorunValue(bool autorun)
        {
            string ExePath = System.Windows.Forms.Application.StartupPath + "\\" + name;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);
                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string GetCPUId()
        {
            List<string> UUID = new List<string>();
            ManagementObjectSearcher searcher;
            try
            {
                searcher = new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT UUID FROM Win32_ComputerSystemProduct");
                foreach (ManagementObject queryObj in searcher.Get())
                    UUID.Add(queryObj["UUID"].ToString());
            }
            catch
            {
                MessageBox.Show("Запустите от имени администратора(");
            }
            this.UUID = UUID.Last();
            return UUID.Last();
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            DefenderMode();
            WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            textBoxPassLogin.Clear();
        }
        private void DefenderMode()
        {
            if (ExistUser(UUID))
            {
                panelLogin.BringToFront();
            }
            else
            {
                panelCheckIn.BringToFront();
            }
            panel2.Visible = false;
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
        }
        static SQLiteConnection sqlConnection = new SQLiteConnection("Data Source=" + Application.StartupPath+"\\SolBlockDB.db; Version=3");
        static int IdUser;
        class StatisticApp
        {
            public string Name { get; set; }
            public Stopwatch Time;
            public DateTime DateLast;
            public PanelStatistics panel;
            public StatisticApp(string Name)
            {
                this.Name = Name;
                Time = new Stopwatch();
                DateLast = new DateTime();
            }
            public void Refresh()
            {
                var t = TimeSpan.FromMilliseconds(Time.Elapsed.TotalMilliseconds);
                panel.Invoke(new Action(() => panel.labelTime.Text = $"{t.Hours}:{t.Minutes}:{t.Seconds}"));
            }
        }
        class MyProcess
        {
            public string ProcessName { get; }
            public string MainWindowTitle { get; }
            public string FileName { get; }
            public MyProcess(string ProcessName, string MainWindowTitle, string FileName)
            {
                this.ProcessName = ProcessName;
                this.MainWindowTitle = MainWindowTitle;
                this.FileName = FileName;
            }

        }
        static Color color;
        private void button1_Click(object sender, EventArgs e)
        {
            blockSite.Add(textBox2.Text);
        }
        private void bunifuImageButtonApp_Click(object sender, EventArgs e)
        {
            line.Top = ((Bunifu.Framework.UI.BunifuImageButton)sender).Top - 5;
            panelBlockProg.Visible = true;
            panelBlockProg.BringToFront();
        }
        
        public void RemovePanelApp(object sender, PanelProg.AppRemoveEventArgs e)
        {
            blockApp.Remove(e.process);
            panelBlockApp.Controls.Remove(sender as PanelProg);    
        }
        private void addActivity(string activ, string processName, string title, string path)
        {
            if (flagActivity)
            {
                color = Color.FromArgb(20, 19, 31);
                flagActivity = false;
            }
            else
            {
                color = Color.FromArgb(36, 35, 50);
                flagActivity = true;
            }
            PanelActivity panel = new PanelActivity(activ, processName, title, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), color, path);
            panel5.Invoke(new Action(() => panel5.Controls.Add(panel)));
            panel.Left = 0;
            panel.Invoke(new Action(() => panel.Dock = DockStyle.Top));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UUID = GetCPUId();
            if (ExistUser(UUID))
            {
                panelLogin.BringToFront();
                IdUser = Convert.ToInt32(getIdUser(UUID));
                Start();
            }
            else
            {
                panelCheckIn.BringToFront();
            }
        }
        public void Start()
        {
            blockDirectory = new BlockDirectory(IdUser.ToString());
            blockSite = new BlockSite(IdUser.ToString());
            blockApp = new BlockApp(IdUser.ToString());
            blockDirectory.eventAdd += AddBlockDirectory;
            blockSite.eventAdd += AddBlockSite;
            blockApp.eventAdd += AddBlockApp;
            blockApp.eventBlock += Message;
            blockDirectory.Load();
            blockSite.Load();
            blockApp.Load();
            blockApp.Start();
            //getProcess(ref oldProcessOpen);
            GetProcess(ref oldProcessClose);
            currentActivity = getCurrentActivity();
            var task4 = Task.Factory.StartNew(checkActivity);
            var taskOpen = Task.Factory.StartNew(checkOpen);
            var taskClose = Task.Factory.StartNew(checkClose);
            var task5 = Task.Factory.StartNew(RefreshStatistics);
            //countDirectory.Text = BlockDirectory.listPanelsDirectory.Count.ToString();
            currentSession();
            lastSession();
            SetAutorunValue(true);
        }
        public bool ExistUser(string ProcessorId)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select COUNT(*) FROM Users where CPU=@CPU", sqlConnection);
            command.Parameters.AddWithValue("CPU", ProcessorId);
            string count = command.ExecuteScalar().ToString();
            sqlConnection.Close();
            if (count == "0")
            {
                return false;
            }
            return true;
        }
        public string getIdUser(string ProcessorId)
        {
            string id = null;
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select IdUser FROM Users where CPU=@CPU", sqlConnection);
            command.Parameters.AddWithValue("CPU", ProcessorId);
            id = command.ExecuteScalar().ToString();
            sqlConnection.Close();
            return id;
        }
        public void showMenu()
        {
            panel2.Visible = true;
        }
        public void RemovePanelDirectory(object sender, DirectoryEventArgs e)
        {
            blockDirectory.Remove(e.BlockDirectory.PathBlock);
            panelDirectory.Controls.Remove(sender as PanelDirectory);
        }
        public void RemovePanelSite(object sender, SiteEventArgs e)
        {
            try
            {
                blockSite.Remove(e.PathBlock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            panelBlockSites.Controls.Remove(sender as PanelSite);
        }
        bool flagActivity = false;
        #region paint
        private void bunifuImageButtonDirectory_Click(object sender, EventArgs e)
        {
            line.Top = ((Bunifu.Framework.UI.BunifuImageButton)sender).Top - 5;
            panelBlockDirectory.Visible = true;
            panelBlockDirectory.BringToFront();
        }
        private void bunifuImageButtonSite_Click(object sender, EventArgs e)
        {
            line.Top = ((Bunifu.Framework.UI.BunifuImageButton)sender).Top - 5;
            panelBlockSite.Visible = true;
            panelBlockSite.BringToFront();
        }
        private void choiceDirectory_MouseEnter(object sender, EventArgs e)
        {
            choiceDirectory.BackColor = Color.FromArgb(91, 87, 92);
        }
        private void choiceDirectory_MouseLeave(object sender, EventArgs e)
        {
            choiceDirectory.BackColor = Color.FromArgb(22, 21, 36);
        }
        private void addDirectory_MouseEnter(object sender, EventArgs e)
        {
            (sender as Button).BackColor = Color.FromArgb(161, 206, 149);
        }
        private void addDirectory_MouseLeave(object sender, EventArgs e)
        {

            (sender as Button).BackColor = Color.FromArgb(62, 215, 21);
        }
        private void choiceDirectory_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void addDirectory_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                try
                {
                    blockDirectory.Add(textBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Такой папки не cуществует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion paint
        List<StatisticApp> listStatistic = new List<StatisticApp>();
        List<PanelStatistics> listPanelStatistic = new List<PanelStatistics>();

        private void checkOpen()
        {
            while (true)
            {
                GetProcess(ref newProcessOpen);
                for (int i = 0; i < newProcessOpen.Count; i++)
                {
                    if (newProcessOpen[i].ProcessName == "SolBlock" || newProcessOpen[i].ProcessName == "Microsoft.Photos" || newProcessOpen[i].ProcessName == "ApplicationFrameHost" || newProcessOpen[i].ProcessName == "SystemSettings")
                        continue;
                    var found = oldProcessOpen.FindIndex(p => p.ProcessName == newProcessOpen[i].ProcessName);
                    if (found < 0)
                    {
                        addActivity("Открыто", newProcessOpen[i].ProcessName, newProcessOpen[i].MainWindowTitle, newProcessOpen[i].FileName);
                        var found1 = listStatistic.FindIndex(p => p.Name == newProcessOpen[i].ProcessName);
                        if (found1 < 0)
                        {
                            listStatistic.Add(new StatisticApp(newProcessOpen[i].ProcessName));
                            listStatistic.Last().Time.Start();
                            listStatistic.Last().DateLast = DateTime.Now;
                            var t = TimeSpan.FromMilliseconds(listStatistic.Last().Time.Elapsed.TotalMilliseconds);
                            listStatistic.Last().panel = newStatistic(newProcessOpen[i].ProcessName, $"{t.Hours}:{t.Minutes}:{t.Seconds}", DateTime.Now.ToShortDateString(), newProcessOpen[i].FileName);
                        }
                        else
                        {
                            listStatistic[found1].Time.Start();
                            listStatistic[found1].DateLast = DateTime.Now;
                        }
                    }
                }
                GetProcess(ref oldProcessOpen);
                Thread.Sleep(4000);
            }
        }
        private void checkClose()
        {
            while (true)
            {
                GetProcess(ref newProcessClose);
                for (int i = 0; i < oldProcessClose.Count; i++)
                {
                    if (oldProcessClose[i].ProcessName == "SolBlock")
                        continue;
                    var found = Process.GetProcessesByName(oldProcessClose[i].ProcessName);
                    if (found.Count() == 0)
                    {
                        addActivity("Закрыто", oldProcessClose[i].ProcessName, oldProcessClose[i].MainWindowTitle, oldProcessClose[i].FileName);
                        var found1 = listStatistic.FindIndex(p => p.Name == oldProcessClose[i].ProcessName);
                        if (found1 >= 0)
                        {
                            listStatistic[found1].Time.Stop();
                        }
                    }
                }
                GetProcess(ref oldProcessClose);
                Thread.Sleep(4000);
            }
        }
        private void checkActivity()
        {
            while (true)
            {
                try
                {
                    if (currentActivity != getCurrentActivity())
                    {
                        foreach (Process p in Process.GetProcessesByName(getCurrentActivity()))
                        {
                            if (p.ProcessName == "SolBlock")
                                continue;
                            if (p.MainWindowTitle.Length > 0)
                            {
                                addActivity("Активно", p.ProcessName, p.MainWindowTitle, p.MainModule.FileName);
                            }
                        }
                        currentActivity = getCurrentActivity();
                    }
                }
                catch (Exception ex)
                {

                }
                Thread.Sleep(4000);
            }
        }
        bool flagStatistic = false;
        private PanelStatistics newStatistic(string name, string time, string last, string path)
        {
            Color color;
            if (flagStatistic)
            {
                color = Color.FromArgb(20, 19, 31);
                flagStatistic = false;
            }
            else
            {
                color = Color.FromArgb(36, 35, 50);
                flagStatistic = true;
            }
            var panel = new PanelStatistics(name, time, last, color, path);
            panel.Dock = DockStyle.Top;
            panelStatisticApp.Invoke(new Action(() => panelStatisticApp.Controls.Add(panel)));
            listPanelStatistic.Add(panel);
            return panel;
        }
        private void addProg_Click(object sender, EventArgs e)
        {
            ChoiseAndAdd formAdd = new ChoiseAndAdd(IdUser);
            formAdd.ShowDialog();
            this.panelBlockApp.Controls.Clear();
            blockApp.Load();
        }
        private void RefreshStatistics()
        {
            while (true)
            {
                for (int i = 0; i < listStatistic.Count; i++)
                {
                    listStatistic[i].Refresh();
                }
                Thread.Sleep(3000);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void bunifuImageButtonActivity_Click(object sender, EventArgs e)
        {
            line.Top = ((Bunifu.Framework.UI.BunifuImageButton)sender).Top - 5;
            panelActivity.Visible = true;
            panelActivity.BringToFront();
        }
        private void GetProcess(ref List<MyProcess> listProcess)
        {
            listProcess.Clear();
            foreach (var p in ProcessHelper.GetRunningApplications())
            {
                if (p.Process.MainWindowTitle.Length > 0)
                {
                    try
                    {

                        listProcess.Add(new MyProcess(p.Process.ProcessName, p.Process.MainWindowTitle, p.Process.MainModule.FileName));
                    }
                    catch
                    {

                    }
                }
            }
        }
        List<MyProcess> oldProcessOpen = new List<MyProcess>();
        List<MyProcess> newProcessOpen = new List<MyProcess>();
        List<MyProcess> oldProcessClose = new List<MyProcess>();
        List<MyProcess> newProcessClose = new List<MyProcess>();
        string currentActivity = null;
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);
        public string getCurrentActivity()
        {
            IntPtr hWnd = GetForegroundWindow();
            int pid;
            //получаем pid потока активного окна
            GetWindowThreadProcessId(hWnd, out pid);
            // ввыводим в listbox PID процесса и имя процесса
            Process p = Process.GetProcessById(pid);
            return p.ProcessName;
        }
        private void bunifuImageButtonHome_Click(object sender, EventArgs e)
        {
            line.Top = ((Bunifu.Framework.UI.BunifuImageButton)sender).Top - 5;
            panelHome.Visible = true;
            panelHome.BringToFront();
        }
        private void bunifuCustomLabel15_Click(object sender, EventArgs e)
        {

        }
        private static string _username;
        private string getUser()
        {
            foreach (var p in Process.GetProcessesByName("explorer"))
            {
                _username = GetProcessOwner(p.Id);
            }

            // remove the domain part from the username
            var usernameParts = _username.Split('\\');

            _username = usernameParts[usernameParts.Length - 1];

            return _username;
        }
        private string GetProcessOwner(int processId)
        {
            var query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectCollection processList;

            using (var searcher = new ManagementObjectSearcher(query))
            {
                processList = searcher.Get();
            }

            foreach (var mo in processList.OfType<ManagementObject>())
            {
                object[] argList = { string.Empty, string.Empty };
                var returnVal = Convert.ToInt32(mo.InvokeMethod("GetOwner", argList));

                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    return argList[1] + "\\" + argList[0];
                }
            }
            return "NO OWNER";
        }
        private Stopwatch timeCurrentSession = new Stopwatch();
        string strTimeVhoda = null;
        string strDateVhoda = null;
        string strTimeSession = null;
        private void currentSession()
        {
            currentUser.Text += getUser();
            dateCurrentUser.Text += DateTime.Now.ToShortDateString();
            strDateVhoda = DateTime.Now.ToShortDateString();
            timeVhodaUser.Text += DateTime.Now.ToShortTimeString();
            strTimeVhoda = DateTime.Now.ToShortTimeString();
            timerCurrentSession.Start();
            timeCurrentSession.Start();
        }
        bool flagUser = false;
        private async Task lastSession()
        {
            await sqlConnection.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT Count(*) FROM Session where IdUser=@IdUser", sqlConnection);
            command.Parameters.AddWithValue("IdUser", IdUser);
            if (command.ExecuteScalarAsync().ToString() != "0")
            {
                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM Session where IdUser=@IdUser";
                command.Parameters.AddWithValue("IdUser", IdUser);
                SQLiteDataReader dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    flagUser = true;
                    lastUser.Text = "Пользователь "+dr["lastUser"].ToString();
                    dateLastUser.Text ="Дата входа "+ dr["lastDate"].ToString();
                    timeVhodaLastUser.Text ="Время входа "+ dr["lastTime"].ToString();
                    timeLastSession.Text="Время сессии "+ dr["lastTimeSession"].ToString();
                }
            }
            else
            {
                lastUser.Text += "---";
                dateLastUser.Text += "---";
                timeVhodaLastUser.Text += "---";
                timeLastSession.Text += "---";
            }
            sqlConnection.Close();
        }
        private void timerCurrentSession_Tick(object sender, EventArgs e)
        {
            var time = TimeSpan.FromMinutes(timeCurrentSession.Elapsed.TotalMinutes);
            timeCurrentUser.Text = $"Время сессии {time.Hours}:{time.Minutes}:{time.Seconds}";
            strTimeSession = $"{time.Hours}:{time.Minutes}:{time.Seconds}";
        }
        private void saveSession()
        {
            sqlConnection.Open();
            if (flagUser)
            {
                SQLiteCommand command = new SQLiteCommand("UPDATE Session set lastUser=@lastUser,lastTime=@lastTime,lastDate=@lastDate,lastTimeSession=@lastTimeSession WHERE IdUser=@IdUser", sqlConnection);
                command.Parameters.AddWithValue("lastUser", getUser());
                command.Parameters.AddWithValue("lastTime", strTimeVhoda);
                command.Parameters.AddWithValue("lastDate", strDateVhoda);
                command.Parameters.AddWithValue("lastTimeSession", strTimeSession);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            else
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO Session  VALUES(@IdUser,@lastUser,@lastTime,@lastDate,@lastTimeSession)", sqlConnection);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.Parameters.AddWithValue("lastUser", getUser());
                command.Parameters.AddWithValue("lastTime", strTimeVhoda);
                command.Parameters.AddWithValue("lastDate", strDateVhoda);
                command.Parameters.AddWithValue("lastTimeSession", strTimeSession);

                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    Application.Exit();
            //}
            saveSession();
        }
        private void bunifuImageButtonSetting_Click(object sender, EventArgs e)
        {
            line.Top = ((Bunifu.Framework.UI.BunifuImageButton)sender).Top - 5;
            panelStatistic.Visible = true;
            panelStatistic.BringToFront();
        }
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (textBoxPassCheckIn.TextLength < 6)
            {
                labelError.Text = "Пароль должен быть больше 6 символов!";
                labelError.Visible = true;
                return;
            }
            if (textBoxPassCheckIn2.Text != textBoxPassCheckIn.Text)
            {
                labelError.Text = "Пароли не совпадают!";
                labelError.Visible = true;
                return;
            }
            checkInUser();
            showMenu();
            panelHome.BringToFront();
            Start();
        }
        private void checkInUser()
        {
            sqlConnection.Open();
            int maxId;
            SQLiteCommand command = new SQLiteCommand("SELECT Max(IdUser) FROM Users", sqlConnection);
            string str = command.ExecuteScalar().ToString();
            if (!string.IsNullOrEmpty(str))
                maxId = Convert.ToInt32(str);
            else
                maxId = 0;
            command.CommandText = "INSERT INTO Users VALUES(@IdUser,@CPU,@Login,@Password)";
            command.Parameters.AddWithValue("IdUser", (maxId + 1));
            command.Parameters.AddWithValue("CPU", UUID);
            command.Parameters.AddWithValue("Login", UUID);
            command.Parameters.AddWithValue("Password", textBoxPassCheckIn.Text);

            command.ExecuteNonQuery();

            sqlConnection.Close();
            IdUser = maxId + 1;
        }
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT Password FROM Users where CPU=@CPU", sqlConnection);
            command.Parameters.AddWithValue("CPU", UUID);
            string pass = command.ExecuteScalar().ToString();
            sqlConnection.Close();
            if (textBoxPassLogin.Text != pass)
            {
                labelErrorLogin.Text = "Неверный пароль";
                labelErrorLogin.Visible = true;
                return;
            }
            showMenu();
            panelHome.BringToFront();
        }
        private void loadId()
        {

        }

        private void panel6_MouseEnter(object sender, EventArgs e)
        {

        }

        private void panelBlockDirectory_Paint(object sender, PaintEventArgs e)
        {

        }
        string startPath = null;
        public void AddPanelFile(string startPath, string path, Color color)
        {
            if (!panelFileSystem.Visible)
            {
                panelFileSystem.Visible = true;
                panelMoveDirectory.Visible = true;
            }
            PanelFile panel = new PanelFile(startPath, path, color);
            labelCurrentDirectory.Text = FileSystem.ParentDirectory(path);
            panel.Dock = DockStyle.Top;
            panelFileSystem.Controls.Add(panel);
            this.startPath = startPath;
        }
        public void ClearPanelFile()
        {
            panelFileSystem.Controls.Clear();
        }
        public string GetDirectoryFile()
        {
            return labelCurrentDirectory.Text;
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            if (labelCurrentDirectory.Text != startPath)
            {
                bool flag = false;
                Color color;
                ClearPanelFile();
                foreach (var item in FileSystem.GetFiles(startPath, FileSystem.ParentDirectory(labelCurrentDirectory.Text)))
                {
                    color = flag ? Color.FromArgb(20, 19, 31) : Color.FromArgb(36, 35, 50);
                    flag = !flag;
                    AddPanelFile(startPath, item, color);
                }
            }
            else
            {
                panelFileSystem.Visible = false;
                panelMoveDirectory.Visible = false;

            }

        }

        private void buttonAddBlockAppByTitle_Click(object sender, EventArgs e)
        {
            if (textBoxBlockTitle.Text.Length > 4)
            {
                blockApp.Add(new ProcessTitle(textBoxBlockTitle.Text));
                textBoxBlockTitle.Clear();
            }
            else
            {
                MessageBox.Show("Ключевое слово должно быть больше 4 букв!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
