using System;
using System.IO;
using System.Security.Cryptography;

namespace Updater
{
    public static class SHA256
    {
        public static string GetChecksum(string file)
        {
            try
            {
                using (var stream = File.OpenRead(file))
                {
                    var sha = new SHA256Managed();
                    byte[] checksum = sha.ComputeHash(stream);
                    return BitConverter.ToString(checksum).Replace("-", String.Empty);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
