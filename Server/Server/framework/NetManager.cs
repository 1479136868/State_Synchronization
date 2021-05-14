using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// 服务器端的代码，网络管理
/// 1. 侦听绑定端口
/// 2. 接收客户端来的链接请求
/// 3. 接收客户端来的数据
/// 4. 向客户端端发送数据。
/// </summary>
public class NetManager : Singleton<NetManager>
{
    private List<Client> allClientList = new List<Client>();
    private Socket listener;

    private int isCompressValue = 200;
    /// <summary>
    /// 网络模块的开启的方法
    /// </summary>
    public void Start()
    {
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //listener.Bind(new IPEndPoint(IPAddress.Parse("10.211.55.3"), 10036));
        listener.Bind(new IPEndPoint(IPAddress.Any, 10086));
        listener.Listen(20);
        ///当有客户端连接的时候，服务器把这个客户端接收下来，形成一个socket和客户端去对应。
        listener.BeginAccept(acceptCallBack, null);
    }

    /// <summary>
    /// 当有客户端链接的时候，会通过调用这个回调函数来通知。
    /// </summary>
    /// <param name="state"></param>
    private void acceptCallBack(IAsyncResult ar)
    {
        Socket clientSocket = listener.EndAccept(ar);

        Console.WriteLine("游客户端链接进来 IP=" + clientSocket.RemoteEndPoint);

        Client cli = new Client(clientSocket);
        allClientList.Add(cli);

        ///通知模块我这里接收到一个客户端的链接，
        new Notification((int)MsgIDDefine.ClientConnectedID, null, cli).Send();
        ///把客户端对应的Socket和这个客户端的收数据的buffer包装成一个对象。
        clientSocket.BeginReceive(cli.buffer, 0, cli.buffer.Length, SocketFlags.None, ReceiveCallback, cli);
        //再次接受下个客户端 的链接请求，不写这个只能把一个客户端接受下来
        listener.BeginAccept(acceptCallBack, null);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        Client cli = ar.AsyncState as Client;
        int len = 0;
        try
        {
            len = cli.clientSocket.EndReceive(ar);  //len为真实的收到的字节数
        }
        catch (Exception)
        {
            ///keep alive  保活
            Console.WriteLine("63和客户端之间断开链接了。 客户端的ip=" + cli.clientSocket.RemoteEndPoint);
            ///通知模块有客户端掉线了。
            new Notification((int)MsgIDDefine.ClientClosedID, null, cli).Send();
        }

        if (len > 0)
        {
            byte[] data = new byte[len];
            Buffer.BlockCopy(cli.buffer, 0, data, 0, len);

            ///从消息中拆分出消息id和pb内容两部分
            int msgID = BitConverter.ToInt32(data, 0);
            byte[] pbData = new byte[len - 4];
            Buffer.BlockCopy(data, 4, pbData, 0, pbData.Length);

            #region 消息的派发
            new Notification(msgID, pbData, cli).Send();
            #endregion
            ///再次收下次的数据，否则只能收取一次数据。
            cli.clientSocket.BeginReceive(cli.buffer, 0, cli.buffer.Length, SocketFlags.None, ReceiveCallback, cli);
        }
        else  //len==0链接断了
        {
            Console.WriteLine("86和客户端之间断开链接了。 客户端的ip=" + cli.clientSocket.RemoteEndPoint);
            ///通知模块有客户端掉线了。
            new Notification((int)MsgIDDefine.ClientClosedID, null, cli).Send();
        }

    }

    public void sendMsgToClient(MsgIDDefine id, IMessage msg, Client cli)
    {
        MyMemoryStream m = new MyMemoryStream();
        m.WriteInt((int)id);
        byte[] pbData = msg.ToByteArray();
        m.Write(pbData, 0, pbData.Length);
        byte[] data = m.ToArray(); ///数据部分
        m.Close();

       data = makeData(data); //封装数据包

        this.sendMsgToClient(data, cli);
    }

    /// <summary>
    /// 封装数据包。
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] makeData(byte[] data)
    {
        bool isCompress = false;
        if (data.Length > 200)
        {
            isCompress = true;
            data = ZlibHelper.CompressBytes(data);
        }
        data = SecurityUtil.Xor(data); //加密
        ushort crc = Crc16.CalculateCrc16(data);  //校验码。
        ushort bodyLen = (ushort)(data.Length + 3);  //包体长度

        MyMemoryStream m = new MyMemoryStream();
        m.WriteUShort(bodyLen);
        m.WriteBool(isCompress);
        m.WriteUShort(crc);
        m.Write(data, 0, data.Length);
        data = m.ToArray();
        m.Close();

        return data;

    } 

    public void sendMsgToClient(byte[] data, Client cli)
    {
        if (cli != null && cli.clientSocket != null && cli.clientSocket.Connected) /// socket.Connected为true，则链接状态下。
        {
            cli.clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, sendCallback, cli);
        }
    }

    private void sendCallback(IAsyncResult ar)
    {
        Client cli = ar.AsyncState as Client;
        try
        {
            int len = cli.clientSocket.EndSend(ar);
            Console.WriteLine("发送成功 字节数=" + len);
        }
        catch (Exception)
        {
            Console.WriteLine("130和客户端之间断开链接了。 客户端的ip=" + cli.clientSocket.RemoteEndPoint);
            ///通知模块有客户端掉线了。
            new Notification((int)MsgIDDefine.ClientClosedID, null, cli).Send();
        }
    }

}

/// <summary>
/// 代表一个客户端的类。
/// </summary>
public class Client
{
    public Socket clientSocket;
    public byte[] buffer = new byte[2048];

    public Client(Socket sc)
    {
        this.clientSocket = sc;
    }


}

