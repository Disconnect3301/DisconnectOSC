using System;
using System.Threading;
using BuildSoft.VRChat.Osc.Chatbox;
using BuildSoft.VRChat.Osc.Avatar;
using BuildSoft.VRChat.Osc.Input;

namespace MainOSC
{
    class SelfBot
    {
        private static readonly Random random = new Random();
        public static void Logic()
        {
            DisconnectOSC.isSelfBot = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("SelfBot - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STARTED!");
            Console.ResetColor();
            var selfBotThread = new Thread(() =>
            {
                Start();
            })
            {
                IsBackground = true
            };
            selfBotThread.Start();
        }
        public static async void Start()
        {
            var config2 = OscAvatarConfig.Create("avtr_2754794c-d551-4882-a82d-0ae7f43a21cb")!;
            await Task.Delay(100);

            var trackingThread = new Thread(() =>
            {
                DateTime lastCheck = DateTime.Now;
                bool lastState = false;
                bool lastHeadCenter = false;

                while (DisconnectOSC.isSelfBot)
                {
                    bool currentState =
                        (config2.Parameters["CheckLeft"] is bool && (bool)config2.Parameters["CheckLeft"]) ||
                        (config2.Parameters["CheckRight"] is bool && (bool)config2.Parameters["CheckRight"]) ||
                        (config2.Parameters["CheckBack"] is bool && (bool)config2.Parameters["CheckBack"]) ||
                        (config2.Parameters["HeadDown"] is bool && (bool)config2.Parameters["HeadDown"]) ||
                        (config2.Parameters["HeadLeft"] is bool && (bool)config2.Parameters["HeadLeft"]) ||
                        (config2.Parameters["HeadLeft2"] is bool && (bool)config2.Parameters["HeadLeft2"]) ||
                        (config2.Parameters["HeadRight"] is bool && (bool)config2.Parameters["HeadRight"]) ||
                        (config2.Parameters["HeadRight2"] is bool && (bool)config2.Parameters["HeadRight2"]) ||
                        (config2.Parameters["HeadUp"] is bool && (bool)config2.Parameters["HeadUp"]) ||
                        (config2.Parameters["FixRLeft"] is bool && (bool)config2.Parameters["FixRLeft"]) ||
                        (config2.Parameters["FixRRight"] is bool && (bool)config2.Parameters["FixRRight"]) ||
                        (config2.Parameters["HeadUpLeft"] is bool && (bool)config2.Parameters["HeadUpLeft"]) ||
                        (config2.Parameters["HeadUpRight"] is bool && (bool)config2.Parameters["HeadUpRight"]) ||
                        (config2.Parameters["HeadDLeft"] is bool && (bool)config2.Parameters["HeadDLeft"]) ||
                        (config2.Parameters["HeadDRight"] is bool && (bool)config2.Parameters["HeadDRight"]) ||
                        (config2.Parameters["TargetPlayer"] is bool && (bool)config2.Parameters["TargetPlayer"]) ||
                        (config2.Parameters["MoveToPlayer"] is bool && (bool)config2.Parameters["MoveToPlayer"]) ||
                        (config2.Parameters["RunToPlayer"] is bool && (bool)config2.Parameters["RunToPlayer"]) ||
                        (config2.Parameters["MoveFromMe"] is bool && (bool)config2.Parameters["MoveFromMe"]) ||
                        (config2.Parameters["DownMoving"] is bool && (bool)config2.Parameters["DownMoving"]);

                    bool currentHeadCenter =
                    config2.Parameters["HeadCenter"] is bool && (bool)config2.Parameters["HeadCenter"];

                    if (currentState != lastState)
                    {
                        if (currentState)
                        {
                            string[] messages = new string[] { "Tracking Found!", "Found You!", "I see you!", "Player Found!", "I can see you!", "Tracking is Good!", "You are being tracked!", "You are the target!", "Фембой Найден!", "Нашел!", "Ты Избранный!", "Ты Особенный!", "То Самое Уже Бежит К Тебе!", "Я хочу пельмешек!", "Tracking FOUND!", "Появился!", "Бегу Бегу!", "Загрызу!", "Вжжжух!", "!" };
                            Random random = new Random(DateTime.Now.Millisecond);
                            int randomNumber = random.Next(0, messages.Length);
                            string randomMessage = messages[randomNumber];
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(randomMessage);
                            OscChatbox.SendMessage(randomMessage + DisconnectOSC.BlankEgg, direct: true, complete: false);
                        }
                        else
                        {
                            string[] messages = new string[] { "ERROR: Tracking Lost!", "ERROR: Cant find Player", "ERROR: Trying to find another player!", "ERROR: Tracking is hard!", "ERROR: Im not good at this!", "ERROR: Please help!"/*, "Куда Блять Вернись!", "Куда нахуй?!", "Нихуя Не Соображаю...", "Убежал..", "Вернись!", "Стой!", "Опять Потерялся..", "Где Я Нахуй?????", "Ошибка: природы", "Tracking LOST!", "Устал бегать и вертеться..", "Ну и куда?", "Класс. И что мне теперь делать?", "?????" */};
                            Random random = new Random(DateTime.Now.Millisecond);
                            int randomNumber = random.Next(0, messages.Length);
                            string randomMessage = messages[randomNumber];
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(randomMessage);
                            OscChatbox.SendMessage(randomMessage + DisconnectOSC.BlankEgg, direct: true, complete: false);
                        }
                        Console.ResetColor();
                    }

                    if (currentHeadCenter != lastHeadCenter && DateTime.Now - lastCheck > TimeSpan.FromMilliseconds(5000))
                    {
                        lastCheck = DateTime.Now;
                        if (currentHeadCenter)
                        {
                            string[] messages = new string[] { "Too many players...", "Slow down movement..", "Head tracking issue...", "Many players ahead..", "Can't keep up speed..", "Slow down, too many players...", "Reduce movement speed.."/*, "Стой Блять Не Двигайся!", "Причина тряски?", "Землетрясение 9 Баллов?!", "Слишком Частое Переключение Игроков..", "Много Игроков Рядом!", "Частый Скип..", "Трясусь!", "Помедленнее!", "Землетрясение!", "Ответ: Тряска - Причина!", "Не Двигайся!" */};
                            Random random = new Random(DateTime.Now.Millisecond);
                            int randomNumber = random.Next(0, messages.Length);
                            string randomMessage = messages[randomNumber];
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(randomMessage);
                            OscChatbox.SendMessage(randomMessage + DisconnectOSC.BlankEgg, direct: true, complete: false);
                        }
                        else
                        {
                            string[] messages = new string[] { "Tracking resumed!", "Player movement detected again!", "Tracking is back on!", "Player tracking has returned!", "Movement detected, tracking restored!", "Resuming player tracking!", "Player movement detected, tracking has been restored"/*, "Пельмешка Вернулась!", "Опять на тебя смотрю!", "Зырьк!", "Ресет Трекинга", "Не Могу Определиться..", "Много....", "Brrrrrr", "Ошибка: Тряска Многих Игроков", "Устал вибрировать..", "Вибрирую!", "Детект Вибрации!!", "Автор Не Предоставил Сообщений" */};
                            Random random = new Random(DateTime.Now.Millisecond);
                            int randomNumber = random.Next(0, messages.Length);
                            string randomMessage = messages[randomNumber];
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(randomMessage);
                            OscChatbox.SendMessage(randomMessage + DisconnectOSC.BlankEgg, direct: true, complete: false);
                        }
                        Console.ResetColor();
                    }
                    lastState = currentState;
                    lastHeadCenter = currentHeadCenter;
                    Thread.Sleep(1500);
                }
            })
            {
                IsBackground = true
            };
            trackingThread.Start();

            var randomMessageThread = new Thread(() =>
            {
                string[] messages = new string[] { /*"Как дела?", "Как твои дела?", "Что нового?", "Что нового в твоей жизни?", "Как погода?", "Как прошел твой день?", "Что делаешь?", "Что ты сегодня делал?", "Как зовут?", "Как проводишь свободное время?", "Где живешь?", "Смотришь какие-нибудь интересные фильмы?", "Что ешь?", "Что тебя вдохновляет?", "Чем занимаешься?", "Какие у тебя хобби?", "Какой фильм?", "Какие места ты любишь посещать?", "Что читаешь?", "Какие книги тебе нравятся читать?", "Какой день?", "Как ты отдыхаешь после работы?", "Что слушаешь?", "Что тебе нравится готовить?", "Какие планы?", "Какие музыкальные жанры тебе нравятся?", "Что нравится?", "Какие страны ты мечтаешь посетить?", "Как настроение?", "Какие спортивные мероприятия тебе интересны?", "Что смотришь?", "Какие новости тебя заинтересовали недавно?", "Какой город?", "Какие проекты ты ведешь в данный момент?", "Что любишь?", "Какие цели ты ставишь перед собой?", "Какой отдых?", "Какие достижения ты гордишься?", "Что планируешь?", "Какие люди вдохновляют тебя?", "Хорошо, спасибо!", "Ничего особенного.", "Дождливо, но тепло.", "Работаю над проектом.", "Весело и радостно.", "Учусь новому навыку.", "Пицца, как всегда.", "Смотрю комедию.", "Читаю фантастику.", "Обычный будний день.", "Слушаю поп-музыку.", "Планирую поездку.", "Люблю смешные мемы.", "Смотрю сериалы.", "Мой любимый город.", "Люблю гулять.", "Отдыхаю на природе.", "Планирую встречу друзей.", "Все хорошо, спасибо!", "Интересно, что дальше.", "Прекрасно, спасибо!", "Ничего особенного.", "Солнечно и тепло.", "Занят работой.", "Отличное настроение!", "Учусь новому навыку.", "Любимый обед сегодня.", "Смешной фильм смотрю.", "Книгу почитываю.", "Обычный день.", "Любимая музыка играет.", "Планирую путешествие.", "Хобби - смешные видео.", "Слежу за сериалом.", "Любимый город отдыха.", "Прогулки на свежем воздухе.", "Отдых на природе.", "Встреча с друзьями.", "Все хорошо, спасибо!", "Интересно, что дальше.", "Отлично!", "Ничего нового.", "Солнечно.", "Работаю.", "Супер!", "Учусь.", "Обычный.", "Поп-музыка.", "Планы есть.", "Шутки люблю.", "Сериалы.", "Мой город.", "Гуляю.", "На природе.", "С друзьями.", "Хорошо.", "Интересно.", "На самом деле я АФК а это рандомные сообщения", "Это просто текст.", "Это бот, который пишет рандомные сообщения", "Устал", "Рил", "Зачем?", "Мех..", */"How are you?", "How are you?", "What's new?", "What's new in your life?", "How's the weather?", "How was your day?", "What are you doing?", "What did you do today?", "What's your name?", "How do you spend your free time?", "Where do you live?", "Do you watch any interesting movies?", "What do you eat?", "What inspires you?", "What do you do?", "What hobbies do you have?", "What the movie?", "What places do you like to visit?", "What do you read?", "What books do you like to read?", "What day is it?", "How do you relax after work?", "What do you listen to?", "What do you like to cook?", "What are the plans?", "What musical genres do you like?", "What do you like?", "Which countries do you dream of visiting?", "How are you feeling?", "What sports events are you interested in?", "What are you watching?", "What news have you been interested in recently?", "Which city?", "What projects are you doing at the moment?", "What do you love?", "What goals do you set for yourself?", "What kind of vacation?", "What achievements are you proud of?", "What are you planning?", "What people inspire you?", "Okay, thank you!", "Nothing special.", "Rainy, but warm.", "Working on a project.", "Fun and joyfully.", "Learning a new skill.", "Pizza, as always.", "I watch comedy.", "I read fiction.", "An ordinary weekday.", "Listening to pop music.", "Planning a trip.", "I love funny memes.", "I watch TV series.", "My favorite city.", "I like to walk.", "I relax in nature.", "I plan to meet friends.", "Everything is fine, thank you!", "I wonder what's next.", "Fine, thank you!", "Nothing special.", "Sunny and warm.", "Busy with work.", "Great mood!", "Learning a new skill.", "Favorite lunch Today.", "I'm watching a funny movie.", "I'm reading a book.", "An ordinary day.", "Favorite music is playing.", "Planning a trip.", "Hobbies - funny videos.", "I'm following the series.", "Favorite vacation city.", "Walking in the fresh air.", "Outdoor recreation.", "Meeting with friends.", "Everything is fine, thank you!", "I wonder what's next.", "Great!", "Nothing new.", "Sunny.", "Working.", "Super!", "Studying.", "Ordinary.", "Pop music.", "There are plans.", "I love jokes.", "TV series.", "My city.", "Walking.", "In nature.", "With friends.", "OK.", "Interesting.", "Actually I'm AFK and these are random messages", "It's just text.", "This is a bot that writes random messages", "Tired", "Real", "Why?", "Nah..", "????????", "!!!" };
                while (DisconnectOSC.isSelfBot)
                {
                    for (int i = messages.Length - 1; i > 0; i--)
                    {
                        int swapIndex = random.Next(i + 1);
                        string temp = messages[i];
                        messages[i] = messages[swapIndex];
                        messages[swapIndex] = temp;
                    }

                    // Вывод отсортированного массива
                    foreach (string message in messages)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(message);
                        OscChatbox.SetIsTyping(true);
                        Thread.Sleep(3000);
                        OscChatbox.SendMessage(message, direct: true, complete: false);
                        Console.ResetColor();
                        Thread.Sleep(78000);
                    }
                }
            })
            {
                IsBackground = true
            };
            randomMessageThread.Start();


