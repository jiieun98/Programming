using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    float InkFadeOuttime = 0f;
    float InkFadeIntime = 0f;
    float Inkfades = 1f;
    float InkBigtime = 0;
    public bool InkBig = false;

    public Transform Object;
    public bool ObjBig = false;


    // Update is called once per frame
    void Update()
    {
        if (Object.GetComponent<Octopus>().isBig) // 오브젝트가 커지면 
        {
            //Invoke("InkFadeInFadeOut", 0.2f);
            StartCoroutine(InkFadeInFadeOut(1.5f)); //먹물 뿜음
        }
    }

    IEnumerator InkFadeInFadeOut(float time)
    {
        yield return new WaitForSeconds(0.5f);
        InkFadeOuttime += Time.deltaTime;

        if (InkFadeOuttime < time && !InkBig)
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, InkFadeOuttime / time);
        }
        else
        {
            InkFadeOuttime = 0;
            InkBig = true;

            if (Inkfades > 0.0f && InkFadeIntime >= 0.1f)
            {
                Inkfades -= 0.2f;
                transform.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, Inkfades);
                InkFadeIntime = 0;
            }
            else if (Inkfades <= 0.0f)
            {
                InkFadeIntime = 0;

            }
            InkFadeIntime += Time.deltaTime;
        }

        /////////////////////////////////먹물 커짐 (나중에 애니메이션으로 변경 예정)
        InkBigtime += Time.deltaTime;

        if (transform.localScale.x < 0.8f && InkBigtime >= 0.1f)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0);

        }
    }
}
