using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BigOrSmall_MiniMap_QuestQuick : MonoBehaviour, IPointerClickHandler//<-�ּ� ����
{
    //�̴ϸ��� ��ư�� ������ ���� �۾����� Ŀ���ٸ� �Ǵ� �� �� �ְ� �� �ִ� bool���� true�μ���
    bool MiniBtnTrueOrFalse = true;
    //����Ʈâ�� ��ư�� ������ ���� �۾����� Ŀ���ٸ� �Ǵ� �� �� �ְ� �� �ִ� bool���� true�μ���
    bool QuestQuickMenuMiniBtn = true;

    //���� bool�������� Count�������� �غ���
    //int i = 0;

    //�̴ϸ��� RectTransform�� �����´�.
    RectTransform MiniMap_rectTransform;
    //����Ʈâ�� RectTransform�� �����´�.
    RectTransform QuestQuickMenu_rectTransform;
    private void Start()
    {

        MiniMap_rectTransform = GetComponent<RectTransform>();

        //�� ��ũ��Ʈ�� �̴ϸʿ� �پ������� ����Ʈâ������ ���� ���� �±׷� ã�´�.
        QuestQuickMenu_rectTransform = GameObject.Find("QuestQuickMenu").gameObject.GetComponent<RectTransform>();
    }
    //UI�� Ŭ���������� �Ǻ����ִ� �Լ� (������� IPointerClickHandler�� ��������� ����� �����ϴ�.)
    public void OnPointerClick(PointerEventData eventData)
    {
        //���콺 ����Ű���ϸ� �÷��̾���ݰ� ��ø�̵Ǳ⶧���� �ӽ÷� �����ʹ�ư���� �س��Ҵ�.
        //���콺 �����ʹ�ư&&2��������
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            //���� ���� ���� FirstTown�̶��
            if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
            {
                //WorldMap�� ù��°(0����)�ڽ� ������Ʈ�� Ȱ��ȭ��Ų��.
                GameObject.Find("WorldMap").transform.GetChild(0).gameObject.SetActive(true);
                //WorldMap�� ù��° �ڽ��� FirstTown_WorldMap_ImageȰ��ȭ
            }
            //���� ���� ���� Forest�̶��
            else if (SceneManager.GetSceneByName("Forest").name == "Forest")
            {
                //�� ������Ʈ�� �ι�°(1��°) �ڽ��� Ȱ��ȭ ��Ų��.
                GameObject.Find("WorldMap").transform.GetChild(1).gameObject.SetActive(true);
                //WorldMap�� �ι�° �ڽ��� Forest_WorldMap_ImageȰ��ȭ

            }

        }
    }
    //MiniMap�ڽ��� MiniButton�� ��Ŭ������ ���� �Լ�
    public void MiniMap_MiniBtn()
    {
        //Bool ���� ���� �� ���� true / false ��Ű�� ���Ver
        //��ư�� ������
        //MiniBtnTrueOrFalse = true���
        if (MiniBtnTrueOrFalse == true)
        {
            //�̴ϸ��� Width�ǰ��� Height���� 50, 32�� �ٲٰ�
            MiniMap_rectTransform.sizeDelta = new Vector2(50, 32);
            //�̴ϸ��� ���� false�� �ٲ��
            MiniBtnTrueOrFalse = false;
        }
        //���� ������ ������ ���� �ʾҰ� MiniBtnTrueOrFalse = false���
        else if (MiniBtnTrueOrFalse == false)
        {
            //�̴ϸ��� Width�ǰ��� Height���� 330, 330���� �ٲٰ�
            MiniMap_rectTransform.sizeDelta = new Vector2(330, 330);
            //�̴ϸ��� ���� true�� �ٲ��
            MiniBtnTrueOrFalse = true;
        }

        //int ���� ���� �� ���� ������Ű�� ���Ver(Count����)
        /* if (i == 0)
         {
             rectTransform.sizeDelta = new Vector2(50, 32);
             i++;
         }
         else if (i == 1)
         {
             rectTransform.sizeDelta = new Vector2(330, 330);
             i = 0;
         }*/
    }
    //QuestQuickMini_Small_Button�� ��Ŭ���� ���� �Լ�
    public void QuestQuickMenu_MiniBtn()
    {
        //��ư�� ������
        //QuestQuickMenuMiniBtn = true���
        if (QuestQuickMenuMiniBtn == true)
        {
            //����Ʈâ�� Width�ǰ��� Height���� 50, 32�� �ٲٰ�
            QuestQuickMenu_rectTransform.sizeDelta = new Vector2(50, 32);
            //�̴ϸ��� ���� false�� �ٲ��
            QuestQuickMenuMiniBtn = false;
        }
        //���� ������ ������ ���� �ʾҰ� QuestQuickMenuMiniBtn = false���
        else if (QuestQuickMenuMiniBtn == false)
        {
            //����Ʈâ�� Width�ǰ��� Height����330,160���� �ٲٰ�
            QuestQuickMenu_rectTransform.sizeDelta = new Vector2(330, 160);
            //����Ʈâ�� ���� false�� �ٲ��
            QuestQuickMenuMiniBtn = true;
        }

    }
    private void Update()
    {
        #region ���� �����
        //���� ���� ���� FirstTown�̶��
        if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
        {



            //�߰�    �ּ� ��ǿ� �ø� ����
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(6).gameObject.SetActive(false);
            //







            //WorldMap�� ù��°(0����)�ڽ� ������Ʈ�� Ȱ��ȭ �Ǿ��ִٸ�(FirstTown_WorldMap_Image)
            if (GameObject.FindGameObjectWithTag("WorldMap").transform.GetChild(0).gameObject.activeSelf == true)
            {
                //�� ������Ʈ�� ù��°(0��°) �ڽ�(FirstTown_MiniMapCamera)�� ��Ȱ��ȭ ���Ѷ�
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            //���� if���� �ƴҰ��
            else
            {
                //�� ������Ʈ�� ù��°(0��°) �ڽ��� Ȱ��ȭ ���Ѷ�
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        #endregion

        #region �� �����
        //���� ���� ���� Forest�̶��
        if (SceneManager.GetSceneByName("Forest").name == "Forest")
        {





            //�߰�    �ּ� ��ǿ� �ø� ����
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            //







            //WorldMap�� �ι�°(1����)�ڽ� ������Ʈ(Forest_WorldMap_Image)�� Ȱ��ȭ �Ǿ��ִٸ�
            if (GameObject.FindGameObjectWithTag("WorldMap").transform.GetChild(1).gameObject.activeSelf == true)
            {
                //�� ������Ʈ�� �ټ���°(4��°) �ڽ�(Forest_MiniMapCamera)�� ��Ȱ��ȭ ���Ѷ�
                gameObject.transform.GetChild(4).gameObject.SetActive(false);
            }
            //���� if���� �ƴҰ��
            else
            {
                //�� ������Ʈ�� �ټ���°(4��°) �ڽ��� Ȱ��ȭ ���Ѷ�
                gameObject.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
        #endregion
    }
}