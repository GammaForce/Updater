// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Files
// LAST MODIFICATION: 23/04/2014 @ 16:32
// **********************************************

namespace Updater
{
    using System;

    public static class Files
    {
        public class LocalFile
        {
            public string Filename;
            public string MD5Hash;
            public string SHA1Hash;
            public bool correctHash;

            public LocalFile(string filename, string md5hash, string sha1hash)
            {
                this.Filename = filename;
                this.MD5Hash = md5hash;
                this.SHA1Hash = sha1hash;
            }

            public LocalFile(string filename, string md5hash, string sha1hash, bool correcthash)
            {
                this.Filename = filename;
                this.MD5Hash = md5hash;
                this.SHA1Hash = sha1hash;
                this.correctHash = correcthash;
            }
        }

        public class RemoteFile
        {
            public string Filename;
            public string MD5Hash;
            public string SHA1Hash;

            public RemoteFile(string filename, string md5hash, string sha1hash)
            {
                this.Filename = filename;
                this.MD5Hash = md5hash;
                this.SHA1Hash = sha1hash;
            }
        }
    }
}
