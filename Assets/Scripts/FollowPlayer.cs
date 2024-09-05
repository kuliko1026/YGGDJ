using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 玩家角色的Transform
    public float offsetX = 1f; // 与玩家角色的水平偏移量
    public float offsetY = 1f; // 与玩家角色的垂直偏移量
    private Vector3 initialScale;

    void Start()
    {
        // 保存初始缩放
        initialScale = transform.localScale;
    }


    void Update()
    {
        // 更新"fkey"对象的位置，使其保持在玩家角色的右侧
        Vector3 newPosition = player.position;
        newPosition.x += offsetX;
        newPosition.y += offsetY;
        transform.position = newPosition;

        // 确保缩放始终保持初始值
        transform.localScale = initialScale;
    }
}

