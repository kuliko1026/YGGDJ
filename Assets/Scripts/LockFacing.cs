using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockFacing : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        // �����ʼ����
        initialScale = transform.localScale;
    }

    void Update()
    {
        // ȷ������ʼ�ձ��ֳ�ʼֵ
        transform.localScale = initialScale;
    }
}
