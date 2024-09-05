using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // ��ҽ�ɫ��Transform
    public float offsetX = 1f; // ����ҽ�ɫ��ˮƽƫ����
    public float offsetY = 1f; // ����ҽ�ɫ�Ĵ�ֱƫ����
    private Vector3 initialScale;

    void Start()
    {
        // �����ʼ����
        initialScale = transform.localScale;
    }


    void Update()
    {
        // ����"fkey"�����λ�ã�ʹ�䱣������ҽ�ɫ���Ҳ�
        Vector3 newPosition = player.position;
        newPosition.x += offsetX;
        newPosition.y += offsetY;
        transform.position = newPosition;

        // ȷ������ʼ�ձ��ֳ�ʼֵ
        transform.localScale = initialScale;
    }
}

