using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject invenPanel; //인벤토리 표시 패널
    public Scrollbar scrollbar;   //인벤토리 스크롤바
    public TextMeshProUGUI money; //소지금 표시

    public Transform slotRoot;    //슬롯을 담은 오브젝트(Slots)
    public UI_Slot[] slots;       //인벤토리 슬롯

    public Button[] buttons;      //인벤토리 탭
    public Sprite[] buttonImage;  //버튼 비활성, 활성 이미지

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //I버튼 입력 시
        {
            if (!invenPanel.activeInHierarchy) { InvenPanelOn(); }
            else { invenPanel.SetActive(false); }

        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //ESC버튼 입력 시
        {
            invenPanel.SetActive(false); //인벤토리 패널 비활성화
        }
    }

    public void InvenPanelOn()
    {
        invenPanel.SetActive(true); //인벤토리 패널 활성화
        PotionInventory();          //초기 화면: 소비재 탭
        UpdateMoney();              //소지금 업데이트
    }

    public void PotionInventory()
    {
        ShutOffAllBtns();                         //버튼 이미지 일괄 변경(비활성)
        buttons[0].image.sprite = buttonImage[1]; //선택한 버튼 이미지 변경(활성)

        UpdateInventory(Player_Data.Instance.potionInven); //인벤토리 업데이트
        scrollbar.value = 1; //스크롤바 초기화
    }

    public void EquipInventory()
    {
        ShutOffAllBtns();                         //버튼 이미지 일괄 변경(비활성)
        buttons[1].image.sprite = buttonImage[1]; //선택한 버튼 이미지 변경(활성)

        UpdateInventory(Player_Data.Instance.equipInven); //인벤토리 업데이트
        scrollbar.value = 1; //스크롤바 초기화
    }

    public void ExtraInventory()
    {
        ShutOffAllBtns();                         //버튼 이미지 일괄 변경(비활성)
        buttons[2].image.sprite = buttonImage[1]; //선택한 버튼 이미지 변경(활성)

        UpdateInventory(Player_Data.Instance.extraInven); //인벤토리 업데이트
        scrollbar.value = 1; //스크롤바 초기화
    }

    public void CashInventory()
    {
        ShutOffAllBtns();                         //버튼 이미지 일괄 변경(비활성)
        buttons[3].image.sprite = buttonImage[1]; //선택한 버튼 이미지 변경(활성)

        List<ItemData> cashInven = new();

        //플레이어인벤에 있는 itemData 중 cash 플래그가 true인 데이터만 인벤토리에 추가
        foreach (ItemData itemData in Player_Data.Instance.potionInven)
        {
            if (itemData.cash) { cashInven.Add(itemData); }
        }

        foreach (ItemData itemData in Player_Data.Instance.equipInven)
        {
            if (itemData.cash) { cashInven.Add(itemData); }
        }

        foreach (ItemData itemData in Player_Data.Instance.equipInven)
        {
            if (itemData.cash) { cashInven.Add(itemData); }
        }

        cashInven.TrimExcess();
        UpdateInventory(cashInven); //인벤토리 업데이트
        scrollbar.value = 1; //스크롤바 초기화
    }

    public void ShutOffAllBtns() //버튼 이미지 일괄 변경(비활성)
    {
        foreach (Button btn in buttons)
        {
            btn.image.sprite = buttonImage[0];
        }
    }

    public void UpdateInventory(List<ItemData> inventory) //인벤토리 업데이트
    {
        int slotCnt = slotRoot.childCount; //슬롯 수 계산

        for (int i = 0; i < slotCnt; i++)
        {
            //슬롯들에서 Item 컴포넌트 불러오기
            Item item = slotRoot.GetChild(i).GetComponentInChildren<Item>();

            if (i < inventory.Count) //인벤토리 수만큼
            {
                item.SetItem(inventory[i]); //SetItem 함수 호출
            }
        }
    }

    public void UpdateMoney() //소지금 업데이트
    {
        money.text = Player_Data.Instance.playerData.Money + " G";
    }

}
