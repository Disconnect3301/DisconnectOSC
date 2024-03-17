using System.Runtime.InteropServices;

namespace MainOSC
{
    public class DisconnectOSC
    {
        public static bool isPlayerLogger = false;
        public static bool isInfinityTyping = false;
        public static bool isBooping = false;
        public static bool isHideName = false;
        public static bool isSpinBot = false;
        public static bool isHelp = false;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder text, int count);

        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Command List:\n'1' - PlayerLogger\n'2' - InfinityTyping\n'3' - Booping\n'4' - HideName\n'5' - SpinBot");
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-------------------");
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
            Console.WriteLine("Command List:\n'1' - PlayerLogger\n'2' - InfinityTyping\n'3' - Booping\n'4' - HideName\n'5' - SpinBot");
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
                        })
                        {
                            IsBackground = true
                        };
                        playerLoggerThread.Start();
                    }
                    else if (isBooping)
                    {
                        isPlayerLogger = true;
                        isBooping = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        var playerLoggerThread = new Thread(() =>
                        {
                            PlayerLogger.Start();
                        })
                        {
                            IsBackground = true
                        };
                        playerLoggerThread.Start();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Disabled!");
                        Console.ResetColor();
                    }
                    else if (isHideName)
                    {
                        isPlayerLogger = true;
                        isHideName = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        var playerLoggerThread = new Thread(() =>
                        {
                            PlayerLogger.Start();
                        })
                        {
                            IsBackground = true
                        };
                        playerLoggerThread.Start();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Disabled!");
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
                        })
                        {
                            IsBackground = true
                        };
                        infinityTypingThread.Start();
                    }
                    else if (isBooping)
                    {
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isInfinityTyping = true;
                        isBooping = false;
                        var infinityTypingThread = new Thread(() =>
                        {
                            InfinityTyping.Start();
                        })
                        {
                            IsBackground = true
                        };
                        infinityTypingThread.Start();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isHideName)
                    {
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isInfinityTyping = true;
                        isHideName = false;
                        var infinityTypingThread = new Thread(() =>
                        {
                            InfinityTyping.Start();
                        })
                        {
                            IsBackground = true
                        };
                        infinityTypingThread.Start();

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
                else if (userInput == ConsoleKey.D3 || userInput == ConsoleKey.NumPad3)
                {
                    if (!isBooping && !isPlayerLogger && !isInfinityTyping && !isHideName)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isBooping = true;
                        var boopThread = new Thread(() =>
                        {
                            Boop.Start();
                        })
                        {
                            IsBackground = true
                        };
                        boopThread.Start();
                    }
                    else if (isPlayerLogger && isInfinityTyping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isBooping = true;
                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        var boopThread = new Thread(() =>
                        {
                            Boop.Start();
                        })
                        {
                            IsBackground = true
                        };
                        boopThread.Start();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger | InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isPlayerLogger)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isBooping = true;
                        isPlayerLogger = false;
                        var boopThread = new Thread(() =>
                        {
                            Boop.Start();
                        })
                        {
                            IsBackground = true
                        };
                        boopThread.Start();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isInfinityTyping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isBooping = true;
                        isInfinityTyping = false;
                        var boopThread = new Thread(() =>
                        {
                            Boop.Start();
                        })
                        {
                            IsBackground = true
                        };
                        boopThread.Start();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isHideName)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Booping - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isBooping = true;
                        isHideName = false;
                        var boopThread = new Thread(() =>
                        {
                            Boop.Start();
                        })
                        {
                            IsBackground = true
                        };
                        boopThread.Start();
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
                else if (userInput == ConsoleKey.D4 || userInput == ConsoleKey.NumPad4)
                {
                    if (!isHideName && !isPlayerLogger && !isInfinityTyping && !isBooping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isHideName = true;
                        var hideNameThread = new Thread(() =>
                        {
                            HideName.Start();
                        })
                        {
                            IsBackground = true
                        };
                        hideNameThread.Start();
                    }
                    else if (isPlayerLogger && isInfinityTyping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isHideName = true;
                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        var hideNameThread = new Thread(() =>
                        {
                            HideName.Start();
                        })
                        {
                            IsBackground = true
                        };
                        hideNameThread.Start();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger | InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isPlayerLogger)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isHideName = true;
                        isPlayerLogger = false;
                        var hideNameThread = new Thread(() =>
                        {
                            HideName.Start();
                        })
                        {
                            IsBackground = true
                        };
                        hideNameThread.Start();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PlayerLogger - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isInfinityTyping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isHideName = true;
                        isInfinityTyping = false;
                        var hideNameThread = new Thread(() =>
                        {
                            HideName.Start();
                        })
                        {
                            IsBackground = true
                        };
                        hideNameThread.Start();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("InfinityTyping - ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Automatically Disabled!");
                        Console.ResetColor();
                    }
                    else if (isBooping)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("HideName - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isHideName = true;
                        isBooping = false;
                        var hideNameThread = new Thread(() =>
                        {
                            HideName.Start();
                        })
                        {
                            IsBackground = true
                        };
                        hideNameThread.Start();
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
                else if (userInput == ConsoleKey.D5 || userInput == ConsoleKey.NumPad5)
                {
                    if (!isSpinBot)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("SpinBot - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("STARTED!");
                        Console.ResetColor();
                        isSpinBot = true;

                        var spinBotThread = new Thread(() =>
                        {
                            SpinBot.Start();
                        })
                        {
                            IsBackground = true
                        };
                        spinBotThread.Start();
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
                else if (userInput == ConsoleKey.D0 || userInput == ConsoleKey.NumPad0)
                {
                    if (!isHelp)
                    {
                        isHelp = true;
                        isPlayerLogger = false;
                        isInfinityTyping = false;
                        isBooping = false;
                        isHideName = false;
                        isSpinBot = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("All Functions Disabled!");
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
dotnet publish -c Release -p:DebugType=none -r win-x64 --self-contained false -p:PublishSingleFile=true - FIX Компилирование EXE файла.

git status - показывает состояние проекта.
git add . - добавляет изменения в проект.
git commit -m "message" - коммит.
git push origin master - отправка изменений на GitHub.
*/