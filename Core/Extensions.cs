using System;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Updater.Core
{
    public class Extensions
    {
        public static bool CheckFile(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open)) ;
            }
            catch (IOException ex)
            {
                int num = Marshal.GetHRForException((Exception) ex) & (int) ushort.MaxValue;
                return num == 32 || num == 33;
            }

            return false;
        }

        public static bool Ping()
        {
            try
            {
                PingReply pingReply = new Ping().Send("ninonline.org");
                return pingReply != null && pingReply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Connect()
        {
            try
            {
                new TcpClient().Connect("ninonline.org", (int) Convert.ToInt16(80));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool FileLocked(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            FileStream fileStream = (FileStream) null;

            try
            {
                fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }

            catch (IOException ex)
            {
                return true;
            }

            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }

            return false;
        }

        public static void ForceDelete(string path)
        {
            int num = 0;

            while (!Extensions.AttemptDelete(path))
            {
                ++num;
                if (num == 10)
                    break;
                Thread.Sleep(100);
            }
        }

        public static bool AttemptDelete(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
