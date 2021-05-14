using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetManager.GetInstance().Start();
            Console.WriteLine("服务器启动成功...");

            UserManager.GetInstance().init();
            Console.WriteLine("用户管理模块启动。");

            ChatManager.GetInstance().init();
            Console.WriteLine("聊天管理器初始化完毕");

            SceneManager.GetInstance().Init();
            Console.WriteLine("场景管理器启动成功");
            while (true)
            {

            }
        }
    }
}
