using BuildSoft.VRChat.Osc.Chatbox;

namespace MainOSC
{
    class Boop
    {
        public static void Start()
        {
            while (DisconnectOSC.isBooping)
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 3);

                string wowBoop = "";
                switch (randomNumber)
                {
                    case 1:
                        wowBoop = "*boop*";
                        break;
                    case 2:
                        wowBoop = "*\nb\no\no\np\n*";
                        break;
                }
                OscChatbox.SendMessage(wowBoop, direct: true, complete: true);
                Thread.Sleep(100);
                OscChatbox.SendMessage("", direct: true, complete: false);
                Thread.Sleep(1500);
            }
        }
    }
}
