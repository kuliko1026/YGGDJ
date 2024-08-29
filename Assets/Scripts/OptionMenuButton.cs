using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenuButton : MonoBehaviour
{
    void OnMouseDown()
        {
            // 检测鼠标左键点击
            if (Input.GetMouseButtonDown(0))
            {
                // 加载名为 "OptionsMenu" 的场景
                SceneManager.LoadScene("OptionsMenu");
            }
        }
    }