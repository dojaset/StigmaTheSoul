using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���ӽ����̽� �߰�

public class SkillManager : Singletone<SkillManager>
{
    PlayerData playerData; //�÷��̾� ������

    // �� ��ų�� �ش��ϴ� �̹����� ã�� ���� ���� ��������Ʈ �̹����� �ܾ� Ű������ ����
    public Dictionary<string, Sprite> skillList = new Dictionary<string, Sprite>();

    // Dictionary �� ��������Ʈ �̹����� ����� ��������Ʈ ����Ʈ
    public List<Sprite> skill = new List<Sprite>();

    // ��ų ���� ���� ��, ���� �����ǰ� �ִ� ������ slot
    Transform emphasizeField;

    // �����ǰ� �ִ� slot �� �ڽ� ������Ʈ�� �����ϱ� ���� �迭
    Transform[] slotChildren;
    Transform[] slot2Children;

    // ���������� ���� ��� ������ ��ų�� �������� ����� ����
    Sprite thisSkill;
    SkillData skillData;

    public GameObject slashPre; // ��ų ������ 
    public Transform skillPos;  // ��ų�� ���� ���
    public ParticleSystem AttackUpPre;  // �÷�ġ�⽺ų ��ƼŬ

    Player_Move player_Move;

    void Start()
    {
        //Player_Move��ũ��Ʈ�� ���� ���� FindObjectOfType�� �� �ش�.
        player_Move = FindObjectOfType<Player_Move>();
        playerData = Player_Data.Instance.playerData; //�÷��̾� ������ �ҷ�����
        skillPos = GameObject.Find("SkillPos").GetComponent<Transform>(); //skillPos ��ġ ����
        AttackUpPre = GameObject.Find("AttackUpParticlePos").GetComponent<ParticleSystem>();

        // ��ų�����͵� �߰������ ��
        // Dictionary�� ��ų�� ��������Ʈ �̹����� ���� ����ش�
        skillList.Add("����", skill[0]);
        skillList.Add("�÷�ġ��", skill[1]);
        skillList.Add("������", skill[2]);
        skillList.Add("����ũ", skill[3]);
        skillList.Add("���̽�Ʈ", skill[4]);
        skillList.Add("����", skill[5]);

        skillList.TrimExcess(); //���ʿ��� �޸� ��ȯ
    }

    // ��ų Ű�� ������ �� �ߵ� (���콺 ��Ŭ��?) �÷��̾�� ȣ��
    public void ChooseSkill()
    {
        // ���� ���õǾ� �ִ� slot
        emphasizeField = GetComponentInParent<SkillScroll>().slots[2];

        // �� slot�� �ڽĿ�����Ʈ �迭
        slotChildren = emphasizeField.gameObject.GetComponentsInChildren<Transform>();

        if (slotChildren[2].name == "Skill")
        {
            // �� slot�� �ڽĿ�����Ʈ �迭
            slot2Children = slotChildren[2].gameObject.GetComponentsInChildren<Transform>();

            // �� ��ų�� ��������Ʈ �̹���
            thisSkill = slot2Children[1].GetComponent<Image>().sprite;

            //�ش� ���Կ� ��� ��ų���� ��ų ������Ʈ �ҷ�����
            skillData = slotChildren[2].GetComponent<SkillCooltime>().skillData;

            //�����ִ� ��Ÿ���� ���� MP�� �ܷ��� �䱸�ϴ� �纸�� ���ٸ�
            if (skillData.CurrentTime == 0 && playerData.Mp >= skillData.Mp && playerData.Cp >= skillData.Cp)
            {
                slotChildren[2].GetComponent<SkillCooltime>().CoolTime(); //��ų ��Ÿ�� ȿ�� ����

                playerData.Mp -= skillData.Mp; //MP ���
                playerData.Cp -= skillData.Cp; //Cp ���

                UI_StatusBar.Instance.MpBarUpdate();
                UI_StatusBar.Instance.CpBarUpdate();
                //�������ͽ��� ������Ʈ

                SkillTrigger();
            }
        }
    }

    void SkillTrigger()
    {
        //���� �����ǰ��ִ� slot ������Ʈ�� �ڽĿ�����Ʈ�� "Image"�� Image ������Ʈ�� ��������Ʈ �̹����� ��ų����Ʈ�� �̹��� ��

        if (thisSkill == skillList["����"])
        {
            Slash();
        }

        if (thisSkill == skillList["�÷�ġ��"])
        {
            Uppercut();
        }

        if (thisSkill == skillList["������"])
        {
            Whirlwind();
        }

        if (thisSkill == skillList["����ũ"])
        {
            Berserk();
        }

        if (thisSkill == skillList["���̽�Ʈ"])
        {
            Haste();
        }

        if (thisSkill == skillList["����"])
        {
            Shield();
        }
    }

    // ��ų �Ӽ��� ���� �߰� (�׽�Ʈ�� ���� Ʋ�� �ϼ�) (��ų���� summary ���ָ� ���ҵ�~)
    void Slash()
    {
        Debug.Log("����");
        Instantiate(slashPre, skillPos.position, skillPos.rotation); //��ų�������� ��ų ����

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[3]; 
        Player_Data.Instance.audioSource.Play(); //ȿ���� Ŭ�� ���� �� ���
    }

    void Uppercut()
    {
        Debug.Log("�÷�ġ��");

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[4];
        Player_Data.Instance.audioSource.Play(); //ȿ���� Ŭ�� ���� �� ���

        //player_Move�� AttackUp������ true�� �ٲ㼭 ����,������,�����⸦ �����ش�.
        player_Move.AttackUp = true;
        //player_Move�� �ִϸ����� �Ķ������ isWalk�� ���� 0���� �ٲ㼭 �÷��̾ �������� �ʰ� �� �ش�.
        player_Move.ani.SetFloat("isWalk", 0);
        //player_Move�� �ִϸ����� �Ķ������ Attack_Up�� ��������ش�.
        player_Move.ani.SetTrigger("Attack_Up");
        //�÷�ġ�� ��ƼŬ ����
        AttackUpPre.Play();
        //2,5���Ŀ� player_Move�� AttackUp������ fale�� �ٲ㼭 ����,������,�����⸦ Ǯ���ش�.
        StartCoroutine(ReplayPlayerMove());
    }
    void Whirlwind()
    {
        Debug.Log("������");
        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[5];
        Player_Data.Instance.audioSource.Play();
    }

    void Berserk()
    {
        Debug.Log("����ũ");

        BuffManager.Instance.CreateBuff(skillData);
        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[6];
        Player_Data.Instance.audioSource.Play();
    }

    void Haste()
    {
        Debug.Log("���̽�Ʈ");

        BuffManager.Instance.CreateBuff(skillData);
        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[7];
        Player_Data.Instance.audioSource.Play();
    }

    void Shield()
    {
        Debug.Log("����");

        playerData.PlusHp = (int)(playerData.MaxHp * 0.3f);
        UI_StatusBar.Instance.HpBar2Update();

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[8];
        Player_Data.Instance.audioSource.Play();
    }

    IEnumerator ReplayPlayerMove()
    {
        //2.5�� �Ŀ�
        yield return new WaitForSeconds(2.5f);
        //player_Move�� AttackUp�� false�� �ٲ��ش�.
        player_Move.AttackUp = false;
    }
}