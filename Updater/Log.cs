// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Log
// LAST MODIFICATION: 23/04/2014 @ 16:33
// **********************************************

namespace Updater
{
    using System;

    /// <summary>
    /// Simple and primitive logging class.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Writes a console message with a timestamp.
        /// </summary>
        /// <param name="Message">Message to be printed</param>
        public static void Write(string Message)
        {
            Message = DateTime.Now.ToString("[dd-MM-yyyy @ HH:mm:ss]") + " - " + Message;
            Console.WriteLine(Message);
        }
    }
}
