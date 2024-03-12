using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;
using System.Threading.Tasks;
using System.Globalization;

class DisconnectOSC
{
    static bool firstFunctionRunning = false;
    static bool secondFunctionRunning = false;
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Blue;
        string Creator = "{ Made by Disconnect3301 with Love <3. }";
        int width = Console.WindowWidth;
        int padding = (width - Creator.Length) / 2;
        Console.WriteLine(Creator.PadLeft(padding + Creator.Length));
        Console.WriteLine("Command List:\n1 - Start PlayerLogger     |   2 - Stop PlayerLogger\n3 - Start InfinityTyping   |   4 - Stop InfinityTyping\n0 - STOP PlayerLogger AND InfinityTyping\n\nhelp - View Command List");
        Console.ResetColor();

        Thread? firstThread = null;
        Thread? secondThread = null;

        while (true)
        {
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    if (firstThread == null || !firstThread.IsAlive)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Player Logger - STARTED!");
                        Console.ResetColor();
                        firstFunctionRunning = true;
                        firstThread = new Thread(FirstFunction);
                        firstThread.Start();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("PlayerLogger - Is Already Running!");
                        Console.ResetColor();
                    }
                    break;
                case "2":
                    firstFunctionRunning = false;
                    firstThread?.Join();
                    firstThread = null;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("PlayerLogger - STOPPED!");
                    Console.ResetColor();
                    break;
                case "3":
                    if (secondThread == null || !secondThread.IsAlive)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("InfinityTyping - STARTED!");
                        Console.ResetColor();
                        secondFunctionRunning = true;
                        secondThread = new Thread(SecondFunction);
                        secondThread.Start();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("InfinityTyping - Is Already Running!");
                        Console.ResetColor();
                    }
                    break;
                case "4":
                    secondFunctionRunning = false;
                    secondThread?.Join();
                    secondThread = null;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("InfinityTyping - STOPPED!");
                    Console.ResetColor();
                    break;
                case "0":
                    firstFunctionRunning = false;
                    secondFunctionRunning = false;
                    firstThread?.Join();
                    secondThread?.Join();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("PlayerLogger AND InfinityTyping - STOPPED!");
                    Console.ResetColor();
                    break;
                case "help":
                    PrintHelp();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Incorrect Input, Try 'help' Command!");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void PrintHelp()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Command List:\n1 - Start PlayerLogger     |   2 - Stop PlayerLogger\n3 - Start InfinityTyping   |   4 - Stop InfinityTyping\n0 - STOP PlayerLogger AND InfinityTyping");
        Console.ResetColor();
    }

    static void FirstFunction()
    {
        Dictionary<string, string> replacementMap = new Dictionary<string, string>
        {
            { " OnPlayerJoinComplete ", "\n<G>[OnPlayerJoinComplete]</G>\n✔️ <B>-</B> <P>" },
            { " OnPlayerJoined ", "<G>[OnPlayerJoined]</G>\n✔️ <B>-</B> <P>" },
            { " Log - [Behaviour]", "" },
            { " OnPlayerLeft ", "<R>[OnPlayerLeft]</R>\n❌ <B>-</B> <P>" },
        };
        bool isEmptyMessageSent = false;

        while (firstFunctionRunning)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\LocalLow\VRChat\VRChat");
            string[] files = Directory.GetFiles(directoryPath, "*.txt");

            if (files.Length > 0)
            {
                string lastFile = files.OrderByDescending(f => new FileInfo(f).CreationTime).First();
                DateTime lastReadTime = DateTime.Now;
                string lastTriggerLine = "";
                bool isNewTrigger = false;
                string lastTriggerTime = "";
                DateTime lastTriggerUpdateTime = DateTime.Now;

                while (firstFunctionRunning)
                {
                    FileInfo fileInfo = new FileInfo(lastFile);

                    if (fileInfo.LastWriteTime > lastReadTime)
                    {
                        string triggerLine = "";

                        using (FileStream fs = new FileStream(lastFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                string? line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line.Contains("OnPlayer"))
                                    {
                                        triggerLine = line;
                                        isNewTrigger = true;
                                    }
                                }
                            }
                        }

                        if (isNewTrigger && !string.IsNullOrEmpty(triggerLine) && triggerLine != lastTriggerLine)
                        {
                            triggerLine = Regex.Replace(triggerLine, @"\s+", " ");

                            foreach (var replacement in replacementMap)
                            {
                                triggerLine = triggerLine.Replace(replacement.Key, replacement.Value);
                            }

                            string triggerTime = ExtractTimeFromLine(triggerLine);
                            triggerLine = Regex.Replace(triggerLine, @"(\d{4}\.\d{2}\.\d{2})\s(\d{2}:\d{2}:\d{2})", "\n<B>[$2]</B>");

                            if (triggerTime != lastTriggerTime)
                            {
                                string messageToSend = RemoveColorTags(triggerLine);
                                PrintColoredText(triggerLine + "</P>\n");
                                OscChatbox.SendMessage(messageToSend, direct: true);

                                isEmptyMessageSent = false;
                                lastTriggerLine = triggerLine;
                                lastTriggerTime = triggerTime;
                                lastTriggerUpdateTime = DateTime.Now;
                            }
                            isNewTrigger = false;
                        }

                        if ((DateTime.Now - lastTriggerUpdateTime).TotalSeconds > 5 && !isEmptyMessageSent)
                        {
                            OscChatbox.SendMessage("", direct: true);
                            lastTriggerUpdateTime = DateTime.Now;
                            isEmptyMessageSent = true;
                        }
                        lastReadTime = fileInfo.LastWriteTime;
                    }
                    Thread.Sleep(200);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("There are no txt files in the specified folder.");
                Console.ResetColor();
                OscChatbox.SendMessage("There are no txt files in the specified folder.", direct: true);
            }
        }

        static string RemoveColorTags(string text)
        {
            return Regex.Replace(text, @"<.*?>", string.Empty);
        }

        static void PrintColoredText(string triggerLine)
        {
            var regex = new Regex(@"<([BGRP])>(.*?)</\1>");
            int lastIndex = 0;

            foreach (Match match in regex.Matches(triggerLine))
            {
                Console.Write(triggerLine.Substring(lastIndex, match.Index - lastIndex));

                switch (match.Groups[1].Value)
                {
                    case "B":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "R":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "G":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "P":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }
                Console.Write(match.Groups[2].Value);
                Console.ResetColor();
                lastIndex = match.Index + match.Length;
            }
            Console.Write(triggerLine.Substring(lastIndex));
        }

        static string ExtractTimeFromLine(string line)
        {
            Match match = Regex.Match(line, @"\d{2}:\d{2}:\d{2}");
            if (match.Success)
            {
                DateTime time = DateTime.ParseExact(match.Value, "HH:mm:ss", CultureInfo.InvariantCulture);
                return time.ToString("HH:mm:ss");
            }
            return "";
        }
    }

    static void SecondFunction()
    {
        while (secondFunctionRunning)
        {
            OscChatbox.SetIsTyping(true);
            Thread.Sleep(2000);
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