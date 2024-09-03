using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntreactingDialogue : MonoBehaviour
{
    public GameObject dialogueBox;  //对话框
    public Text dialogueText;       //对话文本
    public string[] dialogueLines;  //多行对话内容
    public float textSpeed = 0.05f; // 文本显示速度
    public HeroController2D heroController; // 玩家控制脚本


    private bool playerNpc;     //触发判断
    private bool isTyping;      // 是否正在逐个显示文本
    private int currentLine = 0; // 当前显示的文本行索引

    void Start()
    {
        // 调试信息：确保dialogueLines数组已正确分配
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogError($"{gameObject.name}的dialogueLines数组为空或未分配。");
        }
        else
        {
            Debug.Log($"{gameObject.name}的dialogueLines数组已分配，长度为{dialogueLines.Length}。");
        }
    }

    void Update()
    {
        // 检测玩家是否在NPC附近并且按下F键
        if (playerNpc && Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            dialogueBox.SetActive(true);
            heroController.SetCanMove(false); // 禁用玩家移动
            StartCoroutine(TypeText(dialogueLines[currentLine]));
        }

        // 检测鼠标左键或空格键点击以跳转到下一行文本
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
                heroController.SetCanMove(true); // 启用玩家移动
                currentLine = 0; // 重置索引以便下次对话
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNpc = true;
            Debug.Log($"{gameObject.name}触发区域进入。");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            playerNpc = false;
            currentLine = 0; // 重置索引以便下次对话
            heroController.SetCanMove(true); // 启用玩家移动
            Debug.Log($"{gameObject.name}触发区域退出。");
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
