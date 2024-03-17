using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class InfinityTyping
    {
        public static void Logic()
        {
            DisconnectOSC.isInfinityTyping = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("InfinityTyping - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STARTED!");
            Console.ResetColor();
            var infinityTypingThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            infinityTypingThread.Start();
        }
        public static void Start()
        {
            while (DisconnectOSC.isInfinityTyping)
            {
                OscChatbox.SetIsTyping(true);
                Thread.Sleep(2000);
            }
        }
    }
}
