// ------------------------------------
// Inseon.cs
//
// Inseon의 전반적인 활동
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inseon : MonoBehaviour
{
    public static float speed = 5f;
    private Rigidbody2D rigid;
    private Vector3 movement;

    // 물질 중인지 판단하는 변수
    public static bool isTaking = false;
    // 조개와 부딪혔는지 판단하는 변수
    private bool isClamCollide = false;

    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // E키를 누르고
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 충돌 중이고 물질 중이 아니면
            if (isClamCollide && !isTaking)
            {
                // 물질 시작
                isTaking = true;
                speed = 0f;

                ShellCtrl.instance.MakeArrow();
            }
            // 충돌 중이고 물질 중이면
            else if (isClamCollide && isTaking)
            {
                ShellCtrl.instance.RemoveArrowAll();

                speed = 5f;
                isTaking = false;
            }
        }

        // 충돌 중이고 물질 중이면
        if (isClamCollide && isTaking)
        {
            int presskey = -1;

            // 0우, 1좌, 2상, 3하
            if (Input.GetKeyDown(KeyCode.RightArrow)) presskey = 0;
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) presskey = 1;
            else if (Input.GetKeyDown(KeyCode.UpArrow)) presskey = 2;
            else if (Input.GetKeyDown(KeyCode.DownArrow)) presskey = 3;

            if(presskey != -1) ShellCtrl.instance.RemoveArrow(presskey);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Swim(h, v);
    }

    void Swim(float h, float v)
    {
        movement.Set(h, v, 0);
        movement = movement.normalized * speed * Time.deltaTime;
        rigid.MovePosition(transform.position + movement);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 조개랑 부딪히면
        if (collision.gameObject.tag == "Shellfish")
        {
            isClamCollide = true;
            ShellCtrl.instance.colobj = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isClamCollide = false;
    }
}
