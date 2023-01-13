using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// ���� �����̽� �߰�

public class Player_Combat : MonoBehaviour
{
    public PlayerData playerData; //�÷��̾� ������
    public Animator ani;   //�ִϸ�����

    //�÷��̾� ����(�Է�) ����
    bool fdown; //����
    bool sdown; //��ų

    // ���� ���� ����
    //Rigidbody rid;
    public Collider sword; //public GameObject sword ���� -> public Collider sword�� ����
    BoxCollider boxCollider;

    // �ǰ� ���� ����
    public Collider Punch;//Attack ������Ʈ�� ����� ColliderŸ�� ���� ����

    Rigidbody rigid;
    //�� ������Ʈ�� ������ٵ� ������Ʈ

    // �÷��̾ �׾��� �� �ֺ� ���͵��� �¸� �׼��� ���ϰ� �� ����
    Collider[] monsters; // �÷��̾� �ֺ� �ݰ� �� ������ �ݶ��̴��� ���� �迭 ���� 
    public float limitDetectRange = 50f; // �ݶ��̴��� �����ϱ� ���� ���� ���� �Ÿ��� ������ ����

    void Awake()
    {
        // �ڽ��� �ִϸ����� ������Ʈ�� �ҷ��´�.
        ani = GetComponentInChildren<Animator>();

        // ���� ������Ʈ�� �ڽ��ݶ��̴� ������Ʈ�� �ҷ��´�.
        boxCollider = sword.GetComponent<BoxCollider>();

        // ������ٵ� ������Ʈ�� �ҷ��´�.
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //�÷��̾� ������ ������Ʈ�� �ҷ��´�.
        playerData = Player_Data.Instance.playerData;

        playerData.Hp = playerData.MaxHp; //ü�� �ʱ�ȭ
        playerData.Mp = playerData.MaxMp; //���� �ʱ�ȭ

        UI_StatusBar.Instance.hpBar.value = playerData.MaxHp;  //ü�¹� �ʱ�ȭ
        UI_StatusBar.Instance.mpBar.value = playerData.MaxMp;  //���¹� �ʱ�ȭ
    }

    void Update()
    {
        // Ű �� �Է�
        fdown = Input.GetButtonDown("Fire1"); //���콺 Ŭ������ ����!
        sdown = Input.GetButtonDown("Fire2"); //���콺 ��Ŭ������ ��ų ���!

        if (!SceneLoadManager.Instance.isLoading && !playerData.isDie)
        //�ε� ���� �ƴ� �� + PlayerData���� false�� �� �Լ� ����, true�� ����X
        {
            // �Լ� ����
            Attack(); // ���ݿ� ���� Ŭ���� �߰�!
            Skill();  // ��ų ��뿡 ���� �Լ�
        }

        // enemy�� ������ ����ϹǷ� Start���� ã���� null�� �ȴ�.
        // Enemy�±װ� �پ��ִ� ������Ʈ�� ������ ��´�.
        GameObject enemyobjects = GameObject.FindGameObjectWithTag("Enemy");

        // enemyobjects�� null�� �ƴ϶��
        if (enemyobjects != null)
        {
            // PUNCH�±��� ������Ʈ �ݶ��̴� ������Ʈ�� ��´�.
            Punch = GameObject.FindGameObjectWithTag("PUNCH").GetComponent<Collider>();
        }
    }

    // ����
    void Attack()
    {
        //���� UI�� Ŭ���� �Ǿ��ٸ�
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //�ؿ� ������ �������� ���� �� �Լ��� ���� ������
            return;
        }

