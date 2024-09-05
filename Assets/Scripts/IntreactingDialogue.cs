using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntreactingDialogue : MonoBehaviour
{
    public GameObject dialogueBox;  //�Ի���
    public Text dialogueText;       //�Ի��ı�
    public string[] dialogueLines;  //���жԻ�����
    public float textSpeed = 0.05f; // �ı���ʾ�ٶ�
    public HeroController2D heroController; // ��ҿ��ƽű�
    public SkeletonAnimation fKeyPrompt; // ��ʾ����


    private bool playerItem;     //�����ж�
    private bool isTyping;      // �Ƿ����������ʾ�ı�
    private int currentLine = 0; // ��ǰ��ʾ���ı�������

    void Start()
    {
        // ������Ϣ��ȷ��dialogueLines��������ȷ����
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogError($"{gameObject.name}��dialogueLines����Ϊ�ջ�δ���䡣");
        }
        else
        {
            Debug.Log($"{gameObject.name}��dialogueLines�����ѷ��䣬����Ϊ{dialogueLines.Length}��");
        }

        // ȷ����ʾ����Ĭ������
        if (fKeyPrompt != null)
        {
            fKeyPrompt.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        CheckPlayerInput();
    }

    void CheckPlayerInput()
    {
        if (playerItem && Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            StartDialogue();
        }

        if (isTyping && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            SkipTyping();
        }
        else if (!isTyping && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        dialogueBox.SetActive(true);
        heroController.SetCanMove(false);
        StartCoroutine(TypeText(dialogueLines[currentLine = 0]));
    }

    void SkipTyping()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[currentLine];
        isTyping = false;
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(TypeText(dialogueLines[currentLine]));
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        heroController.SetCanMove(true);
        currentLine = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fKeyPrompt.gameObject.SetActive(true);
            playerItem = true;
            Debug.Log($"{gameObject.name}����������롣");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            fKeyPrompt.gameObject.SetActive(false);
            playerItem = false;
            currentLine = 0; // ���������Ա��´ζԻ�
            heroController.SetCanMove(true); // ��������ƶ�
            Debug.Log($"{gameObject.name}���������˳���");
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }
}
