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
    private const float defaultSpeed = 5f;
    private Rigidbody2D rigid;
    private Vector3 movement;

    // 물질 중인지 판단하는 변수
    public static bool isTaking = false;
    // 조개와 부딪혔는지 판단하는 변수
    private bool isClamCollide = false;
    private int ClamSize;

    private float timer = 0f;

    public static bool inkAttacked = false;
    public static bool isTrapped = false;
    public static bool isEscaped = false;
    public Transform trap;
    public Transform Escape;


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

                // 화살표 생성
                TakingClamCtrl.instance.MakeArrow(ClamSize);
            }
            // 충돌 중이고 물질 중이면
            else if (isClamCollide && isTaking)
            {
                // 화살표 제거
                TakingClamCtrl.instance.RemoveArrowAll();

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

            if (presskey != -1) TakingClamCtrl.instance.RemoveArrow(presskey);
        }

        if (speed < defaultSpeed && inkAttacked) // 먹물에 맞아서 속도가 줄어들면
        {
            RecoverSpeed(5f);   // 5초 후 원래 속도로 회복
        }

        if (isTrapped) // 미역에 닿으면
        {
            GotoTarget(trap, 10); // 미역의 중간지점으로 이동
        }

        if (isEscaped) // 물질 끝나면 탈출
        {
            GotoTarget(Escape, 20);
        }

        if (transform.position == Escape.position)
        {
            isEscaped = false;
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
        if (collision.CompareTag("SmallClam"))
        {
            isClamCollide = true;
            TakingClamCtrl.instance.colobj = collision.gameObject;
            ClamSize = 4;
        }
        else if (collision.CompareTag("BigClam"))
        {
            isClamCollide = true;
            TakingClamCtrl.instance.colobj = collision.gameObject;
            ClamSize = 7;
        }
        else if (collision.CompareTag("Seaweed")) // 미역이랑 부딫히면
        {
            isClamCollide = true;
            TakingClamCtrl.instance.colobj = collision.gameObject;
            ClamSize = Random.Range(4, 6); // 인선이 손 콜라이더가 미역 콜라이더 밖에 있으면 화살표가 안뜸..
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isClamCollide = false;
    }

    void RecoverSpeed(float recoveryTime) // 속도 회복 함수
    {
        if (timer > recoveryTime)
        {
            speed = defaultSpeed;
            inkAttacked = false;
        }
        timer += Time.deltaTime;
    }

    public void GotoTarget(Transform Target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }

}
