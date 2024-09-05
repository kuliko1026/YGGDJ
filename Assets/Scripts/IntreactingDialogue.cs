using Spine.Unity;
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
    public SkeletonAnimation fKeyPrompt; // 提示动画


    private bool playerItem;     //触发判断
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

        // 确保提示动画默认隐藏
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
            Debug.Log($"{gameObject.name}触发区域进入。");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            fKeyPrompt.gameObject.SetActive(false);
            playerItem = false;
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
