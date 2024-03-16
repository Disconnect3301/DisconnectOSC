using BuildSoft.VRChat.Osc.Chatbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOSC
{
    class Boop
    {
        public static void Start()
        {
            while (DisconnectOSC.isBooping)
            {
                OscChatbox.SendMessage("*boop*", direct: true, complete: true);
                Thread.Sleep(100);
                OscChatbox.SendMessage("", direct: true, complete: false);
                Thread.Sleep(1500);
            }
        }
    }
}
