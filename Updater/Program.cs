// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Program
// LAST MODIFICATION: 24/04/2014 @ 18:31
// **********************************************

namespace Updater
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Updater's main class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Returns the project's name the Updater has to check updates for.
        /// </summary>
        public static string projectName
        {
            get
            {
                string currentPath = Environment.CurrentDirectory;
                DirectoryInfo dirInfo = new DirectoryInfo(currentPath);
                if ((dirInfo.GetFiles("BlackOps*").Length > 0) || dirInfo.GetFiles("t5*").Length > 0)
                    return "Ultimatium";
                if (dirInfo.GetFiles("iw5*").Length > 0)
                    return "Plutonium";
                else return string.Empty;
            }
        }

        /// <summary>
        /// Returns the arguments (previously stored in an array) in a unique string.
        /// </summary>
        public static string Arguments
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                string[] commandLine = Environment.GetCommandLineArgs();
                foreach (string current in commandLine)
                {
                    stringBuilder.Append(current);
                    stringBuilder.Append(" ");
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Program's execution point.
        /// It calls a method to check for updates.
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {
            Log.Write("Initializing...");
            if (projectName == string.Empty)
            {
                Log.Write("ERROR: The Updater has to be located in the game's main folder. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0x1);
            }
            Updater.Init();
            Console.ReadKey();
        }
    }
}
