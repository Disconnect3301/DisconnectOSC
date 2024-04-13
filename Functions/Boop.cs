using System;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class Boop
    {
        public static void Logic()
        {
            DisconnectOSC.isBooping = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Booping - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STARTED!");
            Console.ResetColor();
            var boopThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            boopThread.Start();
        }
        public static void Start()
        {
            while (DisconnectOSC.isBooping)
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 3);

                string wowBoop = "";
                switch (randomNumber)
                {
                    case 1:
                        wowBoop = "*boop*";
                        break;
                    case 2:
                        wowBoop = "*\nb\no\no\np\n*";
                        break;
                }
                OscChatbox.SendMessage(wowBoop, direct: true, complete: true);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[LOG] Sending Boop!");
                Console.ResetColor();
                Thread.Sleep(100);
                OscChatbox.SendMessage("", direct: true, complete: false);
                Thread.Sleep(1500);
            }
        }
    }
}
