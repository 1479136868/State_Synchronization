// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: LoginMsg.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace MyGame {

  #region Messages
  /// <summary>
  /////客户端----》服务器     登录的消息
  /// </summary>
  public sealed class C2S_LoginMsg : pb::IMessage {
    private static readonly pb::MessageParser<C2S_LoginMsg> _parser = new pb::MessageParser<C2S_LoginMsg>(() => new C2S_LoginMsg());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<C2S_LoginMsg> Parser { get { return _parser; } }

    /// <summary>Field number for the "userName" field.</summary>
    public const int UserNameFieldNumber = 1;
    private string userName_ = "";
    /// <summary>
    ///1号字段是用户名。
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserName {
      get { return userName_; }
      set {
        userName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "password" field.</summary>
    public const int PasswordFieldNumber = 2;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UserName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(UserName);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Password);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UserName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserName);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            UserName = input.ReadString();
            break;
          }
          case 18: {
            Password = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /////服务器---》 客户端-    登录的响应消息
  /// </summary>
  public sealed class S2C_LoginResponeMsg : pb::IMessage {
    private static readonly pb::MessageParser<S2C_LoginResponeMsg> _parser = new pb::MessageParser<S2C_LoginResponeMsg>(() => new S2C_LoginResponeMsg());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<S2C_LoginResponeMsg> Parser { get { return _parser; } }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private bool result_;
    /// <summary>
    ////成功  和失败。
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "errorCode" field.</summary>
    public const int ErrorCodeFieldNumber = 2;
    private int errorCode_;
    /// <summary>
    ///错误信息
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ErrorCode {
      get { return errorCode_; }
      set {
        errorCode_ = value;
      }
    }

    /// <summary>Field number for the "userID" field.</summary>
    public const int UserIDFieldNumber = 3;
    private int userID_;
    /// <summary>
    ////用户id
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int UserID {
      get { return userID_; }
      set {
        userID_ = value;
      }
    }

    /// <summary>Field number for the "userName" field.</summary>
    public const int UserNameFieldNumber = 4;
    private string userName_ = "";
    /// <summary>
    ///1号字段是用户名。
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserName {
      get { return userName_; }
      set {
        userName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != false) {
        output.WriteRawTag(8);
        output.WriteBool(Result);
      }
      if (ErrorCode != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(ErrorCode);
      }
      if (UserID != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(UserID);
      }
      if (UserName.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(UserName);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != false) {
        size += 1 + 1;
      }
      if (ErrorCode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ErrorCode);
      }
      if (UserID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(UserID);
      }
      if (UserName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserName);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Result = input.ReadBool();
            break;
          }
          case 16: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 24: {
            UserID = input.ReadInt32();
            break;
          }
          case 34: {
            UserName = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
