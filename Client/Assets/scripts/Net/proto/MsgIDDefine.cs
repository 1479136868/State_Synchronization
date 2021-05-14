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


}

