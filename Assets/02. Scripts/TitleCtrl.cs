// ------------------------------------
// TitleCtrl.cs
//
// Title 화면 UI Ctrl
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleCtrl : MonoBehaviour
{
    [SerializeField]
    private Image TitleImage, LobbyImage, FadeImage;
    [SerializeField]
    private GameObject TitleMenu, SettingMenu, LobbyBtns, ProgressBtn;

    // 0 : Title, 1 : Lobby, 2 : Story...
    private int SceneState;

    void Start()
    {
        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, false);

        SoundManager.instance.SetBGM("Title");
        SoundManager.instance.CreateEffect();

        SceneState = 0;
        StartCoroutine(FadeIn(2f, 0.5f));
    }


    ///////////////////// Btn /////////////////////

    // 버튼을 눌렀을 때
    public void OnClickButton()
    {
        SoundManager.instance.PlayEffect("Click");
    }

    // 환경설정 버튼 눌렀을 때
    public void OnClickSettingBtn()
    {
        OnClickButton();
        TitleMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }

    // 환경설정 닫기 버튼 눌렀을 때
    public void OnClickSettingXBnt()
    {
        OnClickButton();
        SettingMenu.SetActive(false);
        if(SceneState == 0) TitleMenu.SetActive(true);
    }

    // 게임 종료 버튼 눌렀을 때
    public void OnClickExitBtn()
    {
        OnClickButton();
        OnApplicationQuit();
    }


    ///////////////////// Title Btn /////////////////////

    // 기존 플레이 버튼 눌렀을 때
    public void OnClickProgressBtn()
    {
        OnClickButton();
        TitleMenu.SetActive(false);
        SceneState = 1;
        StartCoroutine(FadeIn(0f, 1f));
    }

    // 새 플레이 버튼 눌렀을 때
    public void OnClickNewBtn()
    {
        OnClickButton();
        //
    }


    ///////////////////// Lobby Btn /////////////////////




    ///////////////////// Fade /////////////////////

    // FadeIn 함수
    IEnumerator FadeIn(float FadeTime, float FadeAlpha)
    {
        Color FadeColor = FadeImage.color;
        float time = 0f;
        FadeColor.a = 0;

        yield return new WaitForSeconds(FadeTime);

        FadeImage.gameObject.SetActive(true);

        while (FadeColor.a < FadeAlpha)
        {
            time += Time.deltaTime * 0.5f;
            FadeColor.a = Mathf.Lerp(0, 1, time);
            FadeImage.color = FadeColor;

            yield return null;
        }

        if (SceneState == 0) // 타이틀 화면이면
        {
            // Title 메뉴 판넬 활성화
            TitleMenu.SetActive(true);
            // Button 선택 Focus 설정
            EventSystem.current.SetSelectedGameObject(ProgressBtn);
        }
        else if (SceneState == 1) // 로비 화면이면
        {
            // Lobby 버튼 활성화
            LobbyBtns.SetActive(true);

            LobbyImage.gameObject.SetActive(true);
            TitleImage.gameObject.SetActive(false);
            StartCoroutine(FadeOut(1f));
        }
    }

    // FadeOut 함수
    IEnumerator FadeOut(float FadeTime)
    {
        Color FadeColor = FadeImage.color;
        float time = 0f;
        FadeColor.a = 1;

        yield return new WaitForSeconds(FadeTime);

        while (FadeColor.a > 0f)
        {
            time += Time.deltaTime * 0.3f;
            FadeColor.a = Mathf.Lerp(1, 0, time);
            FadeImage.color = FadeColor;

            yield return null;
        }
    }


    // 종료 버튼 눌렀을 시
    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
