using MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责接收从服务器发过来的同步消息，进行处理。
/// </summary>
public class RoleSyncCtrl : MonoBehaviour
{
    // Start is called before the first frame update


    /// <summary>
    /// 字典中存储其他玩家
    /// </summary>
    private Dictionary<int, OtherRoleCtrl> otherRoleDic = new Dictionary<int, OtherRoleCtrl>();
    void Start()
    {
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.S2C_SyncMsgID, syncHandler);
    }

    private void syncHandler(Notification obj)
    {
        SyncMsg msg = SyncMsg.Parser.ParseFrom(obj.content);

        if (msg.Userid != PlayerInfoModel.GetInstance().userid) //消息里的用户id是自己 ，说明是自己的位置服务器又发回来的。
        {
            if (otherRoleDic.ContainsKey(msg.Userid))  //场景中已经有这个位置通过的角色模型了。
            {
                ///让本地已经存在的代表其他玩家的模型，去到消息里指定的位置去，
                otherRoleDic[msg.Userid].MoveTo(new Vector3(msg.Pos.X, 0, msg.Pos.Z));
            }
            else
            {
                GameObject prefab = Resources.Load<GameObject>("otherRole");
                GameObject role = GameObject.Instantiate<GameObject>(prefab);
                OtherRoleCtrl ctrl = role.GetComponent<OtherRoleCtrl>();
                otherRoleDic.Add(msg.Userid, ctrl);
                ///闪现
                ctrl.SetInitPos(new Vector3(msg.CrtPos.X, 0, msg.CrtPos.Z));
                StartCoroutine(Move(ctrl,msg));
            }
        }
        else
        {
            ///服务器转发的坐标是自己的坐标。
        }

    }

    public IEnumerator Move(OtherRoleCtrl ctrl, SyncMsg msg)
    {
        yield return new WaitForSeconds(0.2f);
        ctrl.MoveTo(new Vector3(msg.Pos.X, 0, msg.Pos.Z));
    }

 

    // Update is called once per frame
    void Update()
    {

    }
}
