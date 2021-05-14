using UnityEngine;
using UnityEngine.UI;

public class MainCityUI : MonoBehaviour
{
    public Image chatBtn;
    public Text usernameTxt;

    public GameObject chatUI;
    void Start()
    {
        UIEventListener.Get(chatBtn.gameObject).onClick += MainCityUI_onClick;
    }

    private void MainCityUI_onClick(GameObject arg1, UnityEngine.EventSystems.BaseEventData arg2)
    {
        chatUI.SetActive(true);
    }

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
