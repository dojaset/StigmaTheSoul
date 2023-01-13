using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemData itemData;     //추가될 아이템의 ItemData
    public Image itemImage;       //이미지 표시 오브젝트
    public TextMeshProUGUI count; //아이템 잔량 표시

    void Start()
    {
        if (!itemData)
        {
            itemImage.enabled = false;
            count.enabled = false;
        }
    }

    void Update()
    {
        if (itemData)
        {
            itemImage.enabled = true;
            itemImage.sprite = itemData.itemImage;

            if (itemData.itemType != 1)
            {
                count.enabled = true;
                count.text = itemData.count.ToString();
            }
        }
    }

    public void SetItem(ItemData itemData) //슬롯에 아이템을 표시하는 함수
    {
        this.itemData = itemData; //이 ItemData에 추가될 ItemData를 대입

        itemImage.enabled = true;
        itemImage.sprite = itemData.itemImage;

        if (itemData.itemType != 1)
        {
            count.enabled = true;
            count.text = itemData.count.ToString();
        }
    }
}
