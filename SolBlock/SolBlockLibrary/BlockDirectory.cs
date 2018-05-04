using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SolBlock;
using System.Security.AccessControl;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace SolBlockLibrary
{
    public class DirectoryEventArgs : EventArgs
    {
        public ModelBlockDirectory BlockDirectory { get; }
        public DirectoryEventArgs(ModelBlockDirectory model)
        {
            this.BlockDirectory = model;
        }
    }
    public class BlockDirectory
    {
        private List<ModelBlockDirectory> list = new List<ModelBlockDirectory>();
        public EventHandler<DirectoryEventArgs> eventAdd;
        private string IdUser;
        public BlockDirectory(string IdUser)
        {
            this.IdUser = IdUser;
        }
        private void AddBlock(ModelBlockDirectory model)
        {
            list.Add(model);
            eventAdd?.Invoke(model,new DirectoryEventArgs(model));
        }
        public void Load()
        {
            foreach(var item in SolBlockDB.GetBlockDirectory(IdUser))
            {
                AddBlock(item);
            }
        }
        public void Add(string path)
        {
            if (IsNotSystem(path))
            {
                try
                {
                    FileSystem.SaveStructureFile(path);
                    DirectorySecurity dirSec = new DirectorySecurity(); ;
                    System.Security.Principal.WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();
                    string user = wi.Name;
                    FileSystemAccessRule myRule = new FileSystemAccessRule(@user, FileSystemRights.FullControl, AccessControlType.Deny);
                    dirSec.AddAccessRule(myRule);
                    Directory.SetAccessControl(path, dirSec);
                }
                catch
                {
                    throw new Exception("Ошибка блокировки!Попробуйте запустить от имени администратора!");
                }
                try
                {
                    SolBlockDB.InsertBlockDirectory(path, IdUser);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                AddBlock(new ModelBlockDirectory(null, path));
                
            }
            else
            {
                throw new Exception("Нельзя блокировать диски и системные папки");
            }
        }
        private bool IsNotSystem(string path)
        {
            if (path.Length <= 3) return false;
            if (path.Length >= 10 && path.Substring(path.IndexOf('\\') + 1, 7) == "Windows") return false;
            if (path.Length >= 22 && path.Substring(path.IndexOf('\\') + 1, 19) == "Program Files (x86)") return false;
            if (path.Length >= 16 && path.Substring(path.IndexOf('\\') + 1, 13) == "Program Files") return false;
            return true;
        }
        public void Remove(string path)
        {
            try
            {
                IdentityReference identityUserData = new NTAccount(SystemInformation.UserDomainName, SystemInformation.UserName);
                var fSec = File.GetAccessControl(path);
                fSec.RemoveAccessRule(new FileSystemAccessRule(identityUserData, FileSystemRights.FullControl, AccessControlType.Deny));
                File.SetAccessControl(path, fSec);
            }
            catch{throw new Exception("Ошибка блокировки!Попробуйте запустить от имени администратора!");}
            try { SolBlockDB.RemoveBlockDirectory(path, IdUser); }
            catch(Exception ex) { throw new Exception(ex.Message); }
            
        }
    }

}
