using System;
using System.Threading;
using System.Runtime.InteropServices;
using BuildSoft.VRChat.Osc.Chatbox;
using BuildSoft.VRChat.Osc.Avatar;
using BuildSoft.VRChat.Osc.Input;

namespace MainOSC
{
    #region Start Setup
    public class DisconnectOSC
    {
        public static bool isPlayerLogger = false;
        public static bool isInfinityTyping = false;
        public static bool isBooping = false;
        public static bool isHideName = false;
        public static bool isSpinBot = false;
        public static bool isRecording = false;
        public static bool isHelp = false;
        public static bool isSelfBot = false;
        public static int consoleWidth = Console.WindowWidth;
        public static string separator = new string('-', consoleWidth);
        public static string BlankEgg = "\u0003\u001f";

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder text, int count);

        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Command List:\n'1' - PlayerLogger\n'2' - InfinityTyping\n'3' - Booping\n'4' - HideName\n'5' - SpinBot\n'6' - Recording");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("'0' - Disable ALL");
            Console.ResetColor();
            if (isHelp)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Debug();
                Console.ResetColor();
            }
        }

        public static void Debug()
        {
            Console.WriteLine("\n-------------------\nDebug Panel\n-------------------");
            CheckDebugParameter("PlayerLogger", isPlayerLogger);
            CheckDebugParameter("InfinityTyping", isInfinityTyping);
            CheckDebugParameter("Booping", isBooping);
            CheckDebugParameter("HideName", isHideName);
            CheckDebugParameter("SpinBot", isSpinBot);
            CheckDebugParameter("Recording", isRecording);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(separator);
            Console.ResetColor();
        }
        static void CheckDebugParameter(string paramName, bool paramValue)
        {
            string status = paramValue ? "ON" : "OFF";

            Console.ForegroundColor = ConsoleColor.Blue;
            switch (paramName)
            {
                case "PlayerLogger":
                case "InfinityTyping":
                case "Booping":
                case "HideName":
                case "SpinBot":
                case "Recording":
                    Console.Write($"{paramName}: ");
                    Console.ForegroundColor = paramValue ? ConsoleColor.Green : ConsoleColor.DarkRed;
                    Console.WriteLine($"{status}");
                    break;
            }
            Console.ResetColor();
        }
        public static string GetActiveWindowTitle(IntPtr hWnd)
        {
            const int nChars = 256;
            System.Text.StringBuilder buff = new System.Text.StringBuilder(nChars);
            if (GetWindowText(hWnd, buff, nChars) > 0)
            {
                return buff.ToString();
            }
            return "";
        }

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            OscChatbox.SendMessage("Starting. ✖️" + DisconnectOSC.BlankEgg, direct: true);
            Thread.Sleep(1200);
            OscChatbox.SendMessage("Search Updates.. ➕" + DisconnectOSC.BlankEgg, direct: true);
            Thread.Sleep(1200);
            OscChatbox.SendMessage("Loading Scripts... ✖️" + DisconnectOSC.BlankEgg, direct: true);
            Thread.Sleep(1200);
            OscChatbox.SendMessage("Initializing. ➕" + DisconnectOSC.BlankEgg, direct: true);
            Thread.Sleep(1200);
            OscChatbox.SendMessage("Done! ✔️" + DisconnectOSC.BlankEgg, direct: true);
            Thread.Sleep(1000);
            OscChatbox.SendMessage("", direct: true);
            OscChatbox.SendMessage("", direct: true);


            BoopMe.Logic();

            Random random = new Random();
            ConsoleColor[] colors = Enum.GetValues(typeof(ConsoleColor))
                                      .Cast<ConsoleColor>()
                                      .Where(c => c != ConsoleColor.Black && c != ConsoleColor.White)
                                      .ToArray();

            ConsoleColor randomColor = colors[random.Next(colors.Length)];
            Console.ForegroundColor = randomColor;
            string Creator = "{ Made by Disconnect3301 with Love <3. }";
            int consolePadding = (consoleWidth - Creator.Length) / 2;
            Console.WriteLine(Creator.PadLeft(consolePadding + Creator.Length));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Command List:\n'1' - PlayerLogger\n'2' - InfinityTyping\n'3' - Booping\n'4' - HideName\n'5' - SpinBot\n'6' - Recording");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("'0' - Disable ALL");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n'9' - View Command List");
            Console.ResetColor();
            #endregion

            while (true)
            {
                OscAxisInput.Vertical.Send(-0.0f);
                #region Player Logger
                var userInput = Console.ReadKey().Key;
                Console.WriteLine();
                if (userInput == ConsoleKey.D1 || userInput == ConsoleKey.NumPad1)
                {
                    if (!isPlayerLogger && !isBooping && !isHideName)
                    {
                        PlayerLogger.Logic();
                    }
                    else if (isBooping)
                    {
                        PlayerLogger.Logic();

                        isBooping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isHideName)
                    {
                        PlayerLogger.Logic();

                        isHideName = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else
                    {
                        isPlayerLogger = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #endregion
                #region Infinity Typing
                else if (userInput == ConsoleKey.D2 || userInput == ConsoleKey.NumPad2)
                {
                    if (!isInfinityTyping && !isBooping && !isHideName)
                    {
                        InfinityTyping.Logic();
                    }
                    else if (isBooping)
                    {
                        InfinityTyping.Logic();

                        isBooping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isHideName)
                    {
                        InfinityTyping.Logic();

                        isHideName = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else
                    {
                        isInfinityTyping = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #endregion
                #region Booping
                else if (userInput == ConsoleKey.D3 || userInput == ConsoleKey.NumPad3)
                {
                    if (!isBooping && !isPlayerLogger && !isInfinityTyping && !isHideName)
                    {
                        Boop.Logic();
                    }
                    else if (isPlayerLogger && isInfinityTyping)
                    {
                        Boop.Logic();

                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger | InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isPlayerLogger)
                    {
                        Boop.Logic();

                        isPlayerLogger = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isInfinityTyping)
                    {
                        Boop.Logic();

                        isInfinityTyping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isHideName)
                    {
                        Boop.Logic();

                        isHideName = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else
                    {
                        isBooping = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #endregion
                #region Hide Name
                else if (userInput == ConsoleKey.D4 || userInput == ConsoleKey.NumPad4)
                {
                    if (!isHideName && !isPlayerLogger && !isInfinityTyping && !isBooping)
                    {
                        HideName.Logic();
                    }
                    else if (isPlayerLogger && isInfinityTyping)
                    {
                        HideName.Logic();

                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger | InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isPlayerLogger)
                    {
                        HideName.Logic();

                        isPlayerLogger = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isInfinityTyping)
                    {
                        HideName.Logic();

                        isInfinityTyping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isBooping)
                    {
                        HideName.Logic();
                        isBooping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else
                    {
                        isHideName = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #endregion
                #region Spin Bot
                else if (userInput == ConsoleKey.D5 || userInput == ConsoleKey.NumPad5)
                {
                    if (!isSpinBot)
                    {
                        SpinBot.Logic();
                    }
                    else
                    {
                        isSpinBot = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("SpinBot - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #endregion
                #region Recording
                else if (userInput == ConsoleKey.D6 || userInput == ConsoleKey.NumPad6)
                {
                    if (!isRecording)
                    {
                        Recording.Logic();
                    }
                    else
                    {
                        isRecording = false;
                        OscChatbox.SendMessage("", direct: true, complete: false);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Recording - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #endregion

                else if (userInput == ConsoleKey.D7 || userInput == ConsoleKey.NumPad7)
                {
                    if (!isSelfBot)
                    {
                        SelfBot.Logic();
                    }
                    else
                    {
                        isSelfBot = false;
                        OscChatbox.SendMessage("", direct: true, complete: false);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("SelfBot - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                }
                #region Functions Disable
                else if (userInput == ConsoleKey.D0 || userInput == ConsoleKey.NumPad0)
                {
                    isPlayerLogger = false;
                    isInfinityTyping = false;
                    isBooping = false;
                    isHideName = false;
                    isSpinBot = false;
                    isRecording = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("All Functions Disabled!");
                    Console.ResetColor();
                }
                #endregion
                #region Help Menu
                else if (userInput == ConsoleKey.D9 || userInput == ConsoleKey.NumPad9)
                {
                    if (!isHelp)
                    {
                        isHelp = true;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(separator);
                        Console.ResetColor();
                        Help();
                    }
                    else
                    {
                        isHelp = false;
                        Console.Clear();
                        Help();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Incorrect Input, Try '9' To See Command List!");
                    Console.ResetColor();
                }
                #endregion
            }
        }
    }
}
/*
Полезные Команды:
dotnet add package VRCOscLib --version 1.4.3 - добавление библиотеки VRChat в проект.
dotnet publish -c Release -p:DebugType=none -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true - Компилирование EXE файла.
dotnet publish -c Release -p:DebugType=none -r win-x64 --self-contained false -p:PublishSingleFile=true - FIX Компилирование EXE файла.

git status - показывает состояние проекта.
git add . - добавляет изменения в проект.
git commit -m "message" - коммит.
git push origin master - отправка изменений на GitHub.
*/