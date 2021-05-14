using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button loginbtn;
    public InputField usernameInput;
    public InputField pwdInput;
    public Text infoTxt;

    void Start()
    {
        loginbtn.onClick.AddListener(loginhandler);
        Message_manager.GetInstance().Addlistener((int)MsgIDDefine.S2C_LoginReturnMSG_ID, loginHandler);
    }

    private void loginHandler(Notification obj)
    {
        MyGame.S2C_LoginResponeMsg m = MyGame.S2C_LoginResponeMsg.Parser.ParseFrom(obj.content);
        if (m.Result)   ///登录成功，
        {
            ///登录成功后，有数据，存下。
            PlayerInfoModel.GetInstance().userid = m.UserID;
            PlayerInfoModel.GetInstance().username = m.UserName;

            SceneManager.LoadScene("game");
        }
        else
        {
            infoTxt.text = "登录失败";
        }
 
    }

    private void loginhandler()
    {
        MyGame.C2S_LoginMsg msg = new MyGame.C2S_LoginMsg();
        msg.UserName = this.usernameInput.text;
        msg.Password = this.pwdInput.text;

        NetManager.GetInstance().sendMsgToServer(MsgIDDefine.C2S_LoginMsg, msg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
