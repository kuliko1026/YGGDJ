using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    public GameObject mainScreen;      // ������
    public GameObject messageScreen;   // ��Ϣ����

    void Start()
    {
        // Ϊ��ǰ GameObject ��ӵ���¼�����
        Button millButton = GetComponent<Button>();
        if (millButton != null)
        {
            millButton.onClick.AddListener(OnMillImageClick);
        }
    }

    void OnMillImageClick()
    {
        // ���������沢��ʾ��Ϣ����
        if (mainScreen != null)
        {
            mainScreen.SetActive(false); // ����������
        }

        if (messageScreen != null)
        {
            messageScreen.SetActive(true); // ��ʾ��Ϣ����
        }
    }
}
