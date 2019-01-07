namespace Oxide.Ext.Reloader
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public static class Utilities
    {
        public static string GetMd5Checksum(string path)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(path))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash)
                    .Replace("-", string.Empty)
                    .ToLower();
            }
        }
    }
}
