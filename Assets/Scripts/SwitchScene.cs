using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // ȷ�����˽ű����ӵ� player ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�Ķ����Ƿ��� arrow
        if (collision.gameObject.name == "arrow")
        {
            // ��ȡ��ǰ����������
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // ��ת����һ������
            // ע�⣺��ǰ������������С���ܳ�����
            if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("�Ѿ������һ���������޷���ת��");
            }
        }
    }
}