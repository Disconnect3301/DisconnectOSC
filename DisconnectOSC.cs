using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;
using System.Threading.Tasks;
using System.Globalization;
namespace MainOSC
{
    public class DisconnectOSC
    {
        public static bool isPlayerLogger = false;
        public static bool isInfinityTyping = false;
        public static bool isBooping = false;
        public static bool isHideName = false;
        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Command List:\n'1' - PlayerLogger\n'2' - InfinityTyping\n'3' - Booping\n'4' - HideName");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("'0' - Disable ALL");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Debug();
            Console.ResetColor();
        }
        
        public static void Debug()
        {
            Console.WriteLine("\nDebug Panel\n----------");
            CheckDebugParameter("PlayerLogger", isPlayerLogger);
            CheckDebugParameter("InfinityTyping", isInfinityTyping);
            CheckDebugParameter("Booping", isBooping);
            CheckDebugParameter("HideName", isHideName);
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
                    Console.Write($"{paramName}: ");
                    Console.ForegroundColor = paramValue ? ConsoleColor.Green : ConsoleColor.DarkRed;
                    Console.WriteLine($"{status}");
                    break;
            }
            Console.ResetColor();
        }
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random random = new Random();

            ConsoleColor[] colors = Enum.GetValues(typeof(ConsoleColor))
                                      .Cast<ConsoleColor>()
                                      .Where(c => c != ConsoleColor.Black && c != ConsoleColor.White)
                                      .ToArray();

            ConsoleColor randomColor = colors[random.Next(colors.Length)];
            Console.ForegroundColor = randomColor;
            string Creator = "{ Made by Disconnect3301 with Love <3. }";
            int consoleWidth = Console.WindowWidth;
            int consolePadding = (consoleWidth - Creator.Length) / 2;
            Console.WriteLine(Creator.PadLeft(consolePadding + Creator.Length));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Command List:\n'1' - PlayerLogger\n'2' - InfinityTyping\n'3' - Booping\n'4' - HideName");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("'0' - Disable ALL");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n'9' - View Command List");
            Console.ResetColor();

            while (true)
            {
                var userInput = Console.ReadKey().Key;
                Console.WriteLine();
                if (userInput == ConsoleKey.D1 || userInput == ConsoleKey.NumPad1)
                {
                    if (!isPlayerLogger && !isBooping && !isHideName)
                    {
                        isPlayerLogger = true;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        var playerLoggerThread = new Thread(() =>
                        {
                            PlayerLogger.Start();
                        });
                        playerLoggerThread.IsBackground = true;
                        playerLoggerThread.Start();
                    }
                    else if (!isBooping && !isHideName)
                    {
                        isPlayerLogger = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Cannot be enabled while [HideName] or [Booping] is running!\n");
                        Console.ResetColor();
                    }
                }
                else if (userInput == ConsoleKey.D2 || userInput == ConsoleKey.NumPad2)
                {
                    if (!isInfinityTyping && !isBooping && !isHideName)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isInfinityTyping = true;
                        var infinityTypingThread = new Thread(() =>
                        {
                            InfinityTyping.Start();
                        });
                        infinityTypingThread.IsBackground = true;
                        infinityTypingThread.Start();
                    }
                    else if (!isBooping && !isHideName)
                    {
                        isInfinityTyping = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("STOPPED!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Cannot be enabled while [HideName] or [Booping] is running!\n");
                        Console.ResetColor();
                    }
                }
                else if (userInput == ConsoleKey.D3 || userInput == ConsoleKey.NumPad3)
                {
                    if (!isBooping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isBooping = true;
                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        isHideName = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("WARNING: PlayerLogger, InfinityTyping, HideName - Disabled!");
                        Console.ResetColor();
                        var boopThread = new Thread(() =>
                        {
                            Boop.Start();
                        });
                        boopThread.IsBackground = true;
                        boopThread.Start();
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
                else if (userInput == ConsoleKey.D4 || userInput == ConsoleKey.NumPad4)
                {
                    if (!isHideName)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isHideName = true;
                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        isBooping = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("WARNING: PlayerLogger, InfinityTyping, Booping - Disabled!");
                        Console.ResetColor();
                        var hideNameThread = new Thread(() =>
                        {
                            HideName.Start();
                        });
                        hideNameThread.IsBackground = true;
                        hideNameThread.Start();
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
                else if (userInput == ConsoleKey.D0 || userInput == ConsoleKey.NumPad0)
                {
                    isPlayerLogger = false;
                    isInfinityTyping = false;
                    isBooping = false;
                    isHideName = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("All Functions Disabled!");
                    Console.ResetColor();
                }
                else if (userInput == ConsoleKey.D9 || userInput == ConsoleKey.NumPad9)
                {
                    Help();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Incorrect Input, Try '9' To See Command List!");
                    Console.ResetColor();
                }
            }
        }
    }
}
/*
Полезные Команды:
dotnet add package VRCOscLib --version 1.4.3 - добавление библиотеки VRChat в проект.
dotnet publish -c Release -p:DebugType=none -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true - Компилирование EXE файла.

git status - показывает состояние проекта.
git add . - добавляет изменения в проект.
git commit -m "message" - коммит.
*/