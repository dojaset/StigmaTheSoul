using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : Singletone<UIManager>
{
    Canvas[] UI; //UI 오브젝트들의 캔버스 컴포넌트를 담을 배열

    void Start()
    {
        UI = GetComponentsInChildren<Canvas>();
    }

    void Update()
    {
        switch (SceneLoadManager.Instance.CurrentScene) //현재 씬에 따라서
        {
            default:
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = true ; //기본적으로는 UI가 보이는 상태
                }
                break;

            case SceneLoadManager.SceneStart.LOADING: //로딩중이라면
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = false; //UI를 보이지 않게 한다.
                }             
                break;
        }
    }
}