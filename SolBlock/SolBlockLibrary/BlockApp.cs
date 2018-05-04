using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolBlockLibrary
{
    public class AppEventArgs : EventArgs
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public AppEventArgs(string Name, string Type, string Info)
        {
            this.Name = Name;
            this.Type = Type;
            this.Info = Info;
        }
    }
    public interface IProcess
    {
        string Name { get; set; }
    }
    public class ProcessAnyTime : IProcess
    {
        public string Name { get; set; }
        public int Time { get; set; }
        public ProcessAnyTime(string Name, int Time)
        {
            this.Name = Name;
            this.Time = Time;
        }
    }
    public class ProcessDiapasonTime : IProcess
    {
        public string Name { get; set; }
        public int From { get; set; }
        public int Before { get; set; }
        public ProcessDiapasonTime(string Name, int From, int Before)
        {
            this.Name = Name;
            this.From = From;
            this.Before = Before;
        }
    }
    public class ProcessAllTime : IProcess
    {
        public string Name { get; set; }
        public ProcessAllTime(string Name)
        {
            this.Name = Name;
        }
    }
    public class ProcessTitle : IProcess
    {
        public string Name { get; set; }
        public ProcessTitle(string Name)
        {
            this.Name = Name;
        }
    }
    public class BlockApp
    {
        List<ProcessAllTime> listProcessAllTime = new List<ProcessAllTime>();
        List<ProcessAnyTime> listProcessAnyTime = new List<ProcessAnyTime>();
        List<ProcessDiapasonTime> listProcessDiapasonTime = new List<ProcessDiapasonTime>();
        List<ProcessTitle> listTitle = new List<ProcessTitle>();
        public EventHandler<AppEventArgs> eventAdd;
        public EventHandler<AppEventArgs> eventBlock;
        string IdUser;
        public BlockApp(string IdUser)
        {
            this.IdUser = IdUser;
        }
        private void AddBlock(IProcess process)
        {
            if (process is ProcessAllTime)
            {
                var item = process as ProcessAllTime;
                listProcessAllTime.Add(item);
                eventAdd?.Invoke(item, new AppEventArgs(item.Name, "AllTime", null));
                return;
            }
            if (process is ProcessAnyTime)
            {
                var item = process as ProcessAnyTime;
                listProcessAnyTime.Add(item);
                eventAdd?.Invoke(item, new AppEventArgs(item.Name, "AnyTime", item.Time.ToString()));
                return;
            }
            if (process is ProcessDiapasonTime)
            {
                var item = process as ProcessDiapasonTime;
                listProcessDiapasonTime.Add(item);
                eventAdd?.Invoke(item, new AppEventArgs(item.Name, "DiapasonTime", item.From.ToString() + "-" + item.Before.ToString()));
                return;
            }
            if(process is ProcessTitle)
            {
                var item = process as ProcessTitle;
                listTitle.Add(item);
                eventAdd?.Invoke(item, new AppEventArgs(item.Name, "Title", null));
                return;
            }

        }
        public void Load()
        {
            listProcessAllTime.Clear();
            listProcessAnyTime.Clear();
            listProcessDiapasonTime.Clear();
            listTitle.Clear();
            List<IProcess> listProcess = new List<IProcess>();
            listProcess.AddRange(SolBlockDB.GetBlockAppAllTime(IdUser));
            listProcess.AddRange(SolBlockDB.GetBlockAppAnyTime(IdUser));
            listProcess.AddRange(SolBlockDB.GetBlockAppDiapasonTime(IdUser));
            listProcess.AddRange(SolBlockDB.GetBlockAppTitle(IdUser));
            foreach (var item in listProcess)
            {
                AddBlock(item);
            }
        }
        public void Add(IProcess process)
        {
            SolBlockDB.InsertBlockApp(process, IdUser);
            AddBlock(process);
        }
        public void Remove(IProcess process)
        {
            
            if(process is ProcessAllTime)
            {
                var item = process as ProcessAllTime;
                listProcessAllTime.Remove(item);
                SolBlockDB.RemoveBlockApp(item.Name, IdUser);
                return;
            }
            if(process is ProcessAnyTime)
            {
                var item = process as ProcessAnyTime;
                listProcessAnyTime.Remove(item);
                SolBlockDB.RemoveBlockApp(item.Name, IdUser);
                return;
            }
            if(process is ProcessDiapasonTime)
            {
                var item = process as ProcessDiapasonTime;
                listProcessDiapasonTime.Remove(item);
                SolBlockDB.RemoveBlockApp(item.Name, IdUser);
                return;
            }
            if (process is ProcessTitle)
            {
                var item = process as ProcessTitle;
                listTitle.Remove(item);
                SolBlockDB.RemoveBlockApp(item.Name, IdUser);
                return;
            }
        }
        public static int hours(string time)
        {
            return Convert.ToInt32(time.Substring(0, time.IndexOf(":")));
        }
        public void Start()
        {
            var task1 = Task.Factory.StartNew(CheckAnyTime);
            var task2 = Task.Factory.StartNew(CheckAllTime);
            var task3 = Task.Factory.StartNew(CheckDiapasonTime);
            var task4 = Task.Factory.StartNew(CheckTitle);
        }
        private void CheckDiapasonTime()
        {
            bool test = false;
            string path = null;
            string process = null;
            while (true)
            {
                for (int i = 0; i < listProcessDiapasonTime.Count; i++)
                {
                    test = false;
                    DateTime time = new DateTime();
                    time = DateTime.Now;
                    int hour = hours(time.ToShortTimeString());
                    foreach (Process p in Process.GetProcessesByName(listProcessDiapasonTime[i].Name))
                    {

                        if (hour >= listProcessDiapasonTime[i].From && hour < listProcessDiapasonTime[i].Before)
                        {
                            break;
                        }
                        else
                        {
                            try
                            {
                                path = p.MainModule.FileName;
                                process = p.ProcessName;
                            }
                            catch { }
                            p.Kill();
                            if (!test)
                            {
                                test = true;
                                eventBlock?.Invoke(listProcessDiapasonTime[i], new AppEventArgs(listProcessDiapasonTime[i].Name, "DiapasonTime", path));
                            }

                        }
                    }

                }
                Thread.Sleep(2000);
            }
        }
        private void CheckAnyTime()
        {
            bool flag = false;
            string path = null;
            string process = null;
            while (true)
            {
                for (int i = 0; i < listProcessAnyTime.Count; i++)
                {

                    foreach (Process p in Process.GetProcessesByName(listProcessAnyTime[i].Name))
                    {

                        listProcessAnyTime[i].Time = SolBlockDB.GetTimeBlockApp(p.ProcessName, IdUser);
                        if (listProcessAnyTime[i].Time == 0)
                        {
                            try
                            {
                                path = p.MainModule.FileName;
                                process = p.ProcessName;
                            }
                            catch { }
                            p.Kill();
                            flag = true; 
                        }
                        else
                        {
                            SolBlockDB.SetTimeBlockApp(listProcessAnyTime[i].Name, (listProcessAnyTime[i].Time - 1).ToString(), IdUser);
                            break;
                        }
                    }
                    if (flag)
                    {
                        eventBlock?.Invoke(listProcessAnyTime[i], new AppEventArgs(listProcessAnyTime[i].Name, "AnyTime", path));
                    }
                    flag = false;
                }
                Thread.Sleep(60000);
            }
        }
        private void CheckAllTime()
        {
            bool flag = false;
            string path = null;
            string process = null;
            while (true)
            {
                for (int i = 0; i < listProcessAllTime.Count; i++)
                {
                    foreach (Process p in Process.GetProcessesByName(listProcessAllTime[i].Name))
                    {
                        try
                        {
                            p.Kill();
                            flag = true;
                            path = p.MainModule.FileName;
                            process = p.ProcessName;
                            p.CloseMainWindow();
                        }
                        catch
                        {

                        }   
                    }
                    if (flag)
                    {
                        eventBlock?.Invoke(listProcessAllTime[i], new AppEventArgs(listProcessAllTime[i].Name, "AllTime", path));
                    }
                    flag = false;
                }
                Thread.Sleep(2000);
            }
        }
        private void CheckTitle()
        {
            bool flag = false;
            string path = null;
            string process = null;
            while (true)
            {
                for (int i = 0; i < listTitle.Count; i++)
                {
                    foreach (Process p in GetProccesByTitle(listTitle[i].Name))
                    {
                        try
                        {
                            p.Kill();
                            flag = true;
                            path = p.MainModule.FileName;
                            process = p.ProcessName;
                            p.CloseMainWindow();
                        }
                        catch
                        {

                        }
                    }
                    if (flag)
                    {
                        eventBlock?.Invoke(listTitle[i], new AppEventArgs(process, "Title", path));
                    }
                    flag = false;
                }
                Thread.Sleep(2000);
            }
        }
        private List<Process> GetProccesByTitle(string Title)
        {
            List<Process> list = new List<Process>();
            foreach(Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.Length > 0 && p.MainWindowTitle.ToLower().Contains(Title.ToLower()))
                {
                    foreach(Process item in Process.GetProcessesByName(p.ProcessName))
                    {
                        list.Add(item);
                    }
                    return list;
                }
            }
            return list;
        }
    }
}
