using WindowsInput;

namespace MainOSC
{
    class SpinBot
    {
        public static void Start()
        {
            while (DisconnectOSC.isSpinBot)
            {
                InputSimulator simulator = new InputSimulator();
                IntPtr foregroundWindowHandle = DisconnectOSC.GetForegroundWindow();
                string windowTitle = DisconnectOSC.GetActiveWindowTitle(foregroundWindowHandle);

                if (windowTitle.Contains("VRChat"))
                {
                    simulator.Mouse.MoveMouseBy(1000, 0);
                    Thread.Sleep(5);
                }
            }
        }
    }
}
