using MyGame;
using UnityEngine;
using UnityEngine.UI;

public class OnlineUserItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Text usernameTxt;
    private Image bg;

    private RoleMsg data;
    public RoleMsg Data { get => data;}

    private void Awake()
    {
        bg = this.GetComponent<Image>();
        bg.color = Color.white;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setData(RoleMsg item)
    {
        this.data = item;
        this.usernameTxt.text = item.Username;
    }

    public void SetSelect(bool v)
    {
        if (v)
        {
            bg.color = Color.red;
        }
        else
        {
            bg.color = Color.white;
        }
    }
}
