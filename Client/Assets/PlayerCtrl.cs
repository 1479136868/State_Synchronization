using UnityEngine;
using UnityEngine.AI;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent nav;
    private Animator ani;

    //private bool isMove = false;

    private void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        ani = this.GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("ground")))
            {
                Vector3 pos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);

                ani.SetBool("run", true);
                //nav.isStopped = false;
                nav.SetDestination(pos);
                //动画控制。
                //isMove = true;
                ///和服务器发送同步的消息。
                MyGame.SyncMsg m = new MyGame.SyncMsg();
                m.Userid = PlayerInfoModel.GetInstance().userid;
                ///确定了目标点，向服务器发消息，
                m.Pos = new MyGame.Vec3();
                m.Pos.X = pos.x;
                m.Pos.Z = pos.z;
                //当前位置。
                m.CrtPos = new MyGame.Vec3();
                m.CrtPos.X = this.transform.position.x;
                m.CrtPos.Z = this.transform.position.z;
                NetManager.GetInstance().sendMsgToServer(MsgIDDefine.C2S_SyncMsgID, m);
            }
        }
        ///快到达停止距离了，还差0.2米，停止。
        else if (nav.remainingDistance - nav.stoppingDistance < 0.5f)
        {
            ani.SetBool("run", false);
            //isMove = false;
            //nav.SetDestination(transform.position);
            //nav.isStopped = true;
        }

    }
}
