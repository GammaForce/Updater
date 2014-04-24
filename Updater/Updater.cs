// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Updater
// LAST MODIFICATION: 24/04/2014 @ 20:24
// **********************************************

namespace Updater
{
    using System;
    using System.Threading;
    using System.Net;
    using System.IO;
    using System.Xml;
    using System.Collections.Generic;

    /// <summary>
    /// The 'core' of the Updater.
    /// Class for: checking updates, updating and such.
    /// </summary>
    public static class Updater
    {
        private static WebClient webClient = new WebClient();
        private static List<string> outdatedFiles = new List<string>();

        /// <summary>
        /// Returns the remote address for downloading and checking updates.
        /// </summary>
        /// <example>http://127.0.0.1/updater/ultimatium/</example>
        private static string Host
        {
            get
            {
                string host = "http://127.0.0.1/updater/";
                if (Program.projectName == "Ultimatium")
                    return host + "ultimatium/";
                if (Program.projectName == "Plutonium")
                    return host + "plutonium/";
                else return string.Empty;
            }
        }

        /// <summary>
        /// Returns whether the Update Server is available or not.
        /// </summary>
        private static bool updateServerAvailable
        {
            get
            {
                try
                {
                    while (webClient.IsBusy)
                    {
                        Thread.Sleep(100);
                    }
                    webClient.DownloadData(new Uri(Host + "info.xml"));
                    return true;
                }
                catch (Exception) { return false; }
            }
        }

        /// <summary>
        /// New updates checking.
        /// </summary>
        public static void Init()
        {
            if (!updateServerAvailable)
            {
                Log.Write("ERROR: Update Server not available. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0x2);
            }
            Log.Write("Update Server is online.");
            if (Outdated())
            {
                Update();
                Launcher.Run();
            }
            else Launcher.Run();
        }

        /// <summary>
        /// Return whether the client is outdated or not
        /// </summary>
        private static bool Outdated()
        {
            while (webClient.IsBusy)
            {
                Thread.Sleep(100);
            }

            try
            {
                Log.Write("Checking for updates...");
                List<Files.RemoteFile> remoteFiles = new List<Files.RemoteFile>();
                XmlDocument XML = new XmlDocument();
                XML.Load(Host + "info.xml");
                XmlNodeList Updater = XML.GetElementsByTagName("Updater");
                XmlNodeList Info = ((XmlElement)Updater[0]).GetElementsByTagName("FileInfo");
                XmlNodeList CacheInfo = ((XmlElement)Info[0]).GetElementsByTagName("File");
                foreach (XmlElement File in CacheInfo)
                {
                    remoteFiles.Add(new Files.RemoteFile(File.GetAttribute("name"), File.GetAttribute("md5"), File.GetAttribute("sha1")));
                }
                Log.Write("Remote updater information obtained.");
                Log.Write("Current version: 1337 - Newest version: 1337");
                List<Files.LocalFile> localFiles = new List<Files.LocalFile>();
                foreach (Files.RemoteFile file in remoteFiles)
                {
                    if (!File.Exists(Environment.CurrentDirectory + @"\" + file.Filename))
                    {
                        outdatedFiles.Add(file.Filename);
                        continue;
                    }
                    string path = Environment.CurrentDirectory + @"\" + file.Filename;
                    string MD5Hash;
                    string SHA1Hash;
                    Hash.GetHash(path, out MD5Hash, out SHA1Hash);
                    Files.LocalFile localFile = new Files.LocalFile(file.Filename, MD5Hash, SHA1Hash);
                    localFiles.Add(localFile);
                    if ((localFile.MD5Hash == file.MD5Hash) && (localFile.SHA1Hash == file.SHA1Hash))
                    {
                        localFile.correctHash = true;
                    }
                    else
                    {
                        localFile.correctHash = false;
                    }
                }


                foreach (Files.LocalFile file in localFiles.FindAll(x => x.correctHash == false))
                {
                    outdatedFiles.Add(file.Filename);
                }

                if (outdatedFiles.Count > 1) Log.Write(outdatedFiles.Count + " files need update.");
                if (outdatedFiles.Count == 1) Log.Write(outdatedFiles.Count + " file needs update.");

                if (outdatedFiles.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            catch (Exception)
            {
                Log.Write("ERROR: Could not retrieve outdated files. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0x1);
                return false;
            }
        }

        /// <summary>
        /// Updates the client.
        /// </summary>
        private static void Update()
        {
            try
            {
                foreach (string file in outdatedFiles.ToArray())
                {
                    while (webClient.IsBusy)
                    {
                        Thread.Sleep(100);
                    }
                    Log.Write("Downloading " + file + "...");
                    /*byte[] _fileContent = webClient.DownloadData(new Uri(Host + file));
                    byte[] fileContent = Utils.Decompress(_fileContent);*/
                    // Remote files aren't compressed yet, skip decompression step ^
                    byte[] fileContent = webClient.DownloadData(new Uri(Host + file));
                    if (File.Exists(Environment.CurrentDirectory + @"\" + file)) File.Delete(Environment.CurrentDirectory + @"\" + file);
                    File.WriteAllBytes(Environment.CurrentDirectory + @"\" + file, fileContent);
                    Log.Write("Download successful for " + file);
                    lock (outdatedFiles)
                    {
                        outdatedFiles.Remove(file);
                    }
                }

                Log.Write("Updates successfully installed.");
                return;
            }
            catch (Exception)
            {
                Log.Write("ERROR: Could not update. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0x1);
                return;
            }
        }
    }
}