            bool switchMovement = false;
            int switchCount = 0;

            while (DisconnectOSC.isSelfBot)
            {
                // Body
                if (config2.Parameters["CheckLeft"] is bool && (bool)config2.Parameters["CheckLeft"])
                {
                    OscAxisInput.LookHorizontal.Send(-2.0f);
                }
                else if (config2.Parameters["CheckRight"] is bool && (bool)config2.Parameters["CheckRight"])
                {
                    OscAxisInput.LookHorizontal.Send(2.0f);
                }
                else if (config2.Parameters["CheckBack"] is bool && (bool)config2.Parameters["CheckBack"])
                {
                    OscAxisInput.LookHorizontal.Send(-2.0f);
                }
                else
                {
                    OscAxisInput.LookHorizontal.Send(0.0f);
                }

                // Head
                if (config2.Parameters["HeadCenter"] is bool && (bool)config2.Parameters["HeadCenter"])
                {
                    OscAxisInput.LookVertical.Send(0.0f);
                    OscAxisInput.LookHorizontal.Send(0.0f);
                }
                else if (config2.Parameters["HeadDown"] is bool && (bool)config2.Parameters["HeadDown"])
                {
                    OscAxisInput.LookVertical.Send(-0.6f);
                }
                else if (config2.Parameters["HeadLeft"] is bool && (bool)config2.Parameters["HeadLeft"])
                {
                    OscAxisInput.LookHorizontal.Send(-0.6f);
                }
                else if (config2.Parameters["HeadLeft2"] is bool && (bool)config2.Parameters["HeadLeft2"])
                {
                    OscAxisInput.LookHorizontal.Send(-1.0f);
                }
                else if (config2.Parameters["HeadRight"] is bool && (bool)config2.Parameters["HeadRight"])
                {
                    OscAxisInput.LookHorizontal.Send(0.6f);
                }
                else if (config2.Parameters["HeadRight2"] is bool && (bool)config2.Parameters["HeadRight2"])
                {
                    OscAxisInput.LookHorizontal.Send(1.0f);
                }
                else if (config2.Parameters["HeadUp"] is bool && (bool)config2.Parameters["HeadUp"])
                {
                    OscAxisInput.LookVertical.Send(0.6f);
                }
                else if (config2.Parameters["FixRLeft"] is bool && (bool)config2.Parameters["FixRLeft"])
                {
                    OscAxisInput.LookHorizontal.Send(-2.0f);
                }
                else if (config2.Parameters["FixRRight"] is bool && (bool)config2.Parameters["FixRRight"])
                {
                    OscAxisInput.LookHorizontal.Send(2.0f);
                }
                else if (config2.Parameters["HeadUpLeft"] is bool && (bool)config2.Parameters["HeadUpLeft"])
                {
                    OscAxisInput.LookVertical.Send(2.0f);
                    OscAxisInput.LookHorizontal.Send(-2.0f);
                }
                else if (config2.Parameters["HeadUpRight"] is bool && (bool)config2.Parameters["HeadUpRight"])
                {
                    OscAxisInput.LookVertical.Send(2.0f);
                    OscAxisInput.LookHorizontal.Send(2.0f);
                }
                else if (config2.Parameters["HeadDLeft"] is bool && (bool)config2.Parameters["HeadDLeft"])
                {
                    OscAxisInput.LookVertical.Send(-2.0f);
                    OscAxisInput.LookHorizontal.Send(-2.0f);
                }
                else if (config2.Parameters["HeadDRight"] is bool && (bool)config2.Parameters["HeadDRight"])
                {
                    OscAxisInput.LookVertical.Send(-2.0f);
                    OscAxisInput.LookHorizontal.Send(2.0f);
                }
                else
                {
                    OscAxisInput.LookVertical.Send(0.0f);
                }

                // Move
                if (config2.Parameters["TargetPlayer"] is bool && (bool)config2.Parameters["TargetPlayer"])
                {
                    OscAxisInput.Vertical.Send(0.0f);
                    OscButtonInput.Run.Send(false);
                }
                else if (config2.Parameters["MoveToPlayer"] is bool && (bool)config2.Parameters["MoveToPlayer"])
                {
                    OscAxisInput.Vertical.Send(2.0f);
                    OscButtonInput.Run.Send(false);
                }
                else if (config2.Parameters["RunToPlayer"] is bool && (bool)config2.Parameters["RunToPlayer"])
                {
                    OscAxisInput.Vertical.Send(4.0f);
                    OscButtonInput.Run.Send(true);
                }
                else if (config2.Parameters["MoveFromMe"] is bool && (bool)config2.Parameters["MoveFromMe"])
                {
                    OscAxisInput.Vertical.Send(-1.0f);
                }
                else if (config2.Parameters["DownMoving"] is bool && (bool)config2.Parameters["DownMoving"])
                {
                    OscAxisInput.Vertical.Send(2.0f);
                    OscButtonInput.Run.Send(false);
                }
                else
                {
                    OscAxisInput.Vertical.Send(0.0f);
                    OscButtonInput.Run.Send(false);
                }

                bool currentState =
                                        (config2.Parameters["CheckLeft"] is bool && (bool)config2.Parameters["CheckLeft"]) ||
                                        (config2.Parameters["CheckRight"] is bool && (bool)config2.Parameters["CheckRight"]) ||
                                        (config2.Parameters["CheckBack"] is bool && (bool)config2.Parameters["CheckBack"]) ||
                                        (config2.Parameters["HeadDown"] is bool && (bool)config2.Parameters["HeadDown"]) ||
                                        (config2.Parameters["HeadLeft"] is bool && (bool)config2.Parameters["HeadLeft"]) ||
                                        (config2.Parameters["HeadLeft2"] is bool && (bool)config2.Parameters["HeadLeft2"]) ||
                                        (config2.Parameters["HeadRight"] is bool && (bool)config2.Parameters["HeadRight"]) ||
                                        (config2.Parameters["HeadRight2"] is bool && (bool)config2.Parameters["HeadRight2"]) ||
                                        (config2.Parameters["HeadUp"] is bool && (bool)config2.Parameters["HeadUp"]) ||
                                        (config2.Parameters["FixRLeft"] is bool && (bool)config2.Parameters["FixRLeft"]) ||
                                        (config2.Parameters["FixRRight"] is bool && (bool)config2.Parameters["FixRRight"]) ||
                                        (config2.Parameters["HeadUpLeft"] is bool && (bool)config2.Parameters["HeadUpLeft"]) ||
                                        (config2.Parameters["HeadUpRight"] is bool && (bool)config2.Parameters["HeadUpRight"]) ||
                                        (config2.Parameters["HeadDLeft"] is bool && (bool)config2.Parameters["HeadDLeft"]) ||
                                        (config2.Parameters["HeadDRight"] is bool && (bool)config2.Parameters["HeadDRight"]) ||
                                        (config2.Parameters["TargetPlayer"] is bool && (bool)config2.Parameters["TargetPlayer"]) ||
                                        (config2.Parameters["MoveToPlayer"] is bool && (bool)config2.Parameters["MoveToPlayer"]) ||
                                        (config2.Parameters["RunToPlayer"] is bool && (bool)config2.Parameters["RunToPlayer"]) ||
                                        (config2.Parameters["MoveFromMe"] is bool && (bool)config2.Parameters["MoveFromMe"]) ||
                                        (config2.Parameters["DownMoving"] is bool && (bool)config2.Parameters["DownMoving"]) ||
                                        (config2.Parameters["HeadCenter"] is bool && (bool)config2.Parameters["HeadCenter"]);
                if (currentState == false)
                {
                    OscAxisInput.Vertical.Send(1.0f);

                    if (config2.Parameters["VelocityZ"] is float && (float)config2.Parameters["VelocityZ"] <= 0.6)
                    {
                        if (!switchMovement)
                        {
                            OscAxisInput.LookHorizontal.Send(-1.0f);
                            await Task.Delay(400);
                        }
                        else
                        {
                            OscAxisInput.LookHorizontal.Send(1.0f);
                            await Task.Delay(400);
                        }

                        switchMovement = !switchMovement;
                        switchCount++;
                        if (switchCount > 5)
                        {
                            switchCount = 0;
                            await Task.Delay(750);
                        }
                    }
                    else
                    {
                        switchCount = 0;
                    }
                }
                await Task.Delay(50);
            }
        }
    }
}