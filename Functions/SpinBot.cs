using WindowsInput;
using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class SpinBot
    {
        public static void Logic()
        {
            DisconnectOSC.isSpinBot = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("SpinBot - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STARTED!");
            Console.ResetColor();
            var spinBotThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            spinBotThread.Start();
        }
        public static void Start()
        {
            Random random = new Random();
            int randomSpeed = random.Next(-500, 501);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Random Speed: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(randomSpeed + "!");
            Console.ResetColor();

            while (DisconnectOSC.isSpinBot)
            {
                InputSimulator simulator = new InputSimulator();
                IntPtr foregroundWindowHandle = DisconnectOSC.GetForegroundWindow();
                string windowTitle = DisconnectOSC.GetActiveWindowTitle(foregroundWindowHandle);

                if (windowTitle.Contains("VRChat"))
                {
                    simulator.Mouse.MoveMouseBy(randomSpeed, 0);
                    Thread.Sleep(5);
                }
            }
        }
    }
}
