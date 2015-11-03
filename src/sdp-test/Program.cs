using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tmds.Sdp;
namespace sdp_test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("SDP file parser\r\n---------------\r\n");
            
            string filePath = "";
            if (args.Length > 0) filePath = args[0];

            if (!Directory.Exists(filePath))
            {
                Console.WriteLine("Directory not found: {0}\r\nUsage: sdp-test path-to-folder\r\nPress any key to exit...", filePath);
                Console.ReadKey();
                Environment.Exit(3);
            }

            string[] sessionFiles = Directory.GetFiles(filePath, "*.sdp", SearchOption.TopDirectoryOnly);
            List<SessionDescription> sessions = new List<SessionDescription>();
            foreach (string sdp in sessionFiles)
            {
                try
                {
                    var sessionDescription = SessionDescription.Load(File.OpenRead(sdp));
                    sessions.Add(sessionDescription);
                    Console.WriteLine("OK: {0} - {1}",sdp, sessionDescription.Name);
                }
                catch (SdpException sdpEx)
                {
                    Console.WriteLine("\r\nERROR: {0}:\r\n{1}\r\n", sdp, sdpEx.Message);
                }
            }
            Console.WriteLine("\r\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
