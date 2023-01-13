using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemData itemData;     //�߰��� �������� ItemData
    public Image itemImage;       //�̹��� ǥ�� ������Ʈ
    public TextMeshProUGUI count; //������ �ܷ� ǥ��

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

    public void SetItem(ItemData itemData) //���Կ� �������� ǥ���ϴ� �Լ�
    {
        this.itemData = itemData; //�� ItemData�� �߰��� ItemData�� ����

        itemImage.enabled = true;
        itemImage.sprite = itemData.itemImage;

        if (itemData.itemType != 1)
        {
            count.enabled = true;
            count.text = itemData.count.ToString();
        }
    }
}
