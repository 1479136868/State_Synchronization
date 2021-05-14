using MyGame;
using System.Collections.Generic;

public class ChatManager : Singleton<ChatManager>
{
    public void init()
    {
        ///侦听网络模块（NetManager）从客户端收到的消息。
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.C2S_ChatMsgID, chatMsgHandler);

        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.C2S_RequestOnLineUserMsgID, requestOnlineUserMsgHandler);
    }

    private void requestOnlineUserMsgHandler(Notification obj)
    {
        byte[] pbData = obj.content;
        RequestOnlineRoleListMsg msg = RequestOnlineRoleListMsg.Parser.ParseFrom(pbData);

        Client cli = obj.client;  //消息从这个客户端来的。

        ///返回消息。
        OnlineRoleListMsg returnMsg = UserManager.GetInstance().GetOnlineUserMsg();
        NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_OnLineRoleMsgID, returnMsg, cli);
    }

    private void chatMsgHandler(Notification obj)
    {
        byte[] pbData = obj.content;

        ChatMsg msg = ChatMsg.Parser.ParseFrom(pbData);
        Client cli = obj.client;  //消息从这个客户端来的。

        switch (msg.ChatType)
        {
            case ChatType.PublicChat:
                ///转发。
                foreach (var item in UserManager.GetInstance().AllLoginedUser.Values)
                {
                    //if (item.cli == cli)
                    //    continue;
                    NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_ChatMsg, msg, item.cli);
                }
                break;
            case ChatType.PrivateChat:
                ///找人
                Dictionary<int, Role> dic = UserManager.GetInstance().AllLoginedUser;
                if(dic.ContainsKey(msg.ToID))
                {
                    NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_ChatMsg, msg, dic[msg.ToID].cli);
                }
                break;
            default:
                break;
        }
    }
}

