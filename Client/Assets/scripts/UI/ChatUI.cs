using MyGame;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public Text msgArea;
    public Button sendmsgBtn;
    public InputField msgInput;
    private string privateChatname;

    public Image closeBtn;
    public Transform content;
    public Button refreshBtn;

    /// <summary>
    /// 当前选择的私聊对象item
    /// </summary>
    private OnlineUserItem selectedUserGo;
    private void Awake()
    {
        sendmsgBtn.onClick.AddListener(sendMsgHandler);
        refreshBtn.onClick.AddListener(refreshHandler);
        ///自动发一个刷新在线人员名单的消息给服务器。
        refreshHandler();

        UIEventListener.Get(closeBtn.gameObject).onClick += ChatUI_onClick;
        this.gameObject.SetActive(false);
        ////当有服务器来的消息的时候，网络管理器会通过消息中心进行派发，
        ///所以，在这我们要收到服务器的消息，就只能通过消息中心进行侦听。
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.S2C_ChatMsg, chathandler);
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.S2C_OnLineRoleMsgID, showOnlineUserList);
    }

    /// <summary>
    /// 向服务器发请求，要最新的在线人员的名单。
    /// </summary>
    private void refreshHandler()
    {
        RequestOnlineRoleListMsg mm = new RequestOnlineRoleListMsg();
        mm.Userid = PlayerInfoModel.GetInstance().userid;

        NetManager.GetInstance().sendMsgToServer(MsgIDDefine.C2S_RequestOnLineUserMsgID, mm);
    }

    private void showOnlineUserList(Notification obj)
    {
        foreach (Transform item in content)
        {
            GameObject.Destroy(item.gameObject);
        }
        OnlineRoleListMsg m = OnlineRoleListMsg.Parser.ParseFrom(obj.content);
        foreach (var item in m.RoleList)
        {
            GameObject prefab = Resources.Load<GameObject>("onlineUserItem");
            GameObject go = GameObject.Instantiate<GameObject>(prefab);
            go.transform.SetParent(content, false);

            go.GetComponent<OnlineUserItem>().setData(item);
            UIEventListener.Get(go).onClick += ChatUI_onClick1;
        }
    }

    private void ChatUI_onClick1(GameObject go, UnityEngine.EventSystems.BaseEventData arg2)
    {
        OnlineUserItem tmp = go.GetComponent<OnlineUserItem>(); ///点击选择的。
        if (selectedUserGo != null)
        {
            selectedUserGo.SetSelect(false);
            if (selectedUserGo == tmp)
            {
                selectedUserGo = null;///取消选择
            }
            else
            {
                selectedUserGo = tmp;
                selectedUserGo.SetSelect(true);
            }
        }
        else
        {
            selectedUserGo = tmp;
            selectedUserGo.SetSelect(true);
        }

    }

    private void chathandler(Notification obj)
    {
        ChatMsg m = ChatMsg.Parser.ParseFrom(obj.content);
        string str = string.Empty;
        switch (m.ChatType)
        {
            case ChatType.PublicChat:
                str = "[公聊]" + m.FromName + "对大家说：" + m.Msg;
                break;
            case ChatType.PrivateChat:
                str = "[私聊]" + m.FromName + "对你悄悄说：" + m.Msg;
                break;
            default:
                break;
        }
        msgArea.text += str + "\r\n";
    }

    private void ChatUI_onClick(GameObject go, UnityEngine.EventSystems.BaseEventData arg2)
    {
        this.gameObject.SetActive(false);
    }

    private void sendMsgHandler()
    {
        ChatMsg msg = new ChatMsg();
        msg.Msg = msgInput.text;  //话
        msg.FromID = PlayerInfoModel.GetInstance().userid;
        msg.FromName = PlayerInfoModel.GetInstance().username;
        if (selectedUserGo == null)  ///公聊
        {
            msg.ChatType = ChatType.PublicChat;  //公聊私聊的类型。
        }
        else
        {
            msg.ChatType = ChatType.PrivateChat;  //公聊私聊的类型。
            msg.ToID = selectedUserGo.Data.Userid;
            msg.ToName = selectedUserGo.Data.Username;
        }
        NetManager.GetInstance().sendMsgToServer(MsgIDDefine.C2S_ChatMsgID, msg);

    }

}
