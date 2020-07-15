using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] Transform target; // 이동할 위치

    bool isContact = false; // 탐지 범위에 들어왔는지 안들어왔는지
    public bool isBig = false; // 오브젝트가 커졌는지 아닌지 (눈뜨는 애니메이션 대신 구현한 것)

    float Fadetime = 0;
    float Bigtime = 0;
    float fades = 1f;

    bool InkBig = false; // 먹물이 뿜어나왔는지 아닌지
    [SerializeField] Transform ink;

   
    // Update is called once per frame
    void Update()
    {
        if (isContact)
        {
            Bigger();
            if (isBig) // 커지면 
            {
                InkBig = ink.GetComponent<Ink>().InkBig; // 먹물 뿜고

                if (InkBig)
                {
                    MoveNFadeOut(); // 도망
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inseon"))
        {
            isContact = true;
            Inseon.speed /= 2f; // 속도 반으로 감소
            Inseon.inkAttacked = true;
        }
    }


    void MoveNFadeOut()  // 움직이면서 페이드 아웃
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Fadetime += Time.deltaTime;

        if (fades > 0.0f && Fadetime >= 0.1f)
        {
            fades -= 0.1f;
            transform.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, fades);
            Fadetime = 0;
        }
        else if (fades <= 0.0f)
        {
            Fadetime = 0;

        }
    }

    void Bigger()  // 눈 뜨는 애니메이션으로 변경 예정
    {
        Bigtime += Time.deltaTime;

        if (transform.localScale.x < 1.5 && Bigtime >= 0.1f)
        {
            transform.localScale += new Vector3(0.3f, 0.3f, 0);
        }
        else if (transform.localScale.x >= 1.5f)
        {
            isBig = true;
        }
    }

}
