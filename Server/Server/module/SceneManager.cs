using System;


public class SceneManager : Singleton<SceneManager>
{

    public void Init()
    {
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.C2S_SyncMsgID, syncHandler);
    }

    private void syncHandler(Notification obj)
    {
        MyGame.SyncMsg m = MyGame.SyncMsg.Parser.ParseFrom(obj.content);
        Console.WriteLine("pos x = {0}  z = {1}", m.Pos.X, m.Pos.Z);

        foreach (var item in UserManager.GetInstance().AllLoginedUser.Values)
        {
            //if (item.cli == cli)
            //    continue;
            NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_SyncMsgID, m, item.cli);
        }
    }
}

