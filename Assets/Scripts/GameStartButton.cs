using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    void OnMouseDown()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            // ������Ϊ "Scene1" �ĳ���
            SceneManager.LoadScene("Scene0");
        }
    }
}
