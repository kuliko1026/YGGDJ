using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenuButton : MonoBehaviour
{
    void OnMouseDown()
        {
            // ������������
            if (Input.GetMouseButtonDown(0))
            {
                // ������Ϊ "OptionsMenu" �ĳ���
                SceneManager.LoadScene("OptionsMenu");
            }
        }
    }