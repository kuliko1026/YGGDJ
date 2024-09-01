using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntreactingDialogue : MonoBehaviour
{
    public GameObject dialogueBox;  //对话框
    public Text dialogueText;       //对话文本
    public string clothesText;             //对话内容

    private bool playerNpc;     //触发判断

    void Start()
    {
        
    }

  
    void Update()
    {
         /*if (Input.GetKeyDown(KeyCode.D))
        {
            dialogueText.text = clothesText;
            dialogueBox.SetActive(true);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueText.text = clothesText;
            dialogueBox.SetActive(true);
            
            playerNpc = true;
            //Debug.Log("1");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            playerNpc = false;
            //Debug.Log("2");
        }

    }
}
