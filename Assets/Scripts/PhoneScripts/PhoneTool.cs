using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTool : MonoBehaviour
{
    public RectTransform phone; // 将phone的RectTransform组件引用到该变量
    public float slideSpeed = 0.5f; // 弹出速度（可以根据需要调整）

    public Vector2 customPopupPosition; // 自定义弹出位置
    private bool isPhoneVisible = false; // 标记手机是否可见

    public Image backgroundMask; // 引用BackgroundMask

    private void Start()
    {
        // 确保背景遮罩初始时为完全透明
        if (backgroundMask)
        {
            Color color = backgroundMask.color;
            color.a = 0; // 设置为完全透明
            backgroundMask.color = color;
        }
    }

    private void Update()
    {
        // 检测鼠标左键点击或E键按下弹出手机
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            TogglePhone(); // 移入手机
        }

        // 检测B键按下以隐藏手机
        if (Input.GetKeyDown(KeyCode.B))
        {
            HidePhone(); // 隐藏手机
        }
    }

    private void TogglePhone()
    {
        if (!isPhoneVisible)
        {
            StartCoroutine(SlideIn()); // 弹出手机
        }
    }

    private void HidePhone()
    {
        if (isPhoneVisible)
        {
            StartCoroutine(SlideOut()); // 隐藏手机
        }
    }

    private IEnumerator SlideIn()
    {
        float timeElapsed = 0;

        Vector2 hiddenPosition = new Vector2(phone.anchoredPosition.x, -Screen.height); // 隐藏位置在屏幕底部
        phone.anchoredPosition = hiddenPosition; // 设置初始位置为隐藏

        // 从隐藏位置滑入到定义的自定义弹出位置
        while (timeElapsed < slideSpeed)
        {
            phone.anchoredPosition = Vector2.Lerp(hiddenPosition, customPopupPosition, timeElapsed / slideSpeed);
            timeElapsed += Time.deltaTime; // 更新时间
            yield return null; // 返回控制权，等待下一帧
        }

        // 确保最终位置在自定义位置
        phone.anchoredPosition = customPopupPosition;
        isPhoneVisible = true; // 设置手机为可见

        // 让背景变暗
        StartCoroutine(FadeInBackground());
    }

    private IEnumerator SlideOut()
    {
        float timeElapsed = 0;

        Vector2 hiddenPosition = new Vector2(phone.anchoredPosition.x, -Screen.height); // 隐藏位置在屏幕底部

        // 从可见位置滑出到隐藏位置
        while (timeElapsed < slideSpeed)
        {
            phone.anchoredPosition = Vector2.Lerp(customPopupPosition, hiddenPosition, timeElapsed / slideSpeed);
            timeElapsed += Time.deltaTime; // 更新时间
            yield return null; // 返回控制权，等待下一帧
        }

        // 确保最终位置在隐藏位置
        phone.anchoredPosition = hiddenPosition; // 确保最终位置在隐藏
        isPhoneVisible = false; // 设置手机为不可见

        // 直接将背景遮罩透明度设置为完全透明
        if (backgroundMask)
        {
            Color color = backgroundMask.color;
            color.a = 0;  // 立即设置为透明
            backgroundMask.color = color;
        }
    }

    private IEnumerator FadeInBackground()
    {
        float duration = 0.15f; // 遮罩变暗的持续时间
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            Color color = backgroundMask.color;
            color.a = Mathf.Clamp01(timeElapsed / duration * 0.5f); // 渐变到半透明
            backgroundMask.color = color;
            yield return null; // 返回控制权，等待下一帧
        }
    }

    

    private bool IsPointerOverGameObject()
    {
        // 检测鼠标是否在UI元素上
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}