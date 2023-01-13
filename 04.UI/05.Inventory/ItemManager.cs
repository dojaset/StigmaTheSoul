using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    PlayerData playerData; //�÷��̾� ������

    Transform slotRoot; //���Ե��� ���� ������Ʈ
    UI_Slot[] slots;    //�����۹� ����

    void Start()
    {
        playerData = Player_Data.Instance.playerData; //�÷��̾� ������

        slotRoot = GameObject.Find("ItemBarSlots").transform; //ItemBarSlots�� �̸����� ã��
        slots = slotRoot.GetComponentsInChildren<UI_Slot>();  //slotRoot�� �ڽĵ鿡�� UI_Slot ������Ʈ�� �ҷ�����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //���� 1 Ű�� ������
        {
            Item item = slots[0].GetComponentInChildren<Item>(); //ù��° ������ �ڽĿ��� ������ ������Ʈ �ҷ�����
            UseItem(item);                                       //������ ��� �Լ� ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //���� 2 Ű�� ������
        {
            Item item = slots[1].GetComponentInChildren<Item>(); //�ι�° ������ �ڽĿ��� ������ ������Ʈ �ҷ�����
            UseItem(item);                                       //������ ��� �Լ� ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //���� 3 Ű�� ������
        {
            Item item = slots[2].GetComponentInChildren<Item>(); //����° ������ �ڽĿ��� ������ ������Ʈ �ҷ�����
            UseItem(item);                                       //������ ��� �Լ� ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //���� 4 Ű�� ������
        {
            Item item = slots[3].GetComponentInChildren<Item>(); //�׹�° ������ �ڽĿ��� ������ ������Ʈ �ҷ�����
            UseItem(item);                                       //������ ��� �Լ� ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) //���� 5 Ű�� ������
        {
            Item item = slots[4].GetComponentInChildren<Item>(); //�ټ���° ������ �ڽĿ��� ������ ������Ʈ �ҷ�����
            UseItem(item);                                       //������ ��� �Լ� ����
        }
    }

    void UseItem(Item item) //������ ��� �Լ�
    {
        if (!item) { return; } //������ ����ִٸ� �������� ����

        ItemData itemData = item.itemData;

        if (itemData.itemType == 0) //������ �������� �Һ� �������̶��
        {
            if (itemData.count > 0) itemData.count -= 1; //������ �ܷ��� �����ִٸ� 1�� ����
            item.count.text = itemData.count.ToString(); //�ܷ� �ؽ�Ʈ�� �ݿ�

            int newHP = playerData.Hp + itemData.Hp;                     //������ ���밪 = ���� ü�� + ������ ȸ����
            if (newHP >= playerData.MaxHp) { newHP = playerData.MaxHp; } //���밪�� �ִ� ü�º��� ���ٸ� ���밪�� �ִ� ü������ ����
            playerData.Hp = newHP;                                       //���� ü�¿� ���밪 ����

            int newMP = playerData.Mp + itemData.Mp;                     //������ ���밪 = ���� ���� + ������ ȸ����
            if (newMP >= playerData.MaxMp) { newMP = playerData.MaxMp; } //���밪�� �ִ� �������� ���ٸ� ���밪�� �ִ� ������ ����
            playerData.Mp = newMP;                                       //���� ������ ���밪 ����

            Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[2];
            Player_Data.Instance.audioSource.Play();
        }
    }
}
