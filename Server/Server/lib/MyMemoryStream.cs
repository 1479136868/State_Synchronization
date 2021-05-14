using System;
using System.IO;
using System.Text;

public class MyMemoryStream : MemoryStream
{
    #region 构造函数
    public MyMemoryStream(byte[] data) : base(data) { }
    public MyMemoryStream() { }

    #endregion

    #region bool
    public void WriteBool(bool boo)
    {
        byte[] arr = BitConverter.GetBytes(boo);
        this.Write(arr, 0, arr.Length);

    }

    public bool ReadBool()
    {
        byte[] arr = new byte[1];
        this.Read(arr, 0, arr.Length);
        return BitConverter.ToBoolean(arr, 0);
    }

    #endregion

    #region int
    public void WriteInt(int val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        this.Write(arr, 0, arr.Length);

    }

    public int ReadInt()
    {
        byte[] arr = new byte[4];
        this.Read(arr, 0, arr.Length);
       
        return BitConverter.ToInt32(arr, 0);
    }

    #endregion

    #region Ushort
    public void WriteUShort(ushort val)
    {
        byte[] arr = BitConverter.GetBytes(val);
        this.Write(arr, 0, arr.Length);

    }

    public ushort ReadUshort()
    {
        byte[] arr = new byte[2];
        this.Read(arr, 0, arr.Length);
        return BitConverter.ToUInt16(arr, 0);
    }

    #endregion

    #region String
    public void WriteUString(string val)
    {
        byte[] arr = UTF8Encoding.UTF8.GetBytes(val);
        this.WriteUShort((ushort)arr.Length);
        this.Write(arr, 0, arr.Length);
    }

    public string ReadString()
    {
        ushort len = this.ReadUshort();

        byte[] arr = new byte[len];
        this.Read(arr, 0, arr.Length);
        return UTF8Encoding.UTF8.GetString(arr);
    }

    #endregion

}

