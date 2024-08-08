using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController2D : MonoBehaviour
{
    public float speed = 1f;
    private float h, v;
    private Animator ani;
    private Rigidbody2D rigid;
    private Vector3 movement; // 移动向量
    private bool facingRight = true; // 是否面向右边

    void Start()
    {
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
            h *= 2f;

        // 判断翻面
        if (h > 0 && !facingRight)
            SetFacing(true);
        else if (h < 0 && facingRight)
            SetFacing(false);

        ani.SetFloat("Speed", h);
    }
    void SetFacing(bool fr)
    {
        facingRight = fr;
        Vector3 ac = transform.localScale;
        if ((fr && ac.x < 0) || (!fr && ac.x > 0)) // 需要翻面
            ac.x *= -1;
        transform.localScale = ac;
    }

    private void FixedUpdate()
    {
        movement = Vector3.zero;
        movement.x = h * speed;
        movement.y = rigid.velocity.y;
        rigid.velocity = movement;
    }
}
