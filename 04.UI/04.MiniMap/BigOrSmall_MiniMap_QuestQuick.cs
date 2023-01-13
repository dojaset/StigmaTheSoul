using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BigOrSmall_MiniMap_QuestQuick : MonoBehaviour, IPointerClickHandler//<-주석 해제
{
    //미니맵의 버튼을 누를때 마다 작아졌다 커졌다를 판단 할 수 있게 해 주는 bool변수 true로선언
    bool MiniBtnTrueOrFalse = true;
    //퀘스트창의 버튼을 누를때 마다 작아졌다 커졌다를 판단 할 수 있게 해 주는 bool변수 true로선언
    bool QuestQuickMenuMiniBtn = true;

    //위에 bool변수들을 Count버전으로 해본것
    //int i = 0;

    //미니맵의 RectTransform을 가져온다.
    RectTransform MiniMap_rectTransform;
    //퀘스트창의 RectTransform을 가져온다.
    RectTransform QuestQuickMenu_rectTransform;
    private void Start()
    {

        MiniMap_rectTransform = GetComponent<RectTransform>();

        //이 스크립트가 미니맵에 붙었있으니 퀘스트창에서도 쓰기 위해 태그로 찾는다.
        QuestQuickMenu_rectTransform = GameObject.Find("QuestQuickMenu").gameObject.GetComponent<RectTransform>();
    }
    //UI를 클릭했을때를 판별해주는 함수 (상속으로 IPointerClickHandler를 써줘야지만 사용이 가능하다.)
    public void OnPointerClick(PointerEventData eventData)
    {
        //마우스 왼쪽키로하면 플레이어공격과 중첩이되기때문에 임시로 오른쪽버튼으로 해놓았다.
        //마우스 오른쪽버튼&&2번누르면
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            //만약 지금 씬이 FirstTown이라면
            if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
            {
                //WorldMap의 첫번째(0번쨰)자식 오브젝트를 활성화시킨다.
                GameObject.Find("WorldMap").transform.GetChild(0).gameObject.SetActive(true);
                //WorldMap의 첫번째 자식인 FirstTown_WorldMap_Image활성화
            }
            //만약 지금 씬이 Forest이라면
            else if (SceneManager.GetSceneByName("Forest").name == "Forest")
            {
                //이 오브젝트의 두번째(1번째) 자식을 활성화 시킨다.
                GameObject.Find("WorldMap").transform.GetChild(1).gameObject.SetActive(true);
                //WorldMap의 두번째 자식인 Forest_WorldMap_Image활성화

            }

        }
    }
    //MiniMap자식의 MiniButton의 온클릭으로 넣을 함수
    public void MiniMap_MiniBtn()
    {
        //Bool 변수 선언 후 값을 true / false 시키는 방식Ver
        //버튼을 누르고
        //MiniBtnTrueOrFalse = true라면
        if (MiniBtnTrueOrFalse == true)
        {
            //미니맵의 Width의값과 Height값을 50, 32로 바꾸고
            MiniMap_rectTransform.sizeDelta = new Vector2(50, 32);
            //미니맵의 값을 false로 바꿔라
            MiniBtnTrueOrFalse = false;
        }
        //만약 위에가 성립이 되지 않았고 MiniBtnTrueOrFalse = false라면
        else if (MiniBtnTrueOrFalse == false)
        {
            //미니맵의 Width의값과 Height값을 330, 330으로 바꾸고
            MiniMap_rectTransform.sizeDelta = new Vector2(330, 330);
            //미니맵의 값을 true로 바꿔라
            MiniBtnTrueOrFalse = true;
        }

        //int 변수 선언 후 값을 증가시키는 방식Ver(Count버젼)
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
    //QuestQuickMini_Small_Button의 온클릭에 넣을 함수
    public void QuestQuickMenu_MiniBtn()
    {
        //버튼을 누르고
        //QuestQuickMenuMiniBtn = true라면
        if (QuestQuickMenuMiniBtn == true)
        {
            //퀘스트창의 Width의값과 Height값을 50, 32로 바꾸고
            QuestQuickMenu_rectTransform.sizeDelta = new Vector2(50, 32);
            //미니맵의 값을 false로 바꿔라
            QuestQuickMenuMiniBtn = false;
        }
        //만약 위에가 성립이 되지 않았고 QuestQuickMenuMiniBtn = false라면
        else if (QuestQuickMenuMiniBtn == false)
        {
            //퀘스트창의 Width의값과 Height값을330,160으로 바꾸고
            QuestQuickMenu_rectTransform.sizeDelta = new Vector2(330, 160);
            //퀘스트창의 값을 false로 바꿔라
            QuestQuickMenuMiniBtn = true;
        }

    }
    private void Update()
    {
        #region 마을 월드맵
        //만약 지금 씬이 FirstTown이라면
        if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
        {



            //추가    주석 노션에 올릴 예정
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(6).gameObject.SetActive(false);
            //







            //WorldMap의 첫번째(0번쨰)자식 오브젝트가 활성화 되어있다면(FirstTown_WorldMap_Image)
            if (GameObject.FindGameObjectWithTag("WorldMap").transform.GetChild(0).gameObject.activeSelf == true)
            {
                //이 오브젝트의 첫번째(0번째) 자식(FirstTown_MiniMapCamera)을 비활성화 시켜라
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            //위에 if문이 아닐경우
            else
            {
                //이 오브젝트의 첫번째(0번째) 자식을 활성화 시켜라
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        #endregion

        #region 숲 월드맵
        //만약 지금 씬이 Forest이라면
        if (SceneManager.GetSceneByName("Forest").name == "Forest")
        {





            //추가    주석 노션에 올릴 예정
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            //







            //WorldMap의 두번째(1번쨰)자식 오브젝트(Forest_WorldMap_Image)가 활성화 되어있다면
            if (GameObject.FindGameObjectWithTag("WorldMap").transform.GetChild(1).gameObject.activeSelf == true)
            {
                //이 오브젝트의 다섯번째(4번째) 자식(Forest_MiniMapCamera)을 비활성화 시켜라
                gameObject.transform.GetChild(4).gameObject.SetActive(false);
            }
            //위에 if문이 아닐경우
            else
            {
                //이 오브젝트의 다섯번째(4번째) 자식을 활성화 시켜라
                gameObject.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
        #endregion
    }
}