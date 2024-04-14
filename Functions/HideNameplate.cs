using System;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class HideName
    {
        public static void Logic()
        {
            DisconnectOSC.isHideName = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("HideName - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STARTED!");
            Console.ResetColor();
            var hideNameThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            hideNameThread.Start();
        }
        public static void Start()
        {
            while (DisconnectOSC.isHideName)
            {
                string HideMe = "";
                OscChatbox.SendMessage(HideMe, direct: true);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[LOG] Sending HideName!");
                Console.ResetColor();
                Thread.Sleep(5000);
            }
        }
    }
}