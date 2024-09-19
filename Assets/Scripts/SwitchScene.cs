using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // 确保将此脚本附加到 player 对象上
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否是 arrow
        if (collision.gameObject.name == "arrow")
        {
            // 获取当前场景的索引
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // 跳转到下一个场景
            // 注意：当前场景索引必须小于总场景数
            if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("已经是最后一个场景，无法跳转。");
            }
        }
    }
}