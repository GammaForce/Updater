// **********************************************
// Updater - By UngarMax
// ----------------------------------------------
// PROJECT: GammaForce
// COMPONENT: Updater
// SUBCOMPONENT: Utils
// LAST MODIFICATION: 24/04/2014 @ 18:24
// **********************************************

namespace Updater
{
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Random general functions.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Returns a decompressed byte array of a file compressed with GZip
        /// </summary>
        /// <param name="Input">Compressed file bytes</param>
        /// <returns>Decompressed file bytes</returns>
        /// <remarks>Find a better compression system?</remarks>
        public static byte[] Decompress(byte[] Input)
        {
            MemoryStream InputMS = new MemoryStream(Input);
            MemoryStream OutputMS = new MemoryStream();
            using (GZipStream GZip = new GZipStream(InputMS, CompressionMode.Decompress))
            {
                GZip.CopyTo(OutputMS);
                GZip.Dispose();
            }
            return OutputMS.ToArray();
        }
    }
}
