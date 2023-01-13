using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : Singletone<BuffManager>
{
    public GameObject buffPrefab; //버프 프리팹

    public List<Buff> onBuff = new(); //작동 중인 버프

    PlayerData playerData; //플레이어 데이터

    void Start()
    {
        playerData = Player_Data.Instance.playerData;
    }

    public void CreateBuff(SkillData skillData) //버프창에 버프 생성
    {
        GameObject buff = Instantiate(buffPrefab, transform); //버프 프리팹 생성

        //생성된 프리팹의 아이콘을 현재 스킬 아이콘으로 변경
        buff.GetComponentInChildren<Image>().sprite = skillData.SkillIcon;

        buff.GetComponent<Buff>().Execute(skillData); //버프실행 함수 불러오기
    }

    public float AddBuff(float origin) //버프 발동을 통한 능력치 변경
    {
        float temp = 0;

        //리스트에 버프가 담기면
        if (onBuff.Count > 0)
        {
            //버프수만큼 반복을 통해 버프 누적
            for (int i = 0; i < onBuff.Count; i++)
            {
                temp = origin * onBuff[i].skillData.Percentage;
            }
        }
        //버프가 끝나면(없다면) 기본값 
        return temp;
    }

    public void ChooseBuff(string skillName) //입력된 값에 따른 버프 추가
    {
        switch (skillName)
        {
            case "버서크":
                playerData.Attack += (int)AddBuff(playerData.oAttack);
                playerData.Defence -= (int)AddBuff(playerData.oDefence);
                break;

            case "헤이스트":
                playerData.Speed += AddBuff(playerData.oSpeed);
                break;
        }
    }
    
    public void RemoveBuff(string skillName) //입력된 값에 따른 버프 삭제
    {
        switch (skillName)
        {
            case "버서크":
                playerData.Attack -= (int)AddBuff(playerData.oAttack);
                playerData.Defence += (int)AddBuff(playerData.oDefence);
                break;

            case "헤이스트":
                playerData.Speed -= AddBuff(playerData.oSpeed);
                break;
        }
    }
}