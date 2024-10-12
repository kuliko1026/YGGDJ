using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    public GameObject mainScreen;      // 主界面
    public GameObject messageScreen;   // 消息界面

    void Start()
    {
        // 为当前 GameObject 添加点击事件监听
        Button millButton = GetComponent<Button>();
        if (millButton != null)
        {
            millButton.onClick.AddListener(OnMillImageClick);
        }
    }

    void OnMillImageClick()
    {
        // 隐藏主界面并显示消息界面
        if (mainScreen != null)
        {
            mainScreen.SetActive(false); // 隐藏主界面
        }

        if (messageScreen != null)
        {
            messageScreen.SetActive(true); // 显示消息界面
        }
    }
}
