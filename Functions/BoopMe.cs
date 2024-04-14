using System;
using System.Collections.Concurrent;
using System.Threading;
using BuildSoft.VRChat.Osc.Avatar;
using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class BoopMe
    {
        public static void Logic()
        {
            var boopMeThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            boopMeThread.Start();
        }
        public static async void Start()
        {
            try
            {
                var config = OscAvatarConfig.Create("avtr_97c252b5-48f7-4435-a16f-da1448711953")!;

                while (true)
                {
#pragma warning disable CS8605
                    if (config.Parameters["Boop"] is bool && (bool)config.Parameters["Boop"])
                    {
                        OscChatbox.SendMessage("Boop! >.<", direct: true, complete: true);
                        await Task.Delay(1000);
                        OscChatbox.SendMessage("", direct: true, complete: false);
                    }
#pragma warning restore CS8605
                    await Task.Delay(10);
                }
            }
            catch (ArgumentNullException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] In BoopMe.Start: " + e.Message);
                Console.ResetColor();
            }
            catch (InvalidOperationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] In BoopMe.Start: " + e.Message);
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] In BoopMe.Start: " + e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ResetColor();
            }

        }

    }
}