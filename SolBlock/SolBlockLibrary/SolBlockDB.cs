using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

using System.Windows.Forms;

namespace SolBlockLibrary
{
    public class SolBlockDB
    {
        private static SQLiteConnection sqlConnection = new SQLiteConnection(@"Data Source="+Application.StartupPath+"\\SolBlockDB.db; Version=3");
        public static List<ModelBlockDirectory> GetBlockDirectory(string IdUser)
        {
            var list = new List<ModelBlockDirectory>();
            try
            {
                sqlConnection.Open();
                var command = new SQLiteCommand("SELECT * FROM BlockDirectory where IdUser=@IdUser", sqlConnection);
                command.Parameters.AddWithValue("IdUser", IdUser);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ModelBlockDirectory(dr["IdBlock"].ToString(), dr["PathBlock"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return list;
        }
        public static void InsertBlockDirectory(string Directory, string IdUser)
        {
            try
            {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockDirectory VALUES(@IdBlock,@Directory,@IdUser)", sqlConnection);
                command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
                command.Parameters.AddWithValue("Directory", Directory);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static void RemoveBlockDirectory(string Directory, string IdUser)
        {
            try
            {
                sqlConnection.Open();
                var command = new SQLiteCommand("DELETE FROM BlockDirectory WHERE PathBlock=@Path and IdUser=@IdUser", sqlConnection);
                command.Parameters.AddWithValue("Path", Directory);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static List<string> GetBlockSite(string IdUser)
        {
            var list = new List<string>();
            try
            {
                sqlConnection.Open();
                var command = new SQLiteCommand("SELECT * FROM BlockSite where IdUser=@IdUser", sqlConnection);
                command.Parameters.AddWithValue("IdUser", IdUser);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr["SiteBlock"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return list;
        }
        public static void InsertBlockSite(string PathBlock, string IdUser)
        {
            try
            {
                sqlConnection.Open();
                var command = new SQLiteCommand($"INSERT INTO BlockSite VALUES(@IdBlock,@Site,@IdUser)", sqlConnection);
                command.Parameters.AddWithValue("IdBlock", Guid.NewGuid());
                command.Parameters.AddWithValue("Site", PathBlock);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static void RemoveBlockSite(string PathBlock, string IdUser)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("DELETE FROM BlockSite WHERE SiteBlock=@Site and IdUser=@IdUser", sqlConnection);
            command.Parameters.AddWithValue("Site", PathBlock);
            command.Parameters.AddWithValue("IdUser", IdUser);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public static List<ProcessAllTime> GetBlockAppAllTime(string IdUser)
        {
            List<ProcessAllTime> list = new List<ProcessAllTime>();
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM BlockProg where IdUser=@IdUser AND TypeBlock='AllTime'", sqlConnection);
            command.Parameters.AddWithValue("IdUser", IdUser);
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new ProcessAllTime(dr["NameProgBlock"].ToString()));
                }
            }
            sqlConnection.Close();
            return list;
        }
        public static List<ProcessAnyTime> GetBlockAppAnyTime(string IdUser)
        {
            List<ProcessAnyTime> list = new List<ProcessAnyTime>();
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM BlockProg where IdUser=@IdUser AND TypeBlock='AnyTime'", sqlConnection);
            command.Parameters.AddWithValue("IdUser", IdUser);
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new ProcessAnyTime(dr["NameProgBlock"].ToString(), Convert.ToInt32(dr["TimeBlock"])));
                }
            }
            sqlConnection.Close();
            return list;
        }
        public static List<ProcessDiapasonTime> GetBlockAppDiapasonTime(string IdUser)
        {
            List<ProcessDiapasonTime> list = new List<ProcessDiapasonTime>();
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM BlockProg where IdUser=@IdUser AND TypeBlock='DiapasonTime'", sqlConnection);
            command.Parameters.AddWithValue("IdUser", IdUser);
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new ProcessDiapasonTime(dr["NameProgBlock"].ToString(), ParseTime(dr["DiapasonBlock"].ToString(), true), ParseTime(dr["DiapasonBlock"].ToString(), false)));
                }
            }
            sqlConnection.Close();
            return list;
        }
        public static List<ProcessTitle> GetBlockAppTitle(string IdUser)
        {
            List<ProcessTitle> list = new List<ProcessTitle>();
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM BlockProg where IdUser=@IdUser AND TypeBlock='Title'", sqlConnection);
            command.Parameters.AddWithValue("IdUser", IdUser);
            using (SQLiteDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new ProcessTitle(dr["NameProgBlock"].ToString()));
                }
            }
            sqlConnection.Close();
            return list;
        }
        public static void RemoveBlockApp(string NameApp, string IdUser)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("DELETE FROM BlockProg WHERE NameProgBlock=@Prog and IdUser=@IdUser", sqlConnection);
            command.Parameters.AddWithValue("Prog", NameApp);
            command.Parameters.AddWithValue("IdUser", IdUser);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public static void InsertBlockApp(IProcess process, string IdUser)
        {
            sqlConnection.Open();
            if (process is ProcessAnyTime)
            {
                var item = process as ProcessAnyTime;
                SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,TimeBlock,NameProgBlock,IdUser) values(@IdBlock,@TypeBlock,@TimeBlock,@NameBlock,@IdUser)", sqlConnection);
                command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
                command.Parameters.AddWithValue("TypeBlock", "AnyTime");
                command.Parameters.AddWithValue("TimeBlock", item.Time.ToString());
                command.Parameters.AddWithValue("NameBlock", item.Name);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            if (process is ProcessAllTime)
            {
                var item = process as ProcessAllTime;
                SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,NameProgBlock,IdUser) VALUES (@IdBlock,@TypeBlock,@NameBlock,@IdUser)", sqlConnection);
                command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
                command.Parameters.AddWithValue("TypeBlock", "AllTime");
                command.Parameters.AddWithValue("NameBlock", item.Name);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            if (process is ProcessDiapasonTime)
            {
                var item = process as ProcessDiapasonTime;
                SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,DiapasonBlock,NameProgBlock,IdUser) VALUES (@IdBlock,@TypeBlock,@DiapasonBlock,@NameBlock,@IdUser)", sqlConnection);
                command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
                command.Parameters.AddWithValue("TypeBlock", "DiapasonTime");
                command.Parameters.AddWithValue("DiapasonBlock", item.From + "-" + item.Before);
                command.Parameters.AddWithValue("NameBlock", item.Name);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            if(process is ProcessTitle)
            {
                var item = process as ProcessTitle;
                SQLiteCommand command = new SQLiteCommand("INSERT INTO BlockProg (IdBlock,TypeBlock,NameProgBlock,IdUser,Title) VALUES (@IdBlock,@TypeBlock,@NameBlock,@IdUser,@Title)", sqlConnection);
                command.Parameters.AddWithValue("IdBlock", System.Guid.NewGuid());
                command.Parameters.AddWithValue("TypeBlock", "Title");
                command.Parameters.AddWithValue("Title", item.Name);
                command.Parameters.AddWithValue("NameBlock", item.Name);
                command.Parameters.AddWithValue("IdUser", IdUser);
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        public static int GetTimeBlockApp(string NameApp, string IdUser)
        {
            sqlConnection.Open();
            var command = new SQLiteCommand("SELECT TimeBlock FROM BlockProg where NameProgBlock=@Name and IdUser=@IdUser", sqlConnection);
            command.Parameters.AddWithValue("Name", NameApp);
            command.Parameters.AddWithValue("IdUser", IdUser);
            SQLiteDataReader dr = command.ExecuteReader();
            int time=0;
            while (dr.Read())
            {
                time=Convert.ToInt32(dr["TimeBlock"]);
            }
            sqlConnection.Close();
            return time;
        }
        public static void SetTimeBlockApp(string NameApp,string Time,string IdUser)
        {
            sqlConnection.Open();
            var command = new SQLiteCommand("UPDATE BlockProg SET TimeBlock=@Time where NameProgBlock=@Name and IdUser=@IdUser", sqlConnection);
            command.Parameters.AddWithValue("Time", Time);
            command.Parameters.AddWithValue("Name", NameApp);
            command.Parameters.AddWithValue("IdUser", IdUser);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private static int ParseTime(string FromBefore, bool first)
        {
            if (first)
            {
                return Convert.ToInt32(FromBefore.Substring(0, FromBefore.IndexOf("-")));
            }
            else
            {
                return Convert.ToInt32(FromBefore.Substring(1 + FromBefore.IndexOf("-")));
            }
        }

    }
}
