using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : Singletone<BuffManager>
{
    public GameObject buffPrefab; //���� ������

    public List<Buff> onBuff = new(); //�۵� ���� ����

    PlayerData playerData; //�÷��̾� ������

    void Start()
    {
        playerData = Player_Data.Instance.playerData;
    }

    public void CreateBuff(SkillData skillData) //����â�� ���� ����
    {
        GameObject buff = Instantiate(buffPrefab, transform); //���� ������ ����

        //������ �������� �������� ���� ��ų ���������� ����
        buff.GetComponentInChildren<Image>().sprite = skillData.SkillIcon;

        buff.GetComponent<Buff>().Execute(skillData); //�������� �Լ� �ҷ�����
    }

    public float AddBuff(float origin) //���� �ߵ��� ���� �ɷ�ġ ����
    {
        float temp = 0;

        //����Ʈ�� ������ ����
        if (onBuff.Count > 0)
        {
            //��������ŭ �ݺ��� ���� ���� ����
            for (int i = 0; i < onBuff.Count; i++)
            {
                temp = origin * onBuff[i].skillData.Percentage;
            }
        }
        //������ ������(���ٸ�) �⺻�� 
        return temp;
    }

    public void ChooseBuff(string skillName) //�Էµ� ���� ���� ���� �߰�
    {
        switch (skillName)
        {
            case "����ũ":
                playerData.Attack += (int)AddBuff(playerData.oAttack);
                playerData.Defence -= (int)AddBuff(playerData.oDefence);
                break;

            case "���̽�Ʈ":
                playerData.Speed += AddBuff(playerData.oSpeed);
                break;
        }
    }
    
    public void RemoveBuff(string skillName) //�Էµ� ���� ���� ���� ����
    {
        switch (skillName)
        {
            case "����ũ":
                playerData.Attack -= (int)AddBuff(playerData.oAttack);
                playerData.Defence += (int)AddBuff(playerData.oDefence);
                break;

            case "���̽�Ʈ":
                playerData.Speed -= AddBuff(playerData.oSpeed);
                break;
        }
    }
}