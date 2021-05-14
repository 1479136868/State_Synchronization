using UnityEngine;
using UnityEngine.AI;

public class OtherRoleCtrl : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator ani;
    //private bool isMove = false;
    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ///快到达停止距离了，还差0.2米，停止。
        if (nav.remainingDistance - nav.stoppingDistance < 0.5f)
        {
            ani.SetBool("run", false);
            //isMove = false;
            //nav.SetDestination(transform.position);
            //nav.isStopped = true;
        }
    }

    /// <summary>
    /// 接收到服务器转发的位置消息的时候，进行调用。
    /// </summary>
    /// <param name="pos"></param>
    public void MoveTo(Vector3 pos)
    {
        ani.SetBool("run", true);
        //nav.isStopped = false;
        nav.SetDestination(pos);
        //isMove = true;
    }

    public void SetInitPos(Vector3 pos)
    {
        this.transform.position = pos;
    }
}
