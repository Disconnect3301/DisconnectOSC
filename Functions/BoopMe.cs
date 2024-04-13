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
            var config = OscAvatarConfig.Create("avtr_97c252b5-48f7-4435-a16f-da1448711953")!;

            while (true)
            {
                if (config.Parameters["Boop"] is bool && (bool)config.Parameters["Boop"])
                {
                    OscChatbox.SendMessage("Boop! >.<", direct: true, complete: true);
                    await Task.Delay(1000);
                    OscChatbox.SendMessage("", direct: true, complete: false);
                }
                await Task.Delay(10);
            }

        }
    }
}
