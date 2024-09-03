using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DIalogue : MonoBehaviour
{
    public GameObject dialogueBox;  //对话框
    public Text dialogueText;       //对话文本
    public string[] dialogueLines;      //对话内容
    public float textSpeed = 0.05f; //每个字符显示的时间间隔

    private CanvasGroup dialogueCanvasGroup;
    private int currentLineIndex = 0;
    private bool canInteract = false;
    private bool isTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        // 获取 dialogueBox 的 CanvasGroup 组件
        dialogueCanvasGroup = dialogueBox.GetComponent<CanvasGroup>();
        // 场景启动时将 dialogueBox 设置为 false
        dialogueBox.SetActive(false);

        // 启动协程，3秒后将 dialogueBox 设置为 true
        StartCoroutine(ShowDialogueBoxAfterDelay(3f));
    }

    IEnumerator ShowDialogueBoxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueBox.SetActive(true);
        canInteract = true;
        ShowNextLine();

        // 启动协程，使 dialogueBox 缓慢出现
        StartCoroutine(FadeInDialogueBox(1f));
    }

    IEnumerator FadeInDialogueBox(float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;
            dialogueCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        dialogueCanvasGroup.alpha = 1f; // 确保最终透明度为1
    }

    // Update is called once per frame
    void Update()
    {
        // 检测鼠标左键或空格键点击
        if (canInteract && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (isTyping)
            {
                // 如果还在打字，直接显示完整文本
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLineIndex - 1];
                isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            // 对话结束，3秒后跳转到 scene1
            StartCoroutine(TransitionToNextScene(3f));
        }
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    IEnumerator TransitionToNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("scene1");
    }
}
