using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTool : MonoBehaviour
{
    public RectTransform phone; // ��phone��RectTransform������õ��ñ���
    public float slideSpeed = 0.5f; // �����ٶȣ����Ը�����Ҫ������

    public Vector2 customPopupPosition; // �Զ��嵯��λ��
    private bool isPhoneVisible = false; // ����ֻ��Ƿ�ɼ�

    public Image backgroundMask; // ����BackgroundMask

    private void Start()
    {
        // ȷ���������ֳ�ʼʱΪ��ȫ͸��
        if (backgroundMask)
        {
            Color color = backgroundMask.color;
            color.a = 0; // ����Ϊ��ȫ͸��
            backgroundMask.color = color;
        }
    }

    private void Update()
    {
        // ��������������E�����µ����ֻ�
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            TogglePhone(); // �����ֻ�
        }

        // ���B�������������ֻ�
        if (Input.GetKeyDown(KeyCode.B))
        {
            HidePhone(); // �����ֻ�
        }
    }

    private void TogglePhone()
    {
        if (!isPhoneVisible)
        {
            StartCoroutine(SlideIn()); // �����ֻ�
        }
    }

    private void HidePhone()
    {
        if (isPhoneVisible)
        {
            StartCoroutine(SlideOut()); // �����ֻ�
        }
    }

    private IEnumerator SlideIn()
    {
        float timeElapsed = 0;

        Vector2 hiddenPosition = new Vector2(phone.anchoredPosition.x, -Screen.height); // ����λ������Ļ�ײ�
        phone.anchoredPosition = hiddenPosition; // ���ó�ʼλ��Ϊ����

        // ������λ�û��뵽������Զ��嵯��λ��
        while (timeElapsed < slideSpeed)
        {
            phone.anchoredPosition = Vector2.Lerp(hiddenPosition, customPopupPosition, timeElapsed / slideSpeed);
            timeElapsed += Time.deltaTime; // ����ʱ��
            yield return null; // ���ؿ���Ȩ���ȴ���һ֡
        }

        // ȷ������λ�����Զ���λ��
        phone.anchoredPosition = customPopupPosition;
        isPhoneVisible = true; // �����ֻ�Ϊ�ɼ�

        // �ñ����䰵
        StartCoroutine(FadeInBackground());
    }

    private IEnumerator SlideOut()
    {
        float timeElapsed = 0;

        Vector2 hiddenPosition = new Vector2(phone.anchoredPosition.x, -Screen.height); // ����λ������Ļ�ײ�

        // �ӿɼ�λ�û���������λ��
        while (timeElapsed < slideSpeed)
        {
            phone.anchoredPosition = Vector2.Lerp(customPopupPosition, hiddenPosition, timeElapsed / slideSpeed);
            timeElapsed += Time.deltaTime; // ����ʱ��
            yield return null; // ���ؿ���Ȩ���ȴ���һ֡
        }

        // ȷ������λ��������λ��
        phone.anchoredPosition = hiddenPosition; // ȷ������λ��������
        isPhoneVisible = false; // �����ֻ�Ϊ���ɼ�

        // ֱ�ӽ���������͸��������Ϊ��ȫ͸��
        if (backgroundMask)
        {
            Color color = backgroundMask.color;
            color.a = 0;  // ��������Ϊ͸��
            backgroundMask.color = color;
        }
    }

    private IEnumerator FadeInBackground()
    {
        float duration = 0.15f; // ���ֱ䰵�ĳ���ʱ��
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            Color color = backgroundMask.color;
            color.a = Mathf.Clamp01(timeElapsed / duration * 0.5f); // ���䵽��͸��
            backgroundMask.color = color;
            yield return null; // ���ؿ���Ȩ���ȴ���һ֡
        }
    }

    

    private bool IsPointerOverGameObject()
    {
        // �������Ƿ���UIԪ����
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}