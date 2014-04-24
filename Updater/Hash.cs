// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Hash
// LAST MODIFICATION: 24/04/2014 @ 18:21
// **********************************************

namespace Updater
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Proper hash comparison for files.
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Returns MD5 and SHA1 hashes for requested file.
        /// </summary>
        /// <param name="Path">File full location</param>
        /// <param name="MD5Hash">File's MD5 Hash</param>
        /// <param name="SHA1Hash">File's SHA1 Hash</param>
        public static void GetHash(string Path, out string MD5Hash, out string SHA1Hash)
        {
            FileInfo fileInfo = new FileInfo(Path);
            FileStream file = fileInfo.OpenRead();

            using (var md5 = MD5.Create())
            {
                MD5Hash = BitConverter.ToString(md5.ComputeHash(file)).Replace("-", "").ToLower();
            }

            using (var sha = new SHA1Managed())
            {
                SHA1Hash = BitConverter.ToString(sha.ComputeHash(file)).Replace("-", "").ToLower();
            }

            file.Dispose();
            return;
        }

        /// <summary>
        /// Returns whether the hashes are equal.
        /// </summary>
        /// <param name="File1">String array for MD5 and SHA1 Hashes for first file</param>
        /// <param name="File2">Second file location</param>
        /// <returns></returns>
        public static bool Compare(string[] File1, string File2)
        {
            FileInfo fileInfo2 = new FileInfo(File2);
            FileStream file2 = fileInfo2.OpenRead();
            string MD5File1 = File1[0];
            string MD5File2;
            string SHA1File1 = File1[1];
            string SHA1File2;

            using (var md5 = MD5.Create())
            {
                MD5File2 = BitConverter.ToString(md5.ComputeHash(file2)).Replace("-", "").ToLower();
            }

            using (var sha = new SHA1Managed())
            {
                SHA1File2 = BitConverter.ToString(sha.ComputeHash(file2)).Replace("-", "").ToLower();
            }

            file2.Dispose();
            return ((MD5File1 == MD5File2) && (SHA1File1 == SHA1File2));
        }
    }
}