        //���� ����
        // ���� Ŭ���� �Ѵٸ� ���� ����� ������!
        if (fdown)
        {
            // �ڷ�ƾ�� �̿��Ͽ� �ð� ����
            StartCoroutine(EnemyHit());

            Debug.Log("����");
            // �ִϸ��̼� ����
            ani.SetTrigger("doAttack");
            // ���� �� ������ ����!
            //moveSpeed = 0; 

            Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[1];
            Player_Data.Instance.audioSource.Play();
        }
    }

    // ���ݵ��� �浹 ���� �ڷ�ƾ �Լ�
    IEnumerator EnemyHit()
    {
        // ������ �ڽ� �ݶ��̴��� isTrigger üũ
        boxCollider.isTrigger = true;

        // 1�� ���� ��Ų ��
        yield return new WaitForSeconds(0.5f);

        // ���� �ݶ��̴��� isTrigger üũ ����
        boxCollider.isTrigger = false;
    }

    // �÷��̾ ���ݴ��ϸ� ȣ��Ǵ� �Լ�
    public void DamageAction(int damage)
    {
        if (playerData.PlusHp > 0) //��ȣ���� ���� ���
        {
            playerData.PlusHp -= damage;          //���ʹ��� ���ݷ¸�ŭ ��ȣ�� hp ����
            UI_StatusBar.Instance.HpBar2Update(); //��ȣ�� �����̴��� �ݿ�

            if (damage > playerData.PlusHp)            //�������� ��ȣ�� hp���� ū ���
            {
                int temp = damage - playerData.PlusHp; //���������� ��ȣ�� �ܷ��� ���� ��ġ�� ���

                playerData.PlusHp = 0;                 //��ȣ�� hp�� 0���� �ʱ�ȭ
                UI_StatusBar.Instance.HpBar2Update();  //�ش� ��ġ�� �����̴��� �ݿ�

                playerData.Hp -= temp;                 //�ռ� ����� temp���� �÷��̾��� ü�¿��� ����
                UI_StatusBar.Instance.HpBarUpdate();   //���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ�
            }
        }
        else //��ȣ���� ���� ���(�Ϲ����� ����)
        {
            Debug.Log("Player�� HP : " + playerData.Hp);

            playerData.Hp -= damage;             //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü���� ��´�
            UI_StatusBar.Instance.HpBarUpdate(); //���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ�
        }

        playerData.Cp += Random.Range(1, 6); //Ŭ���� ������ ȹ��
        UI_StatusBar.Instance.CpBarUpdate(); //�����̴��� �ݿ�


        // �÷��̾��� ü���� 0���� �۴ٸ�
        if (playerData.Hp <= 0)
        {
            Debug.Log("����");//�ܼ�â�� "����"���

            // �÷��̾� ���¸� ���� ���·� ����
            playerData.isDie = true;

            // �÷��̾� �ֺ� limitDetectRange��ŭ�� �ݶ��̴��� �����Ͽ� �迭�� ��´�.
            monsters = Physics.OverlapSphere(transform.position, limitDetectRange);

            // ��� �ݶ��̴� ����ŭ �ݺ��� �ݺ���
            foreach(Collider monster in monsters)
            {
                // ������ ��� ������ �±װ� Enemy���
                if(monster.gameObject.tag == "Enemy")
                {
                    // �� ������ EnemyManager�� ������Ʈ ���� PlayerCheck()�Լ��� ȣ��
                    monster.GetComponent<EnemyManager>().PlayerDieAction();
                }
            }

            ani.SetTrigger("doHited");
            // �ǰ� �ִϸ��̼� ����
            sword.enabled = false;
            //�÷��̾ �׾����Ƿ� sword������Ʈ�� ��Ȱ��ȭ ���༭ �浹������ �Ͼ���ʵ��� �� �ش�.
            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            //�÷��̾��� �ݶ��̴��� ��Ȱ�� �Ǿ����Ƿ� �÷��̾ �ٴ��� �հ� ��� �������� �ʵ��� ������ٵ����� �������ش�.
            rigid.useGravity = false;
            rigid.isKinematic = true;

            //�÷��̾��� CapsuleCollider�� ��Ȱ��ȭ ���༭ "PUNCH"�±׸� ���� ������Ʈ�� �浹ó���� ���� �ʵ��� ���ش�.

            //Destroy(gameObject, 4.5f);
            //4.5�� �Ŀ� �÷��̾� ����

            // PlayerHitEffect �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(PlayerHitEffect());
        }
    }


    // ������ ������ �Ǵ��ϱ� ���� �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        // ���� �浹�� ������Ʈ�� �±װ� PUNCH���
        if (other.gameObject.tag == "PUNCH")
        {
            Debug.Log("�¾Ҵ�");

            // �浹�� ������Ʈ�� �θ��� ������Ʈ EnemyManager�� �ҷ��� ������ ���ݷ��� ��´�.
            int monsterAttack = other.gameObject.GetComponentInParent<EnemyManager>().monsterData.Attack;

            // ������ ���ݷ¸�ŭ �÷��̾��� ü���� ��� �Լ��� ȣ���Ѵ�.
            DamageAction(monsterAttack);
        }
    }

    // �ڷ�ƾ �Լ�
    IEnumerator PlayerHitEffect()
    {
        // 0.75�� ����
        yield return new WaitForSeconds(0.75f);

        Debug.Log("Game Over");
    }

    //��ų
    void Skill()
    {
        //���� UI�� Ŭ���� �Ǿ��ٸ� �ؿ� ������ �������� ���� �� �Լ��� ���� ������
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        //��Ŭ�� �� ��ų ����
        if (sdown) { SkillManager.Instance.ChooseSkill(); }
    }
}