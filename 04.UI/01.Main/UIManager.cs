using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : Singletone<UIManager>
{
    Canvas[] UI; //UI ������Ʈ���� ĵ���� ������Ʈ�� ���� �迭

    void Start()
    {
        UI = GetComponentsInChildren<Canvas>();
    }

    void Update()
    {
        switch (SceneLoadManager.Instance.CurrentScene) //���� ���� ����
        {
            default:
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = true ; //�⺻�����δ� UI�� ���̴� ����
                }
                break;

            case SceneLoadManager.SceneStart.LOADING: //�ε����̶��
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = false; //UI�� ������ �ʰ� �Ѵ�.
                }             
                break;
        }
    }
}