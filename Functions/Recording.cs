using System;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class Recording
    {
        public static void Logic()
        {
            DisconnectOSC.isRecording = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Recording - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STARTED!");
            Console.ResetColor();
            var recordingThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            recordingThread.Start();
        }
        public static void Start()
        {
            DateTime startTime = DateTime.Now;
            while (DisconnectOSC.isRecording)
            {
                TimeSpan elapsed = DateTime.Now - startTime;
                string timeString = string.Format("[REC {0}] {1:hh\\:mm\\:ss}", GetRecordingSymbol(), elapsed);
                OscChatbox.SendMessage(timeString, direct: true, complete: false);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[LOG] " + timeString);
                Console.ResetColor();
                Thread.Sleep(1500);
            }
        }

        static string GetRecordingSymbol()
        {
            return DateTime.Now.Second % 2 == 0 ? "🔘" : "  ";
        }
    }
}