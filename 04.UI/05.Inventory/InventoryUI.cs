using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject invenPanel; //�κ��丮 ǥ�� �г�
    public Scrollbar scrollbar;   //�κ��丮 ��ũ�ѹ�
    public TextMeshProUGUI money; //������ ǥ��

    public Transform slotRoot;    //������ ���� ������Ʈ(Slots)
    public UI_Slot[] slots;       //�κ��丮 ����

    public Button[] buttons;      //�κ��丮 ��
    public Sprite[] buttonImage;  //��ư ��Ȱ��, Ȱ�� �̹���

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //I��ư �Է� ��
        {
            if (!invenPanel.activeInHierarchy) { InvenPanelOn(); }
            else { invenPanel.SetActive(false); }

        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //ESC��ư �Է� ��
        {
            invenPanel.SetActive(false); //�κ��丮 �г� ��Ȱ��ȭ
        }
    }

    public void InvenPanelOn()
    {
        invenPanel.SetActive(true); //�κ��丮 �г� Ȱ��ȭ
        PotionInventory();          //�ʱ� ȭ��: �Һ��� ��
        UpdateMoney();              //������ ������Ʈ
    }

    public void PotionInventory()
    {
        ShutOffAllBtns();                         //��ư �̹��� �ϰ� ����(��Ȱ��)
        buttons[0].image.sprite = buttonImage[1]; //������ ��ư �̹��� ����(Ȱ��)

        UpdateInventory(Player_Data.Instance.potionInven); //�κ��丮 ������Ʈ
        scrollbar.value = 1; //��ũ�ѹ� �ʱ�ȭ
    }

    public void EquipInventory()
    {
        ShutOffAllBtns();                         //��ư �̹��� �ϰ� ����(��Ȱ��)
        buttons[1].image.sprite = buttonImage[1]; //������ ��ư �̹��� ����(Ȱ��)

        UpdateInventory(Player_Data.Instance.equipInven); //�κ��丮 ������Ʈ
        scrollbar.value = 1; //��ũ�ѹ� �ʱ�ȭ
    }

    public void ExtraInventory()
    {
        ShutOffAllBtns();                         //��ư �̹��� �ϰ� ����(��Ȱ��)
        buttons[2].image.sprite = buttonImage[1]; //������ ��ư �̹��� ����(Ȱ��)

        UpdateInventory(Player_Data.Instance.extraInven); //�κ��丮 ������Ʈ
        scrollbar.value = 1; //��ũ�ѹ� �ʱ�ȭ
    }

    public void CashInventory()
    {
        ShutOffAllBtns();                         //��ư �̹��� �ϰ� ����(��Ȱ��)
        buttons[3].image.sprite = buttonImage[1]; //������ ��ư �̹��� ����(Ȱ��)

        List<ItemData> cashInven = new();

        //�÷��̾��κ��� �ִ� itemData �� cash �÷��װ� true�� �����͸� �κ��丮�� �߰�
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
        UpdateInventory(cashInven); //�κ��丮 ������Ʈ
        scrollbar.value = 1; //��ũ�ѹ� �ʱ�ȭ
    }

    public void ShutOffAllBtns() //��ư �̹��� �ϰ� ����(��Ȱ��)
    {
        foreach (Button btn in buttons)
        {
            btn.image.sprite = buttonImage[0];
        }
    }

    public void UpdateInventory(List<ItemData> inventory) //�κ��丮 ������Ʈ
    {
        int slotCnt = slotRoot.childCount; //���� �� ���

        for (int i = 0; i < slotCnt; i++)
        {
            //���Ե鿡�� Item ������Ʈ �ҷ�����
            Item item = slotRoot.GetChild(i).GetComponentInChildren<Item>();

            if (i < inventory.Count) //�κ��丮 ����ŭ
            {
                item.SetItem(inventory[i]); //SetItem �Լ� ȣ��
            }
        }
    }

    public void UpdateMoney() //������ ������Ʈ
    {
        money.text = Player_Data.Instance.playerData.Money + " G";
    }

}
