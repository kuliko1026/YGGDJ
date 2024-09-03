using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DIalogue : MonoBehaviour
{
    public GameObject dialogueBox;  //�Ի���
    public Text dialogueText;       //�Ի��ı�
    public string[] dialogueLines;      //�Ի�����
    public float textSpeed = 0.05f; //ÿ���ַ���ʾ��ʱ����

    private CanvasGroup dialogueCanvasGroup;
    private int currentLineIndex = 0;
    private bool canInteract = false;
    private bool isTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ dialogueBox �� CanvasGroup ���
        dialogueCanvasGroup = dialogueBox.GetComponent<CanvasGroup>();
        // ��������ʱ�� dialogueBox ����Ϊ false
        dialogueBox.SetActive(false);

        // ����Э�̣�3��� dialogueBox ����Ϊ true
        StartCoroutine(ShowDialogueBoxAfterDelay(3f));
    }

    IEnumerator ShowDialogueBoxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueBox.SetActive(true);
        canInteract = true;
        ShowNextLine();

        // ����Э�̣�ʹ dialogueBox ��������
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

        dialogueCanvasGroup.alpha = 1f; // ȷ������͸����Ϊ1
    }

    // Update is called once per frame
    void Update()
    {
        // �����������ո�����
        if (canInteract && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (isTyping)
            {
                // ������ڴ��֣�ֱ����ʾ�����ı�
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
            // �Ի�������3�����ת�� scene1
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
