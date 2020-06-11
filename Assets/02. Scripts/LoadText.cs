// ------------------------------------
// LoadText.cs
//
// 대사 xml 파일 불러옴
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LoadText : MonoBehaviour
{
    public Dictionary<int, LoadText> dicLoadText;
    public int id;
    public string text;
    public Text uiText, NextBtnTxt;

    private int chidx = 1, idx;

    private void Start()
    {
        this.dicLoadText = new Dictionary<int, LoadText>();
        this.idx = chidx * 100 + 1;

        var obj = Resources.Load("dialogue") as TextAsset;

        var jlist = JsonConvert.DeserializeObject<List<LoadText>>(obj.text);

        foreach (var data in jlist)
            this.dicLoadText.Add(data.id, data);

        ShowText();
    }

    void ShowText()
    {
        uiText.text = this.dicLoadText[idx].text;

        if (dicLoadText.Count == idx - chidx * 100)
            NextBtnTxt.text = "확인";

        //print(dicLoadText[idx].id + " " + dicLoadText[idx].text);
    }

    // prev btn
    public void OnClickPrevBtn()
    {
        this.idx--;
        ShowText();
    }

    // next btn
    public void OnClickNextBtn()
    {
        if (dicLoadText.Count == idx - chidx * 100)
            gameObject.SetActive(false);
        else
        {
            this.idx++;
            ShowText();
        }
    }

    // exit btn
    public void OnClickExitBtn()
    {
        idx = chidx * 100 + 1;
        gameObject.SetActive(false);
    }
}
