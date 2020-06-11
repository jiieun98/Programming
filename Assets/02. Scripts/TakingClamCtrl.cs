// ------------------------------------
// TakingClamCtrl.cs
//
// 물질 시스템 Ctrl
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingClamCtrl : MonoBehaviour
{
    public static TakingClamCtrl instance;

    // 조개 (임시로 5개)
    public GameObject[] Shell = new GameObject[5];

    // 화살표 4*4개씩
    public GameObject[] arrow = new GameObject[16];

    // 삼각함수 (Mathf는 과부하가 있어서 Start 함수에서 계산하고 시작)
    public float[,] trifunc = new float[4, 2];

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
}
