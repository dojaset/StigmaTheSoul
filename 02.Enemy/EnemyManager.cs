using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : Enemy
{
    float distance;// �÷��̾�� ������ �Ÿ�

    // ������ ���� ��ġ�� ������ ����
    Vector3 originPos;

    // ������
    Vector3 dir;// ���� ����

    // Ÿ�̸� ����
    float currentTime = 0; // Ÿ�� ī��Ʈ
    float attackDelay = 2f; // ���� ���� �ð�

    // �÷��̾� ������Ʈ ����
    Transform playerTr; // �÷��̾� Ʈ������

    // monster �׼��� ���� ����ü ����
    MonsterAction m_act;

    // wolf�� ��ä�ο� idle������ ���� �ð� ����
    float stayTime;

    // ���Ͱ� ���� �ڿ� ������ ����� ���� ����
    int randomItem; // ������ ������ �������� ���õ� ���� �ε��� ����
    float randomItemNum; // �������� ���� ������ ������ �����ϴ� ����
    public int randomItemlimitNum = 3; // �������� ���� ������ ������ �Ѱ� ���� ����
    float itemRangeOffset = 2f; // ������ ��ġ���� ��ŭ �������� ������ ���� ������ �����ϱ� ���� ����
    float randomDropRangeX; // ��ӵ� ��ġ�� ������ �Ѱ�Ÿ� �ȿ��� ���õ� X��ǥ ���� ����
    float randomDropRangeZ; // ��ӵ� ��ġ�� ������ �Ѱ�Ÿ� �ȿ��� ���õ� Z��ǥ ���� ����
    public float coinNum = 1; // ���� ������ ����

    // Start is called before the first frame update
    void Start()
    {

        // Player�� Ʈ������ ������Ʈ �ҷ�����
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        // ���� ���¸� Idle���·� �ʱ�ȭ
        m_State = MonsterState.Idle;

        // ������ ó�� ��ġ�� ����
        originPos = transform.position;

        hp = monsterData.MaxHp; //���� ü�� �ʱ�ȭ

        // ����ü���� �ִϸ����� Ư���� ���� ����� �� ���� ������ ������ �� �� �� ������ ��´�.
        m_act.ani = anim;


    }

    // Update is called once per frame
    void Update()
    {
        // Player�� �׾��� �� ���� ���� �Ǵ� ��� ��ȯ
        if (player.playerData.isDie)
        {
            return;
        }

        // �÷��̾� ���� ����ȭ
        dir = (playerTr.position - transform.position).normalized;

        // �÷��̾���� �Ÿ� ����
        distance = Vector3.SqrMagnitude(playerTr.position - transform.position);

        switch (m_State)
        {
            case MonsterState.Idle:
                Idle();
                m_act.Walk(false);
                break;

            case MonsterState.Move:
                m_act.Walk(true);
                m_act.Attack(false);
                Move();
                break;

            case MonsterState.Attack:
                Attack();
                m_act.Attack(true);
                break;

            case MonsterState.Return:
                Return();
                break;
        }

        hpSlider.value = (float)hp / monsterData.MaxHp; //���� hp�� �����̴��� value�� �ݿ�
    }

    #region ���� ��ȭ ���� �Լ�
    // ���� ���°� Idle �� �� ȣ��
    void Idle()
    {
        // �ִϸ����� Ʈ������ breathe -> sit (�Ķ���� : stayTime greater 20)
        // Wolf�� ��ä�ο� Idle ����� ����
        stayTime += Time.deltaTime; // Idle�� �ӹ��� �ð��� ������

        // ���� 25�̻��̸�
        if (stayTime >= 25)
        {
            // 0���� �ʱ�ȭ
            stayTime = 0;
        }

        // monsterData�� Wolf�϶��� ȣ��Ǵ� �׼� ���� �Լ�
        m_act.StayIdle(monsterData, stayTime);

        // ���Ϳ� �÷��̾� �Ÿ��� ã�� ���� �ݰ�ȿ� ���´ٸ�
        if (distance <= monsterData.findDistance)
        {
            // ���� ���¸� Move�� ����
            m_State = MonsterState.Move;

            stayTime = 0;
        }
    }

    // ���� ���°� Move�� �� ȣ��
    void Move()
    {
        // ������ ���� ��ġ�� ���� ��ġ�� �Ѱ� ������ ����� ��
        if (Vector3.SqrMagnitude(originPos - this.transform.position) >= monsterData.limitDistance)
        {
            // ���� ���¸� Return���� ����
            m_State = MonsterState.Return;
        }
        // �װ� �ƴϰ�, �÷��̾���� �Ÿ��� ���ݹ����� ��� ������ ��
        else if (distance >= monsterData.attackDistance)
        {
            // �÷��̾ ��� �ٶ󺸰�
            transform.LookAt(playerTr);

            // ���� �����̱�
            transform.position += dir * monsterData.moveSpeed * Time.deltaTime;
        }
        // �ƴϸ�
        else
        {
            // ���� ���¸� Attack���� ����
            m_State = MonsterState.Attack;

            // ���� �����ð��� ī��Ʈ�ð��� ó���� ��ġ �����ְ� �ٷ� ����
            currentTime = attackDelay;
        }

    }

    // ���� ���°� Attack �� �� ȣ��
    void Attack()
    {
        // �ð� ī��Ʈ
        currentTime += Time.deltaTime;

        // �÷��̾�� ������ �Ÿ��� ���ݹ��� ���̶��
        if (distance <= monsterData.attackDistance)
        {
            // ���� �����ð��� ī��Ʈ Ÿ�̸ӿ� �������� ��
            if (currentTime >= attackDelay)
            {
                // ī��Ʈ Ÿ�� �ʱ�ȭ
                currentTime = 0;
            }
        }
        // �÷��̾�� ������ �Ÿ��� ���ݹ��� ���̶��
        else
        {
            // ������ ���¸� Move�� ����
            m_State = MonsterState.Move;

            // ī��Ʈ Ÿ�� �ʱ�ȭ
            currentTime = 0;
        }
    }

    // ���� ���°� Return �� �� ȣ��
    void Return()
    {
        // �÷��̾�� ������ �Ÿ��� ���ݹ��� ���̶��
        if (distance <= monsterData.attackDistance)
        {
            // ���� ���¸� Move���� ����
            m_State = MonsterState.Move;
        }
        // ������ ���� ��ġ�� ���� ��ġ�� 0.1���� ũ�ٸ�
        if (Vector3.Distance(originPos, this.transform.position) > 0.1f)
        {
            // ���� ��ġ�� ���� ���� ����ȭ
            Vector3 retDirection = (originPos - transform.position).normalized;

            // originPos�� ��� �ٶ󺸰�
            transform.LookAt(originPos);

            // ��Ȯ�� ��ġ ������ ���� ���� ��ġ�� ��� �����δ�.
            transform.position += retDirection * monsterData.moveSpeed * Time.deltaTime;
        }
        // �װ� �ƴ϶��
        else
        {
            // ���� ��ġ�� ���� ��ġ�� ����
            transform.position = originPos;

            // ó�� �����ߴ� �ִ� ü������ �ٽ� ȸ��
            hp = monsterData.MaxHp;

            // ���� ���¸� Idle �� ����
            m_State = MonsterState.Idle;
        }

    }

    // ���� ���°� Damaged �� ��
    void Damaged()
    {
        // Hit �׼� ȣ��
        m_act.Hit();
        // DamageProcess �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(DamageProcess());
    }

    // DamageProcess �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // 0.5�� ���� ��
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� Move�� ����
        m_State = MonsterState.Move;
    }

    // ���Ͱ� �׾��� ��
    void Die()
    {
        // Die �׼� ȣ��
        m_act.Die();

        // �ڷ�ƾ �Լ� ���� ����
        StopAllCoroutines();

        // DieProcess �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(DieProcess());

        // ���Ͱ� �װ� �������� ����� �� �Լ��� ȣ��
        ItemDrop();

        PlayerData playerData = Player_Data.Instance.playerData;

        if (playerData.Level >= 5) return; //�÷��̾� ������ 5��� ����ġ ȹ�� �Ұ�

        Player_Data.Instance.playerData.Exp += (int)(GetComponent<Enemy>().monsterData.Exp * Random.Range(0.8f, 1.2f));
        //����ġ�� ���Ͱ� ����ϴ� ����ġ(*0.8���� 1.2)�� ����

        Debug.Log("player ����ġ : " + playerData.Exp);

        UI_StatusBar.Instance.ExpBarUpdate(); //����ġ�� ������Ʈ

        if (playerData.Exp >= playerData.MaxExp) { Player_Data.Instance.LevelUp(); }
        //���� ����ġ ���� �ִ� ����ġ �纸�� Ŭ ��� ������ �Լ� ����
    }

    // DieProcess �ڷ�ƾ �Լ�
    IEnumerator DieProcess()
    {
        monsterBody.enabled = true;

        // 2�� ���� �ڿ�
        yield return new WaitForSeconds(2f);

        // ���� ������Ʈ ����
        Destroy(gameObject);
    }

    // Player�� �׾��� �� Player_Combat���� ȣ��Ǵ� �Լ�
    public void PlayerDieAction()
    {
        // ���� ������ �̸��� Golem�̰ų� Wolf�� ��
        if (monsterData.Pname == "Golem" || monsterData.Pname == "Wolf" || monsterData.Pname == "SkullKing")
        {
            // ȣ��Ǵ� �¸� �׼� ���� �Լ�
            m_act.Victory();
        }
        // �� ���� ���� �����Ͷ��
        else if (monsterData != null)
        {
            // ������ ���¸� Idle���·� ��ȯ
            m_State = MonsterState.Idle;
        }

        // ���� ���¸� PlayerDie�� ����
        m_State = MonsterState.PlayerDie;
    }
    #endregion

    #region ������ ���� �Լ�
    // ���� �ǰ� ������ ���� �Լ�
    public override void TakeDamage(int hitPower)
    {
        base.TakeDamage(hitPower); //�θ� Ŭ������ TakeDamage ���� ���

        // ���� ���°� �¾Ұų� ���ư��ų�, ���� ���¶��
        if (m_State == MonsterState.Damaged || m_State == MonsterState.Return || m_State == MonsterState.Die)
        {
            // ��� ��ȯ
            return;
        }

        // ü���� 0���� ũ�ٸ�
        if (hp > 0)
        {
            // ���� ���¸� Damaged �� ����
            m_State = MonsterState.Damaged;

            Damaged();
        }
        // �׷��� ������
        else
        {
            // ���� ���¸� Die�� ����
            m_State = MonsterState.Die;

            Die();
        }
    }
    #endregion

    #region ���� �׼� ���� ����ü
    // ������ �׼��� �޸��ϱ� ����
    public struct MonsterAction
    {
        // �ҷ��� �ִϸ������� ����
        public Animator ani;

        // Wolf�� ��ä�ο� idle������ ���� �Լ�
        public void StayIdle(MonsterData monster, float stayTime)
        {
            // ���� monsterData�� �̸��� Wolf���
            if (monster.Pname == "Wolf")
            {
                // stayTime�̶�� �Ķ������ �Ű������� ����
                ani.SetFloat("stayTime", stayTime);
            }
        }

        // �ȴ� �׼��� �Ű������� �޾� ����
        public void Walk(bool tf)
        {
            ani.SetBool("isWalk", tf);
        }
        // ���� �׼��� �Ű������� �޾� ����
        public void Attack(bool tf)
        {
            ani.SetBool("doAttack", tf);
        }
        // �´� �׼�
        public void Hit()
        {
            ani.SetTrigger("getHit");
        }
        // �״� �׼�
        public void Die()
        {
            ani.SetTrigger("isDie");
        }
        // Player�� �׾��� �� �׼� (Golem�� �ش� �������� Idle�� ���ư��� �ϱ� ����)
        public void Victory()
        {
            ani.SetTrigger("victory");
        }
    }
    #endregion

    // ���Ͱ� �װ��� �������� ����� �Լ�
    void ItemDrop()
    {
        // ��ӵ� �����۰����� �������� �����ֱ�
        randomItemNum = Random.Range(0, randomItemlimitNum);

        // 10%Ȯ�� �̸��̸�
        if (randomItemNum < 0.1)
        {
            // ��ӵ� ������ ������ 1��
            randomItemNum = 0;
        }
        // �װ� �ƴϰ�, 10%Ȯ�� �̻��̰�, 60%Ȯ�� �̸��̸�
        else if (0.1 <= randomItemNum && randomItemNum < 0.6)
        {
            // ��ӵ� ������ ������ 2��
            randomItemNum = 1;
        }
        // �װ� �ƴϰ�, 60%Ȯ�� �̻��̸�
        else if (0.6 <= randomItemNum)
        {
            // ��ӵ� ������ ������ 3��
            randomItemNum = 2;
        }

        Debug.Log("randomItemNum : " + randomItemNum);

        // ��ӵ� ������ ������ŭ �������� ������ų �ݺ���
        for (int i = 0; i < randomItemNum; i++)
        {
            // getItems �迭���� ������ ������ index��ȣ�� �������� ����
            randomItem = Random.Range(0, getItems.Length);

            // ������ ���������ǿ��� ������ �ּ�-�ִ�Ÿ��� ������ǥ X, Z�� ����
            randomDropRangeX = Random.Range(transform.position.x - itemRangeOffset, transform.position.x + itemRangeOffset);
            randomDropRangeZ = Random.Range(transform.position.z - itemRangeOffset, transform.position.z + itemRangeOffset);

            // ���� �ε�����ȣ �� �� ���� �������� �����Ͽ� ���ο� ���ӿ�����Ʈ ������ ��´�.
            GameObject newItem = Instantiate(getItems[randomItem]);

            // ���� ������ �������� ��ǥ�� �������� ������ X,Z��ǥ ������ ���� Y��ǥ ���������� ��ġ
            newItem.transform.position = new Vector3(randomDropRangeX, transform.position.y, randomDropRangeZ);
        }
        for (int i = 0; i < coinNum; i++)
        {
            Instantiate(getCoin, transform.position, transform.rotation);
        }
    }
}

