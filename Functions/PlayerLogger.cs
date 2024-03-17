using System.Text.RegularExpressions;
using BuildSoft.VRChat.Osc.Chatbox;
using System.Globalization;

namespace MainOSC
{
    class PlayerLogger
    {
        public static void Start()
        {

            Dictionary<string, string> replacementMap = new Dictionary<string, string>
            {
                { " OnPlayerJoinComplete ", "\n<G>[OnPlayerJoinComplete]</G>\n✔️ <B>-</B> <P>" },
                { " OnPlayerJoined ", "<G>[OnPlayerJoined]</G>\n✔️ <B>-</B> <P>" },
                { " Log - [Behaviour]", "" },
                { " OnPlayerLeft ", "<R>[OnPlayerLeft]</R>\n❌ <B>-</B> <P>" },
                { " OnPlayerEnteredRoom", "\n<P>[OnPlayerEnteredRoom]" },
            };
            bool isEmptyMessageSent = false;

            while (DisconnectOSC.isPlayerLogger)
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

                    while (DisconnectOSC.isPlayerLogger)
                    {
                        FileInfo fileInfo = new FileInfo(lastFile);

                        if (fileInfo.LastWriteTime > lastReadTime)
                        {
                            string triggerLine = "";

                            using (FileStream fileSearch = new FileStream(lastFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                using (StreamReader reader = new StreamReader(fileSearch))
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
            static string RemoveColorTags(string remover)
            {
                return Regex.Replace(remover, @"<.*?>", string.Empty);
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
                            Console.ForegroundColor = ConsoleColor.DarkRed;
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
    }
}