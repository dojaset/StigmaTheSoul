using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUI : MonoBehaviour
{
    public GameObject skillPanel;
    public List<GameObject> skillSlots = new(); //스킬 슬롯(자식)들을 담을 리스트

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!skillPanel.activeInHierarchy) { SkillPanelOn(); }
            else { skillPanel.SetActive(false); } //스킬 패널 비활성화
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //ESC버튼 입력 시
        {
            skillPanel.SetActive(false); //스킬 패널 비활성화
        }
    }

    public void SkillPanelOn()
    {
        skillPanel.SetActive(true); //skillPanel 오브젝트 활성화

        GameObject ss = GameObject.Find("SkillSlots"); //SkillSlots(슬롯들을 담은 오브젝트) 찾기

        for (int i = 0; i < ss.transform.childCount; i++)
        {
            skillSlots.Add(ss.transform.GetChild(i).gameObject);
            //SkillSlots 안에 담긴 슬롯들의 수만큼 반복해서 리스트에 추가
        }
        skillSlots.TrimExcess(); //리스트 생성 후 남은 메모리 반환

        SkillTextUpdate(); //스킬 이름 및 설명 텍스트 업데이트 함수 호출
    }

    void SkillTextUpdate()
    {
        for (int i = 0; i < skillSlots.Count; i++) //스킬 슬롯 수만큼 반복
        {
            TextMeshProUGUI skillText = skillSlots[i].GetComponentInChildren<TextMeshProUGUI>();
            //스킬 설명 TMP 컴포넌트 불러오기

            if (skillSlots[i].GetComponentInChildren<SkillCooltime>() != null) //슬롯 안에 스킬이 있다면
            {
                SkillData skillData = skillSlots[i].GetComponentInChildren<SkillCooltime>().skillData;
                //해당 슬롯에 담긴 스킬 데이터 불러오기

                skillText.text = "​<size=23>" + skillData.SkillName + "</size>"
                    + "\n<size=18>" + skillData.SkillInfo + "</size>";
                //skillText에 해당 스킬의 이름과 설명 삽입
            }
            else { skillText.text = null; } //스킬이 없다면 설명 텍스트를 비운다.
        }
    }
}
