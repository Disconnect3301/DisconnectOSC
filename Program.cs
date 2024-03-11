using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;
using System.Threading.Tasks;
using System.Globalization;

class OnPlayerLogger
{c

    static Dictionary<string, string> replacementMap = new Dictionary<string, string>
    {
        { " OnPlayerJoinComplete ", "\n[OnPlayerJoinComplete] \n✔ - " },
        { " OnPlayerJoined ", "[OnPlayerJoined] \n✔ - " },
        { " Log - [Behaviour]", "" },
        { " OnPlayerLeft ", "[OnPlayerLeft] \n❌ - " },
    };
    static bool isEmptyMessageSent = false;
    static void Main()
    {

        Console.WriteLine("Press 'Ctrl + C' to exit the application.");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if ((key.Modifiers & ConsoleModifiers.Control) != 0 && key.Key == ConsoleKey.C)
                {
                    Console.WriteLine("Exiting the application...");
                    Environment.Exit(0);
                }
            }
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

                while (true)
                {
                    FileInfo fileInfo = new FileInfo(lastFile);
                    if (fileInfo.LastWriteTime > lastReadTime)
                    {
                        string triggerLine = "";

                        using (FileStream fs = new FileStream(lastFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                string line;
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
                            triggerLine = Regex.Replace(triggerLine, @"(\d{4}\.\d{2}\.\d{2})\s(\d{2}:\d{2}:\d{2})", "[$2]");

                            if (triggerTime != lastTriggerTime)
                            {
                                Console.WriteLine("\nNew entry from 'OnPlayer':");
                                Console.WriteLine(triggerLine);
                                OscChatbox.SendMessage(triggerLine, direct: true);
                                isEmptyMessageSent = false;
                                lastTriggerLine = triggerLine;
                                lastTriggerTime = triggerTime;
                                lastTriggerUpdateTime = DateTime.Now;
                            }
                            isNewTrigger = false;
                        }

                        if ((DateTime.Now - lastTriggerUpdateTime).TotalSeconds > 5 && !isEmptyMessageSent)
                        {
                            Console.WriteLine("No updates for 5 seconds. Sending empty message.");
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
                Console.WriteLine("There are no txt files in the specified folder.");
                OscChatbox.SendMessage("There are no txt files in the specified folder.", direct: true);
            }
        }
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