using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockFacing : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        // 保存初始缩放
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 确保缩放始终保持初始值
        transform.localScale = initialScale;
    }
}
