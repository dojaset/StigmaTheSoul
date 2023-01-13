using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ü�¹� ���ӽ����̽� �߰�

public class Enemy : MonoBehaviour
{
    // ���� ���¿� ���� enum ����
    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die,
        PlayerDie
    }

    public MonsterState m_State; // ���� ���¿� ���� enum ����

    public MonsterData monsterData;            //���� ������(public)

    protected Animator anim;                   //�ִϸ����� ������Ʈ
    protected BoxCollider monsterBody; //�� �ݶ��̴� ������Ʈ

    protected Player_Combat player;            //�÷��̾�
    protected bool Sword_check = false;        //�÷��̾��� Sword�� �浹 ������ bool����

    protected int hp;                          //ü��
    public Slider hpSlider;                    //ü�¹�(public)

    // ������ ������Ʈ ������ ���� ������ ����
    public GameObject[] getItems; // Item ������ ������ ��� ���� �迭 ����
    public GameObject getCoin; // ���� ������ ����

    void Awake()
    {
        //�ռ� ������ ������Ʈ �ҷ�����
        anim = GetComponentInChildren<Animator>(); // ������ �ڽ� ������Ʈ�� �ִϸ����͸� ������ ��´�.
        monsterBody = GetComponent<BoxCollider>(); // ���� ��ü �ݶ��̴� ������Ʈ�� ������ ��´�

        //�÷��̾� �ҷ�����
        player = FindObjectOfType<Player_Combat>();
    }

    public virtual void TakeDamage(int damage) //���� ���ݴ��ϸ� Player�� ��ũ��Ʈ���� ȣ��Ǵ� �Լ�
    {
        hp -= damage;
        Debug.Log("Monster HP :"+hp);

        //�÷��̾� -> ��ȿŸ �� Ŭ���� ������ ȹ�� �� �����̴��� �ݿ�
        Player_Data.Instance.playerData.Cp += Random.Range(5, 10);
        UI_StatusBar.Instance.CpBarUpdate();

        //������� ��� ���� ����ǳ�, ���� �߰��� ������ �ٸ�.
        //���� virtual�� ����, �ڽĵ�� �Ͽ��� �� Ŭ������ ��ӹ޾� override�ϵ��� ��.
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && player.sword.isTrigger == true)
        //�Ʒ��� �ִ� Player_Move��ũ��Ʈ�� �����ϸ� ����
        //�� �������.
        // "Sword"�±׸� ���� ������Ʈ�� �浹�� �ϰ� �÷��̾��� sword�� Ʈ���Ű� ������                             
        {
            TakeDamage(monsterData.Attack); //TakeDamage�� �Լ� ȣ��

            Sword_check = true;//Sword_check�� false���� true�� �ٲ۴�.
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sword") && player.sword.isTrigger == false && Sword_check == true)
        {
            Sword_check = false;
        }
    }
}