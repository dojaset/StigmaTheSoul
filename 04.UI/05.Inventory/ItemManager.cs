using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    PlayerData playerData; //플레이어 데이터

    Transform slotRoot; //슬롯들을 담은 오브젝트
    UI_Slot[] slots;    //아이템바 슬롯

    void Start()
    {
        playerData = Player_Data.Instance.playerData; //플레이어 데이터

        slotRoot = GameObject.Find("ItemBarSlots").transform; //ItemBarSlots를 이름으로 찾기
        slots = slotRoot.GetComponentsInChildren<UI_Slot>();  //slotRoot의 자식들에서 UI_Slot 컴포넌트들 불러오기
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //숫자 1 키를 누르면
        {
            Item item = slots[0].GetComponentInChildren<Item>(); //첫번째 슬롯의 자식에서 아이템 컴포넌트 불러오기
            UseItem(item);                                       //아이템 사용 함수 실행
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //숫자 2 키를 누르면
        {
            Item item = slots[1].GetComponentInChildren<Item>(); //두번째 슬롯의 자식에서 아이템 컴포넌트 불러오기
            UseItem(item);                                       //아이템 사용 함수 실행
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //숫자 3 키를 누르면
        {
            Item item = slots[2].GetComponentInChildren<Item>(); //세번째 슬롯의 자식에서 아이템 컴포넌트 불러오기
            UseItem(item);                                       //아이템 사용 함수 실행
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //숫자 4 키를 누르면
        {
            Item item = slots[3].GetComponentInChildren<Item>(); //네번째 슬롯의 자식에서 아이템 컴포넌트 불러오기
            UseItem(item);                                       //아이템 사용 함수 실행
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) //숫자 5 키를 누르면
        {
            Item item = slots[4].GetComponentInChildren<Item>(); //다섯번째 슬롯의 자식에서 아이템 컴포넌트 불러오기
            UseItem(item);                                       //아이템 사용 함수 실행
        }
    }

    void UseItem(Item item) //아이템 사용 함수
    {
        if (!item) { return; } //슬롯이 비어있다면 실행하지 않음

        ItemData itemData = item.itemData;

        if (itemData.itemType == 0) //슬롯의 아이템이 소비 아이템이라면
        {
            if (itemData.count > 0) itemData.count -= 1; //아이템 잔량이 남아있다면 1개 차감
            item.count.text = itemData.count.ToString(); //잔량 텍스트에 반영

            int newHP = playerData.Hp + itemData.Hp;                     //아이템 적용값 = 현재 체력 + 아이템 회복력
            if (newHP >= playerData.MaxHp) { newHP = playerData.MaxHp; } //적용값이 최대 체력보다 높다면 적용값을 최대 체력으로 변경
            playerData.Hp = newHP;                                       //현재 체력에 적용값 대입

            int newMP = playerData.Mp + itemData.Mp;                     //아이템 적용값 = 현재 마나 + 아이템 회복력
            if (newMP >= playerData.MaxMp) { newMP = playerData.MaxMp; } //적용값이 최대 마나보다 높다면 적용값을 최대 마나로 변경
            playerData.Mp = newMP;                                       //현재 마나에 적용값 대입

            Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[2];
            Player_Data.Instance.audioSource.Play();
        }
    }
}
