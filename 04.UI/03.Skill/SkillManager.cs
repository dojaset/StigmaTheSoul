using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 네임스페이스 추가

public class SkillManager : Singletone<SkillManager>
{
    PlayerData playerData; //플레이어 데이터

    // 각 스킬에 해당하는 이미지를 찾아 쓰기 위해 스프라이트 이미지를 단어 키값으로 저장
    public Dictionary<string, Sprite> skillList = new Dictionary<string, Sprite>();

    // Dictionary 에 스프라이트 이미지를 담아줄 스프라이트 리스트
    public List<Sprite> skill = new List<Sprite>();

    // 스킬 강조 구역 즉, 현재 강조되고 있는 상태의 slot
    Transform emphasizeField;

    // 강조되고 있는 slot 의 자식 오브젝트에 접근하기 위한 배열
    Transform[] slotChildren;
    Transform[] slot2Children;

    // 최종적으로 지금 사용 가능한 스킬이 무엇인지 담아줄 변수
    Sprite thisSkill;
    SkillData skillData;

    public GameObject slashPre; // 스킬 프리팹 
    public Transform skillPos;  // 스킬이 나갈 장소
    public ParticleSystem AttackUpPre;  // 올려치기스킬 파티클

    Player_Move player_Move;

    void Start()
    {
        //Player_Move스크립트를 쓰기 위해 FindObjectOfType을 써 준다.
        player_Move = FindObjectOfType<Player_Move>();
        playerData = Player_Data.Instance.playerData; //플레이어 데이터 불러오기
        skillPos = GameObject.Find("SkillPos").GetComponent<Transform>(); //skillPos 위치 지정
        AttackUpPre = GameObject.Find("AttackUpParticlePos").GetComponent<ParticleSystem>();

        // 스킬데이터도 추가해줘야 함
        // Dictionary에 스킬의 스프라이트 이미지를 각각 담아준다
        skillList.Add("참격", skill[0]);
        skillList.Add("올려치기", skill[1]);
        skillList.Add("휠윈드", skill[2]);
        skillList.Add("버서크", skill[3]);
        skillList.Add("헤이스트", skill[4]);
        skillList.Add("쉴드", skill[5]);

        skillList.TrimExcess(); //불필요한 메모리 반환
    }

    // 스킬 키를 눌렀을 때 발동 (마우스 우클릭?) 플레이어에서 호출
    public void ChooseSkill()
    {
        // 지금 선택되어 있는 slot
        emphasizeField = GetComponentInParent<SkillScroll>().slots[2];

        // 그 slot의 자식오브젝트 배열
        slotChildren = emphasizeField.gameObject.GetComponentsInChildren<Transform>();

        if (slotChildren[2].name == "Skill")
        {
            // 그 slot의 자식오브젝트 배열
            slot2Children = slotChildren[2].gameObject.GetComponentsInChildren<Transform>();

            // 그 스킬의 스프라이트 이미지
            thisSkill = slot2Children[1].GetComponent<Image>().sprite;

            //해당 슬롯에 담긴 스킬에서 스킬 컴포넌트 불러오기
            skillData = slotChildren[2].GetComponent<SkillCooltime>().skillData;

            //남아있는 쿨타임이 없고 MP의 잔량이 요구하는 양보다 많다면
            if (skillData.CurrentTime == 0 && playerData.Mp >= skillData.Mp && playerData.Cp >= skillData.Cp)
            {
                slotChildren[2].GetComponent<SkillCooltime>().CoolTime(); //스킬 쿨타임 효과 실행

                playerData.Mp -= skillData.Mp; //MP 사용
                playerData.Cp -= skillData.Cp; //Cp 사용

                UI_StatusBar.Instance.MpBarUpdate();
                UI_StatusBar.Instance.CpBarUpdate();
                //스테이터스바 업데이트

                SkillTrigger();
            }
        }
    }

    void SkillTrigger()
    {
        //현재 강조되고있는 slot 오브젝트의 자식오브젝트인 "Image"의 Image 컴포넌트의 스프라이트 이미지와 스킬리스트의 이미지 비교

        if (thisSkill == skillList["참격"])
        {
            Slash();
        }

        if (thisSkill == skillList["올려치기"])
        {
            Uppercut();
        }

        if (thisSkill == skillList["휠윈드"])
        {
            Whirlwind();
        }

        if (thisSkill == skillList["버서크"])
        {
            Berserk();
        }

        if (thisSkill == skillList["헤이스트"])
        {
            Haste();
        }

        if (thisSkill == skillList["쉴드"])
        {
            Shield();
        }
    }

    // 스킬 속성은 차차 추가 (테스트를 위한 틀만 완성) (스킬마다 summary 해주면 편할듯~)
    void Slash()
    {
        Debug.Log("참격");
        Instantiate(slashPre, skillPos.position, skillPos.rotation); //스킬포스에서 스킬 생성

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[3]; 
        Player_Data.Instance.audioSource.Play(); //효과음 클립 설정 및 재생
    }

    void Uppercut()
    {
        Debug.Log("올려치기");

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[4];
        Player_Data.Instance.audioSource.Play(); //효과음 클립 설정 및 재생

        //player_Move의 AttackUp변수를 true로 바꿔서 공격,움직임,구르기를 막아준다.
        player_Move.AttackUp = true;
        //player_Move의 애니메이터 파라미터의 isWalk의 값을 0으로 바꿔서 플레이어가 움직이지 않게 해 준다.
        player_Move.ani.SetFloat("isWalk", 0);
        //player_Move의 애니메이터 파라미터의 Attack_Up을 실행시켜준다.
        player_Move.ani.SetTrigger("Attack_Up");
        //올려치기 파티클 실행
        AttackUpPre.Play();
        //2,5초후에 player_Move의 AttackUp변수를 fale로 바꿔서 공격,움직임,구르기를 풀어준다.
        StartCoroutine(ReplayPlayerMove());
    }
    void Whirlwind()
    {
        Debug.Log("휠윈드");
        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[5];
        Player_Data.Instance.audioSource.Play();
    }

    void Berserk()
    {
        Debug.Log("버서크");

        BuffManager.Instance.CreateBuff(skillData);
        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[6];
        Player_Data.Instance.audioSource.Play();
    }

    void Haste()
    {
        Debug.Log("헤이스트");

        BuffManager.Instance.CreateBuff(skillData);
        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[7];
        Player_Data.Instance.audioSource.Play();
    }

    void Shield()
    {
        Debug.Log("쉴드");

        playerData.PlusHp = (int)(playerData.MaxHp * 0.3f);
        UI_StatusBar.Instance.HpBar2Update();

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[8];
        Player_Data.Instance.audioSource.Play();
    }

    IEnumerator ReplayPlayerMove()
    {
        //2.5초 후에
        yield return new WaitForSeconds(2.5f);
        //player_Move의 AttackUp을 false로 바꿔준다.
        player_Move.AttackUp = false;
    }
}