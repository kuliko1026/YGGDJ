using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGuide : MonoBehaviour
{
    public Image introGuideImage; // 将《introguide》的Image属性引用到该公共变量
    public float fadeDuration = 1f; // 渐变持续时间
    public float delayBeforeFadeIn = 2f; // 在dialoguebox出现后等待的时间

    private bool hasFadedOut = false; // 标记是否已经消失

    private void Start()
    {
        introGuideImage.color = new Color(1, 1, 1, 0); // 确保初始为完全透明
        StartCoroutine(ShowIntroGuide()); // 开始显示introguide
    }

    private IEnumerator ShowIntroGuide()
    {
        // 等待2秒
        yield return new WaitForSeconds(delayBeforeFadeIn);
        // 进行渐显
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        // 检测鼠标左键或空格键，并且确保introguide尚未消失
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !hasFadedOut)
        {
            StartCoroutine(FadeOut()); // 开始渐隐
            hasFadedOut = true; // 设置已经消失标志
        }
    }

    private IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);
            introGuideImage.color = new Color(1, 1, 1, alpha); // 渐变到不透明
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
            introGuideImage.color = new Color(1, 1, 1, alpha); // 渐变到透明
            yield return null;
        }

        // 确保最终状态是完全透明
        introGuideImage.color = new Color(1, 1, 1, 0);
    }
}
