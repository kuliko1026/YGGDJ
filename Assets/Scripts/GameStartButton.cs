using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    void OnMouseDown()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 加载名为 "Scene1" 的场景
            SceneManager.LoadScene("Scene0");
        }
    }
}
