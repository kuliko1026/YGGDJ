using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGuide : MonoBehaviour
{
    public Image introGuideImage; // ����introguide����Image�������õ��ù�������
    public float fadeDuration = 1f; // �������ʱ��
    public float delayBeforeFadeIn = 2f; // ��dialoguebox���ֺ�ȴ���ʱ��

    private bool hasFadedOut = false; // ����Ƿ��Ѿ���ʧ

    private void Start()
    {
        introGuideImage.color = new Color(1, 1, 1, 0); // ȷ����ʼΪ��ȫ͸��
        StartCoroutine(ShowIntroGuide()); // ��ʼ��ʾintroguide
    }

    private IEnumerator ShowIntroGuide()
    {
        // �ȴ�2��
        yield return new WaitForSeconds(delayBeforeFadeIn);
        // ���н���
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        // �����������ո��������ȷ��introguide��δ��ʧ
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !hasFadedOut)
        {
            StartCoroutine(FadeOut()); // ��ʼ����
            hasFadedOut = true; // �����Ѿ���ʧ��־
        }
    }

    private IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);
            introGuideImage.color = new Color(1, 1, 1, alpha); // ���䵽��͸��
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(time / fadeDuration);
            introGuideImage.color = new Color(1, 1, 1, alpha); // ���䵽͸��
            yield return null;
        }

        // ȷ������״̬����ȫ͸��
        introGuideImage.color = new Color(1, 1, 1, 0);
    }
}
