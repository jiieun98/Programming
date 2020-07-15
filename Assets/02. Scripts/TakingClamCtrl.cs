// ------------------------------------
// TakingClamCtrl.cs
//
// 물질 시스템 컨트롤러
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingClamCtrl : MonoBehaviour
{
    public static TakingClamCtrl instance;

    // 조개 (임시로 5개)
    public GameObject[] Clam = new GameObject[5];
    // 화살표 4*4개씩
    public GameObject[] arrow = new GameObject[16];

    // 삼각함수 (Mathf는 과부하가 있어서 Start 함수에서 계산하고 시작)
    public float[,] trifunc = new float[4, 2];

    private int[] arrownum = new int[7];
    private int currnum;
    private Vector3[] arrowpos = new Vector3[4];
    private int ClamSize;

    // 부딪힌 조개 오브젝트 (Inseon.cs에서 받아옴)
    public GameObject colobj;

    void Start()
    {
        instance = this;

        int rad = 450;
        for (int i = 0; i < 4; ++i)
        {
            trifunc[i, 0] = Mathf.Cos(rad * Mathf.Deg2Rad);
            trifunc[i, 1] = Mathf.Sin(rad * Mathf.Deg2Rad);

            rad -= 60;
        }

        // 객체 생성
        //for (int i = 0; i < 5; ++i)
        //{
        //    // 랜덤 좌표
        //    float RandomX = Random.Range(-7f, 28f);
        //    float RandomY = Random.Range(-3f, 5f);
        //
        //    int randnum = Random.Range(0, 4);
        //
        //    // ********* 바꿀 의향 있음 *********
        //    GameObject tmpShell = (GameObject)Instantiate(Shell[randnum], 
        //       new Vector3(RandomX, RandomY, 0), Quaternion.identity);
        //}
    }

    // 화살표 생성 함수
    public void MakeArrow(int size)
    {
        currnum = 0;
        ClamSize = size;

        // randnum은 화살표 4방향 중 하나를 선택
        // arrownum은 활성화시킬 화살표 번호를 넣을 배열
        for (int i = 0; i < ClamSize; ++i)
        {
            int randnum = Random.Range(0, 4);
            arrownum[i] = (randnum + i * 4) % 16;
        }

        // arrowpos라는 화살표가 나타나는 위치를 아예 저장시킴
        // 초기에는 4개만 SetActive(true)
        for (int i = 0; i < 4; ++i)
        {
            arrowpos[i] = new Vector3(colobj.transform.position.x + trifunc[i, 0],
                colobj.transform.position.y + trifunc[i, 1], 0f);

            arrow[arrownum[i]].transform.position =
                new Vector3(arrowpos[i].x, arrowpos[i].y, 0f);

            arrow[arrownum[i]].SetActive(true);
        }
    }

    // 화살표 전체 제거 함수 (틀렸을 경우, E로 취소한 경우에 호출)
    public void RemoveArrowAll()
    {
        currnum = 0;

        for (int i = 0; i < ClamSize; ++i)
            arrow[arrownum[i]].SetActive(false);
    }

    // 화살표 하나씩 제거하는 함수
    public void RemoveArrow(int key)
    {
        // 눌러야 할 방향키
        int dir = arrownum[currnum] % 4;

        // 눌러야 할 방향키랑 입력한 방향키가 같으면 제거
        if (key == dir)
        {
            arrow[arrownum[currnum]].SetActive(false);

            currnum++;

            // tmpnum은 SetActive(true)된 화살표 개수
            int tmpnum = 0;
            for (int i = currnum; i < ClamSize; ++i)
            {
                // 4번째 화살표
                // 화살표는 최대 4개만 띄울거니까 break
                if (tmpnum == 3)
                {
                    arrow[arrownum[i]].SetActive(true);

                    arrow[arrownum[i]].transform.position =
                        new Vector3(arrowpos[tmpnum].x, arrowpos[tmpnum].y, 0f);

                    break;
                }

                // 화살표 위로 당겨옴
                arrow[arrownum[i]].transform.position =
                    new Vector3(arrowpos[tmpnum].x, arrowpos[tmpnum].y, 0f);
                tmpnum++;
            }
        }
        // 다르면 처음부터
        else
        {
            currnum = 0;
            RemoveArrowAll();
            MakeArrow(ClamSize);
        }

        // 다 맞추면 조개는 사라지고 헤엄 가능
        if (currnum == ClamSize && (colobj.CompareTag("SmallClam") || colobj.CompareTag("BigClam")))
        {
            colobj.SetActive(false);
            Inseon.speed = 5f;
            Inseon.isTaking = false;
        }
        else if (currnum == ClamSize && colobj.CompareTag("Seaweed")) // 미역은 사라지지 않고 탈출됨
        {
            Inseon.speed = 5f;
            Inseon.isTaking = false;
            Inseon.isEscaped = true;
        }
    }
}