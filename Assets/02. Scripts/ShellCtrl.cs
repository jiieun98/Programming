// ------------------------------------
// ShellCtrl.cs
//
// 조개 생성과 화살표
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCtrl : MonoBehaviour
{
    public static ShellCtrl instance;

    // 화살표 숫자 임시 저장
    private int[] arrownum = new int[4];
    private int currnum;

    // 부딪힌 조개 오브젝트 (Inseon.cs에서 받아옴)
    public GameObject colobj;

    private void Start()
    {
        instance = this;
    }

    // 화살표 생성 함수
    // 조개는 여러 개가 생성되기 때문에 TakingClamCtrl에서 시작할 때
    // 화살표 생성한 것과 삼각함수 계산한 것을 불러오는 형식으로 코딩함
    public void MakeArrow()
    {
        currnum = 0;

        for (int i = 0; i < 4; ++i)
        {
            int randnum = Random.Range(0, 4);

            arrownum[i] = randnum + i * 4;

            TakingClamCtrl.instance.arrow[arrownum[i]].transform.position = 
                new Vector3(colobj.transform.position.x + TakingClamCtrl.instance.trifunc[i, 0],
                colobj.transform.position.y + TakingClamCtrl.instance.trifunc[i, 1], 0f);
            
            TakingClamCtrl.instance.arrow[arrownum[i]].SetActive(true);
        }
    }

    // 화살표 전체 제거 함수 (틀렸을 경우, E로 취소한 경우에 호출)
    public void RemoveArrowAll()
    {
        currnum = 0;

        for (int i = 0; i < 4; ++i)
            TakingClamCtrl.instance.arrow[arrownum[i]].SetActive(false);
    }

    // 화살표 하나씩 제거하는 함수
    public void RemoveArrow(int key)
    {
        // 눌러야 할 방향키
        int dir = arrownum[currnum] % 4;

        // 눌러야 할 방향키랑 입력한 방향키가 같으면 제거
        if (key == dir)
        {
            TakingClamCtrl.instance.arrow[arrownum[currnum]].SetActive(false);
            currnum++;
        }
        // 다르면 처음부터
        else
        {
            currnum = 0;
            RemoveArrowAll();
            MakeArrow();
        }

        // 다 맞추면 조개는 사라지고 헤엄 가능
        if(currnum == 4)
        {
            colobj.SetActive(false);
            Inseon.speed = 5f;
            Inseon.isTaking = false;
        }
    }
}
