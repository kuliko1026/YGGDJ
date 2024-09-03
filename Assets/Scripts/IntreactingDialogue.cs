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


    private bool playerNpc;     //�����ж�
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
    }

    void Update()
    {
        // �������Ƿ���NPC�������Ұ���F��
        if (playerNpc && Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            dialogueBox.SetActive(true);
            heroController.SetCanMove(false); // ��������ƶ�
            StartCoroutine(TypeText(dialogueLines[currentLine]));
        }

        // �����������ո���������ת����һ���ı�
        if (isTyping && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLine];
            isTyping = false;
        }
        else if (!isTyping && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            currentLine++;
            if (currentLine < dialogueLines.Length)
            {
                StartCoroutine(TypeText(dialogueLines[currentLine]));
            }
            else
            {
                dialogueBox.SetActive(false);
                heroController.SetCanMove(true); // ��������ƶ�
                currentLine = 0; // ���������Ա��´ζԻ�
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNpc = true;
            Debug.Log($"{gameObject.name}����������롣");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            playerNpc = false;
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
