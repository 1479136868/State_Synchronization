using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum MsgIDDefine
{
    /// <summary>
    /// 上行协议的编号
    /// </summary>
    C2S_ChatMsgID = 10001,   ///上行  聊天的消息   Client to Server      1开头的上行
    C2S_LoginMsg = 10002,  //登录
    C2S_RequestOnLineUserMsgID = 10003,
    C2S_SyncMsgID = 10004,




    ///下行协议。 
    S2C_ChatMsg = 20001,
    S2C_LoginReturnMSG_ID = 20002,
    S2C_OnLineRoleMsgID = 20003,   ///在线人员消息的id
    S2C_SyncMsgID = 20004,                     




    ///不是网络消息编号  ，用于内部模块间的事件派发。
    ClientConnectedID = 30001,  //当有客户的链接进来
    ClientClosedID = 30002,///当客户端从服务器端掉线了
}

