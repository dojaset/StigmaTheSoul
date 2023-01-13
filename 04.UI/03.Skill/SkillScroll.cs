using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScroll : MonoBehaviour
{
    // ���Ե��� �θ� �Ǵ� LayoutGroup ������Ʈ
    public GameObject skillParant;

    // ���Ե��� ����Ʈ
    public List<Transform> slots = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // ���̾ƿ� �׷��� �� ������ ����Ʈ�� ������ �ǰ��ϱ�
        skillParant.GetComponent<GridLayoutGroup>().constraintCount = slots.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // E Ű�� ������
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ������ 0��° ������Ʈ�� ���������� �����ֱ� (���̾��Ű�� ����)
            slots[0].SetAsLastSibling();

            // ������ 0��°�� ���������� �����ֱ� (����Ʈ ����)
            slots.Insert(slots.Count, slots[0]);

            // ������ 0��° ������Ʈ�� ���� �ߺ��̹Ƿ� �������ֱ� (����Ʈ ����)
            slots.RemoveAt(0);
        }

        // Q Ű�� ������
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ������ ������ ������Ʈ�� 0��°�� �����ֱ� (���̾��Ű�� ����)
            slots[slots.Count - 1].SetAsFirstSibling();

            // ������ �������� 0��°�� �����ֱ� (����Ʈ ����)
            slots.Insert(0, slots[slots.Count - 1]);

            // ������ ������ ������Ʈ�� ���� �ߺ��̹Ƿ� �������ֱ� (����Ʈ ����)
            slots.RemoveAt(slots.Count - 1);
        }
    }
}