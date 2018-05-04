using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolBlockLibrary
{
    public class SiteEventArgs : EventArgs
    {
        public string PathBlock { get; set; }
        public SiteEventArgs(string PathBlock)
        {
            this.PathBlock = PathBlock;
        }
    }
    public class BlockSite
    {
        private List<string> list = new List<string>();
        private string IdUser;
        public EventHandler<SiteEventArgs> eventAdd;
        public BlockSite(string IdUser)
        {
            this.IdUser = IdUser;
        }
        private void AddBlock(string pathBlock)
        {
            list.Add(pathBlock);
            eventAdd?.Invoke(this, new SiteEventArgs(pathBlock));
        }
        public void Load()
        {
            foreach(var item in SolBlockDB.GetBlockSite(IdUser))
            {
                AddBlock(item);
            }
        }
        public void Add(string pathBlock)
        {
            try
            {
                using (var sw = File.AppendText(@"C:\Windows\System32\drivers\etc\hosts"))
                {
                    sw.WriteLine("\n127.0.0.1 " + pathBlock);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Отказано в доступе!Запустите от имени администратора и выключите антивирус");
            }
            try
            {
                SolBlockDB.InsertBlockSite(pathBlock, IdUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            AddBlock(pathBlock);
        }
        public void Remove(string site)
        {
            try
            {
                string[] readText = System.IO.File.ReadAllLines(@"C:\Windows\System32\drivers\etc\hosts");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Windows\System32\drivers\etc\hosts", false))
                {
                    for (int i = 0; i < readText.Length; i++)
                    {
                        if (readText[i] != "127.0.0.1 " + site)
                            file.WriteLine(readText[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            try
            {
                SolBlockDB.RemoveBlockSite(site, IdUser);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
