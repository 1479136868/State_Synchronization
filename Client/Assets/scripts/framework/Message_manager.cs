using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息类，
/// </summary>
public class Notification
{
    public int id;
    public byte[] content;
    public Notification(int id, byte[] content)
    {
        this.id = id;
        this.content = content;
    }
    public void Send()
    {
        Message_manager.GetInstance().Dispatch(this.id, this);
    }
}
/// <summary>
/// 消息中心。
/// </summary>
public class Message_manager : Singleton<Message_manager>
{
    Dictionary<int, Action<Notification>> dic = new Dictionary<int, Action<Notification>>();
    /// <summary>
    /// 侦听的消息。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action"></param>
    public void Addlistener(int id, Action<Notification> action)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] += action;
        }
        else
        {
            dic.Add(id, action);
        }
    }
    /// <summary>
    /// 消息的派发
    /// </summary>
    /// <param name="id"></param>
    /// <param name="noti"></param>
    public void Dispatch(int id, Notification noti)
    {
        if(dic.ContainsKey(id))
        {
            dic[id]?.Invoke(noti);
        }
        else
        {
            Debug.LogWarning("派发的事件id=" + id + "还没有被注册");
        }
      
    }
    /// <summary>
    /// 移除侦听。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action"></param>
    public void RemoveListener(int id, Action<Notification> action)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] -= action;
            if (dic[id] == null)
            {
                dic.Remove(id);
            }
        }
    }
}
