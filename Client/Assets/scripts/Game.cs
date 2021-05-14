using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetManager.GetInstance().Connect("127.0.0.1", 10086);

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        NetManager.GetInstance().Update();
    }
    private void OnApplicationQuit()
    {
        NetManager.GetInstance().Close();
    }
}
