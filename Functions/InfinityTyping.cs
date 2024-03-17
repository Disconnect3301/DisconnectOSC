using BuildSoft.VRChat.Osc.Chatbox;

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
