using BuildSoft.VRChat.Osc.Chatbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOSC
{
    class InfinityTyping
    {
        public static void Start()
        {
            while (DisconnectOSC.isInfinityTyping)
            {
                OscChatbox.SetIsTyping(true);
                Thread.Sleep(2000);
            }
        }
    }
}
