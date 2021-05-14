using MyGame;
using System;
using System.Collections.Generic;

/// <summary>
/// 用户管理，实现服务器端的用户登录注册逻辑。
/// </summary>
public class UserManager : Singleton<UserManager>
{
    private List<DBUser> dbList = new List<DBUser>();

    /// <summary>
    /// 所有已经登录成功的角色  角色是登录成功后产生。
    /// </summary>
    private Dictionary<int, Role> allLoginedUser = new Dictionary<int, Role>();
    public Dictionary<int, Role> AllLoginedUser { get => allLoginedUser; }

    public void init()
    {
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.C2S_LoginMsg, loginHandler);
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.ClientClosedID, disConnectHandler);

        initDB();
    }

    private void disConnectHandler(Notification obj)
    {
        Client cli = obj.client;
        foreach (var item in allLoginedUser)
        {
            if (item.Value.cli == cli)
            {
                allLoginedUser.Remove(item.Key);
                break;
            }
        }
        ///把掉线的消息，剩余的客户端信息，发给所有的剩余的客户端。
        braodCastOnlineUserList();
    }

    private void braodCastOnlineUserList()
    {
        OnlineRoleListMsg msg = GetOnlineUserMsg();
        foreach (var item in allLoginedUser)
        {
            NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_OnLineRoleMsgID, msg, item.Value.cli);
        }
    }

    private void initDB()
    {
        for (int i = 1; i < 10; i++)
        {
            dbList.Add(new DBUser() { id = i, username = "user" + i, pwd = "123", lv = i * 10, exp = 99999 });
        }
    }

    private void loginHandler(Notification obj)
    {
        byte[] pbData = obj.content;
        MyGame.C2S_LoginMsg msg = MyGame.C2S_LoginMsg.Parser.ParseFrom(pbData);
        Client cli = obj.client;  //消息从这个客户端来的。

        Console.WriteLine("收到客户端{0} 的消息 username={1}  pwd={2} ", cli.clientSocket.RemoteEndPoint, msg.UserName, msg.Password);

        DBUser user = dbList.Find(x => x.username == msg.UserName && x.pwd == msg.Password);
        if (user != null)  //成功
        {
            ///登录成功干啥呀？
            ///1。返回登录成功消息
            S2C_LoginResponeMsg returnMSG = new S2C_LoginResponeMsg();
            returnMSG.Result = true;
            returnMSG.UserID = user.id;
            returnMSG.UserName = user.username;

            NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_LoginReturnMSG_ID, returnMSG, cli);

            //2.登录成功的角色存储下来。
            Role role = new Role(cli);
            role.username = user.username;
            role.roleID = user.id;
            allLoginedUser.Add(role.roleID, role);
        }
        else
        {
            ///登录成功干啥呀？
            S2C_LoginResponeMsg returnMSG = new S2C_LoginResponeMsg();
            returnMSG.Result = false;
            returnMSG.ErrorCode = 1;
            NetManager.GetInstance().sendMsgToClient(MsgIDDefine.S2C_LoginReturnMSG_ID, returnMSG, cli);
        }

    }

    /// <summary>
    /// 向所有的在线的客户端。广播在线的人名单，
    /// </summary>
    public OnlineRoleListMsg GetOnlineUserMsg()
    {
        MyGame.OnlineRoleListMsg m = new OnlineRoleListMsg();
        foreach (var item in allLoginedUser.Values)
        {
            m.RoleList.Add(new RoleMsg() { Userid = item.roleID, Username = item.username });
        }
        return m;
    }
}

/// <summary>
/// m模拟数据库
/// </summary>
public class DBUser
{
    public int id;
    public string username;
    public string pwd;
    public int lv;
    public int exp;
}

/// <summary>
/// 登录成功，产生角色对象
/// </summary>
public class Role
{
    public Client cli;
    public string username;
    public int roleID;

    public Role(Client cli)
    {
        this.cli = cli;
    }

}

